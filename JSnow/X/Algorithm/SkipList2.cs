using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.XAlgorithm
{
    /// <summary>
    /// http://igoro.com/archive/skip-lists-are-fascinating/
    /// 
    /// HXY: This skip list implemention is based on block which hold the range of values.
    /// </summary>
    public class SkipList2
    {
        public class Block
        {
            public int Value1 { get; set; }
            public int Value2 { get; set; }
            public Object NodeObject { set; get; }

            public Block(int v1, int v2, Object obj)
            {
                Value1 = v1;
                Value2 = v2;
                NodeObject = obj;
            }
        }

        private class Node
        {
            public Node[] Next { get; private set; }
            public Block Value { get; private set; }

            public Node(Block value, int level)
            {
                Value = value;
                Next = new Node[level];
            }
        }

        private Node _head = new Node(null, 33); // The max. number of levels is 33
        private Random _rand = new Random();
        private int _levels = 1;

        /// <summary>
        /// Inserts a value/block into the skip list.
        /// </summary>
        public void Insert(Block value)
        {
            // Determine the level of the new node. Generate a random number R. The number of
            // 1-bits before we encounter the first 0-bit is the level of the node. Since R is
            // 32-bit, the level can be at most 32.
            int level = 0;
            for (int R = _rand.Next(); (R & 1) == 1; R >>= 1)
            {
                level++;
                if (level == _levels) 
                { 
                    _levels++; break; 
                }
            }
            
            // Insert this node into the skip list
            Node newNode = new Node(value, level + 1);
            Node cur = _head;
            for (int i = _levels - 1; i >= 0; i--)
            {
                for (; cur.Next[i] != null; cur = cur.Next[i])
                {
                    if (cur.Next[i].Value.Value1 > value.Value2) 
                        break;
                }
                if (i <= level) 
                { 
                    newNode.Next[i] = cur.Next[i];
                    cur.Next[i] = newNode;
                }
            }
        }

        /// <summary>
        /// Returns whether a particular value/block already exists in the skip list
        /// </summary>
        public bool Contains(Block value)
        {
            Node cur = _head;
            for (int i = _levels - 1; i >= 0; i--)
            {
                for (; cur.Next[i] != null; cur = cur.Next[i])
                {
                    if (cur.Next[i].Value.Value1 > value.Value2)
                        break;
                    if (cur.Next[i].Value.Value1 <= value.Value1 && cur.Next[i].Value.Value2 >= value.Value2)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns whether a particular value/block already exists in the skip list
        /// </summary>
        public Object Find(int value)
        {
            Node cur = _head;
            for (int i = _levels - 1; i >= 0; i--)
            {
                for (; cur.Next[i] != null; cur = cur.Next[i])
                {
                    if (cur.Next[i].Value.Value1 > value)
                        break;
                    if (cur.Next[i].Value.Value1 <= value && cur.Next[i].Value.Value2 >= value)
                        return cur.Next[i].Value.NodeObject;
                }
            }
            return null;
        }

        /// <summary>
        /// Attempts to remove one occurence of a particular value/block from the skip list. Returns
        /// whether the value was found in the skip list.
        /// </summary>
        public bool Remove(Block value)
        {
            Node cur = _head;

            bool found = false;
            for (int i = _levels - 1; i >= 0; i--)
            {
                for (; cur.Next[i] != null; cur = cur.Next[i])
                {
                    if (cur.Next[i].Value.Value1 == value.Value1 && cur.Next[i].Value.Value2 == value.Value2)
                    {
                        found = true;
                        cur.Next[i] = cur.Next[i].Next[i];
                        break;
                    }

                    if (cur.Next[i].Value.Value1 > value.Value2) break;
                }
            }

            return found;
        }

        /// <summary>
        /// Produces an enumerator that iterates over elements in the skip list in order.
        /// </summary>
        public IEnumerable<Block> Enumerate()
        {
            Node cur = _head.Next[0];
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next[0];
            }
        }
    }
}
