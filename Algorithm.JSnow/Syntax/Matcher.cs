/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2017/2/20
 * Time: 21:30
 * 
 * 
 */
#define DIA_DEBUG_DISABLE
 
using System;
using System.Collections.Generic;

namespace Snow.Syntax
{
	/// <summary>
    /// A streaming Aho-Corasick automation which used for matching strings
    /// 
    /// Pleae note: one Matcher instance can be shared between Matchees that use diffent key word group
	/// </summary>
	public sealed class Matcher
	{
		private Node root = new Node();

        private static int trieTreeSpace = 0;

        public Matcher()
        {
        }

		public Matcher(string[] words)
		{
            AddKeyWord(words);
		}
		
        public void AddKeyWord(string[] words)
        {
            //build Trie tree
            for (int i = 0; i < words.Length; i++)
            {
                Insert(words[i]);
            }

            //setup fail pointer
            Build(root);
        }

		public void OnMatch(string key, int position)
		{
			System.Diagnostics.Debug.WriteLine("[{0}] was found at [{1}]", key, position);
		}

        public int Scan(Source src, Matchable m)
        {
            //HXY: queue which temprely cache leaf Nodes
            var leafNodes = new Queue<Node>(); 

            int count = 0;
            Node p = root;
            char[] str = src.cArray;
            for (int i = 0; i < str.Length; i++)
            {
            	//9 horizental tab is the smallest char
                int index = str[i] - 9;

                //HXY: the char in the array is 16b-it unicode char which used to represent all 
                // writen language throught out the world.
                // in case some char in Chinese or other country which have numeric value large than 127,
                // it will beyond the capacity of current trie tree and "index" will become invalid 
                // therefore leads to a IndexOutOfRange exception.
                //
                // In order to come through, 


                while (p.child[index] == null && p != root)
                {
                    p = p.fail;
                }
                p = p.child[index];
                p = (p == null) ? root : p;
                Node temp = p;
                while (temp != root && temp.count != -1)
                {
                    //HXY: when temp.count > 1, a matching found here. 
                    if (temp.count > 0)
                    {
                        //HXY: since leaf Node will be reset, keep track of them in a queue =>.
                        leafNodes.Enqueue(temp);

                        //tirgger matching event and exist if break condition (OnMatch <0) by the callbacker detected                        
                        if (m.OnMatch(src, temp.nChars, i) < 0)
                            return -1;
                    }

                    count += temp.count;                 
                    temp.count = -1; //Why reset count to -1? 
                    temp = temp.fail;
                }
                
                //<=and then revert it back to original status after while loop
                for (int k = 0; k < leafNodes.Count; k++ )
                {
                    Node n = leafNodes.Dequeue();
                    n.count = 1;
                }
            }
            return count;
        }
		
		/// <summary>
		/// build up Ac Machine by pass through the Nodes of the tree 
		/// </summary>
		/// <param name="root"></param>
		public void Build(Node root)
        {
            var queue = new Queue<Node>();
            root.fail = null;
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node temp = queue.Dequeue();
                Node p = null;
                
                for (int i = 0; i < 127-9+1; i++)
                {
                    if (temp.child[i] != null)
                    {
                        if (temp == root)
                        {
                            temp.child[i].fail = root;
                        }
                        else
                        {
                            p = temp.fail;
                            while (p != null)
                            {
                                if (p.child[i] != null)
                                {
                                    temp.child[i].fail = p.child[i];
                                    break;
                                }
                                p = p.fail;
                            }
                            if (p == null)
                            {
                                temp.child[i].fail = root;
                            }
                        }
                        queue.Enqueue(temp.child[i]);
                    }
                }
            }
        }
		
		public void Insert(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            char[] charArray = str.ToCharArray();
            Node cNode = root;

            for (int i=0; i <str.Length; i++)
            {
            	//9 horizental tab is the smallest char
                int index = charArray[i] - 9;
                if (cNode.child[index] == null)
                {
                    Node pNode = new Node();
                    cNode.child[index] = pNode;
                }
                cNode = cNode.child[index];   
            }
            cNode.count = 1;
            //HXY: assign the char array (keyword) to tail Node                   
            cNode.nChars = str;
        }
		
