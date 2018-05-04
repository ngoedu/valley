using System;
using System.Linq;
using NUnit.Framework;
using Snow.XAlgorithm;
using Snow.X.Algorithm;
using Snow.Syntax;

namespace Snow.X.UnitTest
{
    [TestFixture]
    public class CircuitTest1
    {
        /// <summary>
        /// Test Circuite composition
        /// </summary>


        private Circuit BuildCircuit_Integer()
        {
            return Builder.Integer(); 
        }

        private Circuit BuildCircuit_Fraction()
        {
            return Builder.Fraction(); 
        }

        private Circuit BuildCircuit_Exponent()
        {
            return Builder.Exponent();
        }

        private Circuit BuildCircuit_Number()
        {
            return Builder.NumberLiteral();
        }

        private Circuit BuildCircuit_Name()
        {
            return Builder.Name();
        }


        [Test]
        public void Circuite_Integer_1_Test()
        {
            var integer = BuildCircuit_Integer();

            var src = new Source("120");

            const int stop = -3;
            int actual = integer.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Integer_2_Test()
        {
            var integer = BuildCircuit_Integer();

            var src = new Source("0");

            const int stop = -3;
            int actual = integer.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Fraction_1_Test()
        {
            var integer = BuildCircuit_Fraction();

            var src = new Source(".");

            const int stop = -3;
            int actual = integer.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Fraction_2_Test()
        {
            var integer = BuildCircuit_Fraction();

            var src = new Source(".012");

            const int stop = -3;
            int actual = integer.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Circuite_Exponent_1_Test()
        {
            var integer = BuildCircuit_Exponent();

            var src = new Source("e90");

            const int stop = -3;
            int actual = integer.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Exponent_2_Test()
        {
            var integer = BuildCircuit_Exponent();

            var src = new Source("E90");

            const int stop = -3;
            int actual = integer.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Exponent_3_Test()
        {
            var integer = BuildCircuit_Exponent();

            var src = new Source("E+99");

            const int stop = -3;
            int actual = integer.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Exponent_4_Test()
        {
            var integer = BuildCircuit_Exponent();

            var src = new Source("e-9");

            const int stop = -3;
            int actual = integer.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Circuite_Number_1_Test()
        {
            var number = BuildCircuit_Number();

            var src = new Source("123");

            const int stop = -3;
            int actual = number.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Number_2_Test()
        {
            var number = BuildCircuit_Number();

            var src = new Source("123.21");

            const int stop = -3;
            int actual = number.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Circuite_Number_3_Test()
        {
            var number = BuildCircuit_Number();

            var src = new Source("123.210E+12");

            const int stop = -3;
            int actual = number.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Circuite_Number_4_Test()
        {
            var number = BuildCircuit_Number();

            var src = new Source("0");

            const int stop = -3;
            int actual = number.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Circuite_Number_5_Test()
        {
            var number = BuildCircuit_Number();

            var src = new Source("0e1");

            const int stop = -3;
            int actual = number.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Circuite_Number_6_Test()
        {
            var number = BuildCircuit_Number();

            var src = new Source("e1");

            const int stop = -1;
            int actual = number.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Name_1_Test()
        {
            var name = BuildCircuit_Name();

            var src = new Source("var1");

            const int stop = -3;
            int actual = name.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Name_2_Test()
        {
            var name = BuildCircuit_Name();

            var src = new Source("var_1");

            const int stop = -3;
            int actual = name.Transit(src);
            const int expected = stop;  

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Name_3_Test()
        {
            var name = BuildCircuit_Name();

            var src = new Source("v090_a0r_1");

            const int stop = -3;
            int actual = name.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_HexDecimal4_1_Test()
        {
            var hexd4 = Builder.HexDecimal4();

            var src = new Source("1980");

            const int stop = -3;
            int actual = hexd4.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_HexDecimal4_2_Test()
        {
            var hexd4 = Builder.HexDecimal4();

            var src = new Source("0980");

            const int stop = -1;
            int actual = hexd4.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_HexDecimal4_3_Test()
        {
            var hexd4 = Builder.HexDecimal4();

            var src = new Source("19800");

            const int stop = -1;
            int actual = hexd4.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }
        
    }
}
