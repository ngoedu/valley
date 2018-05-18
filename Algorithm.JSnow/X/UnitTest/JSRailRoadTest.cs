using System;
using System.Linq;
using NUnit.Framework;
using Snow.XAlgorithm;
using Snow.X.Algorithm;
using Snow.Syntax;

namespace Snow.X.UnitTest
{
    [TestFixture]
    public class JSRailroadTest
    {
        /// <summary>
        /// Test JS Railroad
        /// </summary>


        private StateMachine BuildMachine_Series()
        {
            const string integer = @"48,48,2#49,57,1|0,47,-2#48,57,1#58,65536,-2";
            var matrix1 = new StateMatrix(-2);
            matrix1.Append(integer);

            const string faction = @"46,46,1|0,47,-2#48,57,1#58,65536,-2";
            var matrix2 = new StateMatrix(-2);
            matrix2.Append(faction);

            const string exponent = @"69,69,1#101,101,1|43,43,2#45,45,2#49,57,2|0,48,-2#49,57,2#58,65536,-2";
            var matrix3 = new StateMatrix(-2);
            matrix3.Append(exponent);

            var machine = new StateMachine();
            machine.Append(matrix1);
            machine.Append(matrix2);
            machine.Append(matrix3);

            return machine;
        }

        private StateMachine BuildMachine_Parallel()
        {
            const string number =   @"48,48,2#49,57,1|"
                                    +"0,45,-2#46,46,2#48,57,1#69,69,3#101,101,3#102,65536,-2|"
                                    +"0,47,-2#48,57,2#69,69,3#101,101,3#102,65536,-2|"
                                    +"43,43,4#45,45,4#49,57,4|"
                                    +"0,48,-2#49,57,4#58,65536,-2";
            var matrix = new StateMatrix(-2);
            matrix.Append(number);

            var machine = new StateMachine();
            machine.Append(matrix);

            return machine;
        }

        [Test]
        public void Railroad_number_Parallel_1_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("112.02E+12");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_2_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("0");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_3_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("90");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_4_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("900");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_5_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("900.0");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_6_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("900.01");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_7_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("900E2");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Railroad_number_Parallel_7a_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("900E21");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_8_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("900E+2");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_8a_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("900E-2");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_8b_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("900E+21");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_9_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("900.01E+2");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Parallel_9a_Test()
        {
            var machine = BuildMachine_Parallel();

            var src = new Source("900.01e+2");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Railroad_number_Series_2_Test()
        {
            var machine = BuildMachine_Series();

            var src = new Source("112.02E-12");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Railroad_number_Series_1_Test()
        {
            var machine = BuildMachine_Series();

            var src = new Source("1.2e2");

            const int stop = -3;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        
    }
}
