/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-02-20
 * Time: 3:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;

namespace Ngo.Pad.Common
{
	/// <summary>
	/// Description of KeyState.
	/// </summary>
	public class KeyState
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
		public static extern short GetKeyState(int keyCode);
		
		public static bool IsCapsLock() {
			return Console.CapsLock;
			//below does not work in win7 somehow...			
			//return (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
		}
		
		public static bool IsNumLock() {
			return (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
		}
		
		public static bool IsScrollock() {
			return (((ushort)GetKeyState(0x91)) & 0xffff) != 0;
		}

		public static bool IsAlphabet(int keyCode){
			return keyCode >= 65 && keyCode <=90;
		}
	}
}
