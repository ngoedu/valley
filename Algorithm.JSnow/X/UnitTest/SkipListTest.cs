using System;
using System.Linq;
using NUnit.Framework;
using Snow.XAlgorithm;

namespace Snow.X.UnitTest
{
    [TestFixture]
    public class SkipListTest
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/ms379625(VS.80).aspx#vstsunittesting_topic5
        /// </summary>


        [Test]
        public void SkipList_Test()
        {
            SkipList sl = new SkipList();
            sl.Insert(1);
            sl.Insert(3);
            sl.Insert(7);
            sl.Insert(8);
            sl.Insert(9);
            sl.Insert(10);
            sl.Insert(11);
            sl.Insert(12);
            sl.Insert(19);
            sl.Insert(21);

            bool actual = sl.Contains(19);
            const bool expected = true;

            Assert.AreEqual(expected, actual);
        }
    }
}
