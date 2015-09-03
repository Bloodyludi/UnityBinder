using System.Collections.Generic;
using System;
using System.Linq;

namespace Container.Framework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Inject : Attribute
    {
    }

    public interface IBinder
    {
        void Bind<TInter, TClass>() where TClass : class, TInter;

        void BindToInstance<TInter, TClass>(TClass instance = null) where TClass : class, TInter;

        T Resolve<T>();

        void ResolveDependencies(object instance);
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
                instance = (TClass) Instantiate(typeof(TClass)); 
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
            var injectProperties = type.GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(Inject))).GetEnumerator();
            
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
            var instance = Activator.CreateInstance(type);
            InjectProperties(instance);
            return instance;
        }

    }
}