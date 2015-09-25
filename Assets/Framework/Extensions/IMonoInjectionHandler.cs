using UnityEngine.EventSystems;
using UnityEngine;

namespace Container.Framework.Extensions
{
    public interface IMonoInjectionHandler : IEventSystemHandler
    {
        void InjectDependencies(MonoBehaviour script);
    }
}