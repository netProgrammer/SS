using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Funq.Test
{
    [TestClass]
    public class ContainerFixture
    {
        [TestMethod]
        public void CreateInstances()
        {
            var bar = new Bar();
            var foo = new Foo(bar);

            //var foo2 = Activator.CreateInstance(typeof (Foo)); too expencive
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