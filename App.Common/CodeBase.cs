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
using System.Windows.Forms;

namespace App.Common
{
	/// <summary>
	/// the path of executable CodeBase.
	/// </summary>
	public class CodeBase
	{
		public static string COURSE_PACK_FOLDER = "cdat";
		public static string COURSE_WUI_FOLDER = "wui";
		public static string SYS_CONF_FOLDER = "conf";
		

		public static string GetCoursePackPath()
		{
			string path = string.Empty;
			
			#if (DIA_DEBUG)
			return "D:\\NGO\\client\\cdat";
			#endif
			
			#if (DIA_RELEASE)
			path =  Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\",string.Empty);
			path =  path+"\\"+COURSE_PACK_FOLDER;
			#endif
			
			//MessageBox.Show("GetCoursePackPath"+path);
			return path;
		}
		
		
		public static string GetWUIDataPath()
		{
			string path = string.Empty;
				
			#if (DIA_DEBUG)
			path =  "D:\\NGO\\client\\pad\\src\\valley\\wui";
			#endif
			
			#if (DIA_RELEASE)
			path =  Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\",string.Empty);
			path =  path+"\\"+COURSE_WUI_FOLDER;
			#endif
			
			//MessageBox.Show("GetWUIDataPath"+path);
			return path;
			
		}
		
		public static string GetCodePath() {
			string path = string.Empty;
			#if (DIA_DEBUG)
            path =  @"D:\NGO\client";
			if (!Directory.Exists(path+@"\jre"))
            	path =  @"c:\NGO\client";
			#endif
			
			#if (DIA_RELEASE)
			path =  Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\",string.Empty);
			path =  path.Replace(@"bin\Release\", string.Empty);
			#endif
			
			//System.Windows.Forms.MessageBox.Show("GetCodePath="+path);
			return path;
		}
	
	}
}
