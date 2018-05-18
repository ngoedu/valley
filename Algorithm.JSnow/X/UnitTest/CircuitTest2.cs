using System;
using System.Linq;
using NUnit.Framework;
using Snow.XAlgorithm;
using Snow.X.Algorithm;
using Snow.Syntax;

namespace Snow.X.UnitTest
{
    [TestFixture]
    public class CircuitTest2
    {
        /// <summary>
        /// Test Circuite composition
        /// </summary>


        [Test]
        public void Circuite_Escaped_1_Test()
        {
            var escaped = Builder.Escaped();

            var src = new Source("\\\"");

            const int stop = -3;
            int actual = escaped.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Escaped_2_Test()
        {
            var escaped = Builder.Escaped();

            var src = new Source("\\'");

            const int stop = -3;
            int actual = escaped.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Escaped_3_Test()
        {
            var escaped = Builder.Escaped();

            var src = new Source("\\\\");

            const int stop = -3;
            int actual = escaped.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Escaped_4_Test()
        {
            var escaped = Builder.Escaped();

            var src = new Source("\\/");

            const int stop = -3;
            int actual = escaped.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Escaped_5_Test()
        {
            var escaped = Builder.Escaped();

            var src = new Source("\\b");

            const int stop = -3;
            int actual = escaped.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Escaped_6_Test()
        {
            var escaped = Builder.Escaped();

            var src = new Source("\\f");

            const int stop = -3;
            int actual = escaped.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Escaped_7_Test()
        {
            var escaped = Builder.Escaped();

            var src = new Source("\\n");

            const int stop = -3;
            int actual = escaped.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Escaped_8_Test()
        {
            var escaped = Builder.Escaped();

            var src = new Source("\\r");

            const int stop = -3;
            int actual = escaped.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Escaped_9_Test()
        {
            var escaped = Builder.Escaped();

            var src = new Source("\\t");

            const int stop = -3;
            int actual = escaped.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_Escaped_10_Test()
        {
            var escaped = Builder.Escaped();

            var src = new Source("\\u1234");

            const int stop = -3;
            int actual = escaped.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_StringLiteral_1_Test()
        {
            var strLiteral = Builder.StringLiteral();

            var src = new Source("\"1212_asdfscafd\"");

            const int stop = -3;
            int actual = strLiteral.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_StringLiteral_2_Test()
        {
            var strLiteral = Builder.StringLiteral();

            var src = new Source("'1212_asdfscafd \\t \\b'");

            const int stop = -3;
            int actual = strLiteral.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Circuite_StringLiteral_3_Test()
        {
            var strLiteral = Builder.StringLiteral();

            var src = new Source("'12 sa \\t \\b \\f \\\" \\' \\/ \\r \\n'");

            const int stop = -3;
            int actual = strLiteral.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }
    }
}
