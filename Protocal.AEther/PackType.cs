/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/24
 * 时间: 21:31
 * 
 * 
 */
using System;

namespace NGO.Protocol.AEther
{
	/// <summary>
	/// Description of PackType.
	/// </summary>
	public class PackType
	{
		public static  PackType REG = new PackType((byte)1);
	
		public static  PackType REGA = new PackType((byte)5);
		
		public static  PackType DAT = new PackType((byte)127);
	
		public static  PackType DNS = new PackType((byte)2);
		
		public static  PackType KA = new PackType((byte)3);
		
		public static  PackType KAA = new PackType((byte)4);
		
		private  byte num;
	
		private PackType(byte num) {
		        this.num = num;
		}
		
		public byte Value {set {num = value;} get {return num;}}
	
		private PackType()
		{
		}
		
		public EtherPack ToPackage(int dest, string message) {
			switch (this.num)
			{
				case 1: //REG endpoint
					return EtherPack.ToPackage(Endpoint.ID, Endpoint.BRIDGE_ID, 1,  string.Empty);		
				case 3: //KA bridge
					return EtherPack.ToPackage(Endpoint.ID, Endpoint.BRIDGE_ID, 3,  string.Empty);
				case 4: //KAA endpoint
					return EtherPack.ToPackage(Endpoint.ID, Endpoint.BRIDGE_ID, 4,  string.Empty);
				case 5: //REGA bridge
					return EtherPack.ToPackage(Endpoint.ID, Endpoint.BRIDGE_ID, 5,  string.Empty);
				case 127: //DAT
					return EtherPack.ToPackage(Endpoint.ID, dest, 127,  message);
				}
			return null;
		}
		
		public static PackType ValueOf(byte type) {
			switch (type)
			{
				case 1:
					return REG;
				case 3:
					return KA;
				case 4:
					return KAA;
				case 5:
					return REGA;
				case 127:
					return DAT;
			}
			return null;
		}
	}
}
