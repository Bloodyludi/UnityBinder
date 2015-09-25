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
}