/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/26
 * Time: 19:14
 * 
 * 
 */
using System;
using System.IO;
using App.Common;

namespace Control.Eide
{
	
	public enum ClasspathType {
		Java,
		Python
	}
	
	/// <summary>
	/// Description of ClassPath.
	/// </summary>
	public abstract class ClassPath
	{
		
		private static ClassPath javaClasspath = new JavaClasspath();
		private static ClassPath pythonClasspath = new PythonClasspath();
		
		/// <summary>
		/// workspace init
		/// </summary>
		/// <returns>true - first time init, false - NOT first time</returns>
		public abstract bool Init(string classPath);
		
		
		public static ClasspathType CheckClasspathType(string wspath)
		{
			if (File.Exists(wspath+@"\.pydevproject")) {
				return ClasspathType.Python;
			} else if (File.Exists(wspath+@"\.classpath")) {
				return ClasspathType.Java;
			} 
			//TODO: sql workspace type here
			
			throw new InvalidProgramException("No workspace type identified."); 
		}
		
		public static ClassPath GetClasspath(ClasspathType type)
		{
			if (type == ClasspathType.Java) {
				return javaClasspath;
			} else if (type == ClasspathType.Python) {
				return pythonClasspath;
			} 	
			throw new InvalidProgramException("No desired classpath type.");
		}
	}
	
	
	/// <summary>
	/// java
	/// </summary>
	internal class JavaClasspath : ClassPath {
		
		public override bool Init(string classPath) {
			string text = System.IO.File.ReadAllText(classPath+@"\.classpath");
			if (text.IndexOf("${ext.jar.path}") > 0) {
				string path = CodeBase.GetCodePath().Replace(@"\","/")+"/embed/ext/";
				text = text.Replace("${ext.jar.path}", path);
				System.IO.File.WriteAllText(classPath+@"\.classpath", text);
				return true;
			}
			return false;
		}
	}
	
	internal class PythonClasspath : ClassPath {
		
		public override bool Init(string classPath) {
			return true;
		}
	}
	
	
}
