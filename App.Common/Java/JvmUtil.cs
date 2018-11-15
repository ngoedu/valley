/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/14
 * Time: 19:52
 * 
 * 
 */
using System;
using System.Diagnostics;
using System.Threading;
using App.Common.Signal;

namespace App.Common.Java
{
	/// <summary>
	/// Description of JvmUtil.
	/// </summary>
	public class JvmUtil
	{
		private WaitSignal signal = new WaitSignal();
		
		public string Execute(string jar, string parameter)
		{
			string executble = CodeBase.GetCodePath() +@"\jre\bin\java.exe";
			Process process = new Process();
			var processInfo = new ProcessStartInfo(executble, "-jar " + jar + " "+parameter)
                      {
                          CreateNoWindow = true,
                          UseShellExecute = false
                      };
			process.StartInfo = processInfo;
			process.StartInfo.RedirectStandardOutput = true;
		    process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
			
		   
		   // try start process in a thread.
		    ThreadStart ths = new ThreadStart(
		    	delegate() { 	process.Start(); 
		    					process.BeginOutputReadLine();
		    				}
		    			);
		    Thread th = new Thread(ths);
    		th.Start();
    		
    		signal.WaitOne();
			
			return signal.AttechedObject.ToString();
			
		}
		
		private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
		    //* Do your stuff with the output (write to console/log/StringBuilder)
		    if (signal !=null && outLine.Data !=null)
		    {
		    	signal.PushObject(outLine.Data);
		    	signal.Set();
		    }
		}
		
		
	}
}
