namespace DIContainer.Framework.Extensions
{
    public static class ServiceLocator
    {
        private static readonly IContainer binder = new Container();

        public static void RegisterTransient<TInter, TClass>() where TClass : class, TInter
        {
            binder.RegisterTransient<TInter, TClass>();
        }

        public static void RegisterInstance<TInter, TClass>(TClass instance) where TClass : class, TInter
        {
            binder.RegisterInstance<TInter, TClass>(instance);
        }

        public static void RegisterSingleton<TInter, TClass>(bool lazy = true) where TClass : class, TInter
        {
            binder.RegisterSingleton<TInter, TClass>(lazy);
        }

        public static bool HasBinding<T>()
        {
            return binder.HasBinding<T>();
        }

        public static T Resolve<T>()
        {
            return binder.Resolve<T>();
        }
    }
}