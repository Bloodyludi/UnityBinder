using Container.Framework;
using NUnit.Framework;
using UnityEngine;
using System.Diagnostics;

namespace Container.UnitTests
{
    [TestFixture]
    public class MonoInjectionApproach1PerformanceTests
    {
        Stopwatch sw;
        GameObject prefab;
        GameObject root;
        GameObject[] instances;

        [SetUp]
        public void SetUp()
        {
            sw = new Stopwatch();

            root = new GameObject();
            root.AddComponent<TestRoot>().Setup();

            prefab = new GameObject();
            prefab.AddComponent<TestMonoApproach1>();

            instances = new GameObject[500000];
        }

//        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < instances.Length; i++)
            {
                Object.DestroyImmediate(instances[i]);
            }

            Object.DestroyImmediate(prefab);
            Object.DestroyImmediate(root);
        }

        [Test]
        public void TestPerformance()
        {
            sw.Start();

            for (int i = 0; i < instances.Length; i++)
            {
                instances[i] = Object.Instantiate(prefab);
                instances[i].transform.parent = root.transform;
                instances[i].GetComponent<TestMonoApproach1>().Inject();
            }

            sw.Stop();
            for (int j = 0; j < instances.Length; j++)
            {
                Assert.NotNull(instances[j].GetComponent<TestMonoApproach1>().property);
            }
            Assert.Pass(sw.ElapsedMilliseconds.ToString());
        }
    }

    public class TestRoot : EventCompositionRoot
    {
        public void Setup()
                {
                    container = new Binder();
                    container.Bind<ITestInterface, TestClass>();
                }

        protected override void SetupBindings()
        {
            
        }

        protected override void Init()
        {
        }
    }

    public class TestMonoApproach1 : MonoBehaviour
    {
        [Inject]
        public ITestInterface property { get; set; }

        void Start()
        {
            this.Inject();
        }
    }
}
