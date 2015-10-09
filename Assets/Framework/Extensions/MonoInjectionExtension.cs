using UnityEngine;
using UnityEngine.EventSystems;

namespace DIContainer.Framework.Extensions
{
    public static class MonoInjectionExtension
    {
        public static void Inject(this MonoBehaviour script)
        {
            ExecuteEvents.ExecuteHierarchy<IMonoInjectionHandler>(script.gameObject, null, (target, data) => target.InjectDependencies(script));
        }
    }
}