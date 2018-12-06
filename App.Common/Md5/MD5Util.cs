/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/5
 * Time: 14:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Security.Cryptography;
using System.Text;

namespace App.Common.Md5
{
	/// <summary>
	/// Util of MD5Util.
	/// </summary>
	public class MD5Util
	{
		static char[] salt = new char[] {'N','g','0', '@', 'n','d','S','a','l','t'};
            
		public static string  StringMD5(string message, string mysalt)
		{
			var provider = MD5.Create();
            byte[] bytes = provider.ComputeHash(Encoding.ASCII.GetBytes(salt + message + mysalt));
            string computedHash = BitConverter.ToString(bytes);
    
            return computedHash.Replace("-", "");
		}
	}
}
