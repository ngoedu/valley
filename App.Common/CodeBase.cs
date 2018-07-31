/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/9
 * Time: 22:56
 * 
 * 
 */
using System;
using System.IO;

namespace App.Common
{
	/// <summary>
	/// the path of executable CodeBase.
	/// </summary>
	public class CodeBase
	{
		public static string GetCodePath() {
			string path =  Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\",string.Empty);
			path =  path.Replace(@"bin\debug\", string.Empty);
		
			#if (DIA_DEBUG)
            path =  @"D:\NGO\client";
			#endif
			
			#if (DIA_RELEASE)
			if (!Directory.Exists(path+@"\jre"))
            	path =  @"D:\NGO\client\dist";
			if (!Directory.Exists(path+@"\jre"))
           		path =  @"D:\NGO\client";
			#endif
			//System.Windows.Forms.MessageBox.Show(path);
			return path;
		}
	
	}
}
