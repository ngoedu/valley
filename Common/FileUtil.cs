/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/2/20
 * 时间: 0:27
 * 
 * 
 */
using System;

namespace Ngo.Pad.Common
{
	/// <summary>
	/// Description of FileUtil.
	/// </summary>
	public class FileUtil
	{
		public static string LoadContent(string filePath)
		{
			return System.IO.File.ReadAllText(Environment.CurrentDirectory.ToString() + "\\" + filePath);
		}
	}
}
