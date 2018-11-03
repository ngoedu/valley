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
		public static string COURSE_FOLDER = "cdat";
		
		public static string GetCoursePath() {
			return GetCodePath() +@"\"+ COURSE_FOLDER;
		}

		public static string GetCoursePackPath()
		{
			#if (DIA_DEBUG)
			return "D:\\NGO\\client\\cdat";
			#endif
			
			#if (DIA_RELEASE)
			string path =  Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\",string.Empty);
			path =  path+"cdata";
			return Path;
			#endif
		}
		
		
		public static string GetCourseDataPath()
		{
			
				
			#if (DIA_DEBUG)
			return "D:\\NGO\\client\\pad\\src\\valley\\wui";
			#endif
			
			#if (DIA_RELEASE)
			string path =  Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\",string.Empty);
			path =  path+"wui";
			return Path;
			#endif
		}
		
		public static string GetCodePath() {
			string path =  Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\",string.Empty);
			path =  path.Replace(@"bin\debug\", string.Empty);
		
			#if (DIA_DEBUG)
            path =  @"D:\NGO\client";
			if (!Directory.Exists(path+@"\jre"))
            	path =  @"c:\NGO\client";
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
