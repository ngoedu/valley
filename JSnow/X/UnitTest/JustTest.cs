using System;
using System.Linq;
using NUnit.Framework;
using Snow.XAlgorithm;

namespace Snow.X.UnitTest
{
    [TestFixture]
    public class JustTest
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/ms379625(VS.80).aspx#vstsunittesting_topic5
        /// </summary>


        
        [Test]
        [Ignore]
        public void Log2_Test()
        {
            int v = 15;
            int i = 0;
            while (true)
            {
                v = v >> 1;
                if (v > 0)
                    i++;
                else
                    break;
            }
            

            //crop 2
            int actual = i;
            const int expected = 3;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [Ignore]
        public void Mod_Test()
        {
            int v = 15;
            for (int i = 0; i< v; i++)
                Console.WriteLine("{2} - %={0} , /={1} is",i % 2,  i / 2, i) ;

            Assert.AreEqual(1, 1);
        }
    }
}
