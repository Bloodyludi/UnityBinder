using Container.Framework;

namespace Container.Example
{
    public class GameRoot : CompositionRoot 
    {
        protected override void SetupBindings()
        {
            container.BindToInstance<IRandom, UnityRandom>();
        }

        protected override void Init()
        {
            //Startup Logic
        }
    }
}