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


namespace XWindowsService
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class MySQLServer
	{
		private int pid = -1;
		private int status = 0;
		private int PORT_NO = 3306;
		
		
		private WaitSignal startupSyncSignal;
		private WaitSignal shutdownSyncSignal;
		
		
		
		public MySQLServer(int port)
		{
			this.PORT_NO = port;
		}
		 
		 
		 	
		 public void ShutdownSync(string fileName, string para, string pass) {
        	if ( pid == -1 || status != 0)
        		return;
        	shutdownSyncSignal = new WaitSignal();
        	Shutdown(fileName, para, pass);
        	shutdownSyncSignal.WaitOneWhen("mysqld.exe: Shutdown complete","Exception");
        }
		 
		public void StartupSync(string fileName, string para) {
        	if (status == 1 || pid != -1)
				return;
        	startupSyncSignal = new WaitSignal();
			Startup(fileName, para);
			startupSyncSignal.WaitOneWhen("mysqld.exe: ready for connections"," Cannot continue operation");
			if (startupSyncSignal.IsErrorOccured) {
				return;
			}
        }
		
		public void Startup(string fileName,string parameter) {
        	if (status == 1 || pid != -1)
				return;
 
        	//* Create your Process
		    Process process = new Process();
		    process.StartInfo.FileName = fileName;
		    process.StartInfo.Arguments = parameter;
				
			   
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
		    					}
		    				}
		    			);
		    Thread th = new Thread(ths);
    		th.Start();
		}
		
		public void Shutdown(string fileName, string paremeter, string pass) {
        	if ( pid == -1 || status != 0)
        		return;
        	
        	status = 1;
            //var fileName = CodeBase.GetCodePath() +@"\mysql\bin\mysqladmin.exe";
        	
        	//* Create your Process
		    Process process = new Process();
		    process.StartInfo.FileName = fileName;
		    
		    //bin\mysqladmin.exe -uroot -psa shutdown
		    
		    var appOption = @"-P63306 -uroot -p"+pass+" shutdown";
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
		     
		     //notify signal if any
		     if (shutdownSyncSignal!=null)
		     	shutdownSyncSignal.SetWhen(outLine.Data);

        }
        
        private void OnExited(object sender, System.EventArgs e) {
        	pid = -1; 
			status = 2;		    
        }
		
		
		public bool IsStartedUp() {
        	return this.pid !=  -1 && status == 0;
        }
	}
}