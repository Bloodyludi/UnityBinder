using Container.Framework;

namespace Container.Example
{
    public class GameRoot : SceneCompositionRoot
    {
        protected override void SetupBindings()
        {
            container.BindToInstance<IRandom, UnityRandom>();
            container.BindToInstance<IDiceRoller, DiceRoller>();
        }

        protected override void Init()
        {
            //Startup Logic
        }
    }
}