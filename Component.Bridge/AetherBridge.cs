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
using App.Common.Debug;
using App.Common.Proc;
using App.Common.Signal;
using log4net;

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
		private IPidCallback pidCallback;
		private String jrePath;
		private String aetherPath;
		private WaitSignal startupSyncSignal;
		
		private int IDLE = 30;
		private static readonly ILog logger = LogManager.GetLogger(typeof(AetherBridge));  

		/// <summary>
		/// initial status = 0
		/// in shutdown process = 1
		/// already shutted down = 2
		/// </summary>
		int status = 0;
		
		public AetherBridge(int port, IOutputCallback callback, IPidCallback pCallback, String jre, String aether)
		{
			this.callback = callback;
			this.pidCallback = pCallback;
			this.PORT_NO = port;
			this.jrePath = jre;
			this.aetherPath = aether;
		}
		
		public void StartupSync() {
			if (status == 1 || pid != -1)
				return;
			startupSyncSignal = new WaitSignal();
			Startup();
			startupSyncSignal.WaitOneWhen("[aether bridge v1.1] launched","Exception");
			logger.Info(string.Format("[aether bridge] {0} initialized.", pid));
			
		}
		
		public void Startup() {
        	if (status == 1 || pid != -1)
				return;
        	
			//* Create your Process
		    Process process = new Process();
		    process.StartInfo.FileName = jrePath +@"\bin\java.exe";
		    var jvmOption = "-Dngo.bridge.host=127.0.0.1 -Dngo.bridge.port="+this.PORT_NO+" -Dfile.encoding=GBK -Dngo.bridge.idle="+IDLE;
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
		    
		    bool started = false;
		    
		    // try start process in a thread.
		    ThreadStart ths = new ThreadStart(
		    	delegate() { 	started = process.Start(); 
		    					process.BeginOutputReadLine();
		    					process.BeginErrorReadLine();
		    					if (started) {
		    						status = 0;
		    						pid = process.Id;
		    						this.pidCallback.PidCreated("bridge", pid);
		    					}
		    					}
		    				);
		    Thread th = new Thread(ths);
    		th.Start();
		}
		
		private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
		    //notify signal if any
		    if (startupSyncSignal!=null)
		    	startupSyncSignal.SetWhen(outLine.Data);
		    
		    callback.OutputArrived(outLine.Data);
		}
        
        private void OnExited(object sender, System.EventArgs e) {
        	Diagnostics.Debug(string.Format("[aether bridge] pid={0} Exited", pid));					
		    pid = -1; 
        }
		
		public void Shutdown() {
        	if ( pid == -1 || status != 0)
        		return;
        	
        	status = 1;
        	
        	this.pidCallback.KillProcessById("bridge",pid);
        	
			status = 2;          
		}
	}
}
