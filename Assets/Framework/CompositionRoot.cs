using UnityEngine;

namespace Container.Framework
{
    public abstract class CompositionRoot : MonoBehaviour
    {
        protected IBinder container;

        protected virtual void Awake()
        {
            container = new Binder();
            SetupBindings();
            Init();
        }

        protected abstract void SetupBindings();

        protected abstract void Init();
    }

    public abstract class EventCompositionRoot : CompositionRoot, IMonoInjectionHandler
    {
        public void InjectDependencies(MonoBehaviour script)
        {
            container.InjectProperties(script);
        }
    }

    public abstract class SceneCompositionRoot : CompositionRoot
    {
        protected override void Awake()
        {
            container = new Binder();
            SetupBindings();
            ResolveScene();
            Init();
        }

        private void ResolveScene()
        {
            foreach (var script in Object.FindObjectsOfType(typeof(MonoBehaviour)))
            {
                container.InjectProperties(script);
            }
        }
    }
}