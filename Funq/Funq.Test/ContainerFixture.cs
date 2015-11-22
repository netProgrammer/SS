using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Funq.Test
{
    [TestClass]
    public class ContainerFixture
    {
        public void CreateInstances()
        {
            var bar = new Bar();
            var foo = new Foo(bar);

            //var foo2 = Activator.CreateInstance(typeof (Foo)); too expencive
            Func<Bar> barFactory = () => new Bar();
            Func<Foo> fooFactory = () => new Foo(barFactory.Invoke());
            var bar2 = barFactory.Invoke();
        }

        [TestMethod]
        public void RegisterTypeAndGetInstance()
        {
            var container = new Container();
            container.Register<IBar>(c => new Bar());
            var bar = container.Resolve<IBar>();
            Assert.IsNotNull(bar);
            Assert.IsTrue(bar is Bar);
        }

        [TestMethod]
        public void RegisterTypeAndInjectDependencies()
        {
            var container = new Container();
            container.Register<IBar>(c => new Bar());
            container.Register<IFoo>(c => new Foo(container.Resolve<IBar>()));

            var foo = container.Resolve<IFoo>();

            Assert.IsNotNull(foo);
            Assert.IsTrue(foo is Foo);

        }

        [TestMethod]
        public void ConstructorArgumentPassedFromResolve()
        {
            var container = new Container();
            container.Register<IBar, string>((c, s) => new Bar(s));

            var bar = container.Resolve<IBar, string>("foo") as Bar;
            Assert.AreEqual("foo", bar.Arg1);
        }

        [TestMethod]
        public void ConstructorArgumentsPassedFromResolve()
        {
            var container = new Container();
            container.Register<IBar, string, bool>((c, s, b) => new Bar(s, b));

            var bar = container.Resolve<IBar, string, bool>("foo", true) as Bar;
            Assert.AreEqual("foo", bar.Arg1);
            Assert.IsTrue(bar.Arg2);
        }

        [TestMethod]
        public void ConstructorArgumentPassedFromResolve2()
        {
            var container = new Container();
            container.Register<IBar>(c => new Bar());
            container.Register<IBar, string>((c, s) => new Bar(s));

            var bar = container.Resolve<IBar>() as Bar;
            var bar2 = container.Resolve<IBar, string>("foo") as Bar;

            Assert.IsNotNull(bar);
            Assert.IsNotNull(bar2);
            Assert.AreEqual("foo", bar2.Arg1);
        }

        public interface IBar
        {
             
        }

        public interface IFoo
        {
             
        }

        public class Bar: IBar
        {
            public Bar()
            {
                
            }

            public Bar(string arg1)
            {
                Arg1 = arg1;
            }

            public Bar(string arg1, bool arg2)
            {
                Arg1 = arg1;
                Arg2 = arg2;
            }

            public string Arg1 { get; private set; }
            public bool Arg2 { get; private set; }
        }

        public class Foo: IFoo
        {
             public IBar Bar { get; set; }

            public Foo(IBar bar)
            {
                Bar = bar;
            }
        }
    }
}