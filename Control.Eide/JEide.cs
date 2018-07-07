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
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using App.Common.Callback;
using App.Common.Impl;

namespace Control.Eide
{
	/// <summary>
	/// Description of JEide.
	/// </summary>
	public partial class JEide : Panel 
	{
		private IntPtr embedHandle;
		private int embedHwd = -1;
		private Process embedEclipse = null;
		private IPidCallback pidCallback;
		
		const int SW_HIDE =              0;
		const int SW_SHOWNORMAL   =    1;
		const int SW_NORMAL         =  1;
		const int SW_SHOWMINIMIZED  =  2;
		const int SW_SHOWMAXIMIZED  =  3;
		const int SW_MAXIMIZE       =  3;
		const int SW_SHOWNOACTIVATE =  4;
		const int SW_SHOW            = 5;
		const int SW_MINIMIZE       =  6;
		const int SW_SHOWMINNOACTIVE = 7;
		const int SW_SHOWNA         = 8;
		const int SW_RESTORE        =  9;
		const int SW_SHOWDEFAULT    =  10;
		const int SW_FORCEMINIMIZE   = 11;
		const int SW_MAX            =  11;
			
		const short SWP_NOMOVE = 0X2;
		const short SWP_NOSIZE = 1;
		const short SWP_NOZORDER = 0X4;
		const int SWP_SHOWWINDOW = 0x0040;
		const int SWP_FRAMECHANGED = 0x0020;
		const int SWP_NOOWNERZORDER = 0x0200;
		
		//assorted constants needed
		public static uint MF_BYPOSITION = 0x400;
		public static uint MF_REMOVE = 0x1000;
		public static int GWL_STYLE = -16;
		public static int WS_CHILD = 0x40000000; //child window
		public static int WS_BORDER = 0x00800000; //window with border
		public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
		public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar 
		public static int WS_SYSMENU = 0x00080000; //window menu  
		public static int WS_THICKFRAME = 0x00040000;
		
			
		[System.Runtime.InteropServices.DllImport("Kernel32.dll")]
		public extern static int FormatMessage(int flag, ref IntPtr source, int msgid, int langid, ref string buf, int size, ref IntPtr args);
		[DllImport("user32.dll")]
		public static extern int ShowWindow(int hwnd, int cmdShow);

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		
		[DllImport("USER32.DLL")]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
		
		//Gets window attributes
		[DllImport("USER32.DLL")]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
		
		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
		
		[DllImport("user32.dll")]
		static extern IntPtr GetMenu(IntPtr hWnd);
		
		[DllImport("user32.dll")]
		static extern int GetMenuItemCount(IntPtr hMenu);
		
		[DllImport("user32.dll")]
		static extern bool DrawMenuBar(IntPtr hWnd);
		
		[DllImport("user32.dll")]
		static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
		
		
		
		/// <summary>
		/// initial status = 0
		/// in shutdown process = 1
		/// already shutted down = 2
		/// </summary>
		int status = 0;
		private int pid = -1;
		
		
		public JEide(string winTitle, IPidCallback callback)
		{
			InitializeComponent();
			this.eideTitle = winTitle;
			this.pidCallback = callback;
			BackColor = Color.Black;
			this.SizeChanged += new System.EventHandler(this.PanelSizeChanged);
		}
		
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
			
			PidRecorder.Instance.KillProcessById(pid);
			
			base.Dispose(disposing);
		}
		
		private string getParameter() {
		                            
			var osgiRequiredJavaVer_0 = "-Dosgi.requiredJavaVersion=1.8";
			var jvmXX_1 = "-XX:+UseG1GC -XX:+UseStringDeduplication";
			var aether_2 = "-Dngo.bridge.host=127.0.0.1 -Dngo.bridge.port=60001";
			var jvmXX_3 = "-Xms256m -Xmx1024m";
			var jar_4 = @"-jar D:\NGO\client\eide\dist\eclipse\\plugins/org.eclipse.equinox.launcher_1.3.201.v20161025-1711.jar";
			var os_5 = "-os win32";
			var ws_6 = "-ws win32";
			var arch_7 = "-arch x86";
			var splash_8 = "";// @"-showsplash  D:\NGO\client\eide\dist\eclipse\\plugins\org.eclipse.platform_4.6.3.v20170301-0400\splash.bmp";
			var launcher_9 = "";//@"-launcher D:\NGO\client\eide\exe\eclipse\eclipse.exe -name Eclipse";
			var launchlib_10 =	@"--launcher.library D:\NGO\client\eide\dist\eclipse\\plugins/org.eclipse.equinox.launcher.win32.win32.x86_1.1.401.v20161122-1740\eclipse_1617.dll";
			var startup_11 = @"-startup D:\NGO\client\eide\dist\eclipse\\plugins/org.eclipse.equinox.launcher_1.3.201.v20161025-1711.jar";
			var launcher_appVmarg_12 = @"--launcher.appendVmargs -exitdata 11ec_80 -product org.eclipse.epp.package.java.product";
			var vm_13 = @"-vm D:/ngo/client/jdk/bin/javaw.exe";		
			var data_14 = @"-data D:\NGO\client\eide\dist\ws.5";		
			
			var arguments = String.Format("{0} {1} {2} {3} {4} {5} {6} {7}  {8} {9} {10} {11} {12} {13} {14}",
			                              osgiRequiredJavaVer_0,jvmXX_1,aether_2,jvmXX_3,jar_4,os_5,ws_6,arch_7,splash_8,launcher_9,launchlib_10,startup_11,launcher_appVmarg_12,vm_13,data_14);			
			return arguments;
		}
		
