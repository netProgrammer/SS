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

        public interface IBar
        {
             
        }

        public interface IFoo
        {
             
        }

        public class Bar: IBar
        {
             
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