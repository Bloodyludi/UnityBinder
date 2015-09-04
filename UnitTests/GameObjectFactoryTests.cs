﻿using NUnit.Framework;
using NSubstitute;
using Container.Framework;
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
            binder.Bind<ITestInterface, TestClass>();
            
            var prefab = Substitute.For<GameObject>();//new GameObject();
            prefab.AddComponent<TestMono>();

            var instance = factory.Create(prefab);

            Assert.NotNull(instance);
            Assert.NotNull(instance.GetComponent<TestMono>());
            Assert.NotNull(instance.GetComponent<TestMono>().property);
        }
    }

    public class TestMono : MonoBehaviour
    {
        [Inject]
        public ITestInterface property { get; set; }
    }
}

