using Container.Framework;
using UnityEngine;

namespace Container.Example
{
    public class GameRoot : CompositionRoot 
    {
        #region implemented abstract members of CompositionRoot

        protected override void SetupBindings()
        {
            //Setting up the container
            container.Bind<IEngine, RocketEngine>();
            container.Bind<ICar, Car>();
        }

        protected override void Init()
        {
            //using the container
            var car = container.Resolve<ICar>();
            car.Motor = container.Resolve<IEngine>();

            car.TurnKey();
        }

        #endregion
    }

    public interface ICar
    {
        IEngine Motor { get; set; }
        void TurnKey();
    }

    public class Car : ICar
    {
        public IEngine Motor { get; set; }

        public void TurnKey()
        {
            Debug.Log("Turning Key of Car");
            Motor.StartEngine();
        }
    }

    public class V8Engine : IEngine
    {
        public void StartEngine()
        {
            Debug.Log("Starting V8Engine");
        }
    }

    public class RocketEngine : IEngine
    {
        public void StartEngine()
        {
            Debug.Log("Starting RocketEngine!!");
        }
    }

    public interface IEngine
    {
        void StartEngine();
    }
}