using UnityEngine;

namespace Container.Framework
{
    public abstract class CompositionRoot : UnityEngine.MonoBehaviour, IMonoInjectionHandler
    {
        protected IBinder container;

        protected void Awake()
        {
            container = new Binder();
            SetupBindings();
            Init();
        }

        protected abstract void SetupBindings();
        protected abstract void Init();

        public void InjectDependencies(MonoBehaviour script)
        {
            container.ResolveDependencies(script);
        }
    }
}