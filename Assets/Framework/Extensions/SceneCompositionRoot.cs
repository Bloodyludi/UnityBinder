using UnityEngine;

namespace DIContainer.Framework.Extensions
{
    public abstract class SceneCompositionRoot : CompositionRoot
    {
        protected override void Awake()
        {
            container = new Container();
            SetupBindings();
            ResolveScene();
            Init();
        }

        private void ResolveScene()
        {
            foreach (var script in Object.FindObjectsOfType(typeof(MonoBehaviour)))
            {
                container.InjectProperties(script);
            }
        }
    }
}