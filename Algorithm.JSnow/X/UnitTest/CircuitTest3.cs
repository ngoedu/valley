using System;
using System.Linq;
using NUnit.Framework;
using Snow.XAlgorithm;
using Snow.X.Algorithm;
using Snow.Syntax;

namespace Snow.X.UnitTest
{
    [TestFixture]
    public class CircuitTest3
    {
        /// <summary>
        /// Test Circuite composition
        /// </summary>


        [Test]
        public void Circuite_Wormhole_1_Test()
        {
            var objLiteral = Builder.Test_WH_ObjectLiteral();
            const string str = @"{i:{}}";

            var src = new Source(str);

            const int stop = -3;
            int actual = objLiteral.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Wormhole_2_Test()
        {
            var objLiteral = Builder.Test_WH_ObjectLiteral();
            const string str = @"{i:{b:{c:{}}}}";

            var src = new Source(str);

            const int stop = -3;
            int actual = objLiteral.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        
    }
}
