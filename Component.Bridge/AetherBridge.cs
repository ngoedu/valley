/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/22
 * Time: 1:52
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Component.Bridge
{
	/// <summary>
	/// Description of AetherBridge.
	/// </summary>
	public class AetherBridge
	{
		private int pid = -1;
		private int PORT_NO = 60001;
		private IOutputCallback callback;
		private String jrePath;
		private String aetherPath;
		
		/// <summary>
		/// initial status = 0
		/// in shutdown process = 1
		/// already shutted down = 2
		/// </summary>
		int status = 0;
		
		public AetherBridge(int port, IOutputCallback callback, String jre, String aether)
		{
			this.callback = callback;
			this.PORT_NO = port;
			this.jrePath = jre;
			this.aetherPath = aether;
		}
		
		public void Startup() {
        	if (status == 1 || pid != -1)
				return;
        	
			//* Create your Process
		    Process process = new Process();
		    var currentFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
		    process.StartInfo.FileName = jrePath +@"\bin\java.exe";
		    var jvmOption = "-Dngo.bridge.host=127.0.0.1 -Dngo.bridge.port="+this.PORT_NO+" -Dfile.encoding=GBK -Dngo.bridge.idle=30";
		    var classpath = @"-classpath %BRIDGE_HOEM%\aether-bridge-1.1.jar;%BRIDGE_HOEM%\apache-mina.jar;%BRIDGE_HOEM%\slf4j-api-1.7.21.jar;%BRIDGE_HOEM%\aether-common-1.1.jar;%BRIDGE_HOEM%\commons-codec-1.10.jar;%BRIDGE_HOEM%\slf4j-log4j12-1.7.21.jar;%BRIDGE_HOEM%\log4j-1.2.17.jar";
		    classpath = classpath.Replace("%BRIDGE_HOEM%", aetherPath);
		    string main = "org.ngo.ether.bridge.Bridge";
		    process.StartInfo.Arguments =String.Format("{0} {1} {2}",jvmOption,classpath,main);
				
			   
		    process.StartInfo.UseShellExecute = false;
		    process.StartInfo.RedirectStandardOutput = true;
		    process.StartInfo.RedirectStandardError = true;
		    process.StartInfo.CreateNoWindow = true;
		    process.EnableRaisingEvents = true;
		    //* Set your output and error (asynchronous) handlers
		    process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
		    process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
		    process.Exited += new EventHandler(OnExited);
		    
		    //* Start process and handlers
		    
		    // try start process in a thread.
		    ThreadStart ths = new ThreadStart(
		    	delegate() { 	process.Start(); 
		    					process.BeginOutputReadLine();
		    					process.BeginErrorReadLine();
		    					status = 0;
		    					pid = process.Id; 
		    					//process.WaitForExit();
		    					System.Diagnostics.Debug.WriteLine("process {0} started", pid);
		    					}
		    				);
		    Thread th = new Thread(ths);
    		th.Start();
		}
		
		private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
		    //* Do your stuff with the output (write to console/log/StringBuilder)
		    callback.OutputArrived(outLine.Data);
		}
        
        private void OnExited(object sender, System.EventArgs e) {
        	System.Diagnostics.Debug.WriteLine("process {0} Exited", pid);
		    pid = -1; 
        }
		
		public void Shutdown() {
        	if ( pid == -1 || status != 0)
        		return;
        	
        	status = 1;
        	try {
				Process p = Process.GetProcessById(pid);
				p.Kill();
				p.WaitForExit(); // possibly with a timeout
				System.Diagnostics.Debug.WriteLine("Bridge killed");
				pid = -1;	
			} catch (Win32Exception winException) {
				// process was terminating or can't be terminated - deal with it
			} catch (InvalidOperationException invalidException) {
				// process has already exited - might be able to let this one go
			}
			status = 2;          
		}
	}
}
