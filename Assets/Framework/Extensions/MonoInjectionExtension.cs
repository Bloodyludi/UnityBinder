using UnityEngine;
using UnityEngine.EventSystems;

namespace Container.Framework.Extensions
{
    public static class MonoInjectionExtension
    {
        public static void Inject(this MonoBehaviour script)
        {
            ExecuteEvents.ExecuteHierarchy<IMonoInjectionHandler>(script.gameObject, null, (target, data) => target.InjectDependencies(script));
        }
    }
}