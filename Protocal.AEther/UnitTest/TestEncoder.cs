/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-04-24
 * Time: 2:25 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;

namespace NGO.Protocol.AEther.UnitTest
{
	[TestFixture]
	public class TestEncoder
	{
		[Test]
		public void TestMethod_1()
		{
			var data = "<REG>2中12</REG>";
			var package = EtherPack.ToPackage(1,7,2,data);
			
			byte[] encoded = Encoder.Instance.Encode(package);
			
			Decoder dec = Decoder.Instance;
			StateObject state = new StateObject();
			
			bool result = dec.Decode(state, encoded.Length, encoded);
			
			Assert.AreEqual(true, result);
			Assert.AreEqual(data.Length * sizeof(char),state.package.PayloadLen()); //0xF
				
			char[] chars = new char[state.package.Payload.Length / sizeof(char)];
			System.Buffer.BlockCopy(state.package.Payload, 0, chars, 0, state.package.Payload.Length);//verify the payload data
			Assert.AreEqual(data, new string(chars));
		}
	}
}
