using UnityEngine;

namespace DIContainer.Framework
{
    public abstract class CompositionRoot : MonoBehaviour
    {
        protected IContainer container;

        protected virtual void Awake()
        {
            container = new Container();
            SetupBindings();
            Init();
        }

        protected abstract void SetupBindings();

        protected abstract void Init();
    }
}