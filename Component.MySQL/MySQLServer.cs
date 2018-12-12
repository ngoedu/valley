/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/12/12
 * Time: 11:14
 * 
 * 
 */
using System;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using App.Common;
using App.Common.Proc;
using App.Common.Signal;
using log4net;

namespace Component.MySQL
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class MySQLServer
	{
		private int pid = -1;
		private int status = 0;
		private int PORT_NO = 3306;
		
		private IPidCallback pidCallback;
		private IMySQLConsoleCallback callback;
		
		private WaitSignal startupSyncSignal;
		private WaitSignal shutdownSyncSignal;
		
		private static readonly ILog logger = LogManager.GetLogger(typeof(MySQLServer));  

		
		public MySQLServer(IMySQLConsoleCallback callback, IPidCallback pCallback, int port)
		{
			this.PORT_NO = port;
			this.pidCallback = pCallback;
			this.callback = callback;
		}
		 
		 
		 	
		 public void ShutdownSync() {
        	if ( pid == -1 || status != 0)
        		return;
        	shutdownSyncSignal = new WaitSignal();
        	Shutdown();
        	shutdownSyncSignal.WaitOneWhen("mysqld.exe: Shutdown complete","Exception");
        }
		 
		public void StartupSync() {
        	if (status == 1 || pid != -1)
				return;
        	startupSyncSignal = new WaitSignal();
			Startup();
			startupSyncSignal.WaitOneWhen("mysqld.exe: ready for connections"," Cannot continue operation");
			if (startupSyncSignal.IsErrorOccured) {
				logger.Error(string.Format("mysql startup failed - {0}.", startupSyncSignal.AttechedObject.ToString()));
				MessageBox.Show(startupSyncSignal.AttechedObject.ToString());
				return;
			}
			logger.Info(string.Format("catalina {0} started up.", pid));
        }
		
		public void Startup() {
        	if (status == 1 || pid != -1)
				return;
        	
        	var fileName = CodeBase.GetCodePath() +@"\mysql\bin\mysqld.exe";
        	var iniPath = CodeBase.GetCodePath() +@"\mysql\";
        	
        	//* Create your Process
		    Process process = new Process();
		    process.StartInfo.FileName = fileName;
		    
		    //bin\mysqld.exe --defaults-file=%cd%\my-small.ini 
		    
		    var appOption = @"--defaults-file="+iniPath+@"\my-small.ini";
		    var console = @"--console";
		    process.StartInfo.Arguments =String.Format("{0} {1}",appOption, console);
				
			   
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
		    						this.pidCallback.PidCreated("mysql", pid);
		    					}
		    				}
		    			);
		    Thread th = new Thread(ths);
    		th.Start();
		}
		
		public void Shutdown() {
        	if ( pid == -1 || status != 0)
        		return;
        	
        	status = 1;
            var fileName = CodeBase.GetCodePath() +@"\mysql\bin\mysqladmin.exe";
        	var iniPath = CodeBase.GetCodePath() +@"\mysql\";
        	
        	//* Create your Process
		    Process process = new Process();
		    process.StartInfo.FileName = fileName;
		    
		    //bin\mysqladmin.exe -uroot -psa shutdown
		    
		    var appOption = @"-P63306 -uroot -psa shutdown";
		    process.StartInfo.Arguments =String.Format("{0}",appOption);
			
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
		    
		    bool executed = false;
		    
		    // try start process in a thread.
		    ThreadStart ths = new ThreadStart(
		    	delegate() { 	executed = process.Start(); 
		    					process.BeginOutputReadLine();
		    					process.BeginErrorReadLine();
		    					if (executed) {
		    						status = 1;
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
		    
		     System.Diagnostics.Debug.WriteLine(outLine.Data);
		     
		     //notify signal if any
		     if (shutdownSyncSignal!=null)
		     	shutdownSyncSignal.SetWhen(outLine.Data);
		     
		     
		     if (this.callback != null && outLine.Data !=null)
		     	this.callback.MySQLOutputArrived(outLine.Data);
        }
        
        private void OnExited(object sender, System.EventArgs e) {
        	System.Diagnostics.Debug.WriteLine("Catalina process {0} Exited", pid);
		    pid = -1; 
			status = 2;		    
        }
		
		
		public bool IsStartedUp() {
        	return this.pid !=  -1 && status == 0;
        }
	}
}