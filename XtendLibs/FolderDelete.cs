/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/10/16
 * Time: 21:54
 * 
 * 
 */
using System;
using System.IO;

namespace XTendLibs
{
	/// <summary>
	/// Description of FileUtil.
	/// </summary>
	public class FolderDelete
	{
		public static void DeleteDirectory(string directory) {
			System.IO.DirectoryInfo di = new DirectoryInfo(directory);
			if (di.Exists) {
				foreach (var file in di.GetFiles())
				    file.Delete(); 
				foreach (DirectoryInfo dir in di.GetDirectories())
				    dir.Delete(true); 
			}
		}
	}
}
