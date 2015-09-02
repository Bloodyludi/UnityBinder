using Container.Framework;

namespace Container.Framework
{
    public abstract class CompositionRoot : UnityEngine.MonoBehaviour
    {
        public IBinder container;

        protected void Awake()
        {
            container = new Binder();
            SetupBindings();
            Init();
        }

        protected abstract void SetupBindings();
        protected abstract void Init();
    }
}