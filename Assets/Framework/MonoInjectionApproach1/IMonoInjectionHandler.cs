using UnityEngine.EventSystems;
using UnityEngine;

namespace Container.Framework
{
    public interface IMonoInjectionHandler : IEventSystemHandler
    {
        void InjectDependencies(MonoBehaviour script);
    }
}