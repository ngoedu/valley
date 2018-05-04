using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Snow.XAlgorithm
{
    public class BlockSkipList
    {

        /// <summary>
        /// block is object which hode a range (by index 1 & 2)and its value
        /// </summary>
        public class Block
        {
            public int Index1 { get; private set; }
            public int Index2 { get; private set; }
            public int Value { get; private set; }

            /// <summary>
            /// all properties only allowed to be set in a constructor 
            /// </summary>
            /// <param name="i1"></param>
            /// <param name="i2"></param>
            /// <param name="v"></param>
            public Block(int i1, int i2, int v)
            {
                this.Index1 = i1;
                this.Index2 = i2;
                this.Value = v;
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }


        /// <summary>
        /// Hold an internal Block array and provides some mainpulation methods, such as copy, clone and taks
        /// </summary>
        public class BlockArray
        {
            internal Block[] bArray;

            private int start, end;

            /// <summary>
            /// hold start index of internal block array
            /// </summary>
            public int Start
            {
                get { return start; }
                set
                {
                    if (value > bArray.Length || value < 0 || (value > End && End != -1))
                        throw new InvalidProgramException(string.Format("Start {0} is invalid while end is {1}", value, end));
                    else
                    {
                        start = value;
                    }
                }
            }

            /// <summary>
            /// hold the end index of internal block array
            /// </summary>
            public int End
            {
                get { return end; }
                set
                {
                    if (value > bArray.Length || value < -1 || (value < Start && Start != 0))
                        throw new InvalidProgramException(string.Format("End {0} is invalid while start is {1}", value, start));
                    else
                    {
                        end = value;
                    }
                }
            }

            /// <summary>
            /// the length is the count of blocks between start and end position
            /// </summary>
            public int Length
            {
                get
                {
                    return End == -1 ? 0: End - Start + 1; ;
                }
                private set { 
                }
            }

            /// <summary>
            /// instantiate a BlockArray object by given Block array.
            /// CAUTION: blocks in the array must be sorted. otherwise exception might throw.
            /// </summary>
            /// <param name="array"></param>
            public BlockArray(Block[] array)
            {
            	for(int i=array.Length-1; i>0; i--)
            		if (array[i].Index2 < array[i].Index1 || array[i].Index1 <= array[i-1].Index2)
            			throw new InvalidProgramException(string.Format("Block array are not sorted. index={0}", i));
                this.bArray = array;
                this.start = 0;
                this.end = array.Length -1;
            }

            /// <summary>
            /// create a new BlockArray object which share current internal block array
            /// </summary>
            /// <returns></returns>
            public BlockArray Clone()
            {
                var ba = new BlockArray(this.bArray);
                ba.start = start;
                ba.end = end;
                return ba;
            }

            /// <summary>
            /// create a new BlockArray object which share current internal block array.
            /// the start and end position of the new object will be set by given index and length
            /// </summary>
            /// <param name="index"></param>
            /// <param name="length"></param>
            /// <returns></returns>
            public BlockArray Clone(int index, int length)
            {
                var ba = new BlockArray(this.bArray);
                ba.start = this.start + index;
                ba.end = ba.start + length - 1;
                return ba;
            }

            /// <summary>
            /// will return the first available block, Start will be increased by 1  
            /// </summary>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Block TakeFirst()
            {
                if (start > end)
                    return null;
                return bArray[start++];
            }

            /// <summary>
            /// will return the last available block, End will be decreased by 1
            /// </summary>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Block TakeLast()
            {
                if (end < start)
                    return null;
                return bArray[end--];
            }

            /// <summary>
            /// will return the block at specified position.
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Block GetIndexOf(int index)
            {
                if (index + start > end)
                    return null;
                return bArray[index + start];
            }

            /// <summary>
            /// This help to dump the internal block info
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                var temp = new StringBuilder();
                for (int i = start; i <= end; i++)
                    temp.Append(bArray[i]).Append(",");
                return temp.ToString();
            }
        }

        /// <summary>
        /// Node hold the block data and the link to next one 
        /// </summary>
        private class Node
        {
            public Node[] Next { get; private set; }
            public Block Block { get; private set; }

            public Node (Block b, int level)
            {
                Next = new Node[level];
                Block = b;
            }
        }

        /// <summary>
        /// the first entry to the block array
        /// </summary>
        private Node head {get; set;}

        /// <summary>
        /// the height of the highest fast lane
        /// </summary>
        private int level {get; set;}
        
        public BlockSkipList(BlockArray blocks)
        {
            level = GetHeight(blocks.Length);
            Node begin = new Node(blocks.TakeFirst(),level);
            Node end = new Node(blocks.TakeLast(), level);

            head = begin;
            Insert(blocks, begin, end, level);
        }

        /// <summary>
        /// HXY: Since this block skip list only provides one time insertion and do NOT support deletion.
        /// therefore it can strictly enforced balancing and exhibtes stable perfornamce for searching.
        /// so when doing one time insertion, it MUST provides all the nodes, and those nodes MUST be sorted, 
        /// otherwise, a programing exception might throws.
        /// </summary>
        /// <param name="blocks">block array</param>
        /// <param name="begin">first fast lane</param>
        /// <param name="end">last fast lane</param>
        /// <param name="level">height of fast lane</param>
        private void Insert(BlockArray blocks, Node begin, Node end, int level)
        {
            if (begin.Block == null || end.Block == null)
                return;

            int length = blocks.Length;

            switch (length) {
            	
            	case 0:
            		{
    					begin.Next[0] = end;
    					break;
	           		}
            	case 1:
            		{
            			var mNode = new Node(blocks.TakeFirst(), 1);
		                begin.Next[0] = mNode;
		                mNode.Next[0] = end;
		                break;
            		}
            	case 2:
            		{
            			var mNode = new Node(blocks.TakeFirst(), level);
		                //link the first node, leave right resting as block which needs to insert "fast lane station"
		                begin.Next[level - 1] = mNode;
		                mNode.Next[level - 1] = end;
		                Insert(blocks.Clone(), mNode, end, level-1);                          
		                break;
            		}
            	default:
            		{
            			int middle = length / 2 ;
		                var mNode = new Node(blocks.GetIndexOf(middle), level);
		
		                bool odevity = length % 2 == 0;
		
		                //link start, middle and end
		                begin.Next[level - 1] = mNode;
		                mNode.Next[level -1] = end;
		
		                //splite right and left and then do recursive insertion
		                BlockArray lb = null, rb = null;
		                if (odevity)
		                {
		                    int llen = length / 2;
		                    int rlen = length / 2 - 1;
		                    lb = blocks.Clone(0, llen);
		                    rb = blocks.Clone(middle + 1, rlen);
		                }
		                else 
		                {
		                    int len = (length - 1) / 2;
		                    lb = blocks.Clone(0, len);
		                    rb = blocks.Clone(middle + 1, len);
		                }
		                
		                Insert(lb, begin, mNode, level-1);
		                Insert(rb, mNode, end, level-1);
		                break;
            		}
            } 
        }

        /// <summary>
        /// locate the block in the skip list by given value and return the block's value
        /// 
        /// HXY: in general, the value of the block should NOT be set to -1 because it's a reserved fail code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns> -1: not found; >0 : found</returns>
        public int Find(int value)
        {
        	Node temp = head;
        	for (int i = level; i >= 0 ; i--)
        	{
        		while (temp != null)
        		{
        			if (temp.Block.Index1 <= value && temp.Block.Index2 >= value)
        				return temp.Block.Value;

        			if (i >= 1 && temp.Next[i-1] != null && temp.Next[i-1].Block.Index1 <= value)
    					temp = temp.Next[i-1];
    				else
    					break;
        		}
        	}
        	return -1;
        }
        
        /// <summary>
        /// calculate the height of the "fast lane"
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private int GetHeight(int count)
        {
            int h = 0;
            while (true)
            {
                count = count >> 1;
                if (count > 0)
                    h++;
                else
                    break;
            }
            return h;
        }
    }
}
