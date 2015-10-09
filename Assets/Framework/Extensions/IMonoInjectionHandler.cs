using UnityEngine.EventSystems;
using UnityEngine;

namespace DIContainer.Framework.Extensions
{
    public interface IMonoInjectionHandler : IEventSystemHandler
    {
        void InjectDependencies(MonoBehaviour script);
    }
}