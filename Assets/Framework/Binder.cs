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
        void Bind<TInter, TClass>();

        void BindToInstance<TInter, TClass>(TClass instance = null) where TClass : class;

        T Resolve<T>();
    }

    public class Binder : IBinder
    {
        readonly IDictionary<Type, Type> types = new Dictionary<Type, Type>();
        readonly IDictionary<Type, object> bindings = new Dictionary<Type, object>();

        public void Bind<TInter, TClass>()
        {
            types[typeof(TInter)] = typeof(TClass);
        }

        public void BindToInstance<TInter, TClass>(TClass instance = null) where TClass : class
        {
            if (instance == null)
            {
                instance = Activator.CreateInstance<TClass>();
                ResolveDependencies(instance);
            }

            bindings[typeof(TInter)] = instance;
        }

        public T Resolve<T>()
        {
            var type = typeof(T);
            return (T)Resolve(type);
        }

        public object Resolve(Type type)
        {
            object instance;

            if (bindings.ContainsKey(type))
            {
                instance = bindings[type];
            }
            else if (types.ContainsKey(type))
            {
                instance = Activator.CreateInstance(types[type]);
                ResolveDependencies(instance);
            }
            else
            {
                throw new Exception("Couldn't resolve binding for " + type);
            }

            return instance;
        }

        private void ResolveDependencies(object instance)
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
    }
}