using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace Container.Framework
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor)]
    public class Inject : Attribute
    {
    }

    public interface IBinder
    {
        void Bind<TInter, TClass>() where TClass : class, TInter;

        void BindToInstance<TInter, TClass>(TClass instance = null) where TClass : class, TInter;

        T Resolve<T>();

        void InjectProperties(object instance);
    }

    public class Binder : IBinder
    {
        readonly IDictionary<Type, Type> transientMap = new Dictionary<Type, Type>();
        readonly IDictionary<Type, object> singletonMap = new Dictionary<Type, object>();

        public void Bind<TInter, TClass>() where TClass : class, TInter
        {
            transientMap[typeof(TInter)] = typeof(TClass);
        }

        public void BindToInstance<TInter, TClass>(TClass instance = null) where TClass : class, TInter
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

        public void InjectProperties(object instance)
        {
            var type = instance.GetType();
            var injectProperties = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Inject))).GetEnumerator();

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
                instance = constructor.Invoke(paramInstances);
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
                constructor = constructors.Single(x => Attribute.IsDefined(x, typeof(Inject)));
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