using UnityEngine;

namespace DIContainer.Framework.Extensions
{
    public abstract class EventCompositionRoot : CompositionRoot, IMonoInjectionHandler
    {
        public void InjectDependencies(MonoBehaviour script)
        {
            Container.InjectProperties(script);
        }
    }
}