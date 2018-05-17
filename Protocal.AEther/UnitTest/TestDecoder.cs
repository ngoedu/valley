/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/23
 * 时间: 0:14
 * 
 * 
 */
using System;
using NUnit.Framework;
using NGO.Protocol.AEther;

namespace NGO.Protocol.AEther.UnitTest
{
	[TestFixture]
	public class TestDecoder
	{
		//public static byte[] HEADER = new byte[EtherPack.HeaderSize()];
		
		[Test]
		public void TestMethod_1()
		{
			StateObject state = new StateObject();
			
			string data = "<REG>";
			
			Decoder dec = Decoder.Instance;
			
			byte[] buffer = new byte[StateObject.BufferSize];
			buffer[0] = 0xBE;
			buffer[1] = 0xA1;
			
			bool result = dec.Decode(state, 2, buffer);
			
			Assert.AreEqual(result, false);
			//src=1
			buffer[0] = 0x1;
			buffer[1] = 0x0;
			
			result = dec.Decode(state, 2, buffer);
			
			Assert.AreEqual(result, false);
			//dest=7
			buffer[0] = 0x7;
			buffer[1] = 0x0;
			
			result = dec.Decode(state, 2, buffer);
			
			Assert.AreEqual(result, false);
			
			//type=2
			buffer[0] = 0x2;
			buffer[1] = 0x0; //clean 
			
			result = dec.Decode(state, 1, buffer);
			
			Assert.AreEqual(result, false);
			
			//checksum
			buffer[0] = 0x9;
			buffer[1] = 0x0; 
			buffer[2] = 0x0; 			
			buffer[3] = 0x0; 
			buffer[4] = 0x0;  
			buffer[5] = 0x0;  
			buffer[6] = 0x0; 
			buffer[7] = 0x0; 
			buffer[8] = 0x0;  
			buffer[9] = 0x0;  
			buffer[10] = 0x0; 
			buffer[11] = 0x0; 
			buffer[12] = 0x0;
			buffer[13] = 0x0;
			buffer[14] = 0x0; 
			buffer[15] = 0x9; 
			
			result = dec.Decode(state, 16, buffer);
			
			Assert.AreEqual(result, false);
			
			//length
			buffer[0] = 0x0;
			buffer[1] = 0x0; 
			buffer[2] = 0x0; 			
			buffer[3] = 0xF; 
			
			result = dec.Decode(state, 4, buffer);
			
			Assert.AreEqual(result, false);
			Assert.AreEqual(state.package.Length, 15); //0xF
			
		}
		
		
		[Test]
		public void TestMethod_2()
		{
			StateObject state = new StateObject();
			
			string data = "<REG>";
			
			Decoder dec = Decoder.Instance;
			
			byte[] buffer = new byte[StateObject.BufferSize];
			buffer[0] = 0xBE;
			buffer[1] = 0xA1;
			
			bool result = dec.Decode(state, 2, buffer);
			
			Assert.AreEqual(result, false);
			//src=1
			buffer[0] = 0x1;
			buffer[1] = 0x0;
			
			result = dec.Decode(state, 2, buffer);
			
			Assert.AreEqual(result, false);
			//dest=7
			buffer[0] = 0x7;
			buffer[1] = 0x0;
			
			result = dec.Decode(state, 2, buffer);
			
			Assert.AreEqual(result, false);
			
			//type=2
			buffer[0] = 0x2;
			buffer[1] = 0x0; //clean 
			
			result = dec.Decode(state, 1, buffer);
			
			Assert.AreEqual(result, false);
			
			//checksum
			buffer[0] = 0x9;
			buffer[1] = 0x0; 
			buffer[2] = 0x0; 			
			buffer[3] = 0x0; 
			buffer[4] = 0x0;  
			buffer[5] = 0x0;  
			buffer[6] = 0x0; 
			buffer[7] = 0x0; 
			buffer[8] = 0x0;  
			buffer[9] = 0x0;  
			buffer[10] = 0x0; 
			buffer[11] = 0x0; 
			buffer[12] = 0x0;
			buffer[13] = 0x0;
			buffer[14] = 0x0; 
			buffer[15] = 0x9; 
			
			result = dec.Decode(state, 16, buffer);
			
			Assert.AreEqual(result, false);
			
			//length
			buffer[0] = 0x0;
			buffer[1] = 0x0; 
			buffer[2] = 0x0; 			
			buffer[3] = 0xA; //payload.length = 10 
			//with partial data 2 bytes
			buffer[4] = 0x4d;
			buffer[5] = 0x4a;
			result = dec.Decode(state, 6, buffer);
			
			Assert.AreEqual(false, result);
			Assert.AreEqual(state.package.PayloadLen(), 10); //0xF
			
			
			//let give the rest of the data
			buffer[0] = 0x40;
			buffer[1] = 0x0; 
			buffer[2] = 0x42; 			
			buffer[3] = 0x0; 
			buffer[4] = 0x4d;
			buffer[5] = 0x0;
			buffer[6] = 0x4d;
			buffer[7] = 0x0;
			
			result = dec.Decode(state, 8, buffer);
			Assert.AreEqual(true, result);
			Assert.AreEqual(state.package.PayloadLen(), 10); //0xF
			
		}
		
