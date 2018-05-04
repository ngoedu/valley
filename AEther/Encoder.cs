/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-04-24
 * Time: 1:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace NGO.Pad.AEther
{
	/// <summary>
	/// Description of Encoder.
	/// </summary>
	public class Encoder
	{
		public static Encoder Instance = new Encoder();
		
		private static byte MAGIC1 =  0xBE;
		private static byte MAGIC2  = 0xA1;
		
		private Encoder()
		{
		}
		
		/// <summary>
		/// https://stackoverflow.com/questions/1442583/good-way-to-convert-between-short-and-bytes
		/// </summary>
		/// <param name="package"></param>
		/// <returns></returns>	
		public  byte[] Encode(EtherPack package) {
			if (package == null)
				return null;
			byte[] data = new byte[EtherPack.HeaderSize() + package.PayloadLen()];
			
			data[0] = MAGIC1;
			data[1] = MAGIC2;
			//src
			data[2] = (byte)(package.Source >> 8);
			data[3] = (byte)(package.Source & 255);
			
			//desc
			data[4] = (byte)(package.Destination >> 8);
			data[5] = (byte)(package.Destination & 255);
			
			//type
			data[6] = package.Type;
			//checksum
			data[7] = package.Checksum[0];
			data[8] = package.Checksum[1];
			data[9] = package.Checksum[2];
			data[10] = package.Checksum[3];
			data[11] = package.Checksum[4];
			data[12] = package.Checksum[5];
			data[13] = package.Checksum[6];
			data[14] = package.Checksum[7];
			data[15] = package.Checksum[8];
			data[16] = package.Checksum[9];
			data[17] = package.Checksum[10];
			data[18] = package.Checksum[11];
			data[19] = package.Checksum[12];
			data[20] = package.Checksum[13];
			data[21] = package.Checksum[14];
			data[22] = package.Checksum[15];
			//payload len	
			byte[] plen = BitConverter.GetBytes(package.PayloadLen());
			if (BitConverter.IsLittleEndian)
			    	Array.Reverse(plen);
			data[23] = plen[0];
			data[24] = plen[1];
			data[25] = plen[2];
			data[26] = plen[3];
			
			//populate payload
			System.Buffer.BlockCopy(package.Payload, 0, data, 27, package.PayloadLen());
			
			return data;
		}
		
	}
}
