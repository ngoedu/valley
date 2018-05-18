using System;
using System.Linq;
using NUnit.Framework;
using Snow.XAlgorithm;

namespace Snow.X.UnitTest
{
    [TestFixture]
    public class BlockSkipListTest
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/ms379625(VS.80).aspx#vstsunittesting_topic5
        /// </summary>


        [Test]
        [ExpectedException(typeof(InvalidProgramException))]
        public void BlockSkipList_NotSorted_Exception_Test()
        {
            BlockSkipList.Block[] data = new BlockSkipList.Block[2];
            data[0] = new BlockSkipList.Block(0, 1, 1);
            //index of node 2 are not valid and will cause exception
            data[1] = new BlockSkipList.Block(1, 5, 2); 

            BlockSkipList bsl = new BlockSkipList(new BlockSkipList.BlockArray(data));
        }
        
        [Test]
        public void BlockSkipList_1_Node_InScope_Test()
        {
            BlockSkipList.Block[] data = new BlockSkipList.Block[1];
            data[0] = new BlockSkipList.Block(0,1, 1);

            var bsl = new BlockSkipList(new BlockSkipList.BlockArray(data));
            
            int actual = bsl.Find(0);
            const int expected = 1;
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void BlockSkipList_1_Node_OutScope_Test()
        {
            BlockSkipList.Block[] data = new BlockSkipList.Block[1];
            data[0] = new BlockSkipList.Block(0,1, 1);

            var bsl = new BlockSkipList(new BlockSkipList.BlockArray(data));
            
            int actual = bsl.Find(2);
            const int expected = -1;
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
         public void BlockSkipList_2_Nodes_InScope_Test()
        {
            BlockSkipList.Block[] data = new BlockSkipList.Block[2];
            data[0] = new BlockSkipList.Block(0, 1, 1);
            data[1] = new BlockSkipList.Block(2, 5, 2);

            var bsl = new BlockSkipList(new BlockSkipList.BlockArray(data));
            int actual = bsl.Find(5);
            const int expected = 2;
            Assert.AreEqual(expected, actual);
        }
         

        [Test]
         public void BlockSkipList_3_Nodes_InScope_Test()
        {
            BlockSkipList.Block[] data = new BlockSkipList.Block[3];
            data[0] = new BlockSkipList.Block(0, 1, 1);
            data[1] = new BlockSkipList.Block(2, 5, 2);
            data[2] = new BlockSkipList.Block(6, 9, 3);

            var bsl = new BlockSkipList(new BlockSkipList.BlockArray(data));
            int actual = bsl.Find(9);
            const int expected = 3;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BlockSkipList_4_Nodes_InScope_Test()
        {
            BlockSkipList.Block[] data = new BlockSkipList.Block[4];

            data[0] = new BlockSkipList.Block(0, 1, 1);
            data[1] = new BlockSkipList.Block(2, 5, 2);
            data[2] = new BlockSkipList.Block(6, 9, 3);
            data[3] = new BlockSkipList.Block(10, 12, 4);


            var bsl = new BlockSkipList(new BlockSkipList.BlockArray(data));
            int actual = bsl.Find(9);
            const int expected = 3;
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void BlockSkipList_5_Nodes_InScope_Test()
        {
            BlockSkipList.Block[] data = new BlockSkipList.Block[5];

            data[0] = new BlockSkipList.Block(0, 1, 1);
            data[1] = new BlockSkipList.Block(2, 5, 2);
            data[2] = new BlockSkipList.Block(6, 9, 3);
            data[3] = new BlockSkipList.Block(10, 12, 4);
            data[4] = new BlockSkipList.Block(13, 15, 5);

            var bsl = new BlockSkipList(new BlockSkipList.BlockArray(data));
            int actual = bsl.Find(9);
            const int expected = 3;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BlockSkipList_8_Nodes_InScope_Test()
        {
            BlockSkipList.Block[] data = new BlockSkipList.Block[8];

            data[0] = new BlockSkipList.Block(0, 1, 1);
            data[1] = new BlockSkipList.Block(2, 5, 2);
            data[2] = new BlockSkipList.Block(6, 9, 3);
            data[3] = new BlockSkipList.Block(10, 12, 4);
            data[4] = new BlockSkipList.Block(13, 15, 5);
            data[5] = new BlockSkipList.Block(16, 19, 6);
            data[6] = new BlockSkipList.Block(20, 22, 7);
            data[7] = new BlockSkipList.Block(23, 50, 8);

            var bsl = new BlockSkipList(new BlockSkipList.BlockArray(data));

            int actual = bsl.Find(23);
            const int expected = 8;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BlockSkipList_15_Nodes_InScope_Test()
        {
            BlockSkipList.Block[] data = new BlockSkipList.Block[15];

            data[0] = new BlockSkipList.Block(0, 1, 1);
            data[1] = new BlockSkipList.Block(2, 5, 2);
            data[2] = new BlockSkipList.Block(6, 9, 3);
            data[3] = new BlockSkipList.Block(10, 12, 4);
            data[4] = new BlockSkipList.Block(13, 15, 5);
            data[5] = new BlockSkipList.Block(16, 19, 6);
            data[6] = new BlockSkipList.Block(20, 22, 7);
            data[7] = new BlockSkipList.Block(23, 50, 8);
            data[8] = new BlockSkipList.Block(51, 52, 9);
            data[9] = new BlockSkipList.Block(53, 57, 10);
            data[10] = new BlockSkipList.Block(58, 59, 11);
            data[11] = new BlockSkipList.Block(60, 62, 12);
            data[12] = new BlockSkipList.Block(63, 67, 13);
            data[13] = new BlockSkipList.Block(68, 73, 14);
            data[14] = new BlockSkipList.Block(74, 80, 15);

            var bsl = new BlockSkipList(new BlockSkipList.BlockArray(data));
            
            int actual = bsl.Find(68);
            const int expected = 14;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BlockSkipList_16_Nodes_AllOver_Test()
        {
            BlockSkipList.Block[] data = new BlockSkipList.Block[16];

            data[0] = new BlockSkipList.Block(0, 1, 1);
            data[1] = new BlockSkipList.Block(2, 5, 2);
            data[2] = new BlockSkipList.Block(6, 9, 3);
            data[3] = new BlockSkipList.Block(10, 12, 4);
            data[4] = new BlockSkipList.Block(13, 15, 5);
            data[5] = new BlockSkipList.Block(16, 19, 6);
            data[6] = new BlockSkipList.Block(20, 22, 7);
            data[7] = new BlockSkipList.Block(23, 50, 8);
            data[8] = new BlockSkipList.Block(51, 52, 9);
            data[9] = new BlockSkipList.Block(53, 57, 10);
            data[10] = new BlockSkipList.Block(58, 59, 11);
            data[11] = new BlockSkipList.Block(60, 62, 12);
            data[12] = new BlockSkipList.Block(63, 67, 13);
            data[13] = new BlockSkipList.Block(68, 73, 14);
            data[14] = new BlockSkipList.Block(74, 80, 15);
            data[15] = new BlockSkipList.Block(81, 92, 16);

            var bsl = new BlockSkipList(new BlockSkipList.BlockArray(data));

            //--below lowest --
            int actual = bsl.Find(-1);
            int expected = -1;
            Assert.AreEqual(expected, actual);

            actual = bsl.Find(0);
            expected = 1;
            Assert.AreEqual(expected, actual);
            
            actual = bsl.Find(2);
            expected = 2;
            Assert.AreEqual(expected, actual);
            
            actual = bsl.Find(6);
            expected = 3;
            Assert.AreEqual(expected, actual);
            
            actual = bsl.Find(12);
            expected = 4;
            Assert.AreEqual(expected, actual);
            
            actual = bsl.Find(15);
            expected = 5;
            Assert.AreEqual(expected, actual);

            actual = bsl.Find(19);
            expected = 6;
            Assert.AreEqual(expected, actual);


            actual = bsl.Find(20);
            expected = 7;
            Assert.AreEqual(expected, actual);

            actual = bsl.Find(23);
            expected = 8;
            Assert.AreEqual(expected, actual);

            actual = bsl.Find(51);
            expected = 9;
            Assert.AreEqual(expected, actual);


            actual = bsl.Find(57);
            expected = 10;
            Assert.AreEqual(expected, actual);

            actual = bsl.Find(59);
            expected = 11;
            Assert.AreEqual(expected, actual);

            actual = bsl.Find(62);
            expected = 12;
            Assert.AreEqual(expected, actual);


            actual = bsl.Find(63);
            expected = 13;
            Assert.AreEqual(expected, actual);

            actual = bsl.Find(68);
            expected = 14;
            Assert.AreEqual(expected, actual);

            actual = bsl.Find(74);
            expected = 15;
            Assert.AreEqual(expected, actual);


            actual = bsl.Find(82);
            expected = 16;
            Assert.AreEqual(expected, actual);

            //--above upper--
	        actual = bsl.Find(93);
	        expected = -1;
	        Assert.AreEqual(expected, actual);	           
        }
    }
}
