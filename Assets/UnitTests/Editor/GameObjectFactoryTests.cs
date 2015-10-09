using NUnit.Framework;
using DIContainer.Framework;
using DIContainer.Framework.Extensions;
using UnityEngine;

namespace DIContainer.UnitTests
{
    [TestFixture]
    public class GameObjectFactoryTests
    {
        GameObjectFactory factory;
        Container binder;

        [SetUp]
        public void SetUp()
        {
            binder = new Container();
            factory = new GameObjectFactory(binder);
        }

        [Test]
        public void ShouldResolveDependency()
        {
            binder.RegisterTransient<ITestInterface, TestClass>();
            
            var prefab = new GameObject();
            prefab.AddComponent<TestMono>();

            var instance = factory.Create(prefab);

            Assert.NotNull(instance);
            Assert.NotNull(instance.GetComponent<TestMono>());
            Assert.NotNull(instance.GetComponent<TestMono>().property);

            Object.DestroyImmediate(instance);
            Object.DestroyImmediate(prefab);
        }
    }

    public class TestMono : MonoBehaviour
    {
        [Inject]
        public ITestInterface property { get; set; }
    }
}