		[Test]
		public void TestMethod_3()
		{
			StateObject state = new StateObject();
			
			string data = "<REG>中</REG>";
			
			Decoder dec = Decoder.Instance;
			
			byte[] buffer = new byte[StateObject.BufferSize];
			buffer[0] = 0xBE;
			buffer[1] = 0xA1;
			
			bool result = dec.Decode(state, 2, buffer);
			
			Assert.AreEqual(result, false);
			//src=1
			buffer[0] = 0x1;
			buffer[1] = 0x0;
			
			result = dec.Decode(state, 2, buffer);
			
			Assert.AreEqual(result, false);
			//dest=7
			buffer[0] = 0x7;
			buffer[1] = 0x0;
			
			result = dec.Decode(state, 2, buffer);
			
			Assert.AreEqual(result, false);
			
			//type=2
			buffer[0] = 0x2;
			buffer[1] = 0x0; //clean 
			
			result = dec.Decode(state, 1, buffer);
			
			Assert.AreEqual(result, false);
			
			//checksum
			buffer[0] = 0x9;
			buffer[1] = 0x0; 
			buffer[2] = 0x0; 			
			buffer[3] = 0x0; 
			buffer[4] = 0x0;  
			buffer[5] = 0x0;  
			buffer[6] = 0x0; 
			buffer[7] = 0x0; 
			buffer[8] = 0x0;  
			buffer[9] = 0x0;  
			buffer[10] = 0x0; 
			buffer[11] = 0x0; 
			buffer[12] = 0x0;
			buffer[13] = 0x0;
			buffer[14] = 0x0; 
			buffer[15] = 0x9; 
			
			result = dec.Decode(state, 16, buffer);
			
			Assert.AreEqual(result, false);
			
			
			byte[] bytes = new byte[data.Length * sizeof(char)];
  			
			//length
			buffer[0] = 0x0;
			buffer[1] = 0x0; 
			buffer[2] = 0x0; 			
			buffer[3] = (byte)bytes.Length;
			
			
			System.Buffer.BlockCopy(data.ToCharArray(), 0, bytes, 0, bytes.Length);
  			for(int i=0; i<bytes.Length;i++)
  				buffer[4+i] = bytes[i];
			
  			result = dec.Decode(state, bytes.Length + 4, buffer);
			
			Assert.AreEqual(true, result);
			Assert.AreEqual(state.package.PayloadLen(), bytes.Length); //0xF
			
			char[] chars = new char[state.package.Payload.Length / sizeof(char)];
			System.Buffer.BlockCopy(state.package.Payload, 0, chars, 0, state.package.Payload.Length);//verify the payload data
			Assert.AreEqual(data, new string(chars));
			
		}
	}
}
