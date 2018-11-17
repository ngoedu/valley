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
using System.Windows.Forms;
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
		private static Workspace generalWorkspace = new GeneralWorkspace();
		
		
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
			//TODO: always return general workspace
			return generalWorkspace;
			
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
		
		public abstract string WorkspaceFolderName();
		
		/// <summary>
		/// workspace init
		/// </summary>
		/// <returns>true - first time init, false - NOT first time</returns>
		public abstract bool Init(string cdatPath);
	}
	
	internal class JavaWorkspace : Workspace {
		public override bool Init(string cdatPath) {
			bool firstTimeInit = false;
			String wsPath = cdatPath+WorkspaceFolderName();
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
			return @"\ws0\java";
		}
		
		public override string WorkspaceFolderName() {
			return @"\ws.java";
		}
		
	}
	
	internal class GeneralWorkspace : Workspace {
		
		private const string ngo_placeholder = "___NGO_PYTHON_PLACHOLDER___";
		private const string prefPath = @"\.metadata\.plugins\org.eclipse.core.runtime\.settings\org.python.pydev.prefs";
		
		public override bool Init(string cdatPath) {
			bool firstTimeInit = false;
			
			String wsPath = cdatPath+WorkspaceFolderName();
			
			//1.prepare workspace if required.
			if (!(new DirectoryInfo(wsPath).Exists)){
				//XTendLibs.FolderDelete.DeleteDirectory(workspace);
				XTendLibs.FolderCopy.DirectoryCopy(cdatPath +@"\"+ RawWSFolderName(), wsPath, true, null, null);
				firstTimeInit = true;
			
				string codePath = CodeBase.GetCodePath();
				
				string prefText = File.ReadAllText(wsPath+prefPath);
				string pfefCodePath = codePath.Replace(@"\",@"\\");
				prefText = prefText.Replace(ngo_placeholder, pfefCodePath);
				File.WriteAllText(wsPath+prefPath,prefText);
				
			}
			return firstTimeInit;
		}
		
		public override string RawWSFolderName() {
			return @"\ws0\general";
		}
		
		public override string WorkspaceFolderName() {
			return @"\ws.general";
		}
	}
	
	internal class PythonWorkspace : Workspace {
		
		private const string ngo_placeholder = "___NGO_PYTHON_PLACHOLDER___";
		private const string interpreterPath1 = @"\.metadata\.plugins\org.python.pydev\v1_ergtpsw9w30j9cfpojzkryfd\";
		private const string interpreterPath2 = @"\.metadata\.plugins\com.python.pydev.analysis\python_v1_ergtpsw9w30j9cfpojzkryfd\";
		private const string prefPath = @"\.metadata\.plugins\org.eclipse.core.runtime\.settings\org.python.pydev.prefs";
		private const string metaFileModulesKeys = @"modulesKeys";
		private const string metaFilePythonPath = @"pythonpath";
		private const string metaAnalysis = @"python.pydevsysteminfo";
		
		/// <summary>
		/// http://www.pydev.org/manual_101_interpreter.html
		/// </summary>
		/// <param name="cdatPath"></param>
		/// <param name="wsPath"></param>
		/// <returns></returns>
		public override bool Init(string cdatPath) {
			bool firstTimeInit = false;
			
			String wsPath = cdatPath+WorkspaceFolderName();
			
			//1.prepare workspace if required.
			if (!(new DirectoryInfo(wsPath).Exists)){
				//XTendLibs.FolderDelete.DeleteDirectory(workspace);
				XTendLibs.FolderCopy.DirectoryCopy(cdatPath +@"\"+ RawWSFolderName(), wsPath, true, null, null);
				firstTimeInit = true;
			
				string codePath = CodeBase.GetCodePath();
				
				//2. remane workspace
				//var interpreterName = new App.Common.Java.JvmUtil().Execute(codePath+@"\jre\jvmutil\ngoutil.jar", codePath+@"\python\python.exe");
				//string newFolder1 = interpreterPath1.Replace("ergtpsw9w30j9cfpojzkryfd",interpreterName);
				//string newFolder2 = interpreterPath2.Replace("ergtpsw9w30j9cfpojzkryfd",interpreterName);
				
				//Directory.Move(wsPath+interpreterPath1, wsPath+newFolder1);
				//Directory.Move(wsPath+interpreterPath2, wsPath+newFolder2);
				
				//3.make config change for pydev if required.
				//string textModuleKey = File.ReadAllText(wsPath+newFolder1+metaFileModulesKeys);
				//textModuleKey = textModuleKey.Replace(ngo_placeholder, codePath+@"\python");
				//File.WriteAllText(wsPath+newFolder1+metaFileModulesKeys, textModuleKey);
				
				//string pythonPath = File.ReadAllText(wsPath+newFolder1+metaFilePythonPath);
				//pythonPath = pythonPath.Replace(ngo_placeholder, codePath);
				//File.WriteAllText(wsPath+newFolder1+metaFilePythonPath, pythonPath);
				
				//string analysis = File.ReadAllText(wsPath+newFolder2+metaAnalysis);
				//analysis = analysis.Replace(ngo_placeholder, codePath+@"\python\");
				//File.WriteAllText(wsPath+newFolder2+metaAnalysis, analysis);
				
				string prefText = File.ReadAllText(wsPath+prefPath);
				string pfefCodePath = codePath.Replace(@"\",@"\\");
				prefText = prefText.Replace(ngo_placeholder, pfefCodePath);
				File.WriteAllText(wsPath+prefPath,prefText);
				
				
				//MessageBox.Show("3 placeholders replacement done, code path="+codePath);
				
			}
			return firstTimeInit;
		}
		
		public override string RawWSFolderName() {
			return @"\ws0\python";
		}
		
		public override string WorkspaceFolderName() {
			return @"\ws.python";
		}
		
	}
	
	internal class SQLWorkspace : Workspace {
		public override bool Init(string cdatPath) {
			throw new NotImplementedException();
		}
		
		public override string RawWSFolderName() {
			return @"\ws0\sql";
		}
		
		public override string WorkspaceFolderName() {
			return @"\ws.sql";
		}
	}
}
