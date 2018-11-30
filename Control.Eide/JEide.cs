/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/19
 * Time: 1:05
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using App.Common.Net;
using App.Common.Proc;
using App.Common.Reg;
using App.Common.Signal;
using App.Common.Win32;
using log4net;

namespace Control.Eide
{
	/// <summary>
	/// Description of JEide.
	/// </summary>
	public partial class JEide : Panel, IAppEntry, IDatReceiver
	{
		
		public static int ENDPOINT_ID = 9;
		
		public static string CMD_EXIT = "$EXIT=0";
		public static string CMD_ADDPROJ = "$ADDPROJ=";

		
		private IntPtr embedHandle;
		private string codeBase;
		private int embedHwd = -1;
		private Process embedEclipse = null;
		private IPidCallback pidCallback;
		private string eideTitle = "Eclipse";
		
		private static readonly ILog logger = LogManager.GetLogger(typeof(JEide));  

	
		/// <summary>
		/// initial status = 0
		/// in shutdown process = 1
		/// already shutted down = 2
		/// </summary>
		AppStatus status;
		private int pid = -1;
		
		
		public JEide(string winTitle, string cb, IPidCallback callback)
		{
			InitializeComponent();
			this.codeBase = cb;
			this.eideTitle = winTitle;
			this.pidCallback = callback;
			BackColor = Color.Black;
			this.SizeChanged += new System.EventHandler(this.PanelSizeChanged);
		}
		
		#region IAppEntry implementation
		public void SetAppTitleCallback(IAppTitleCallback callback)
		{
			//DO nothing
		}
		public void Init(AppRegistry reg) {
			logger.Debug("Init");
			
			//register DAT receiver
			IClient client = (IClient)reg[AppRegKeys.AETHER_CLIENT];
			client.RegistDataReceiver(this);
			
			var projPath = (string)reg[AppRegKeys.EIDE_PROJ];
			var cdatPath = (string)reg[AppRegKeys.EIDE_CDAT];
			
			ClasspathType ctype = ClassPath.CheckClasspathType(projPath);
			bool initCP = ClassPath.GetClasspath(ctype).Init(projPath);
			
			WorkspaceType wtype = Workspace.CheckWorkspaceType(projPath);
			bool firstTimeInit = Workspace.GetWorkspace(wtype).Init(cdatPath);
			logger.Info(firstTimeInit ? "workspace Init done" : "workspace loaded");
			
			var workspace = cdatPath+Workspace.GetWorkspace(wtype).WorkspaceFolderName();
			                       
			//2.launch EIDE
			this.LoadEide(false, workspace);
			logger.Debug("LoadEide done");
			this.EmbedIde();
			logger.Debug("EmbedIde done");
			this.WindowsReStyle();
			logger.Debug("Restyle done");
			
			
			//3.import project to EIDE
			string response = client.SendToRemoteSync(CMD_ADDPROJ+projPath, ENDPOINT_ID);
			logger.Debug("AddProj cmd sent to remote peer, response="+response);
			
			var eideResponse = EideResponse.Parse(response);
			if (eideResponse.status.Equals(EideResponse.STATUS_OK) && eideResponse.natid==ClientConst.NAT_EIDECLIENT_ID){
				logger.Debug("project "+projPath+" is sucessfully added into workspace.");
			}
			else {
				logger.Error("add project "+projPath+" to workspace is failed - " + response);
				MessageBox.Show("[EIDE] add project "+projPath+" to workspace is failed - " + response);
			}
		
		}
		
		public void Active()
		{
			
		}
		
