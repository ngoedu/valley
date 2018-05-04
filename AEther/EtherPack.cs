/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/22
 * 时间: 21:57
 * 
 * 
 */
using System;
using System.Text;

namespace NGO.Pad.AEther
{
	/// <summary>
	/// Defination of EtherPack.
	/// 
	/// Data type length
	/// https://stackoverflow.com/questions/3431187/c-sharp-is-short-data-type-or-it-is-still-int
	/// http://zetcode.com/lang/csharp/datatypes/
	/// </summary>
	public class EtherPack
	{
		private  static byte MAGIC1 = (byte)0xBE;
		private  static byte MAGIC2 = (byte)0xA1;
		public  static byte [] NULL_CHECKSUM = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}; 
		private static byte [] NULL_PAYLOAD = {}; 
	
		public byte[] Magic = {MAGIC1, MAGIC2};
		
		public EtherPack() {
			Checksum = new byte[16];
			Header = new byte[HeaderSize()];
		}
		
		public static EtherPack ToPackage(int from, int to, byte type, string payload) {
			if (payload.Length == 0 && type == PackType.DAT.Value)
				return null;
			var package =  new EtherPack();
			package.Source = (short)from;
			package.Destination = (short)to;
			package.Type = type;
			
			//payload length and content
			byte[] pArray = Encoding.UTF8.GetBytes(payload);
			package.Length = pArray.Length;
			package.Payload = pArray;
			
			/*byte[] pbytes = new byte[payload.Length * sizeof(char)];
			package.Length = pbytes.Length;
			System.Buffer.BlockCopy(payload.ToCharArray(), 0, pbytes, 0, pbytes.Length);
			package.Payload = pbytes;
			*/
			return package;
		}
		
		/**
		 *  XHO: date type length table in JAVA
		 *  -----------------
		 *  byte 	1
		 *  short 	2
		 *  int		4
		 *  long	8
		 *  char	2
		 * 
		 * data type length in C#
		 * ------------------
		 * 	sbyte	System.SByte	1 byte	-128 to 127
			byte	System.Byte		1 byte	0 to 255
			short	System.Int16	2 bytes	-32,768 to 32,767
			ushort	System.UInt16	2 bytes	0 to 65,535
			int		System.Int32	4 bytes	-2,147,483,648 to 2,147,483,647
			uint	System.UInt32	4 bytes	0 to 4,294,967,295
			long	System.Int64	8 bytes	-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
			ulong	System.UInt64	8 bytes	0 to 18,446,744,073,709,551,615
		 */
	
		public int Arrived = 0;
		
		public byte[] Header {set; get;}
		
		//2 bytes
		public short Source {set; get;}
		//2 bytes
		public short Destination {set; get;}
		//1 byte
		public byte Type {set; get;}
		//16 bytes
		public byte[] Checksum {set; get;}
		//4 bytes
		public int Length;
		//variable
		public byte[] Payload {set; get;}
		
		public int PayloadLen() {
			return (Payload == null ? 0 : Payload.Length);
		}
		/**
		 * 	2 bytes magic;
			2 bytes source;
			2 bytes	destination;
			1 byte	type;
			16 bytes checksum;
			4 bytes	length;
		 * @return
		 */
		public static int HeaderSize(){
			return 2+2+2+1+16+4 ;
		}
		
		public int Capacity() {
			return HeaderSize() + PayloadLen();
		}
		
		public override string ToString()
		{
			//char[] chars = new char[Payload.Length / sizeof(char)];
			//System.Buffer.BlockCopy(Payload, 0, chars, 0, Payload.Length);
			var message = System.Text.Encoding.UTF8.GetString(Payload);
			return message;
		}
	}
}
