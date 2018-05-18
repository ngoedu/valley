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


namespace Component.Catalina
{
	/// <summary>
	/// catalina server bind.
	/// </summary>
	public class Server
	{
		private int pid = -1;
		private int PORT_NO = 8080;
		private int SHUTDOWN_PORT = 6002;
        private string SERVER_IP = "127.0.0.1";
        private string ShutdownCmd = "NGO_CATA_BYE";
        private Component.Catalina.IOutputCallback callback;
        
		/// <summary>
		/// initial status = 0
		/// in shutdown process = 1
		/// already shutted down = 2
		/// </summary>
		int status = 0;
		
        public Server(IOutputCallback callback, string ip, int port, int shutdownPort, string shutdownCmd)
		{
			this.PORT_NO = port;
			this.SHUTDOWN_PORT = shutdownPort;
			this.SERVER_IP = ip;
			this.ShutdownCmd = shutdownCmd;
			this.callback = callback;
		}
		
		public void Startup() {
        	if (status == 1 || pid != -1)
				return;
        	
			//* Create your Process
		    Process process = new Process();
		    process.StartInfo.FileName = @"D:\NGO\client\embed\embed.bat";
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
		    	delegate() { 		process.Start(); 
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
            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, SHUTDOWN_PORT);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(ShutdownCmd);

            //---send the text---
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            client.Close();
            status = 2;          
		}
	}
}
