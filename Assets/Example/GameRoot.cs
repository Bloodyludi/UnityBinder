using Container.Framework.Extensions;
using UnityEngine;

namespace Container.Example
{
    public class GameRoot : EventCompositionRoot
    {
        [SerializeField] GameObject prefab;

        protected override void SetupBindings()
        {
            container.RegisterSingleton<IRandom, UnityRandom>();
            container.RegisterSingleton<IDiceRoller, DiceRoller>();
        }

        protected override void Init()
        {
            //Startup Logic
            var go = Instantiate(prefab);
            go.transform.SetParent(this.transform, false);
        }
    }
}