		public void Inactive()
		{
			
		}
		
		
		public void Reload(AppRegistry reg)
		{
			logger.Info("Reload");
			
			var projPath = (string)reg[AppRegKeys.EIDE_PROJ];
			var cdatPath = (string)reg[AppRegKeys.EIDE_CDAT];
			
			ClasspathType ctype = ClassPath.CheckClasspathType(projPath);
			bool initCP = ClassPath.GetClasspath(ctype).Init(projPath);
			
			WorkspaceType type = Workspace.CheckWorkspaceType(projPath);
			var workspace = cdatPath+Workspace.GetWorkspace(type).WorkspaceFolderName();
			
			//3.import project to EIDE
			IClient client = (IClient)reg[AppRegKeys.AETHER_CLIENT];
			string response = client.SendToRemoteSync(CMD_ADDPROJ+projPath, ENDPOINT_ID);
			logger.Debug("AddProj cmd re-sent to remote peer, response="+response);
			
			var eideResponse = EideResponse.Parse(response);
			if (eideResponse.status.Equals(EideResponse.STATUS_OK) && eideResponse.natid==ClientConst.NAT_EIDECLIENT_ID){
				logger.Debug("project "+projPath+" is sucessfully re-added into workspace.");
			}
			else {
				logger.Debug("reload project "+projPath+" to workspace is failed - " + response);
				MessageBox.Show("[EIDE] reload project "+projPath+" to workspace is failed - " + response);
			}
		}

		public AppStatus Status()
		{
			return this.status;
		}
		
		private WaitSignal exitSignal;
		public void Dispose(AppRegistry reg)
		{
			//shutdown EIDE
			IClient client = (IClient)reg[AppRegKeys.AETHER_CLIENT];
			string response = client.SendToRemoteSync(CMD_EXIT, ENDPOINT_ID);
			var eideResponse = EideResponse.Parse(response);
			if (eideResponse.status.Equals(EideResponse.STATUS_OK) && eideResponse.natid == ClientConst.NAT_EIDECLIENT_ID) {
				
				exitSignal = new WaitSignal();
				exitSignal.WaitOne();
				
				logger.Info("[EIDE] workspace sucessfully exit.");
				
				this.status = AppStatus.Disposed;
			}
		}
		#endregion
	
		void PanelSizeChanged(object sender, EventArgs e)
		{
			ResizeEmebed();
		}
		
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			
			if (pid==-1)
				return;
			
			this.pidCallback.KillProcessById("eide",pid);
			
			
			base.Dispose(disposing);
		}

