using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Snow.Syntax;

namespace Snow.X.UnitTest
{
     [TestFixture]
    public class FormalizerTest
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/ms379625(VS.80).aspx#vstsunittesting_topic5
        /// </summary>

        [TearDown]
        public void CleanUp()
        {
            //do NOTHING
        }
        
        [SetUp]
        public void TestInit()
        {
            Context.Init();
        }


        [Test]
        public void Valid_OneLine_Test()
        {
            string src = @"  alert('hi');";
            
            Source input = new Source(src);

            Formalizer.Process(input, "GLOBAL", Context.GLOBAL);

            // Actuall
            string actual = Context.GLOBAL.ToString();

            // Assert
            string expected = "["+src.Substring(0,src.Length -1)+"]";

            Assert.AreEqual(expected, actual);
        }
    }
}
