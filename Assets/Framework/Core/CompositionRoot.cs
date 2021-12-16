using UnityEngine;

namespace DIContainer.Framework
{
    public abstract class CompositionRoot : MonoBehaviour
    {
        protected IContainer Container;

        protected virtual void Awake()
        {
            Container = new Container();
            SetupBindings();
            Init();
        }

        protected abstract void SetupBindings();

        protected abstract void Init();
    }
}