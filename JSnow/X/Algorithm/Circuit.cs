using Snow.Syntax;
using Snow.XAlgorithm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace Snow.X.Algorithm
{
    public sealed class Builder
    {
        #region Build Circuit componment
        public static Circuit Integer()
        {
            var integer = new Circuit("Integer");

            var line1 = new Line("Int.1");
            line1.Point(integer.Exit);

            var nodeZero = new Node("48,48,0");
            nodeZero.Next = line1;

            var line2 = new Line("Int.2");
            line2.Point(integer.Exit);

            var nodeDigit = new Node("48,57,9");
            line2.Point(nodeDigit);

            nodeDigit.Next = line2;

            var nodeDigitExceptZero = new Node("49,57,9");
            nodeDigitExceptZero.Next = line2;

            var line3 = new Line("Int.3");
            line3.Point(nodeZero);
            line3.Point(nodeDigitExceptZero);

            integer.Entry.Next = line3;

            return integer;
        }

        public static Circuit Fraction()
        {
            var fraction = new Circuit("Fraction");

            var line1 = new Line("Fra.1");
            line1.Point(fraction.Exit);

            var line2 = new Line("Fra.2");
            line2.Point(fraction.Exit);

            var nodeDigit = new Node("48,57,9");
            line2.Point(nodeDigit);

            nodeDigit.Next = line2;

            var nodeDot = new Node("46,46,0");
            nodeDot.Next = line2;

            var line3 = new Line("Fra.3");
            line3.Point(nodeDot);

            fraction.Entry.Next = line3;

            return fraction;
        }

        public static Circuit Exponent()
        {
            var exponent = new Circuit("Exponent");

            var line1 = new Line("Exp.1");
            line1.Point(exponent.Exit);

            var nodeDigit = new Node("48,57,9");
            line1.Point(nodeDigit);

            nodeDigit.Next = line1;

            var line3 = new Line("Exp.3");
            line3.Point(nodeDigit);


            var nodePlus = new Node("43,43,0");
            nodePlus.Next = line3;

            var nodeMinus = new Node("45,45,0");
            nodeMinus.Next = line3;


            var line4 = new Line("Exp.4");
            line4.Point(nodeDigit);
            line4.Point(nodePlus);
            line4.Point(nodeMinus);

            var nodeLE = new Node("101,101,0");
            nodeLE.Next = line4;


            var nodeUE = new Node("69,69,0");
            nodeUE.Next = line4;

            var line5 = new Line("Exp.5");
            line5.Point(nodeLE);
            line5.Point(nodeUE);

            exponent.Entry.Next = line5;

            return exponent;
        }

        public static Circuit NumberLiteral()
        {
            var number = new Circuit("Number");

            var line1 = new Line("Num.1");
            line1.Point(number.Exit);

            var exponent = Exponent();
            exponent.Next = line1;

            var line2 = new Line("Num.2");
            line2.Point(number.Exit);
            line2.Point(exponent);

            var fraction = Fraction();
            fraction.Next = line2;

            var line3 = new Line("Num.3");
            line3.Point(number.Exit);
            line3.Point(exponent);
            line3.Point(fraction);


            var integer = Integer();
            integer.Next = line3;

            var line4 = new Line("Num.4");
            line4.Point(integer);


            number.Entry.Next = line4;
            return number;
        }
    
        public static Circuit Name()
        {
            var name = new Circuit("Name");

            var line1 = new Line("Name.1");
            line1.Point(name.Exit);

            var letter = new Node("65,90,0#97,122,0");
            letter.Next = line1;

            var line2 = new Line("Name.2");
            line2.Point(name.Exit);

            var digit = new Node("48,57,0");
            digit.Next = line2;

            var line3 = new Line("Name.3");
            line3.Point(name.Exit);

            var hyphen = new Node("95,95,0");
            hyphen.Next = line3;

            line1.Point(letter);
            line1.Point(digit);
            line1.Point(hyphen);

            line2.Point(letter);
            line2.Point(digit);
            line2.Point(hyphen);

            line3.Point(letter);
            line3.Point(digit);
            line3.Point(hyphen);

            name.Entry.Next = line1;
            return name;
        }

        public static Circuit Escaped()
        {
            var escaped = new Circuit("Escaped");

            var line1 = new Line("Escaped.1");
            line1.Point(escaped.Exit);

            var doubleQuote = new Node("34,34,0");
            doubleQuote.Next = line1;

            var singleQuote = new Node("39,39,0");
            singleQuote.Next = line1;

            var backSlash = new Node("92,92,0");
            backSlash.Next = line1;

            var slash = new Node("47,47,0");
            slash.Next = line1;

            var backspace = new Node("98,98,0");
            backspace.Next = line1;

            var formfeed = new Node("102,102,0");
            formfeed.Next = line1;

            var newline = new Node("110,110,0");
            newline.Next = line1;

            var carrageReturn = new Node("114,114,0");
            carrageReturn.Next = line1;

            var tab = new Node("116,116,0");
            tab.Next = line1;

            var hexDecimal4 = HexDecimal4();
            carrageReturn.Next = line1;

            var line2 = new Line("HexDecimal4.1");
            line2.Point(hexDecimal4);

            var u = new Node("117,117,0");
            u.Next = line2;

            var line3 = new Line("Escaped.2");
            line3.Point(u);
            line3.Point(tab);
            line3.Point(carrageReturn);
            line3.Point(newline);
            line3.Point(formfeed);
            line3.Point(backspace);
            line3.Point(slash);
            line3.Point(backSlash);
            line3.Point(singleQuote);
            line3.Point(doubleQuote);

            var bslash = new Node("92,92,0");
            bslash.Next = line3;

            var line4 = new Line("Escaped.3");
            line4.Point(bslash);

            escaped.Entry.Next = line4;
            return escaped;
        }

        public static Circuit HexDecimal4()
        {
            var hexd4 = new Circuit("HexDecimal4");

            var line1 = new Line("HexDecimal4.1");
            line1.Point(hexd4.Exit);

            var d4 = new Node("48,57,0");
            d4.Next = line1;

            var line2 = new Line("HexDecimal4.2");
            line2.Point(d4);

            var d3 = new Node("48,57,0");
            d3.Next = line2;

            var line3 = new Line("HexDecimal4.3");
            line3.Point(d3);

            var d2 = new Node("48,57,0");
            d2.Next = line3;

            var line4 = new Line("HexDecimal4.4");
            line4.Point(d2);

            var d1 = new Node("49,57,0");
            d1.Next = line4;

            var line5 = new Line("HexDecimal4.5");
            line5.Point(d1);

            hexd4.Entry.Next = line5;
            return hexd4;
        }

        public static Circuit UnEChars()
        {
            var ue = new Circuit("UnicodeEscape");

            var line1 = new Line("UnicodeEscape.1");
            line1.Point(ue.Exit);

            //any Unicode character except " and \ and control character
            //TODO: exclude control chars
            var anyUniCodeEx = new Node("0,33,0#35,91,0#93,65536,0");
            anyUniCodeEx.Next = line1;

            var escaped = Escaped();
            escaped.Next = line1;

            line1.Point(anyUniCodeEx);
            line1.Point(escaped);

            var line2 = new Line("UnicodeEscape.2");
            line2.Point(anyUniCodeEx);
            line2.Point(escaped);

            ue.Entry.Next = line2;
            return ue;
        }

        public static Circuit StringLiteral()
        {
            var str = new Circuit("String");

            var line1 = new Line("String.1");
            line1.Point(str.Exit);

            var doubleQuote2 = new Node("34,34,0");
            doubleQuote2.Next = line1;

            var line2 = new Line("String.2");
            line2.Point(doubleQuote2);

            var ue = UnEChars();
            ue.Next = line2;

            var line3 = new Line("String.3");
            line3.Point(ue);
            line3.Point(doubleQuote2);

            var doubleQuote1 = new Node("34,34,0");
            doubleQuote1.Next = line3;

            //single part           
            var line21 = new Line("String.21");
            line21.Point(str.Exit);

            var singleQuote2 = new Node("39,39,0");
            singleQuote2.Next = line21;

            var ue2 = UnEChars();
            ue2.Next = line21;

            var line31 = new Line("String.31");
            line31.Point(ue2);
            line31.Point(singleQuote2);

            var singleQuote1 = new Node("39,39,0");
            singleQuote1.Next = line31;

            var line4 = new Line("String.4");
            line4.Point(doubleQuote1);
            line4.Point(singleQuote1);
            
            str.Entry.Next = line4;

            return str;
        }

        /// <summary>
        /// http://www.dyn-web.com/tutorials/object-literal/
        /// </summary>
        /// <returns></returns>
        public static Circuit Test_WH_ObjectLiteral()
        {
            var objLiteral = new Circuit("ObjectLiteral");

            var line1 = new Line("ObjectLiteral.1");
            line1.Point(objLiteral.Exit);

            var closeBrace = new Node("125,125,0");
            closeBrace.Next = line1;

            var line2 = new Line("ObjectLiteral.2");
            line2.Point(closeBrace);

            var comma = new Node("44,44,0");
            line2.Point(comma);

            var line3 = new Line("ObjectLiteral.3");
            var name = Name();
            comma.Next = line3;
            line3.Point(name);

            var strLiteral = StringLiteral();
            line3.Point(strLiteral);
            
            var line4 = new Line("ObjectLiteral.4");
            name.Next = line4;
            strLiteral.Next = line4;

            var colon = new Node("58,58,0");
            line4.Point(colon);

            var line5 = new Line("ObjectLiteral.5");
            colon.Next = line5;

            //TODO: this wormhole with objectLiteral is only for testing purpose.
            var wh = Wormhole("objectWrom", objLiteral);
            line5.Point(wh);
            wh.Next = line2;

            var line6 = new Line("ObjectLiteral.6");
            line6.Point(closeBrace);
            line6.Point(name);
            line6.Point(strLiteral);

            var openBrace = new Node("123,123,0");
            openBrace.Next = line6;

            var line7 = new Line("ObjectLiteral.7");
            line7.Point(openBrace);

            objLiteral.Entry.Next = line7;

            return objLiteral;
        }

        public static Circuit Wormhole(string name, Circuit parent)
        {
            var wormhole = new Wormhole("Wormhole." + name, parent);           
            return wormhole;
        }
    }
    #endregion

    /// <summary>
    /// this make child circuit have a chance to link to its parents.
    /// </summary>
    public class Wormhole : Circuit
    {
        private Circuit parent;

         public Wormhole(string name, Circuit p)
        {
            this.name = name;
            parent = p;
            this.Entry = parent.Entry;
        }

        protected override void OnEnter(int chr)
        {
            System.Diagnostics.Debug.WriteLine("Wormhole - OnEnter for [{0}]", (char)chr);
            parent.Exit.Push(this);
        }

        protected override void OnLeave(int chr)
        {
            System.Diagnostics.Debug.WriteLine("Wormhole - OnLeave for [{0}]", (char)chr);
        }
    }

    public class Circuit : Node
    {
        public Node Entry { set; get; }

        public Node Exit { set; get; }

        public override Line Next
        {
            get
            {
                return base.Next;
            }
            set
            {
                base.Next = value;
                //HXY: link on circuit level
                this.Exit.Next = value;
            }
        }

        public Circuit(string name)
        {
            this.name = name;
            Entry = new Node();
            Exit = new Node();
        }
        
        public Circuit()
        {
            Entry = new Node();
            Exit = new Node();
        }

        internal override Node Select(int chr)
        {
            Node node = Entry.Next.Route(chr);
            if (node != null)
                OnEnter(chr);
            return node;
        }

        protected virtual void OnEnter(int chr)
        {
        }

        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Check if the given src can be successfully transite in this circuit
        /// </summary>
        /// <param name="src"></param>
        /// <returns>-3: reach the end of source;</returns>
        public int Transit(Source src)
        {
            Node current = this.Entry; int chr = 0;
            for (; ;)
            {
                chr = src.Take();
                if (chr == -1) return -3;
                current = current.Next.Route(chr);
                if (current == null) return -1;
            }
        }
    }
    
	/// <summary>
	/// HXY: inherit form stack since a Exit node may hold references to wromholes
	/// </summary>
    public class Node : Stack<Circuit>
    {
        public virtual Line Next {set; get;}

        private BlockSkipList skipList;

        internal List<BlockSkipList.Block> BlockArray;

        protected string name;

        public Node()
        {
        }
        
        public Node(string space)
        {
            if (space.Length == 0)
                return;
            this.name = space;

            BlockArray = new List<BlockSkipList.Block>();

            string[] blocks, items;
            int x, y, z, lb = 0;
            
            blocks = space.Split('#');
            for (int j = 0; j < blocks.Length; j++)
            {
                items = blocks[j].Split(',');
                x = Convert.ToInt32(items[0]);
                y = Convert.ToInt32(items[1]);
                z = Convert.ToInt32(items[2]);
                if (x > lb)
                {
                    //undefined area found
                    BlockArray.Add(new BlockSkipList.Block(lb, x - 1, -1));
                }
                BlockArray.Add(new BlockSkipList.Block(x, y, z));
                lb = y + 1;
            }
            // make sure to block space cover 0 ~ 2(16) 
            if (lb < (65536 + 1))
                BlockArray.Add(new BlockSkipList.Block(lb, 65536, -1));

            skipList = new BlockSkipList(new BlockSkipList.BlockArray(BlockArray.ToArray()));
        }


        protected virtual void OnLeave(int chr)
        {                
        }

        /// <summary>
        /// check whether this node is selectable
        /// </summary>
        /// <param name="chr">given char to match</param>
        /// <returns></returns>
        internal virtual Node Select(int chr)
        {
            //HXY: Wormhole detected
            Circuit wormhole = this.Count > 0 ? this.Pop() : null;
            if (wormhole != null)
            {
                wormhole.OnLeave(chr);
                System.Diagnostics.Debug.WriteLine("Wormhole Select - try Exit.next for [{0}]", (char)chr);
                return wormhole.Exit.Next.Route(chr);
            }

            //HXY: Exit of Circuit 
            if (skipList == null && this.Next != null)
            {
                System.Diagnostics.Debug.WriteLine("Select - try Exit.next for [{0}]", (char)chr);
                return this.Next.Route(chr);
            }
            
			//HXY: node level selection	            
            return skipList == null ? null : skipList.Find(chr) == -1 ? null : this;
        }

        public override string ToString()
        {
            return name;
        }

    }

    /// <summary>
    /// Hode the pointers to the nodes
    /// </summary>
    public class Line
    {
        private List<Node> Distances = new List<Node>();

        /// <summary>
        /// this is a block based skiplist which used to find node even more faster
        /// </summary>
        private SkipList2 _distances = new SkipList2();

        private string name;

        public Line(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// add a pointer to destine node
        /// </summary>
        /// <param name="dest"></param>
        public void Point(Node dest)
        {
            Distances.Add(dest);
            if (dest.BlockArray != null)
                foreach(var block in dest.BlockArray)
                {
                    if (block.Value != -1)
                        _distances.Insert(new SkipList2.Block(block.Index1, block.Index2, dest));
                }                    
        }

        /// <summary>
        /// Route to the correct node, two option are available, skiplist based fast lane and array based normal lane 
        /// </summary>
        /// <param name="chr">given char to be searched</param>
        /// <returns>the online node</returns>
        public Node Route(int chr)
        {
            //1. try fast lane
            Node node = (Node)_distances.Find(chr);
            if (node != null)
            {
                System.Diagnostics.Debug.WriteLine("Route - by fast lane for [{0}]", (char)chr);
                return node;
            }                
            //2. try normal way
            foreach (var dest in Distances)
            {
                node = dest.Select(chr);
                if (node != null)
                {
                    System.Diagnostics.Debug.WriteLine("Route - by normal lane for [{0}]", (char)chr);
                    return node;
                }                        
            }                            
            return null;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