		private readonly WaitSignal wbsSignal = new WaitSignal();
		private bool isWorkbenchActive = false;
		#region IDatReceiver implementation
		public void DataArrived(string message)
		{
			logger.Debug("Eide DAT package arrived - "+message);
			EideResponse resp = EideResponse.Parse(message);
			if (resp.action.Equals("$WBSREADY") && resp.status.Equals("OK"))
			{
				isWorkbenchActive = true;
				//eclipse workbench is now active
				logger.Debug("Eide DAT package notify shell is ready, reset flag and signal...");
				wbsSignal.Set();
			}
			                                                 
		}
		public int NatId()
		{
			return ClientConst.NAT_EIDECLIENT_ID;
		}
		#endregion	

		
		private string GenParameters(string ws) {
		                            
			var osgiRequiredJavaVer_0 = "-Dosgi.requiredJavaVersion=1.8";
			var jvmXX_1 = "-XX:+UseG1GC -XX:+UseStringDeduplication";
			var aether_2 = @"-Dngo.bridge.host=127.0.0.1 -Dngo.bridge.port=60001 -Dext.jar.path=D:\NGO\client\embed\ext";
			var jvmXX_3 = "-Xms256m -Xmx1024m";
			var jar_4 = @"-jar "+this.codeBase+@"\eide\dist\eclipse\\plugins/org.eclipse.equinox.launcher_1.3.201.v20161025-1711.jar";
			var os_5 = "-os win32";
			var ws_6 = "-ws win32";
			var arch_7 = "-arch x86";
			var splash_8 = "";// @"-showsplash  D:\NGO\client\eide\dist\eclipse\\plugins\org.eclipse.platform_4.6.3.v20170301-0400\splash.bmp";
			var launcher_9 = "";//@"-launcher D:\NGO\client\eide\exe\eclipse\eclipse.exe -name Eclipse";
			var launchlib_10 =	@"--launcher.library "+this.codeBase+@"\eide\dist\eclipse\\plugins/org.eclipse.equinox.launcher.win32.win32.x86_1.1.401.v20161122-1740\eclipse_1617.dll";
			var startup_11 = @"-startup "+this.codeBase+@"\eide\dist\eclipse\\plugins/org.eclipse.equinox.launcher_1.3.201.v20161025-1711.jar";
			var launcher_appVmarg_12 = @"--launcher.appendVmargs -exitdata 11ec_80 -product org.eclipse.epp.package.java.product";
			var vm_13 = @"-vm "+this.codeBase+@"/jre/bin/javaw.exe";		
			//var data_14 = @"-data "+this.codeBase+@"\eide\dist\ws.5";		
			var data_14 = @"-data "+ws;		
			
			var arguments = String.Format("{0} {1} {2} {3} {4} {5} {6} {7}  {8} {9} {10} {11} {12} {13} {14}",
			                              osgiRequiredJavaVer_0,jvmXX_1,aether_2,jvmXX_3,jar_4,os_5,ws_6,arch_7,splash_8,launcher_9,launchlib_10,startup_11,launcher_appVmarg_12,vm_13,data_14);			
			
			#if (DIA_RELEASE)
            arguments = arguments.Replace(@"dist\eclipse\","");
			#endif
			
			return arguments;
		}
		
		private bool CreateByProcess(string ws) {
			var fileName = this.codeBase+@"\jre\bin\javaw.exe";
			var arguments = GenParameters(ws);

			ProcessStartInfo psi = new ProcessStartInfo();
			psi.WindowStyle = ProcessWindowStyle.Hidden;
			psi.FileName = fileName;
			psi.Arguments = arguments;
			psi.RedirectStandardOutput = true;
			psi.RedirectStandardError = true;
			psi.UseShellExecute = false;
			psi.CreateNoWindow = true;
			
			Process process = new Process();
			process.StartInfo = psi;
			process.EnableRaisingEvents = true;
			process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
		    process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
			process.Exited += new EventHandler(OnExited);
			//process = Process.Start(fileName, arguments, null, null, null);
			bool started = process.Start(); 
			process.BeginOutputReadLine();
			process.BeginErrorReadLine();
			
			return started;
		}
		
		private void OnExited(object sender, System.EventArgs e) {
        	logger.Info("[EIDE] process Exited");
        	pid = -1;
        	if (exitSignal !=null)
        		exitSignal.Set();
        }
		
		private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
		    //System.Diagnostics.Debug.WriteLine("OutputHandler {0} ", outLine.Data);  
		}
		
		
		
		/// <summary>
		/// embed EIDE into anther window
		/// </summary>
		public void EmbedIde() {
			if (embedHwd > 0)
       			Win32Api.ShowWindow(embedHwd, Win32Api.SW_SHOW);
			
			Process[] processlist = Process.GetProcesses();
			foreach (Process theprocess in processlist) 
			{
				if (theprocess.MainWindowTitle.Contains(eideTitle)) {		
					embedHandle = theprocess.MainWindowHandle;
					var resul1 = Win32Api.SetParent(embedHandle, this.Handle);
					int errCode = Marshal.GetLastWin32Error();
					//System.Diagnostics.Debug.WriteLine("[EIDE] embeding result - " +GetSysErrMsg(errCode));
					break;
				}
			}
			ResizeEmebed();
		}
		
