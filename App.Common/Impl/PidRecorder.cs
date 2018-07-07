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
using App.Common.Callback;

namespace App.Common.Impl
{
	/// <summary>
	/// Singleton of PidRecorder.
	/// </summary>
	public sealed class PidRecorder :  IPidCallback, IPidCleaner
	{
	    private static readonly Lazy<PidRecorder> lazy =
	        new Lazy<PidRecorder>(() => new PidRecorder());
	    
	    public static PidRecorder Instance { get { return lazy.Value; } }
	
	    private string pidFile = @"D:\NGO\client\pad\src\valley\App.Dashboard\bin\Debug\pid";
	    private Dictionary<string, string> OLD_PIDS = new Dictionary<string, string>();
	    private Dictionary<string, string> NEW_PIDS = new Dictionary<string, string>();
	    
	    private PidRecorder()
	    {
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
			string pidData = string.Empty;
			foreach(var entry in NEW_PIDS) {
				pidData += entry.Key+"="+entry.Value+System.Environment.NewLine;
			}
			System.IO.File.WriteAllText(pidFile, pidData);
		}
		#endregion

		#region IPidCleaner implementation
		public void KillProcessById(int pid)
		{
			//kill java process
			try {
				Process p = Process.GetProcessById(pid);
				if (p != null && !p.HasExited) {
					p.Kill();
					p.WaitForExit(); // possibly with a timeout
					System.Diagnostics.Debug.WriteLine("pid={0} killed", pid);
				}
			} catch (Win32Exception winException) {
				// process was terminating or can't be terminated - deal with it
				System.Diagnostics.Debug.WriteLine("pid={0} kill exception", winException.Message);
			} catch (InvalidOperationException invalidException) {
				// process has already exited - might be able to let this one go
				System.Diagnostics.Debug.WriteLine("pid={0} kill exception", invalidException.Message);
			}
		}
		
		public void CleanOldProcess()
		{
			foreach(var entry in OLD_PIDS.Values) {
				KillProcessById(Int16.Parse(entry));
			}
		}
		#endregion

	}
}


