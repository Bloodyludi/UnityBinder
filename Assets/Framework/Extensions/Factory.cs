using System;

namespace DIContainer.Framework.Extensions
{
    public class Factory<T> : IFactory<T>
    {
        private readonly IContainer binder;

        public Factory(IContainer binder)
        {
            this.binder = binder;

            if (!binder.HasBinding<T>())
            {
                throw new Exception("Factory is missing binding of Type: " + typeof(T));
            }
        }

        public T Create()
        {
            return binder.Resolve<T>();
        }
    }
}