		/// <summary>
		/// resize the windwo
		/// </summary>
		private void ResizeEmebed()
		{
			Win32Api.SetWindowPos(embedHandle, 0, 1, 1, this.Width-1, this.Height-1, Win32Api.SWP_NOZORDER | Win32Api.SWP_SHOWWINDOW);   
			//SetWindowPos(embedHandle, 0, 0, 0, this.Width, this.Height, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER);
			
			//try set window no border
	        int style = Win32Api.GetWindowLong(embedHandle, Win32Api.GWL_STYLE);
			Win32Api.SetWindowLong(embedHandle, Win32Api.GWL_STYLE, (style & ~ Win32Api.WS_THICKFRAME )); 
		}
		
		/// <summary>
		/// launch the EIDE
		/// </summary>
		/// <param name="visible"></param>
		public void LoadEide(bool visible, string ws) {
			if ( pid != -1 )
        		return;
			
			//create process
			bool started = CreateByProcess(ws);

			//check if workbench is ready, thread wait when not
			if (!isWorkbenchActive) {
				logger.Debug("LoadEide - workbehch shell is NOT ready now, thread have to wait...");
				wbsSignal.WaitOne();
				logger.Debug("LoadEide - workbehch shell is ready now, thread continue");
			}
			
			//wait until launched and then hide it
			pid = IsRunning();
			if (pid > -1) {
				this.pidCallback.PidCreated("eide", pid);
			
				//check if do window hidden
				if (!visible) {
		
					//hide it now
					int hWnd;
					Process[] processRunning = Process.GetProcesses();
					foreach (Process pr in processRunning){
					    if (pr.MainWindowTitle.Contains(eideTitle)){
					    	embedEclipse = pr;
					        hWnd = pr.MainWindowHandle.ToInt32();
					        embedHwd = hWnd;
					        Win32Api.ShowWindow(hWnd, Win32Api.SW_HIDE);
					        return;
					    }
					}
				}
			}
		}
		
		/// <summary>
		/// hide the window
		/// </summary>
		public void HideEmebed() {
			Win32Api.SetWindowPos(embedHandle, 0, 0, 0, this.Width, this.Height, Win32Api.SWP_NOZORDER | Win32Api.SWP_SHOWWINDOW | 0X80);            
		}
		
		/// <summary>
		/// check if process existed
		/// </summary>
		/// <returns></returns>
		private int IsRunning()
		{
			Process[] processlist = Process.GetProcesses();
			foreach (Process theprocess in processlist) {
				if (theprocess.MainWindowTitle.Contains(eideTitle)) {
					return theprocess.Id;
				}
			}
			return -1;
		}
		
		
		/// <summary>
		/// re-style the window, remove title bar
		/// </summary>
		public void WindowsReStyle()
		{ 
		    if (embedEclipse !=null) 
	        {
	            IntPtr pFoundWindow = embedEclipse.MainWindowHandle;
	            int style = Win32Api.GetWindowLong(pFoundWindow, Win32Api.GWL_STYLE);
	
	            //get menu
	            IntPtr HMENU = Win32Api.GetMenu(embedEclipse.MainWindowHandle);
	            //get item count
	            int count = Win32Api.GetMenuItemCount(HMENU);
	            
	            //below cause some unusual error when editing css & html page.
	            /*for (int i = 0; i < count; i++)
	               	RemoveMenu(HMENU, 0, (MF_BYPOSITION | MF_REMOVE));
	             */

	            //force a redraw
	            Win32Api.DrawMenuBar(embedEclipse.MainWindowHandle);
	         
	            //below cause some unusual error when editing css & html page.
	            /*SetWindowLong(pFoundWindow, GWL_STYLE, (style & ~WS_SYSMENU));*/ 
	            
	            Win32Api.SetWindowLong(pFoundWindow, Win32Api.GWL_STYLE, (style & ~ Win32Api.WS_CAPTION));
	        } 
		    
		}  
		
		public static string GetSysErrMsg(int errCode)
        {
            IntPtr tempptr = IntPtr.Zero;
            string msg = null;
            Win32Api.FormatMessage(0x1300, ref tempptr, errCode, 0, ref msg, 255, ref tempptr);
            return msg;
        }
	}
}
