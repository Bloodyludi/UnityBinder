using UnityEngine;

namespace Container.Framework.Extensions
{

    public abstract class EventCompositionRoot : CompositionRoot, IMonoInjectionHandler
    {
        public void InjectDependencies(MonoBehaviour script)
        {
            container.InjectProperties(script);
        }
    }
    
}