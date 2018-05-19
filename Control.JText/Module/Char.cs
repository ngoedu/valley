/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/10
 * 时间: 10:19
 * 
 * 
 */
using System;

namespace NGO.Pad.JText.Module
{
	/// <summary>
	/// Description of Char.
	/// </summary>
	public class Char : Glyph
	{
		public static Char LF = new LF((char)10);
		public static Char CR = new CR((char)13);
		public static Char A = new Char('A');
		public static Char B = new Char('B');
		public static Char C = new Char('C');
		public static Char D = new Char('D');
		public static Char E = new Char('E');
		public static Char F = new Char('F');
		public static Char G = new Char('G');
		public static Char H = new Char('H');
		public static Char I = new Char('I');
		public static Char J = new Char('G');
		public static Char K = new Char('K');
		public static Char L = new Char('L');
		public static Char M = new Char('M');
		public static Char N = new Char('N');
		public static Char O = new Char('O');
		public static Char P = new Char('P');
		public static Char Q = new Char('Q');
		public static Char R = new Char('R');
		public static Char S = new Char('S');
		public static Char T = new Char('T');
		public static Char U = new Char('U');
		public static Char V = new Char('V');
		public static Char W = new Char('W');
		public static Char X = new Char('X');
		public static Char Y = new Char('Y');
		public static Char Z = new Char('Z');
		
		public static Char a = new Char('a');
		public static Char b = new Char('b');
		public static Char c = new Char('c');
		public static Char d = new Char('d');
		public static Char e = new Char('e');
		public static Char f = new Char('f');
		public static Char g = new Char('g');
		public static Char h = new Char('h');
		public static Char i = new Char('i');
		public static Char j = new Char('j');
		public static Char k = new Char('k');
		public static Char l = new Char('l');
		public static Char m = new Char('m');
		public static Char n = new Char('n');
		public static Char o = new Char('o');
		public static Char p = new Char('p');
		public static Char q = new Char('q');
		public static Char r = new Char('r');
		public static Char s = new Char('s');
		public static Char t = new Char('t');
		public static Char u = new Char('u');
		public static Char v = new Char('v');
		public static Char w = new Char('w');
		public static Char x = new Char('x');
		public static Char y = new Char('y');
		public static Char z = new Char('z');
		
		public static Char num0 = new Char('0');
		public static Char num1 = new Char('1');
		public static Char num2 = new Char('2');
		public static Char num3 = new Char('3');
		public static Char num4 = new Char('4');
		public static Char num5 = new Char('5');
		public static Char num6 = new Char('6');
		public static Char num7 = new Char('7');
		public static Char num8 = new Char('8');
		public static Char num9 = new Char('9');
		
		public static Char Asterisk = new Char('*');
		
		private static Char[] CharMap = new Char[127];
		
		// Static constructor to initialize the static variable.
		// It is invoked before the first instance constructor is run.
		static Char() {
			CharMap['A'] = A;
			CharMap['B'] = B;
			CharMap['C'] = C;
			CharMap['D'] = D;
			CharMap['E'] = E;
			CharMap['F'] = F;
			CharMap['G'] = G;
			CharMap['H'] = H;
			CharMap['I'] = I;
			CharMap['J'] = J;
			CharMap['K'] = K;
			CharMap['L'] = L;
			CharMap['M'] = M;
			CharMap['N'] = N;
			CharMap['O'] = O;
			CharMap['P'] = P;
			CharMap['Q'] = Q;
			CharMap['R'] = R;
			CharMap['S'] = S;
			CharMap['T'] = T;
			CharMap['U'] = U;
			CharMap['V'] = V;
			CharMap['W'] = W;
			CharMap['X'] = X;
			CharMap['Y'] = Y;
			CharMap['Z'] = Z;
			
			CharMap['a'] = a;
			CharMap['b'] = b;
			CharMap['c'] = c;
			CharMap['d'] = d;
			CharMap['e'] = e;
			CharMap['f'] = f;
			CharMap['g'] = g;
			CharMap['h'] = h;
			CharMap['i'] = i;
			CharMap['j'] = j;
			CharMap['k'] = k;
			CharMap['l'] = l;
			CharMap['m'] = m;
			CharMap['n'] = n;
			CharMap['o'] = o;
			CharMap['p'] = p;
			CharMap['q'] = q;
			CharMap['r'] = r;
			CharMap['s'] = s;
			CharMap['t'] = t;
			CharMap['u'] = u;
			CharMap['v'] = v;
			CharMap['w'] = w;
			CharMap['x'] = x;
			CharMap['y'] = y;
			CharMap['z'] = z;
			
			CharMap['0'] = num0;
			CharMap['1'] = num1;
			CharMap['2'] = num2;
			CharMap['3'] = num3;
			CharMap['4'] = num4;
			CharMap['5'] = num5;
			CharMap['6'] = num6;
			CharMap['7'] = num7;
			CharMap['8'] = num8;
			CharMap['9'] = num9;
			
			CharMap['*'] = Asterisk;
			
			CharMap[10] = LF;
			CharMap[13] = CR;

		}
		
		public char Inter {set; get;}
		
		public Char(char c)
		{
			this.Inter = c;
			this.Width = 10;
		}
		
		public static Char ValueOf(char chr) {
			return chr == 9 ? new TAB((char)9) : (chr > 127 ? new UniChar(chr) : CharMap[chr]);
		}
		
		public override string ToString() {
			return this.Inter.ToString();
		}
	}
	
	/**
	 * Special chars with different rendering method 
	 */
	
	public class UniChar : Char {
		public UniChar(char chr) : base(chr) {
			this.Width = 20;
		}
		public override string ToString()
		{
			return Inter.ToString();
		}
	}
	
	public class CR : Char {
		public CR(char chr) : base(chr) {
			this.Width = 8;
		}
		public override string ToString()
		{
			return "Return";
		}
	}
	
	public class TAB : Char {
		public TAB(char chr) : base(chr) {
			this.Width = 10;
		}
		public override string ToString()
		{
			return "Tab";
		}
	}
	
	
	public class LF : Char {
		public LF(char chr) : base(chr) {
			this.Width = 4;
		}
		public override string ToString()
		{
			return "LineFeed";
		}
	}
}
