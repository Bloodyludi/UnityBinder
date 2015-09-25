using NUnit.Framework;
using Container.Framework;
using Container.Framework.Extensions;
using UnityEngine;

namespace Container.UnitTests
{
    [TestFixture]
    public class GameObjectFactoryTests
    {
        GameObjectFactory factory;
        Binder binder;

        [SetUp]
        public void SetUp()
        {
            binder = new Binder();
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

