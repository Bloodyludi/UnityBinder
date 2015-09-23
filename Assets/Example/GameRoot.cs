using Container.Framework;

namespace Container.Example
{
    public class GameRoot : SceneCompositionRoot
    {
        protected override void SetupBindings()
        {
            container.RegisterInstance<IRandom, UnityRandom>();
            container.RegisterInstance<IDiceRoller, DiceRoller>();
        }

        protected override void Init()
        {
            //Startup Logic
        }
    }
}