		/// <summary>
	    /// Char ASCII table (partial A ~ z)
	    /// ---------------------------------
	    /// 65        A         41
	    /// 90        Z         5A
	    /// 91        [         5B
	    /// 92        \         5C
	    /// 93        ]         5D
	    /// 94        ^         5E
	    /// 95        _         5F
	    /// 96        `         60 
	    /// 97        a         61
	    /// 122       z         7A
	    /// 
	    /// 
        /// https://www.techonthenet.com/ascii/chart.php
        /// 
	    /// FULL printable ASCII chars
	    /// ==============================================================
	    /// DEC	OCT	HEX	BIN	Symbol	HTML Number	HTML Name	Description
        /// 9	9		 	            horizontal tab
        /// 10	0A						LF (NL line feed, new line)
        /// 11							vertical Tab
        /// 13	0D						CR (carriage return)
        /// ---------------------------------------------------
		/// 32	040	20	00100000	 	&#32;	 	Space
		/// 33	041	21	00100001	!	&#33;	 	Exclamation mark
		/// 34	042	22	00100010	"	&#34;	&quot;	Double quotes (or speech marks)
		/// 35	043	23	00100011	#	&#35;	 	Number
		/// 36	044	24	00100100	$	&#36;	 	Dollar
		/// 37	045	25	00100101	%	&#37;	 	Procenttecken
		/// 38	046	26	00100110	&	&#38;	&amp;	Ampersand
		/// 39	047	27	00100111	'	&#39;	 	Single quote
		/// 40	050	28	00101000	(	&#40;	 	Open parenthesis (or open bracket)
		/// 41	051	29	00101001	)	&#41;	 	Close parenthesis (or close bracket)
		/// 42	052	2A	00101010	*	&#42;	 	Asterisk
		/// 43	053	2B	00101011	+	&#43;	 	Plus
		/// 44	054	2C	00101100	,	&#44;	 	Comma
		/// 45	055	2D	00101101	-	&#45;	 	Hyphen
		/// 46	056	2E	00101110	.	&#46;	 	Period, dot or full stop
		/// 47	057	2F	00101111	/	&#47;	 	Slash or divide
		/// 48	060	30	00110000	0	&#48;	 	Zero
		/// 49	061	31	00110001	1	&#49;	 	One
		/// 50	062	32	00110010	2	&#50;	 	Two
		/// 51	063	33	00110011	3	&#51;	 	Three
		/// 52	064	34	00110100	4	&#52;	 	Four
		/// 53	065	35	00110101	5	&#53;	 	Five
		/// 54	066	36	00110110	6	&#54;	 	Six
		/// 55	067	37	00110111	7	&#55;	 	Seven
		/// 56	070	38	00111000	8	&#56;	 	Eight
		/// 57	071	39	00111001	9	&#57;	 	Nine
		/// 58	072	3A	00111010	:	&#58;	 	Colon
		/// 59	073	3B	00111011	;	&#59;	 	Semicolon
		/// 60	074	3C	00111100	<	&#60;	&lt;	Less than (or open angled bracket)
		/// 61	075	3D	00111101	=	&#61;	 	Equals
		/// 62	076	3E	00111110	>	&#62;	&gt;	Greater than (or close angled bracket)
		/// 63	077	3F	00111111	?	&#63;	 	Question mark
		/// 64	100	40	01000000	@	&#64;	 	At symbol
		/// 65	101	41	01000001	A	&#65;	 	Uppercase A
		/// 66	102	42	01000010	B	&#66;	 	Uppercase B
		/// 67	103	43	01000011	C	&#67;	 	Uppercase C
		/// 68	104	44	01000100	D	&#68;	 	Uppercase D
		/// 69	105	45	01000101	E	&#69;	 	Uppercase E
		/// 70	106	46	01000110	F	&#70;	 	Uppercase F
		/// 71	107	47	01000111	G	&#71;	 	Uppercase G
		/// 72	110	48	01001000	H	&#72;	 	Uppercase H
		/// 73	111	49	01001001	I	&#73;	 	Uppercase I
		/// 74	112	4A	01001010	J	&#74;	 	Uppercase J
		/// 75	113	4B	01001011	K	&#75;	 	Uppercase K
		/// 76	114	4C	01001100	L	&#76;	 	Uppercase L
		/// 77	115	4D	01001101	M	&#77;	 	Uppercase M
		/// 78	116	4E	01001110	N	&#78;	 	Uppercase N
		/// 79	117	4F	01001111	O	&#79;	 	Uppercase O
		/// 80	120	50	01010000	P	&#80;	 	Uppercase P
		/// 81	121	51	01010001	Q	&#81;	 	Uppercase Q
		/// 82	122	52	01010010	R	&#82;	 	Uppercase R
		/// 83	123	53	01010011	S	&#83;	 	Uppercase S
		/// 84	124	54	01010100	T	&#84;	 	Uppercase T
		/// 85	125	55	01010101	U	&#85;	 	Uppercase U
		/// 86	126	56	01010110	V	&#86;	 	Uppercase V
		/// 87	127	57	01010111	W	&#87;	 	Uppercase W
		/// 88	130	58	01011000	X	&#88;	 	Uppercase X
		/// 89	131	59	01011001	Y	&#89;	 	Uppercase Y
		/// 90	132	5A	01011010	Z	&#90;	 	Uppercase Z
		/// 91	133	5B	01011011	[	&#91;	 	Opening bracket
		/// 92	134	5C	01011100	\	&#92;	 	Backslash
		/// 93	135	5D	01011101	]	&#93;	 	Closing bracket
		/// 94	136	5E	01011110	^	&#94;	 	Caret - circumflex
		/// 95	137	5F	01011111	_	&#95;	 	Underscore
		/// 96	140	60	01100000	`	&#96;	 	Grave accent
		/// 97	141	61	01100001	a	&#97;	 	Lowercase a
		/// 98	142	62	01100010	b	&#98;	 	Lowercase b
		/// 99	143	63	01100011	c	&#99;	 	Lowercase c
		/// 100	144	64	01100100	d	&#100;	 	Lowercase d
		/// 101	145	65	01100101	e	&#101;	 	Lowercase e
		/// 102	146	66	01100110	f	&#102;	 	Lowercase f
		/// 103	147	67	01100111	g	&#103;	 	Lowercase g
		/// 104	150	68	01101000	h	&#104;	 	Lowercase h
		/// 105	151	69	01101001	i	&#105;	 	Lowercase i
		/// 106	152	6A	01101010	j	&#106;	 	Lowercase j
		/// 107	153	6B	01101011	k	&#107;	 	Lowercase k
		/// 108	154	6C	01101100	l	&#108;	 	Lowercase l
		/// 109	155	6D	01101101	m	&#109;	 	Lowercase m
		/// 110	156	6E	01101110	n	&#110;	 	Lowercase n
		/// 111	157	6F	01101111	o	&#111;	 	Lowercase o
		/// 112	160	70	01110000	p	&#112;	 	Lowercase p
		/// 113	161	71	01110001	q	&#113;	 	Lowercase q
		/// 114	162	72	01110010	r	&#114;	 	Lowercase r
		/// 115	163	73	01110011	s	&#115;	 	Lowercase s
		/// 116	164	74	01110100	t	&#116;	 	Lowercase t
		/// 117	165	75	01110101	u	&#117;	 	Lowercase u
		/// 118	166	76	01110110	v	&#118;	 	Lowercase v
		/// 119	167	77	01110111	w	&#119;	 	Lowercase w
		/// 120	170	78	01111000	x	&#120;	 	Lowercase x
		/// 121	171	79	01111001	y	&#121;	 	Lowercase y
		/// 122	172	7A	01111010	z	&#122;	 	Lowercase z
		/// 123	173	7B	01111011	{	&#123;	 	Opening brace
		/// 124	174	7C	01111100	|	&#124;	 	Vertical bar
		/// 125	175	7D	01111101	}	&#125;	 	Closing brace
		/// 126	176	7E	01111110	~	&#126;	 	Equivalency sign - tilde
		/// 127	177	7F	01111111		&#127;	 	Delete
	    /// </summary>
		public class Node {
			public int count {set; get;}
	        public Node fail { set; get; }
	        public Node[] child { set; get; }
	        public string nChars { set; get; }
	
	        public Node()
	        {
	            fail = null;
	            count = 0;
	            child = new Node[127-9+1]; //extend it to all printable ASCII chars
	            trieTreeSpace += child.Length;
#if (DIA_DEBUG)					
System.Diagnostics.Debug.WriteLine(string.Format("{0} Nodes in total were allocated",trieTreeSpace));
#endif 
	        }
		}
	}

    public interface Matchable
    {
        int OnMatch(Source src, string key, int position);
    }
}
