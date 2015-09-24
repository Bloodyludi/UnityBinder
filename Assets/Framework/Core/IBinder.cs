namespace Container.Framework
{
    public interface IBinder
    {
        void RegisterTransient<TInter, TClass>() where TClass : class, TInter;

        void RegisterInstance<TInter, TClass>(TClass instance) where TClass : class, TInter;

        void RegisterSingleton<TInter, TClass>(bool lazy = true) where TClass : class, TInter;

        bool HasBinding<T>();

        T Resolve<T>();

        void InjectProperties(object instance);
    }
    
}