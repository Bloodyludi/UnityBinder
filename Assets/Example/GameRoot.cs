using Container.Framework;

namespace Container.Example
{
    public class GameRoot : SceneCompositionRoot
    {
        protected override void SetupBindings()
        {
            container.RegisterSingleton<IRandom, UnityRandom>();
            container.RegisterSingleton<IDiceRoller, DiceRoller>();
        }

        protected override void Init()
        {
            //Startup Logic
        }
    }
}