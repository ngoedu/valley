/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/13
 * Time: 14:17
 * 
 * 
 */
using System;
using System.IO;
using App.Common;

namespace Control.Eide
{
	
	public enum WorkspaceType {
		Java,
		Python,
		SQL
	}
	/// <summary>
	/// Description of IWorkspace.
	/// </summary>
	public abstract class Workspace
	{
		private static Workspace javaWorkspace = new JavaWorkspace();
		private static Workspace pythonWorkspace = new PythonWorkspace();
		private static Workspace sqlWorkspace = new SQLWorkspace();
		
		
		public static WorkspaceType CheckWorkspaceType(string wspath)
		{
			if (File.Exists(wspath+@"\.pydevproject")) {
				return WorkspaceType.Python;
			} else if (File.Exists(wspath+@"\.classpath")) {
				return WorkspaceType.Java;
			} 
			//TODO: sql workspace type here
			
			throw new InvalidProgramException("No workspace type identified."); 
		}
		
		public static Workspace GetWorkspace(WorkspaceType type)
		{
			if (type == WorkspaceType.Java) {
				return javaWorkspace;
			} else if (type == WorkspaceType.Python) {
				return pythonWorkspace;
			} else if (type == WorkspaceType.SQL)  {
				return sqlWorkspace;
			}		
			throw new InvalidProgramException("No desired workspace type.");
		}
		
		public abstract string RawWSFolderName();
		
		/// <summary>
		/// workspace init
		/// </summary>
		/// <returns>true - first time init, false - NOT first time</returns>
		public abstract bool Init(string cdatPath, string wsPath);
	}
	
	internal class JavaWorkspace : Workspace {
		public override bool Init(string cdatPath, string wsPath) {
			bool firstTimeInit = false;
			//1.prepare workspace if required.
			if (!(new DirectoryInfo(wsPath).Exists)){
				//XTendLibs.FolderDelete.DeleteDirectory(workspace);
				XTendLibs.FolderCopy.DirectoryCopy(cdatPath +@"\"+ RawWSFolderName(), wsPath, true, null, null);
				firstTimeInit = true;
			}
			
			//2.make config change if required.			
			
			return firstTimeInit;
			
		}
		
		public override string RawWSFolderName() {
			return "rws.java";
		}
		
	}
	
	internal class PythonWorkspace : Workspace {
		
		private const string ngo_placeholder = "___NGO_PYTHON_PLACHOLDER___";
		private const string metaFileModulesKeys = @"\.metadata\.plugins\org.python.pydev\v1_ergtpsw9w30j9cfpojzkryfd\modulesKeys";
		private const string metaFilePythonPath = @"\.metadata\.plugins\org.python.pydev\v1_ergtpsw9w30j9cfpojzkryfd\pythonpath";
		
		public override bool Init(string cdatPath,string wsPath) {
			bool firstTimeInit = false;
			//1.prepare workspace if required.
			if (!(new DirectoryInfo(wsPath).Exists)){
				//XTendLibs.FolderDelete.DeleteDirectory(workspace);
				XTendLibs.FolderCopy.DirectoryCopy(cdatPath +@"\"+ RawWSFolderName(), wsPath, true, null, null);
				firstTimeInit = true;
			
				//2.make config change for pydev if required.			
				string textModuleKey = File.ReadAllText(wsPath+metaFileModulesKeys);
				textModuleKey = textModuleKey.Replace(ngo_placeholder, CodeBase.GetCodePath());
				File.WriteAllText(wsPath+metaFileModulesKeys, textModuleKey);
				
				string pythonPath = File.ReadAllText(wsPath+metaFilePythonPath);
				pythonPath = pythonPath.Replace(ngo_placeholder, CodeBase.GetCodePath());
				File.WriteAllText(wsPath+metaFilePythonPath, pythonPath);
			}
			
			return firstTimeInit;
		}
		
		public override string RawWSFolderName() {
			return "rws.python";
		}
		
	}
	
	internal class SQLWorkspace : Workspace {
		public override bool Init(string cdatPath,string wsPath) {
			throw new NotImplementedException();
		}
		
		public override string RawWSFolderName() {
			return "rws.sql";
		}
		
	}
}
