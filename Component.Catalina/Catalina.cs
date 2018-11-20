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
using System.Windows.Forms;
using App.Common;
using App.Common.Proc;
using App.Common.Signal;
using log4net;


namespace Component.Catalina
{
	/// <summary>
	/// catalina server bind.
	/// </summary>
	public class CatalinaServer
	{
		private int pid = -1;
		private int PORT_NO = 8080;
		private int SHUTDOWN_PORT = 60010;
        private string SERVER_IP = "127.0.0.1";
        private string ShutdownCmd = "NGO_CATA_BYE";
        
        private int jdwpPort =60081;
        
        private string contextPath;
        private string webAppPath;
        
        private Component.Catalina.ICatalinaOutputCallback callback;
        private IPidCallback pidCallback;
        
		/// <summary>
		/// initial status = 0
		/// in shutdown process = 1
		/// already shutted down = 2
		/// </summary>
		int status = 0;
		
		private WaitSignal startupSyncSignal;
		private WaitSignal shutdownSyncSignal;
		
		private static readonly ILog logger = LogManager.GetLogger(typeof(CatalinaServer));  

		
        public CatalinaServer(ICatalinaOutputCallback callback,IPidCallback pCallback, string ip, int port,  string webAppPath, string contextPath)
		{
			this.PORT_NO = port;
			this.SERVER_IP = ip;
			this.callback = callback;
			this.pidCallback = pCallback;
			this.webAppPath = webAppPath;
			this.contextPath = contextPath;
		}
        
        public void StartupSync() {
        	if (status == 1 || pid != -1)
				return;
        	startupSyncSignal = new WaitSignal();
			Startup();
			startupSyncSignal.WaitOneWhen("Starting ProtocolHandler","Address already in use");
			if (startupSyncSignal.IsErrorOccured) {
				logger.Error(string.Format("catalina startup failed - {0}.", startupSyncSignal.AttechedObject.ToString()));
				MessageBox.Show(startupSyncSignal.AttechedObject.ToString());
				return;
			}
			logger.Info(string.Format("catalina {0} started up.", pid));
        }
        
        public void Startup() {
        	if (status == 1 || pid != -1)
				return;
        	
        	var fileName = CodeBase.GetCodePath() +@"\jre\bin\java.exe";
        	var extPath = CodeBase.GetCodePath() +@"\embed\ext";
        	var catalinaHome = CodeBase.GetCodePath() +@"\embed";
        	
        	//* Create your Process
		    Process process = new Process();
		    process.StartInfo.FileName = fileName;
		    
		    //%EXECUTABLE% -Xdebug -Xrunjdwp:transport=dt_socket,address=8001,server=y,suspend=n -classpath %EMBED_EXT%\bootstrap.jar;%EMBED_EXT%\ecj-4.5.1.jar;%EMBED_EXT%\tomcat-dbcp.jar;%EMBED_EXT%\tomcat-embed-core.jar;%EMBED_EXT%\tomcat-embed-el.jar;%EMBED_EXT%\tomcat-embed-jasper.jar;%EMBED_EXT%\tomcat-embed-logging-juli.jar;%EMBED_EXT%\tomcat-embed-logging-log4j.jar;%EMBED_EXT%\tomcat-embed-websocket.jar  Program 
		    
		    var appOption = @"-DcatalinaHome="+catalinaHome+" -DwebAppPath="+this.webAppPath+" -DcontextPath="+this.contextPath;
		    var propOption = @"-Dport="+this.PORT_NO+" -DshutPort="+this.SHUTDOWN_PORT+" -DshutCmd="+this.ShutdownCmd;
		    var jvmOption = @"-Xdebug -Xrunjdwp:transport=dt_socket,address="+jdwpPort+",server=y,suspend=n";
		    var classpath = @"-classpath %EMBED_EXT%\bootstrap1.0.jar;%EMBED_EXT%\ecj-4.5.1.jar;%EMBED_EXT%\tomcat-dbcp.jar;%EMBED_EXT%\tomcat-embed-core.jar;%EMBED_EXT%\tomcat-embed-el.jar;%EMBED_EXT%\tomcat-embed-jasper.jar;%EMBED_EXT%\tomcat-embed-logging-juli.jar;%EMBED_EXT%\tomcat-embed-logging-log4j.jar;%EMBED_EXT%\tomcat-embed-websocket.jar";
		    classpath = classpath.Replace("%EMBED_EXT%", extPath);
		    string main = "Program";
		    process.StartInfo.Arguments =String.Format("{0} {1} {2} {3}",appOption, jvmOption,classpath,main);
				
			   
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
		    						this.pidCallback.PidCreated("catalina", pid);
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
		    
		    if (callback!=null)
		     	callback.OutputArrived(outLine.Data);
		    System.Diagnostics.Debug.WriteLine(outLine.Data);
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
	}
}
