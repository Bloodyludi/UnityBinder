using System.Collections.Generic;
using System;

namespace Container.Framework
{
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
                //TODO: Inject Dependencies
            }

            bindings[typeof(TInter)] = instance;
        }

        public T Resolve<T>()
        {
            var type = typeof(T);
            object instance;

            if (bindings.ContainsKey(type))
            {
                instance = bindings[typeof(T)];
            }
            else if (types.ContainsKey(type))
            {
                instance = Activator.CreateInstance(types[type]);
                //TODO: Inject Dependencies
            }
            else
            {
                throw new Exception("Couldn't resolve binding for " + type);
            }

            return (T)instance;
        }

    }
}