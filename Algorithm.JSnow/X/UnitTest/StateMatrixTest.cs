using System;
using System.Linq;
using NUnit.Framework;
using Snow.XAlgorithm;
using Snow.X.Algorithm;
using Snow.Syntax;

namespace Snow.X.UnitTest
{
    /// <summary>
    /// Tests for State Matrix
    /// </summary>
    [TestFixture]
    public class StateMatrixTest
    {
        [Test]
        public void StateMatrix_Keywrod_var_EndWithSpace_Test()
        {
            BlockSkipList.Block[] line1 = new BlockSkipList.Block[7];
            line1[0] = new BlockSkipList.Block(0, 8, -1);
            line1[1] = new BlockSkipList.Block(9, 9, 0);
            line1[2] = new BlockSkipList.Block(10, 31, -1);
            line1[3] = new BlockSkipList.Block(32, 32, 0);
            line1[4] = new BlockSkipList.Block(33, 117, -1);
            line1[5] = new BlockSkipList.Block(118, 118, 1);
            line1[6] = new BlockSkipList.Block(119, 65536, -1); 
            BlockSkipList bsl1 = new BlockSkipList(new BlockSkipList.BlockArray(line1));

            BlockSkipList.Block[] line2 = new BlockSkipList.Block[3];
            line2[0] = new BlockSkipList.Block(0, 96, -1);
            line2[1] = new BlockSkipList.Block(97, 97, 2);
            line2[2] = new BlockSkipList.Block(98, 65536, -1);
            BlockSkipList bsl2 = new BlockSkipList(new BlockSkipList.BlockArray(line2));

            BlockSkipList.Block[] line3 = new BlockSkipList.Block[3];
            line3[0] = new BlockSkipList.Block(0, 113, -1);
            line3[1] = new BlockSkipList.Block(114, 114, 3);
            line3[2] = new BlockSkipList.Block(115, 65536, -1);
            BlockSkipList bsl3 = new BlockSkipList(new BlockSkipList.BlockArray(line3));

            BlockSkipList.Block[] line4 = new BlockSkipList.Block[5];
            line4[0] = new BlockSkipList.Block(0, 8, -1);
            line4[1] = new BlockSkipList.Block(9, 9, 4);
            line4[2] = new BlockSkipList.Block(10, 31, -1);
            line4[3] = new BlockSkipList.Block(32, 32, 4);
            line4[4] = new BlockSkipList.Block(33, 65536, -1);
            BlockSkipList bsl4 = new BlockSkipList(new BlockSkipList.BlockArray(line4));

            var matrix = new StateMatrix();
            
            matrix.Append(bsl1);
            matrix.Append(bsl2);
            matrix.Append(bsl3);
            matrix.Append(bsl4);

            const string testStr = "  \t  var ";

            int actual = matrix.Transit(testStr.ToCharArray());
            const int expected = 4;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMatrix_Keyword_var_EndWithTab_Test()
        {
            const string blocks = @"9,9,0#32,32,0#118,118,1|97,97,2|114,114,3|9,9,4#32,32,4";
            
            var matrix = new StateMatrix();
            matrix.Append(blocks);
 
            const string testString = "  \t  var\t";
            int actual = matrix.Transit(testString.ToCharArray());
            const int expected = 4;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMatrix_Regex_variable_BeginWithNumber_Test()
        {
            const string blocks = @"9,9,0#32,32,0#65,90,1#97,122,1|9,9,2#32,32,2#48,57,1#65,90,1#95,95,1#97,122,1";
            
            var matrix = new StateMatrix();
            matrix.Append(blocks);

            const string varString = "  12da_ade ";
            int actual = matrix.Transit(varString.ToCharArray());
            const int expected = -1;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMatrix_Regex_variable_Valid_Test()
        {
            const string blocks = @"9,9,0#32,32,0#65,90,1#97,122,1|9,9,2#32,32,2#48,57,1#65,90,1#95,95,1#97,122,1";
            
            var matrix = new StateMatrix();
            matrix.Append(blocks);

            const string varString = "  A1_a_d_e_ ";
            int actual = matrix.Transit(varString.ToCharArray());
            const int expected = 2;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMatrix_Regex_equalSign_Valid_Test()
        {
            const string blocks = @"9,9,0#32,32,0#61,61,1";

            var matrix = new StateMatrix();
            matrix.Append(blocks);

            const string varString = " =";
            int actual = matrix.Transit(varString.ToCharArray());
            const int expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMatrix_Regex_string_Valid_Test()
        {
            const string blocks = @"9,9,0#32,32,0#34,34,1|0,33,1#34,34,2#35,65536,1";

            var matrix = new StateMatrix();
            matrix.Append(blocks);

            const string varString = " \" \t this is an string\"";
            int actual = matrix.Transit(varString.ToCharArray());
            const int expected = 2;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMatrix_Regex_stringWithChinese_Valid_Test()
        {
            const string blocks = @"9,9,0#32,32,0#34,34,1|0,33,1#34,34,2#35,65536,1";

            var matrix = new StateMatrix();
            matrix.Append(blocks);

            const string varString = " \" \t this is an 中文string\"";
            int actual = matrix.Transit(varString.ToCharArray());
            const int expected = 2;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMatrix_Keyword_splitor_Valid_Test()
        {
            const string blocks = @"9,9,0#32,32,0#59,59,1";

            var matrix = new StateMatrix();
            matrix.Append(blocks);

            const string varString = "\t ;"; 
            int actual = matrix.Transit(varString.ToCharArray());
            const int expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMatrix_Regex_string_WithStopCode_Test()
        {
            const string blocks = @"9,9,0#32,32,0#34,34,1|0,33,1#34,34,2#35,65536,1";
            const int stop = 2;
            var matrix = new StateMatrix(stop);
            matrix.Append(blocks);

            const string varString = " \" \t this is an string\"  ";
            
            int actual = matrix.Transit(varString.ToCharArray());
            const int expected = 2;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StateMatrix_Regex_string_WithStopCode_SourceObject_Test()
        {
            const string blocks = @"9,9,0#32,32,0#34,34,1|0,33,1#34,34,2#35,65536,1";
            const int stop = 2;
            var matrix = new StateMatrix(stop);
            matrix.Append(blocks);

            const string varString = " \" \t this is an string\"  ";
            var src = new Source(varString);

            int actual = matrix.Transit(src);
            const int expected = stop;

            Assert.AreEqual(expected, actual);
        }
    }
}
