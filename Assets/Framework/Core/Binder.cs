using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace Container.Framework
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor)]
    public class InjectAttribute : Attribute
    {
    }

    public interface IBinder
    {
        void RegisterTransient<TInter, TClass>() where TClass : class, TInter;

        void RegisterInstance<TInter, TClass>(TClass instance = null) where TClass : class, TInter;

        bool HasBinding<T>();

        T Resolve<T>();

        void InjectProperties(object instance);
    }

    public class Binder : IBinder
    {
        private readonly IDictionary<Type, Type> transientMap = new Dictionary<Type, Type>();
        private readonly IDictionary<Type, object> singletonMap = new Dictionary<Type, object>();

        public Binder()
        {
            //Allows the Container to be used as a Service Locator
            RegisterInstance<IBinder, Binder>(this);
        }

        public void RegisterTransient<TInter, TClass>() where TClass : class, TInter
        {
            transientMap[typeof(TInter)] = typeof(TClass);
        }

        public void RegisterInstance<TInter, TClass>(TClass instance = null) where TClass : class, TInter
        {
            if (instance == null)
            {
                instance = (TClass)Instantiate(typeof(TClass)); 
            }

            singletonMap[typeof(TInter)] = instance;
        }

        public T Resolve<T>()
        {
            var type = typeof(T);
            return (T)Resolve(type);
        }

        public void Release<T>()
        {
            var type = typeof(T);
            if (singletonMap.ContainsKey(type))
            {
                singletonMap.Remove(type);
            }
            else if (transientMap.ContainsKey(type))
            {
                transientMap.Remove(type);
            }

            GC.Collect();
        }

        public bool HasBinding<T>()
        {
            var type = typeof(T);
            return (singletonMap.ContainsKey(type) || transientMap.ContainsKey(type));
        }

        public void InjectProperties(object instance)
        {
            var type = instance.GetType();
            var injectProperties = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(InjectAttribute))).GetEnumerator();

            while (injectProperties.MoveNext())
            {
                var property = injectProperties.Current;
                var binding = Resolve(property.PropertyType);
                property.SetValue(instance, binding, null);
            }
        }

        private object Resolve(Type type)
        {
            object instance;

            if (singletonMap.ContainsKey(type))
            {
                instance = singletonMap[type];
            }
            else if (transientMap.ContainsKey(type))
            {
                instance = Instantiate(transientMap[type]); 
            }
            else
            {
                throw new Exception("Couldn't resolve binding for " + type);
            }

            return instance;
        }

        private object Instantiate(Type type)
        {
            object instance;

            var constructor = GetConstructor(type);
            if (constructor != null)
            {
                var paramInfos = constructor.GetParameters();
                var paramInstances = ResolveParameters(paramInfos);
                instance = Activator.CreateInstance(type, paramInstances);
            }
            else
            {
                instance = Activator.CreateInstance(type);
            }

            InjectProperties(instance);
            return instance;
        }

        private ConstructorInfo GetConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            ConstructorInfo constructor;

            if (constructors.Length == 1)
            {
                constructor = constructors[0];
            }
            else
            {
                constructor = constructors.Single(x => Attribute.IsDefined(x, typeof(InjectAttribute)));
            }

            return constructor;
        }

        private object[] ResolveParameters(ParameterInfo[] parameterInfos)
        {
            var parameters = new object[parameterInfos.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameterInfo = parameterInfos[i];

                parameters[i] = Resolve(parameterInfo.ParameterType);
            }

            return parameters;
        }
    }
}