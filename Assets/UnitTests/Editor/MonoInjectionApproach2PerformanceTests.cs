using DIContainer.Framework;
using DIContainer.Framework.Extensions;
using NUnit.Framework;
using UnityEngine;
using System.Diagnostics;

namespace DIContainer.UnitTests
{
    [TestFixture]
    public class MonoInjectionApproach2PerformanceTests
    {
        GameObjectFactory factory;
        Container binder;
        Stopwatch sw;
        GameObject prefab;
        GameObject[] instances;

        [SetUp]
        public void SetUp()
        {
            binder = new Container();
            binder.RegisterTransient<ITestInterface, TestClass>();
            factory = new GameObjectFactory(binder);
            sw = new Stopwatch();
            prefab = new GameObject();
            prefab.AddComponent<TestMono>();

            instances = new GameObject[500000];
        }

        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < instances.Length; i++)
            {
                Object.DestroyImmediate(instances[i]);
            }

            Object.DestroyImmediate(prefab);
        }

        [Test]
        public void TestPerformance()
        {
            sw.Start();

            for (int i = 0; i < instances.Length; i++)
            {
                instances[i] = factory.Create(prefab);
            }

            sw.Stop();

            for (int j = 0; j < instances.Length; j++)
            {
                Assert.NotNull(instances[j].GetComponent<TestMono>().property);
            }

            Assert.Pass(sw.ElapsedMilliseconds.ToString());
        }
    }
}

