/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/22
 * 时间: 23:01
 * 
 * 
 */
using System;
using log4net;

namespace NGO.Protocol.AEther
{
	/// <summary>
	/// Description of Decoder.
	/// </summary>
	public class Decoder
	{
		public static Decoder Instance = new Decoder();
		
		private static readonly ILog logger = LogManager.GetLogger(typeof(Decoder));  

		private Decoder()
		{
		}
		
		public bool Decode(StateObject state, int read, byte[] buffer){
			/**
			 * P1. package null -- first package, or cleaned subsequent package.
			 * P2. header not available
			 * P3. header available
			 * P4. payload not available
			 * P5. payload available
			 */
			//P1
			if (state.package == null)
			{
				//first time populate the package
				state.package = new EtherPack();
			}
			
			int eated = 0;
			int headerSize = EtherPack.HeaderSize();
			
			//P2
			if (state.package.Arrived < headerSize) {
				eated = headerSize - state.package.Arrived;
				eated = eated > read ? read : eated;
				Buffer.BlockCopy(buffer, 0, state.package.Header, state.package.Arrived, eated);
				state.package.Arrived += eated;
				if (state.package.Arrived < headerSize)
					return false;
			}

			
			//P3 header available now
			//state.package.Arrived >= headerSize
			if (state.package.Payload == null) {
				int idx = 0;
				state.package.Magic[0] = state.package.Header[idx++];
				state.package.Magic[1] = state.package.Header[idx++];
				
				//https://stackoverflow.com/questions/1104599/convert-byte-array-to-short-array-in-c-sharp
				//https://stackoverflow.com/questions/1389821/array-copy-vs-buffer-blockcopy
				//convert byte array to short
				state.package.Source  = (short)(state.package.Header[idx++] << 8 | (state.package.Header[idx++] ));
				state.package.Destination  = (short)(state.package.Header[idx++] << 8 | (state.package.Header[idx++] ));
				state.package.Type  = state.package.Header[idx++];
				Buffer.BlockCopy(state.package.Header, idx, state.package.Checksum, 0, state.package.Checksum.Length);
				idx+=state.package.Checksum.Length;
				//read length convert byte[] into int
				//https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-byte-array-to-an-int
				
				byte[] baLength = new byte[4];
				Buffer.BlockCopy(state.package.Header, idx, baLength, 0, 4);
				
				if (BitConverter.IsLittleEndian)
			    	Array.Reverse(baLength);
				//TODO: it seems no needs for a temp int array ?
				state.package.Length = BitConverter.ToInt32(baLength, 0); 
				idx+=4;
				
				//initialize payload
				state.package.Payload = new byte[state.package.Length];
				
				int dataLen = read - eated;
				if (dataLen > 0) {
					logger.Debug("BlockCopy - eated="+eated + ",state.package.Arrived - headerSize="+(state.package.Arrived - headerSize)+",dataLen="+dataLen);
					Buffer.BlockCopy(buffer, eated, state.package.Payload, state.package.Arrived - headerSize, dataLen);				
				}
				
				state.package.Arrived+=dataLen;
				if (state.package.Arrived < headerSize + state.package.PayloadLen())
					return false;
			}
			
			//P4 payload not available
			int packLen = state.package.Capacity();//headerSize + state.package.PayloadLen();
			if (state.package.Arrived < packLen){
				logger.Debug("BlockCopy - destOffset="+(state.package.Arrived - headerSize) + ",read="+read);
				Buffer.BlockCopy(buffer, 0, state.package.Payload, state.package.Arrived - headerSize, read);
				state.package.Arrived+=read;
				if (state.package.Arrived <packLen)
					return false;
			}

			//P5 all arrived.
			return true;
			
		}
	}
}
