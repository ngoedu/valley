using Snow.Syntax;
using Snow.XAlgorithm;
using System;
using System.Collections.Generic;

namespace Snow.X.Algorithm
{
    public sealed class StateMatrix
    {
        private readonly List<BlockSkipList> matrix = new List<BlockSkipList>();

        /// <summary>
        /// this code will stop the machine when matched it,
        /// by default, it set to -1 means any mismatch will stop teh machine
        /// </summary>
        private int stopCode = -1;

        public StateMatrix()
        {
        }

        public StateMatrix(int stop)
        {
            stopCode = stop;
        }

        public void Append(BlockSkipList blocks)
        {
            matrix.Add(blocks);
        }

        public void Append(BlockSkipList.Block[] data)
        {
            var blocks = new BlockSkipList(new BlockSkipList.BlockArray(data));
            Append(blocks);
        }

        /// <summary>
        /// parse string based BSL matrax
        /// '#' is spliter of block in one line
        /// '|' is spliter of lines
        /// </summary>
        /// <param name="matrix">string based matrix</param>
        public void Append(string matrix)
        {
            if (matrix.Length == 0)
                return;
            var bArray = new List<BlockSkipList.Block>();

            string[] lines = matrix.Split('|');
            string[] blocks, items;
            int x, y, z, lb = 0;
            for (int i=0; i<lines.Length; i++)
            {
                 blocks= lines[i].Split('#');
                 for (int j = 0; j < blocks.Length; j++)
                 {
                     items = blocks[j].Split(',');
                     x = Convert.ToInt32(items[0]);
                     y = Convert.ToInt32(items[1]);
                     z = Convert.ToInt32(items[2]);
                     if (x > lb) 
                     {
                         //undefined area found
                         bArray.Add(new BlockSkipList.Block(lb, x - 1, -1));
                     }
                     bArray.Add(new BlockSkipList.Block(x, y, z));
                     lb = y+1;
                 }
                 // make sure to block space cover 0 ~ 2(16) 
                 if (lb < (65536 + 1))
                     bArray.Add(new BlockSkipList.Block(lb, 65536, -1));
                     
                 this.Append(bArray.ToArray());
                 //reset for new line
                 lb = 0;
                 bArray.Clear();
            }

        }

        /// <summary>
        /// Do transite until 
        /// 	1.reach the end of char array or
		/// 	2.meet given stop code or 
		/// 	3.mismatch (state=-1) occures
        /// </summary>
        /// <param name="chars">char array</param>
        /// <returns></returns>
        public int Transit(char[] chars)
        {
            int state = 0;
            for (int idx = 0; idx < chars.Length && state != -1 && state != stopCode; idx++)
            {
                state = matrix[state].Find((int)chars[idx]);
            }
            return state;
        }

        /// <summary>
        /// Do transite until 
        /// 	1.reach the end of source (state=-3)
		/// 	2.meet given stop code or 
        /// 	3.mismatch occures (state=-1) 
        /// </summary>
        /// <param name="src">Source object</param>
       /// <returns></returns>
        public int Transit(Source src)
        {
            int state = 0, chr = 0;
            for (; chr != -1 && state != stopCode && state != -1; )
            {
                chr = src.Take();
                if (chr == -1) return -3;
                state = matrix[state].Find(chr);
            }
            return state;
        }
    }
}
