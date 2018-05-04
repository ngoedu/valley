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
    public class SourceTest
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/ms379625(VS.80).aspx#vstsunittesting_topic5
        /// 
        /// https://www.nuget.org/packages/NUnit/2.6.4
        /// 
        /// </summary>

        [Test]
        public void Property_Content_Valid_Test()
        {
            string src = @"          abc(pa, pb)  {";
            // Arrange
            Source input = new Source(src);

            // Actuall
            string actual = input.Content;

            // Assert
            string expected = src;
            Assert.AreEqual(expected, actual);

            //crop 1
            input.Start = 11;
            expected = src.Substring(11);
            actual = input.Content;
            Assert.AreEqual(expected, actual);

            //crop 2
            input.End = 14;
            expected = src.Substring(11,4);
            actual = input.Content;
            Assert.AreEqual(expected, actual);
        }


        [Test]
        [ExpectedException(typeof(InvalidProgramException))] 
        public void Property_Start_LargeThanLength_Exception_Test()
        {
            string src = @"          abc(pa, pb)  {";
            // Arrange
            Source input = new Source(src);

            //crop 2
            input.Start = 35;
        }

        [Test]
        [ExpectedException(typeof(InvalidProgramException))]
        public void Property_Start_Nagitive_Exception_Test()
        {
            string src = @"          abc(pa, pb)  {";
            // Arrange
            Source input = new Source(src);

            //crop 2
            input.Start = -1;
        }

        [Test]
        [ExpectedException(typeof(InvalidProgramException))]
        public void Property_Start_LargeThanEnd_Exception_Test()
        {
            string src = @"          abc(pa, pb)  {";
            // Arrange
            Source input = new Source(src);

            input.End = 10;

            //trigger exception
            input.Start = 11;
        }

        [Test]
        public void Property_Start_NoCatchUpEnd_Test()
        {
            string src = @"          abc(pa, pb)  {";
            // Arrange
            Source input = new Source(src);

            input.Clear();

            input.Start = 11;

            int actual = input.End;

            int expected = -1;

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Property_Length_Calculate_Test()
        {
            string src = @"          abc(pa, pb)  {";
            // Arrange
            Source input = new Source(src);

            int actual = input.Length;

            int expected = src.Length;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Property_Length_AfterClear_Test()
        {
            string src = @"          abc(pa, pb)  {";
            // Arrange
            Source input = new Source(src);

            input.Clear();

            int actual = input.Length;

            int expected = 0;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Property_Length_Clear_IncrEnd_Test()
        {
            string src = @"          abc(pa, pb)  {";
            
            Source input = new Source(src);

            input.Clear();

            input.End++;

            int expected = 1;

            Assert.AreEqual(expected, input.Length);
        }


        [Test]
        [ExpectedException(typeof(InvalidProgramException))]
        public void Property_End_LargeThanLength_Exception_Test()
        {
            string src = @"          abc(pa, pb)  {";
            // Arrange
            Source input = new Source(src);

            //trigger exception
            input.End = 35;
        }

        [Test]
        public void Property_End_Nagitive_Test()
        {
            string src = @"          abc(pa, pb)  {";
            // Arrange
            Source input = new Source(src);

            //crop 2
            input.End = -1;

            int actual = input.Start;

            int expected = 0;

            Assert.AreEqual(expected, actual);
        }

        //################Take#####################################
        [Test]
        public void Take_Until_Nagitive_Test()
        {
            string src = @"2char";
            Source input = new Source(src);

            for (int i = 0; i < src.Length;i++ )
                input.Take();

            int actual = input.Take();

            int expected = -1;

            Assert.AreEqual(expected, actual);
        }

        //##############IndexOf#################################3

        [Test]
        public void IndexOf_WithoutOffset_Test()
        {
            string src = @"          abc(pa, pb)  {";
            
            Source input = new Source(src);

            int actual = input.IndexOf('c');

            int expected = 12;

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void IndexOf_StartAndEnd_Offset_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            input.Start = 2;
            input.End--;

            int actual = input.IndexOf('c');

            int expected = 12;

            Assert.AreEqual(expected, actual);
        }

        //##############Clone#################################3

        [Test]
        public void Clone_Internal_Same_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            var clone = input.Clone(input.Start, input.End);
            
            var actual = clone.cArray;

            var expected = input.cArray;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Clone_Internal_Same_Offset_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            input.Start = 2;
            input.End = 10;

            var clone = input.Clone(input.Start, input.End);

            var actual = clone.cArray;

            var expected = input.cArray;

            Assert.AreSame(expected, actual);

            Assert.AreEqual(input.Start, clone.Start);

            Assert.AreEqual(input.End, clone.End);
        }

        //##############Copy#################################3

        [Test]
        public void Copy_Internal_NotSame_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            
            var copy = input.Copy();

            var actual = copy.cArray;

            var expected = input.cArray;

            Assert.AreNotSame(expected, actual);
        }

        [Test]
        public void Copy_Internal_SameLength_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            var copy = input.Copy();

            int actual = copy.cArray.Length;

            int expected = input.cArray.Length;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Copy_Internal_SameOffset_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            var copy = input.Copy();

            int actual = copy.Start;

            int expected = input.Start;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(input.End, copy.End);
        }

        [Test]
        public void Copy_Internal_SameContent_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            var copy = input.Copy();

            string expected = src;

            string actual = new string(copy.cArray);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Copy_Internal_Offset_SameLength_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            input.Start = 10;
            input.End = 13;

            var copy = input.Copy();

            int expected = 4;

            int actual = copy.Length;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Copy_Internal_Offset_SameContent_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            input.Start = 10;
            input.End = src.Length - 1;

            var copy = input.Copy();

            string expected = src.Substring(10);

            string actual = copy.Content;

            Assert.AreEqual(expected, actual);
        }


        //##############Copy with Parm#################################3


        [Test]
        public void CopyParm_Internal_NotSame_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            var copy = input.Copy(input.Start, input.End);

            var actual = copy.cArray;

            var expected = input.cArray;

            Assert.AreNotSame(expected, actual);
        }

        [Test]
        public void CopyParm_Internal_SameLength_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            var copy = input.Copy(input.Start, input.End);

            int actual = copy.cArray.Length;

            int expected = input.cArray.Length;

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void CopyParm_Internal_NotSameLength_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            input.Start = 10;
            input.End = src.Length - 1;

            var copy = input.Copy(input.Start, input.End);

            int actual = copy.cArray.Length;

            int expected = input.cArray.Length;

            Assert.AreNotEqual(expected, actual);
        }



        [Test]
        public void CopyParm_Internal_Offset_SameLength_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            input.Start = 10;
            input.End = src.Length - 1;

            var copy = input.Copy(input.Start, input.End);

            int actual = copy.Length;

            int expected = input.Length;

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void CopyParm_Internal_Offset_SameContent_Test()
        {
            string src = @"          abc(pa, pb)  {";

            Source input = new Source(src);

            input.Start = 10;
            input.End = src.Length - 1;

            var copy = input.Copy(input.Start, input.End);

            string actual = copy.Content;

            string expected = input.Content;

            Assert.AreEqual(expected, actual);
        }

        //##############SeekVariable#################################3


        [Test]
        public void SeekVariable_ValidInput_Test()
        {
            // Arrange
            string input = @"          abc(pa, pb)  {";

            // Actuall
            string actual = Source.SeekVariable(input.ToCharArray(), 0, input.Length, '(');

            // Assert
            const string expected = "abc";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsKeyWrod_Variable_Keyword_Test()
        {
            // Arrange
            string input = @"          else(pa, pb)  {";

            // Actuall
            string v1 = Source.SeekVariable(input.ToCharArray(), 0, input.Length, '(');

            bool actual = Source.IsKeyWrod(v1);

            // Assert
            const bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsKeyWrod_Variable_NotKeyword_Test()
        {
            // Arrange
            string input = @"          else1(pa, pb)  {";

            // Actuall
            string v1 = Source.SeekVariable(input.ToCharArray(), 0, input.Length, '(');

            bool actual = Source.IsKeyWrod(v1);

            // Assert
            const bool expected = false;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SeekVariable_ValidInput_MoreCharAfter_Test()
        {
            // Arrange
            string input = @"           abc     (pa, pb)  {";

            // Actuall
            string actual = Source.SeekVariable(input.ToCharArray(), 0, input.Length, '(');

            // Assert
            const string expected = "abc";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SeekVariable_WrongInput_UnwantedSpaceBetween_Test()
        {
            // Arrange
            string input = @"           ab c(pa, pb)  {";

            // Actuall
            string actual = Source.SeekVariable(input.ToCharArray(), 0, input.Length, '(');

            // Assert
            const string expected = null;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SeekVariable_WrongInput_DigitBegin_Test()
        {
            // Arrange
            string input = @"          1abc(pa, pb)  {";

            // Actuall
            string actual = Source.SeekVariable(input.ToCharArray(), 0, input.Length, '(');

            // Assert
            const string expected = null;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SeekCharArrayS_ValidInput_Test()
        {
            // Arrange
            string input = @"        func1 = ";

            // Actuall
            int actual = Source.SeekCharArrayS(input.ToCharArray(), 0, input.Length, "func1".ToCharArray());

            // Assert
            const int expected = 12;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SeekCharArrayS_WrongInput_Newline_Test()
        {
            // Arrange
            string input = @"   

func1 = ";

            // Actuall
            int actual = Source.SeekCharArrayS(input.ToCharArray(), 0, input.Length, "func1".ToCharArray());

            // Assert
            const int expected = -1;
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void SeekCharArrayM_ValidInput_Newline_Test()
        {
            // Arrange
            string input = @"   

func1 = ";

            // Actuall
            int actual = Source.SeekCharArrayM(input.ToCharArray(), 0, input.Length, "func1".ToCharArray());

            // Assert
            const int expected = 11;
            Assert.AreEqual(expected, actual);
        }


       
    }
}
