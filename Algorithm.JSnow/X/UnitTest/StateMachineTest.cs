using System;
using System.Linq;
using NUnit.Framework;
using Snow.XAlgorithm;
using Snow.X.Algorithm;
using Snow.Syntax;

namespace Snow.X.UnitTest
{
    [TestFixture]
    public class StateMachineTest
    {
        /// <summary>
        /// Test statement
        /// </summary>


        private StateMachine BuildMachineForDeclare()
        {
            const string keyVar = @"9,9,0#32,32,0#118,118,1|97,97,2|114,114,3|9,9,4#32,32,4";
            var matrix1 = new StateMatrix(4);
            matrix1.Append(keyVar);

            const string regexVariable = @"9,9,0#32,32,0#65,90,1#97,122,1|9,9,2#32,32,2#48,57,1#65,90,1#95,95,1#97,122,1";
            var matrix2 = new StateMatrix(2);
            matrix2.Append(regexVariable);

            const string equalSign = @"9,9,0#32,32,0#61,61,1";
            var matrix3 = new StateMatrix(1);
            matrix3.Append(equalSign);

            const string literalString = @"9,9,0#32,32,0#34,34,1|0,33,1#34,34,2#35,65536,1";
            var matrix4 = new StateMatrix(2);
            matrix4.Append(literalString);

            const string keySplitor = @"9,9,0#32,32,0#59,59,1";
            var matrix5 = new StateMatrix(1);
            matrix5.Append(keySplitor);


            var machine = new StateMachine();
            machine.Append(matrix1);
            machine.Append(matrix2);
            machine.Append(matrix3);
            machine.Append(matrix4);
            machine.Append(matrix5);

            return machine;
        }

        [Test]
        public void StateMachine_DeclearString_Valid_Test()
        {
            var machine = BuildMachineForDeclare();

            var src = new Source("var m1ychar = \" \t this-is-an-string!\";");

            const int stop = 1;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMachine_DeclearString_InvalidKey_Var_Test()
        {
            var machine = BuildMachineForDeclare();

            var src = new Source("vari m1ychar = \" \t this-is-an-string!\";");

            const int stop = -1;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMachine_DeclearString_InvalidVariable_Test()
        {
            var machine = BuildMachineForDeclare();

            var src = new Source("var 0mychar = \" \t this-is-an-string!\";");

            const int stop = -1;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMachine_DeclearString_NotPairedQuote_Test()
        {
            var machine = BuildMachineForDeclare();

            var src = new Source("var \t mychar \t = \" \t this-is-an-string!;");

            const int stop = -1;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMachine_DeclearString_NoEndSpiltor_Test()
        {
            var machine = BuildMachineForDeclare();

            var src = new Source("var \t mychar \t = \" \t this-is-an-string!\"");

            const int stop = -1;
            int actual = machine.Parse(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }
    }
}
