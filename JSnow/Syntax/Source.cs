/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2017/3/7
 * Time: 15:50
 * 
 * 
 */
#define DIA_DEBUG_DEL
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Snow.Syntax
{
    /// <summary>
    ///  Source object have an internal char buffer which can be shared between other Source object. 
    ///  this behaivor can elimiate creating more char arrays if it is sharable between Source objects .
    /// </summary>
    public sealed class Source
    {
        #region printable ASCII chars code
        /// 
        /// ===================================================
        /// 9   9                       Horizental TAB
        /// 10	0A						LF (NL line feed, new line)
        /// 11							Vertical Tab
        /// 13	0D						CR (carriage return)	    
        /// ---------------------------------------------------
        /// 32	040	20	00100000	 	&#32;	 	Space
        /// 33	041	21	00100001	!	&#33;	 	Exclamation mark
        /// 34	042	22	00100010	"	&#34;		Double quotes (or speech marks)
        /// 35	043	23	00100011	#	&#35;	 	Number
        /// 36	044	24	00100100	$	&#36;	 	Dollar
        /// 37	045	25	00100101	%	&#37;	 	Procenttecken
        /// 38	046	26	00100110	&	&#38;		Ampersand
        /// 39	047	27	00100111	'	&#39;	 	Single quote
        /// 40	050	28	00101000	(	&#40;	 	Open parenthesis (or open bracket)
        /// 41	051	29	00101001	)	&#41;	 	Close parenthesis (or close bracket)
        /// 42	052	2A	00101010	*	&#42;	 	Asterisk
        /// 43	053	2B	00101011	+	&#43;	 	Plus
        /// 44	054	2C	00101100	,	&#44;	 	Comma
        /// 45	055	2D	00101101	-	&#45;	 	Hyphen
        /// 46	056	2E	00101110	.	&#46;	 	Period, dot or full stop
        /// 47	057	2F	00101111	/	&#47;	 	Slash or divide
        /// ---------------------------------------------------
        /// 48	060	30	00110000	0	&#48;	 	Zero
        /// 49	061	31	00110001	1	&#49;	 	One
        /// 50	062	32	00110010	2	&#50;	 	Two
        /// 51	063	33	00110011	3	&#51;	 	Three
        /// 52	064	34	00110100	4	&#52;	 	Four
        /// 53	065	35	00110101	5	&#53;	 	Five
        /// 54	066	36	00110110	6	&#54;	 	Six
        /// 55	067	37	00110111	7	&#55;	 	Seven
        /// 56	070	38	00111000	8	&#56;	 	Eight
        /// 57	071	39	00111001	9	&#57;	 	Nine.
        /// ---------------------------------------------------
        /// 58	072	3A	00111010	:	&#58;	 	Colon
        /// 59	073	3B	00111011	;	&#59;	 	Semicolon
        /// 60	074	3C	00111100	<	&#60;		Less than (or open angled bracket)
        /// 61	075	3D	00111101	=	&#61;	 	Equals
        /// 62	076	3E	00111110	>	&#62;	    Greater than (or close angled bracket)
        /// 63	077	3F	00111111	?	&#63;	 	Question mark
        /// 64	100	40	01000000	@	&#64;	 	At symbol
        /// --------------------------------------------------
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
        /// 123                     {
        /// 124                     |
        /// 125                     }
        /// 126                     ~
        #endregion

        #region Char constant here
        public  const char CHAR_SPACE = ' ';
        public  const char CHAR_SPLITOR = ';';
        public  const char CHAR_LBR = '{';
        public  const char CHAR_RBR = '}';
        public  const char CHAR_LBK = '(';
        public  const char CHAR_RBK = ')';
        public  const char CHAR_EQ = '=';

        public static readonly char[] COMMENT = "//".ToCharArray();
        public static readonly char[] OSC = "/*".ToCharArray();  //Open Section Comment
        public static readonly char[] CSC = "*/".ToCharArray();  //Close Section Comment        
        public static readonly char[] LB = System.Environment.NewLine.ToCharArray();
 
        #endregion

        #region Key words constant here
        public static readonly string KEY_FUNCTION = "function";
        public static readonly string KEY_IF = "if";
        public static readonly string KEY_ELSE = "else";
        public static readonly string KEY_FOR = "for";
        public static readonly string KEY_WHILE = "while";
        public static readonly string KEY_VAR = "var";
        #endregion

        #region System constants here
        public static readonly string CONST_DEFAULT = "__DEFAULT__";
        #endregion

        public static readonly Dictionary<string, string> KEY_DICT = new Dictionary<string, string>();

        static Source()
        {
            //initilze KEY_DICT
            KEY_DICT.Add(KEY_FUNCTION, "?");
            KEY_DICT.Add(KEY_IF, "?");
            KEY_DICT.Add(KEY_ELSE, "?");
            KEY_DICT.Add(KEY_FOR, "?");
            KEY_DICT.Add(KEY_WHILE, "?");
            KEY_DICT.Add(KEY_VAR, "?");
        }

        private int start, end;

        /// <summary>
        /// hold start index of internal char array
        /// </summary>
        public int Start
        {
            get { return start; }
            set
            {
                if (value > cArray.Length || value < 0 || (value > End && End != -1))
                    throw new InvalidProgramException(string.Format("Start {0} is invalid while end is {1}", value, end));
                else 
                {
                    start = value;
                }                
            }           
        }

        /// <summary>
        /// hold the end index of internal char array
        /// </summary>
        public int End
        {
            get { return end; }
            set {
                    if (value > cArray.Length || value < -1 || (value < Start && Start != 0))
                        throw new InvalidProgramException(string.Format("End {0} is invalid while start is {1}", value, start));
                    else
                    {
                        end = value;
                    }                   
                }          
        }

        [System.Obsolete("this property only used for debugging purpose")]
        public string Content
        {
            get
            {
                char[] tempArray = new char[this.Length];
                Array.Copy(cArray, Start, tempArray, 0, Length);
                return new String(tempArray);
            }
        }

        /// <summary>
        /// the total number of chars between start and end
        /// End==-1 means content is cleared, therefore Length = 0 
        /// </summary>
        public int Length
        {
            get { return End == -1 ? 0: End - Start + 1; }
        }

        
        /// <summary>
        /// hole internal char array
        /// CAUTION: make sure you know HOW TO operate with this internal content. 
        /// </summary>
        public char[] cArray;

        /// <summary>
        /// create via string
        /// </summary>
        /// <param name="src"></param>
        public Source(string src)
            : this(src.ToCharArray())
        {
        }

        /// <summary>
        /// create Source object via char array
        /// End == -1 indicated Source is empty
        /// </summary>
        /// <param name="srcArray"></param>
        public Source(char[] srcArray)
        {
            this.cArray = srcArray;
            this.Start = 0;
            this.End = cArray.Length -1;
        }

        /// <summary>
        /// take current char if available, otherwise -1
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Take()
        {
            return Length==0 ? -1: cArray[start++];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Drop()
        {
            if (start > 0)
                start--;
        }

        /// <summary>
        /// reset the End and therefore the length, do NOTHING with content in the char array
        /// </summary>
        public void Clear()
        {
            Start = 0;
            End = -1;
        }

        /// <summary>
        /// get the index of the char in this char array
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int IndexOf(char c)
        {
            for (int i = Start; i < End; i++)
                if (c == cArray[i])
                    return i;
            return -1;
        }

        /// <summary>
        /// replicate a source object and share the same internal char array 
        /// </summary>
        /// <param name="ptrStart"></param>
        /// <param name="ptrEnd"></param>
        /// <returns></returns>
        public Source Clone(int ptrStart, int ptrEnd)
        {
            var clone = new Source(this.cArray);
            clone.Start = ptrStart;
            clone.End = ptrEnd;
            return clone;
        }

        /// <summary>
        /// create a new source array with same [Length] of original, replicate content from original and do NOT share with it 
        /// 
        /// using: public static void Copy(	Array sourceArray,	int sourceIndex, Array destinationArray, int destinationIndex, int length)
        /// 
        /// </summary>
        /// <returns></returns>
        public Source Copy()
        {
            char[] nArray = new Char[this.Length];
            Array.Copy(cArray, Start, nArray, 0, this.Length);
            var copy = new Source(nArray);
            TotalAllocated += copy.Length;
#if (DIA_DEBUG)
System.Diagnostics.Debug.WriteLine("[{0}] was Copied, total=[{1}]", copy.Length,TotalAllocated);
#endif
            return copy;
        }

        /// <summary>
        /// copy original's chars start from startPos with given length to a new source array 
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public Source Copy(int startPos, int endPos)
        {
            char[] nArray = new Char[endPos - startPos + 1];
            Array.Copy(cArray, startPos, nArray, 0, nArray.Length);
            var copy = new Source(nArray);         
            TotalAllocated += copy.Length;
#if (DIA_DEBUG)
System.Diagnostics.Debug.WriteLine("[{0}] was Copied 2, total=[{1}]", copy.Length,TotalAllocated);
#endif
            return copy;
        }

        static int TotalAllocated = 0;
        /// <summary>
        /// Only create a new object and allocate a empty char array with same size of original
        /// </summary>
        /// <returns></returns>
        public Source ClearCopy()
        {
            var copy = new Source(new Char[this.Length]);
            TotalAllocated += copy.Length;
#if (DIA_DEBUG)
            System.Diagnostics.Debug.WriteLine("[{0}] was ClearCopied, total=[{1}]", copy.Length, TotalAllocated);
#endif
            copy.Clear();
            return copy;
        }



        /// <summary>
        /// IN Single LINE, try match target char array in src char array start from ptr with in length tries.
        /// all the char before target MUST be space or v/h tab
        /// </summary>
        /// <param name="src"></param>
        /// <param name="ptr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int SeekCharArrayS(char[] src, int ptr, int end, char[] target)
        {
            //skip Space, Tab, LF and CR
            while (src[ptr] == 32 || src[ptr] == 11 || src[ptr] == 9)
                if (ptr < end)
                    ptr++;
                else
                    break;

            if (target.Length > (end - ptr))
                return -1;

            foreach (var c in target)
                if (c != src[ptr++])
                    return -1;

            return ptr - 1;
        }


        /// <summary>
        /// COULD BE IN MULTIPLE LINE, try match target char array in src char array start from ptr with in length tries.
        /// all the char before target MUST be space or v/h tab
        /// </summary>
        /// <param name="src"></param>
        /// <param name="ptr"></param>
        /// <param name="target"></param>
        /// <returns>The index of right most char which matched target char array</returns>
        public static int SeekCharArrayM(char[] src, int ptr, int end, char[] target)
        {
           
            //skip Space, Tab, LF and CR
            while (src[ptr] == 32 || src[ptr] == 11 || src[ptr] == 9 || src[ptr] == 10 || src[ptr] == 13)
                if (ptr < end)
                    ptr++;
                else
                    break;

            if (target.Length > (end - ptr))
                return -1;

            foreach (var c in target)
                if (c != src[ptr++])
                    return -1;

            return ptr - 1;
        }

        /// <summary>
        /// COULD BE IN MULTIPLE LINE, try match target char in src char array start from ptr, 
        /// all the char before target MUST be space or v/h tab or LF and CR.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="ptr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SeekCharM(char[] src, int ptr, int end, char target)
        {
            while (src[ptr] == 32 || src[ptr] == 11 || src[ptr] == 9 || src[ptr] == 10 || src[ptr] == 13)
                if (ptr < end)
                    ptr++;
                else
                    break;

              if (src[ptr] == target)
                    return true;
                else
                    return false;
         }

        /// <summary>
        /// IN ONE LINE, try seek target char in src char array start from ptr, 
        /// all the char before target MUST be space or v/h tab.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="ptr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SeekCharS(char[] src, int ptr, int end, char target)
        {

            while (src[ptr] == 32 || src[ptr] == 11 || src[ptr] == 9)
                if (ptr < end)
                    ptr++;
                else
                    break;
            
            if (src[ptr] == target)
                return true;
            else
                return false;
        }

        /// <summary>
        /// IN ONE LINE, BACKWARD seek target char in src char array start from ptr, 
        /// all the char after target MUST be space or v/h tab.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="ptr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool BackSeekCharS(char[] src, int ptr, int end, char target)
        {
            while (src[ptr] == 32 || src[ptr] == 11 || src[ptr] == 9)
                if (ptr > end)
                    ptr--;
                else
                    break;

             if (src[ptr] == target)
                    return true;
                else
                    return false;
        }

        /// <summary>
        /// Try to seek a valid 'variable' which MUST start with char 'a~z' or 'A~Z' and 
        /// some subsequents like 'a~z' or 'A~Z' or '0~9' or '_'
        /// </summary>
        /// <param name="src"></param>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static string SeekVariable(char[] src, int ptr, int end, char endChar)
        {
            int state = -1;
            string name = string.Empty;
            for (int i = ptr; i < end; i++)
            {
                switch (state)
                {
                    case -1:  //HXY: validate the start char of a var must be in 'a~z' or 'A~Z'                           
                        {
                            if (src[i] == 32 || src[i] == 11 || src[i] == 9)
                                continue;
                            else if ((src[i] >= 97 && src[i] <= 122) || (src[i] >= 65 && src[i] <= 90))
                            {
                                state = 0; name += src[i];
                            }
                            else
                                return null;
                            //must have a break
                            break;
                        }
                    case 0: //HXY: middle of var name can be in 'a~z' or 'A~Z' or '0~9' or '_'
                        {
                            
                            if ((src[i] >= 97 && src[i] <= 122) || (src[i] >= 65 && src[i] <= 90) || (src[i] >= 48 && src[i] <= 57) || (src[i] == 95))
                            {
                                name += src[i];
                            }
                            else if (src[i] == 32 || src[i] == 11 || src[i] == 9)
                            {
                                state = 1;
                            }
                            else if (src[i] == endChar)
                            {
                                return name;
                            }
                            break;                      
                        }
                    case 1: //HXY: space detected after a var, seek end char
                        {
                            if (SeekCharS(src, i, end, endChar))
                                return name;
                            else
                                return null;
                        }
                }               
            }
            return null;
        }

        /// <summary>
        /// check if a variable is a key word or not
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static bool IsKeyWrod(string variable)
        {
            return KEY_DICT.ContainsKey(variable);
        }

        /// <summary>
        ///  check if the chars before ptr are trimable or not
        /// </summary>
        /// <param name="src"></param>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static bool LeftTrimable(char[] src, int ptr)
        {
            for (int i = ptr; i >= 0; i--)
                if (!(src[i] == 32 || src[i] == 11 || src[i] == 9 || src[i] == 10 || src[i] == 13))
                    return false;
            return true;
        }

        /// <summary>
        /// seek a variable name by a leading 'var' keyword and a trailing '='
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static string ExtractVariable(char[] src, int ptr1, int end, char endChar)
        {
            int ptr = SeekCharArrayM(src, 0, end, Source.KEY_VAR.ToCharArray());
            if (ptr > 0)
            {
                return SeekVariable(src, ptr, end, endChar);
            }
            return null;
        }
    }
}
