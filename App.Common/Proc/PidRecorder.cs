/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/7
 * Time: 17:42
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using App.Common.Debug;
using App.Common.Proc;
using System.Management;

namespace App.Common.Proc
{
	/// <summary>
	/// Singleton of PidRecorder.
	/// </summary>
	public sealed class PidRecorder :  IPidCallback
	{
	    private static readonly Lazy<PidRecorder> lazy =
	        new Lazy<PidRecorder>(() => new PidRecorder());
	    
	    public static PidRecorder Instance { get { return lazy.Value; } }
	
	    private string pidFile;
	    
	    private Dictionary<string, string> OLD_PIDS = new Dictionary<string, string>();
	    private Dictionary<string, string> NEW_PIDS = new Dictionary<string, string>();
	    
	    private PidRecorder()
	    {
	    	pidFile = CodeBase.GetCodePath() + @"\pid";
	    	
	    	if (!File.Exists(pidFile))
	    	{
	    		File.Create(pidFile).Dispose();
	    		return;
	    	}
	    		
	    	string pidData = System.IO.File.ReadAllText(pidFile);
	    	if (string.IsNullOrEmpty(pidData))
	    	    return;
	    	    
	    	string[] pidEntries = pidData.Split( System.Environment.NewLine.ToCharArray());
	    	foreach(var entry in pidEntries) {
	    		if (!string.IsNullOrEmpty(entry))
	    			OLD_PIDS.Add(entry.Split('=')[0], entry.Split('=')[1]);
	    	}
	    }
	    
	    #region IPidCallback implementation
		public void PidCreated(string pName, int pid)
		{
			NEW_PIDS.Add(pName, pid.ToString());
			System.IO.File.WriteAllText(pidFile, BuildPidInfo());
		}
		#endregion

		
		private string BuildPidInfo() {
			string pidData = string.Empty;
			foreach(var entry in NEW_PIDS) {
				pidData += entry.Key+"="+entry.Value+System.Environment.NewLine;
			}
			return pidData;
		}
		
		private bool IsExecutable(string path) {
			string exePath = path.Replace("/", @"\");
			string codeBase = CodeBase.GetCodePath();
			return  (exePath.StartsWith(codeBase));
			//UNCOMMENT IT WHEN GO-LIVE
			//return exePath.Length > 0 && path.StartsWith(CodeBase.GetCodePath());
		}
		
		static private string ProcessExecutablePath(Process process)
		{
			try	{
				return process.MainModule.FileName;
			}
			catch {
				//in windows 10, it's not allowed to access 64bit process from 32bit app.
				return string.Empty;
			}
			return null;
		}
		
		#region IPidCleaner implementation
		public void KillProcessById(string pName, int pid)
		{
			//kill process
			try {
				if (Process.GetProcesses().Any(x => x.Id == pid)) {
					Process p = Process.GetProcessById(pid);
					if (p != null && !p.HasExited && IsExecutable(ProcessExecutablePath(p))) {
						p.Kill();
						p.WaitForExit(); // possibly with a timeout
						Diagnostics.Debug(string.Format("[Proc] pid={0} killed", pid));
					}
				}
			} catch (Win32Exception winException) {
				// process was terminating or can't be terminated - deal with it
				Diagnostics.Debug(string.Format("[Proc] pid={0} kill exception", winException.Message));
			} catch (InvalidOperationException invalidException) {
				// process has already exited - might be able to let this one go
				Diagnostics.Debug(string.Format("[Proc] pid={0} kill exception", invalidException.Message));
			}
			
			//remove pid from cached file
			NEW_PIDS.Remove(pName);
			System.IO.File.WriteAllText(pidFile, BuildPidInfo());
		}
		
		public void CleanOldProcess()
		{
			foreach(var entry in OLD_PIDS) {
				Diagnostics.Debug(string.Format("Stale pid={0} found", Int16.Parse(entry.Value)));
				KillProcessById(entry.Key, Int16.Parse(entry.Value));
			}
		}
		#endregion

	}
}


