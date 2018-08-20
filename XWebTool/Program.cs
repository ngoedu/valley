/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/8/20
 * Time: 22:13
 * 
 * 
 */
using System;
using System.IO;
using XtendLibs;

namespace XWebTool
{
	class Program
	{
		private static string srcPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
		private static string destPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
		private static string suffix = "*.html";
		
		private static bool isRemoveCRLF = false;
		private static bool isRemoveJSComment = false;
		
			
		public static void Main(string[] args)
		{
			
			Console.WriteLine("Welcome to NGO web tool v1.0");
			
			
			if (args.Length < 6)
			{
				Console.WriteLine(@"invalid parameter, try example:  wtool.exe -s c:\ngo -d c:\dest -t *.html [-rj -rn]");
				//return;
			}
				
			
			if (args[0] == "-s")
			{
				srcPath = args[1];
			}
			
			if (args[2] == "-d")
			{
				destPath = args[3];
			}
			
			if (args[4] == "-t")
			{
				suffix = args[5];
			}
			
			if (args[6] == "-rj")
			{
				isRemoveJSComment = true;
			}
			
			if (args[7] == "-rn")
			{
				isRemoveCRLF = true;
			}
			
			
			
			
			
			System.Threading.Thread t = new System.Threading.Thread(ThreadStart);  
            t.SetApartmentState(System.Threading.ApartmentState.STA);  
            t.Start(); 
            
			
		}
		
		private static void ThreadStart()  
        {  
            WebSrcMerger merger = new WebSrcMerger();
            merger.IsRemoveJsComment = isRemoveJSComment;
            merger.IsRemoveNewLine = isRemoveCRLF;

			merger.MergeAllHtmls(srcPath, destPath, suffix);
						
			Console.Write("web src merged.");
        }
	}
}