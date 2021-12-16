using DIContainer.Framework.Extensions;
using UnityEngine;

namespace DIContainer.Example
{
    public class GameRoot : EventCompositionRoot
    {
        [SerializeField] GameObject prefab;

        protected override void SetupBindings()
        {
            Container.RegisterSingleton<IRandom, UnityRandom>();
            Container.RegisterSingleton<IDiceRoller, DiceRoller>();
        }

        protected override void Init()
        {
            //Startup Logic here
            Instantiate(prefab, transform, false);
        }
    }
}