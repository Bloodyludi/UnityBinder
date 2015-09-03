using UnityEngine;

namespace Container.Framework
{
    public abstract class CompositionRoot : MonoBehaviour, IMonoInjectionHandler
    {
        protected IBinder container;

        protected void Awake()
        {
            container = new Binder();
            SetupBindings();
//            ResolveScene();
            Init();
        }

        protected abstract void SetupBindings();

        protected abstract void Init();

        public void InjectDependencies(MonoBehaviour script)
        {
            container.InjectProperties(script);
        }

//        private void ResolveScene()
//        {
//            foreach (var script in Object.FindObjectsOfType(typeof(MonoBehaviour)))
//            {
//                container.InjectProperties(script);
//            }
//        }
    }
}