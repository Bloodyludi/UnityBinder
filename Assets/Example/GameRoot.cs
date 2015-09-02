using Container.Framework;

namespace Container.Example
{
    public class GameRoot : CompositionRoot 
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