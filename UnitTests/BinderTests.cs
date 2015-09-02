﻿using NUnit.Framework;
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
        public void ShouldBindAndResoleClassToInstance()
        {
            binder.BindToInstance<ITestInterface, TestClass>();

            var resolved = binder.Resolve<ITestInterface>();

            Assert.NotNull(resolved);
        }
    }

    public class TestClass : ITestInterface
    {

    }

    public interface ITestInterface
    {

    }
}