		private bool CreateByProcess() {
			var fileName = "d:\\NGO\\client\\jdk\\bin\\javaw.exe";
			var arguments = getParameter();

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
        	System.Diagnostics.Debug.WriteLine("process {0} Exited", pid);
        	//MessageBox.Show("EIDE onExited");
		    pid = -1; 
        }
		
		private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
		    System.Diagnostics.Debug.WriteLine("OutputHandler {0} ", outLine.Data);  
		}
		
		
		private String eideTitle = "Eclipse";
		
		/// <summary>
		/// embed EIDE into anther window
		/// </summary>
		public void EmbedIde() {
			if (embedHwd > 0)
       			ShowWindow(embedHwd, SW_SHOW);
			
			Process[] processlist = Process.GetProcesses();
			foreach (Process theprocess in processlist) 
			{
				if (theprocess.MainWindowTitle.Contains(eideTitle)) {		
					embedHandle = theprocess.MainWindowHandle;
					var resul1 = SetParent(embedHandle, this.Handle);
					int errCode = Marshal.GetLastWin32Error();
					System.Diagnostics.Debug.WriteLine(GetSysErrMsg(errCode));
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
			SetWindowPos(embedHandle, 0, 4, 2, this.Width-4, this.Height-2, SWP_NOZORDER | SWP_SHOWWINDOW);   
			//SetWindowPos(embedHandle, 0, 0, 0, this.Width, this.Height, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER);
			
			//try set window no border
	        int style = GetWindowLong(embedHandle, GWL_STYLE);
			SetWindowLong(embedHandle, GWL_STYLE, (style & ~WS_THICKFRAME )); 
		}
		
		/// <summary>
		/// launch the EIDE
		/// </summary>
		/// <param name="visible"></param>
		public void LoadEide(bool visible) {
			if ( pid != -1 )
        		return;
			
			//create process
			bool started = CreateByProcess();

			//wait until launched and then hide it
			while (started && pid == -1) {
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
						        ShowWindow(hWnd, SW_HIDE);
						    }
						}
						break;
					}
					Thread.Sleep(5);
				}
			}
		}
		
		/// <summary>
		/// hide the window
		/// </summary>
		public void HideEmebed() {
			SetWindowPos(embedHandle, 0, 0, 0, this.Width, this.Height, SWP_NOZORDER | SWP_SHOWWINDOW | 0X80);            
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
					System.Diagnostics.Debug.WriteLine("MainWindowHandle="+theprocess.MainWindowHandle);
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
	            int style = GetWindowLong(pFoundWindow, GWL_STYLE);
	
	            //get menu
	            IntPtr HMENU = GetMenu(embedEclipse.MainWindowHandle);
	            //get item count
	            int count = GetMenuItemCount(HMENU);
	            
	            //below cause some unusual error when editing css & html page.
	            /*for (int i = 0; i < count; i++)
	               	RemoveMenu(HMENU, 0, (MF_BYPOSITION | MF_REMOVE));
	             */

	            //force a redraw
	            DrawMenuBar(embedEclipse.MainWindowHandle);
	         
	            //below cause some unusual error when editing css & html page.
	            /*SetWindowLong(pFoundWindow, GWL_STYLE, (style & ~WS_SYSMENU));*/ 
	            
	            SetWindowLong(pFoundWindow, GWL_STYLE, (style & ~WS_CAPTION));
	        } 
		    
		}  
		
		public static string GetSysErrMsg(int errCode)
        {
            IntPtr tempptr = IntPtr.Zero;
            string msg = null;
            FormatMessage(0x1300, ref tempptr, errCode, 0, ref msg, 255, ref tempptr);
            return msg;
        }
	}
}
