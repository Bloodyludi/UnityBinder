using NUnit.Framework;
using Container.Framework;

namespace Container.UnitTests
{
    [TestFixture]
    public class BinderTests
    {
        Binder binder;

        [SetUp]
        public void Setup()
        {
            binder = new Binder();
        }

        [Test]
        public void ShouldBindAndResolveAbstraction()
        {
            binder.Bind<ITestInterface, TestClass>();

            var resolved = binder.Resolve<ITestInterface>();

            Assert.IsNotNull(resolved);
            Assert.IsInstanceOf<ITestInterface>(resolved);
            Assert.IsInstanceOf<TestClass>(resolved);
        }

        [Test]
        public void ShouldBindAndResolveInstance()
        {
            var instance = new TestClass();

            binder.BindToInstance<ITestInterface, TestClass>(instance);

            var resolved = binder.Resolve<ITestInterface>();

            Assert.AreSame(instance, resolved);
        }

        [Test]
        public void ShouldNotCacheBindings()
        {
            binder.Bind<ITestInterface, TestClass>();

            var first = binder.Resolve<ITestInterface>();
            var second = binder.Resolve<ITestInterface>();

            Assert.AreNotSame(first, second);
        }

        [Test]
        public void ShouldBindAndResolveClassToInstance()
        {
            binder.BindToInstance<ITestInterface, TestClass>();

            var resolved = binder.Resolve<ITestInterface>();

            Assert.NotNull(resolved);
        }

        [Test]
        public void ShouldInjectProperties()
        {
            binder.Bind<ITestInterface, TestClass>();
            binder.Bind<ITestInterfaceWithProperty, TestClassWithProperty>();

            var resolved = binder.Resolve<ITestInterfaceWithProperty>();

            Assert.NotNull(resolved.test);
        }

        [Test]
        public void ShouldInstantiateClassWithDefaultConstructor()
        {
            binder.Bind<ITestInterface, TestClassWithDefaultConstructor>();

            var resolved = binder.Resolve<ITestInterface>();

            Assert.NotNull(resolved);
        }

        [Test]
        public void ShouldInstantiateClassWithConstructorWithParams()
        {
            binder.Bind<ITestInterface, TestClass>();
            binder.Bind<ITestInterfaceWithProperty, TestClassWithConstructor>();

            var resolved = binder.Resolve<ITestInterfaceWithProperty>();

            Assert.NotNull(resolved.test);
        }

        [Test]
        public void ShouldInstantiateNestedConstructors()
        {
            binder.Bind<ITestInterface, TestClass>();
            binder.Bind<ITestInterfaceWithProperty, TestClassWithConstructor>();
            binder.Bind<IClassWithNestedElement, ClassWithNestedElement>();

            var resolved = binder.Resolve<IClassWithNestedElement>();

            Assert.NotNull(resolved.nested.test);
        }

        [Test]
        public void ShouldBindMultipleGenericObjects()
        {
            binder.Bind<ITestInterface, TestClass>();
            binder.Bind<ITestInterfaceWithProperty, TestClassWithProperty>();

            binder.BindToInstance<IFactory<ITestInterface>, Factory<ITestInterface>>();
            binder.BindToInstance<IFactory<ITestInterfaceWithProperty>, Factory<ITestInterfaceWithProperty>>();

            var facA = binder.Resolve<IFactory<ITestInterface>>();
            var facB = binder.Resolve<IFactory<ITestInterfaceWithProperty>>();

            Assert.AreNotSame(facA, facB);
        }

        [Test]
        public void TestFactory()
        {
            binder.Bind<ITestInterface, TestClass>();
            binder.BindToInstance<IFactory<ITestInterface>, Factory<ITestInterface>>();

            var factory = binder.Resolve<IFactory<ITestInterface>>();
            Assert.NotNull(factory);

            var product = factory.Create();
            Assert.NotNull(product);
        }

        [Test]
        public void FactoryShouldThrowExceptionForNullBinding()
        {
            Assert.Throws<System.Reflection.TargetInvocationException>(() => binder.BindToInstance<IFactory<ITestInterface>, Factory<ITestInterface>>());
        }
    }

    public interface IClassWithNestedElement
    {
        ITestInterfaceWithProperty nested { get; }
    }

    public class ClassWithNestedElement : IClassWithNestedElement
    {
        public ITestInterfaceWithProperty nested { get; }

        public ClassWithNestedElement(ITestInterfaceWithProperty nested)
        {
            this.nested = nested;
        }
    }

    public class TestClassWithConstructor : ITestInterfaceWithProperty
    {
        public ITestInterface test { get; }

        [Inject]
        public TestClassWithConstructor(ITestInterface test)
        {
            this.test = test;
        }

        public TestClassWithConstructor(ITestInterface test, int x)
        {
            this.test = test;
        }
    }

    public interface ITestInterfaceWithProperty
    {
        ITestInterface test { get; }
    }

    public class TestClassWithProperty : ITestInterfaceWithProperty
    {
        [Inject]
        public ITestInterface test { get; set; }
    }

    public class TestClass : ITestInterface
    {
    }

    public class TestClassWithDefaultConstructor : ITestInterface
    {
        public TestClassWithDefaultConstructor()
        {
        }
    }

    public interface ITestInterface
    {
    }
}
