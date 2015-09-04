using System;

namespace Container.Framework
{
    public interface IFactory<T>
    {
        T Create();
    }

    public class Factory<T> : IFactory<T>
    {
        private readonly IBinder binder;

        public Factory(IBinder binder)
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