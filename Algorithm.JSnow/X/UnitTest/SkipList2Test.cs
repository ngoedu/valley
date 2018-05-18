using System;
using System.Linq;
using NUnit.Framework;
using Snow.XAlgorithm;

namespace Snow.X.UnitTest
{
    [TestFixture]
    public class SkipList2Test
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/ms379625(VS.80).aspx#vstsunittesting_topic5
        /// </summary>
      
        [Test]
        public void SkipList2_Search_NotInScope_1_Test()
        {
            SkipList2 sl = new SkipList2();

            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(15, 15, null));
            sl.Insert(new SkipList2.Block(16, 41, null));

            Object actual = sl.Find(9);
            const Object expected = null;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Search_NotInScope_2_Test()
        {
            SkipList2 sl = new SkipList2();

            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(15, 15, null));
            sl.Insert(new SkipList2.Block(16, 41, null));

            Object actual = sl.Find(12);
            const Object expected = null;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Search_nScope_1_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object(); 

            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(15, 15, ValueObject));
            sl.Insert(new SkipList2.Block(16, 41, null));

            Object actual = sl.Find(15);
            Object expected = ValueObject;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Search_InScope_2_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object();

            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(15, 15, null));
            sl.Insert(new SkipList2.Block(16, 41, ValueObject));

            Object actual = sl.Find(17);
            Object expected = ValueObject;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Search_InScope_NotInOrdered_1_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object();

            sl.Insert(new SkipList2.Block(16, 41, null));
            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(15, 15, ValueObject));
            sl.Insert(new SkipList2.Block(1, 4, null));

            Object actual = sl.Find(15);
            Object expected = ValueObject;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Remove_InScope_1_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object();

            sl.Insert(new SkipList2.Block(16, 41, null));
            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(15, 15, ValueObject));
            sl.Insert(new SkipList2.Block(1, 4, null));

            sl.Remove(new SkipList2.Block(10, 11, null));
            
            Object actual = sl.Find(15);
            Object expected = ValueObject;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Remove_NotInScope_1_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object();

            sl.Insert(new SkipList2.Block(16, 41, null));
            sl.Insert(new SkipList2.Block(10, 11, ValueObject));
            sl.Insert(new SkipList2.Block(15, 15, ValueObject));
            sl.Insert(new SkipList2.Block(1, 4, null));

            sl.Remove(new SkipList2.Block(10, 11, ValueObject));

            Object actual = sl.Find(10);
            Object expected = null;

            Assert.AreEqual(expected, actual);
        }



        [Test]
        public void SkipList2_Contains_InScope_1_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object();

            sl.Insert(new SkipList2.Block(16, 41, null));
            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(12, 15, ValueObject));
            sl.Insert(new SkipList2.Block(1, 4, null));

            sl.Remove(new SkipList2.Block(10, 11, null));

            bool actual = sl.Contains(new SkipList2.Block(12, 13, ValueObject));
            const bool expected = true;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Contains_InScope_2_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object();

            sl.Insert(new SkipList2.Block(16, 41, null));
            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(12, 15, ValueObject));
            sl.Insert(new SkipList2.Block(1, 4, null));

            sl.Remove(new SkipList2.Block(10, 11, null));

            bool actual = sl.Contains(new SkipList2.Block(12, 15, ValueObject));
            const bool expected = true;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Contains_InScope_3_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object();

            sl.Insert(new SkipList2.Block(16, 41, null));
            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(12, 15, ValueObject));
            sl.Insert(new SkipList2.Block(1, 4, null));

            sl.Remove(new SkipList2.Block(10, 11, null));

            bool actual = sl.Contains(new SkipList2.Block(14, 15, ValueObject));
            const bool expected = true;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Contains_NotInScope_1_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object();

            sl.Insert(new SkipList2.Block(16, 41, null));
            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(12, 15, ValueObject));
            sl.Insert(new SkipList2.Block(1, 4, null));

            sl.Remove(new SkipList2.Block(10, 11, null));

            bool actual = sl.Contains(new SkipList2.Block(11, 12, ValueObject));
            const bool expected = false;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Contains_NotInScope_2_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object();

            sl.Insert(new SkipList2.Block(16, 41, null));
            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(12, 15, ValueObject));
            sl.Insert(new SkipList2.Block(1, 4, null));

            sl.Remove(new SkipList2.Block(10, 11, null));

            bool actual = sl.Contains(new SkipList2.Block(13, 16, ValueObject));
            const bool expected = false;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SkipList2_Contains_NotInScope_3_Test()
        {
            SkipList2 sl = new SkipList2();
            Object ValueObject = new Object();

            sl.Insert(new SkipList2.Block(16, 41, null));
            sl.Insert(new SkipList2.Block(10, 11, null));
            sl.Insert(new SkipList2.Block(12, 15, ValueObject));
            sl.Insert(new SkipList2.Block(1, 4, null));

            sl.Remove(new SkipList2.Block(10, 11, null));

            bool actual = sl.Contains(new SkipList2.Block(11, 16, ValueObject));
            const bool expected = false;

            Assert.AreEqual(expected, actual);
        }
    }
}
