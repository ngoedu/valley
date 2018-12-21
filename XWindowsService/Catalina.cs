/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/15
 * Time: 6:59
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
	/// catalina server bind.
	/// </summary>
	public class CatalinaServer
	{
		private int pid = -1;
		private int PORT_NO = 80;
		private int SHUTDOWN_PORT = 8005;
        private string SERVER_IP = "127.0.0.1";
        private string ShutdownCmd = "Sh0td0w#";
    
        
		/// <summary>
		/// initial status = 0
		/// in shutdown process = 1
		/// already shutted down = 2
		/// </summary>
		int status = 0;
		
		private WaitSignal startupSyncSignal;
		private WaitSignal shutdownSyncSignal;
		
		
		
        public CatalinaServer( string ip, int port)
		{
			this.PORT_NO = port;
			this.SERVER_IP = ip;
		}
        
        public int StartupSync(string fileName, string parameter) {
        	if (status == 1 || pid != -1)
				return 0;
        	startupSyncSignal = new WaitSignal();
			Startup(fileName, parameter);
			startupSyncSignal.WaitOneWhen("Starting ProtocolHandler","Address already in use");
			if (startupSyncSignal.IsErrorOccured) {
				return -1;
			}
			
			return 1;
        }
        
        public void Startup(string fileName, string parameter) {
        	if (status == 1 || pid != -1)
				return;
        	
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
		
		
		private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
		     //notify signal if any
		     if (startupSyncSignal!=null)
		     	startupSyncSignal.SetWhen(outLine.Data);
        }
        
        private void OnExited(object sender, System.EventArgs e) {
        	System.Diagnostics.Debug.WriteLine("Catalina process {0} Exited", pid);
		    pid = -1; 
		    //notify signal if any
		     if (shutdownSyncSignal!=null)
		     	shutdownSyncSignal.SetWhen("A valid shutdown command was received via the shutdown port. Stopping the Server instance");
		    
        }
		
        public void ShutdownSync() {
        	if ( pid == -1 || status != 0)
        		return;
        	shutdownSyncSignal = new WaitSignal();
        	Shutdown();
        	shutdownSyncSignal.WaitOneWhen("A valid shutdown command was received via the shutdown port. Stopping the Server instance","Exception");
        }
        
		public void Shutdown() {
        	if ( pid == -1 || status != 0)
        		return;
        	
        	status = 1;
            //---create a TCPClient object at the IP and port no.---
            using ( TcpClient client = new TcpClient(SERVER_IP, SHUTDOWN_PORT)) {
            	NetworkStream nwStream = client.GetStream();
            	byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(ShutdownCmd);
	            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
	            status = 2; 
            }
                     
		}
        
        public bool IsStartedUp() {
        	return this.pid !=  -1 && status == 0;
        }
	}
}
