/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-04-27
 * Time: 2:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EmbedIDE
{
	/// <summary>
	/// Description of MainForm.
	/// 
	/// https://msdn.microsoft.com/en-us/library/system.diagnostics.processstartinfo.useshellexecute(v=vs.110).aspx
	/// https://stackoverflow.com/questions/10773003/attach-form-window-to-another-window-in-c-sharp
	/// https://stackoverflow.com/questions/14308836/process-arguments-in-createprocess
	/// https://stackoverflow.com/questions/10554913/how-to-call-createprocess-with-startupinfoex-from-c-sharp-and-re-parent-the-ch
	/// </summary>
	public partial class MainForm : Form
	{
		
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		
		private IntPtr embedHandle;
		 
		private void EmbedNotepad()
		{
			
			ProcessStartInfo _processStartInfo = new ProcessStartInfo();
			//_processStartInfo.WorkingDirectory = @"C:\Users\xho\Downloads\npp.6.3.3.bin\";
			_processStartInfo.FileName = @"notepad.exe";
			//_processStartInfo.Arguments        = "test.txt";
			_processStartInfo.CreateNoWindow = false;

			Process myProcess = Process.Start(_processStartInfo);
			myProcess.WaitForInputIdle();
			embedHandle = myProcess.MainWindowHandle;
			var result = SetParent(myProcess.MainWindowHandle, this.panel1.Handle);
			System.Diagnostics.Debug.WriteLine("result={0}", result);
		}
		
		private void EmbedIE()
		{
			
			ProcessStartInfo _processStartInfo = new ProcessStartInfo();
			//_processStartInfo.WorkingDirectory = @"C:\Users\xho\Downloads\npp.6.3.3.bin\";
			_processStartInfo.FileName = @"D:\NGO\tools\firefox43\FirefoxPortable.exe";
			_processStartInfo.Arguments        = "http://localhost:8080/share.html";
			_processStartInfo.CreateNoWindow = false;

			Process myProcess = Process.Start(_processStartInfo);
			myProcess.WaitForInputIdle();
			embedHandle = myProcess.MainWindowHandle;
			var result = SetParent(myProcess.MainWindowHandle, this.panel1.Handle);
			System.Diagnostics.Debug.WriteLine("result={0}", result);
		}
		
		private void EmbedEIDE()
		{						
			// Use ProcessStartInfo class
			var startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = false;
			startInfo.UseShellExecute = true;
			startInfo.FileName = "d:\\NGO\\client\\jdk\\bin\\javaw.exe";
			startInfo.WindowStyle = ProcessWindowStyle.Normal;

			//D:/ngo/client/jdk/bin/javaw.exe               -vmargs -Dosgi.requiredJavaVersion=1.8 -XX:+UseG1GC -XX:+UseStringDeduplication -Dosgi.requiredJavaVersion=1.8 -Dngo.bridge.host=aether.kidsmath.cn -Dngo.bridge.port=60001 -Xms256m -Xmx1024m  
			
			var osgiRequiredJavaVer_0 = "-Dosgi.requiredJavaVersion=1.8";
			var jvmXX_1 = "-XX:+UseG1GC -XX:+UseStringDeduplication";
			var aether_2 = "-Dngo.bridge.host=aether.kidsmath.cn -Dngo.bridge.port=60001";
			var jvmXX_3 = "-Xms256m -Xmx1024m";
			var jar_4 = @"-jar D:\NGO\client\eide\dist\eclipse\\plugins/org.eclipse.equinox.launcher_1.3.201.v20161025-1711.jar";
			var os_5 = "-os win32";
			var ws_6 = "-ws win32";
			var arch_7 = "-arch x86";
			var splash_8 = @"-showsplash  D:\NGO\client\eide\dist\eclipse\\plugins\org.eclipse.platform_4.6.3.v20170301-0400\splash.bmp";
			var launcher_9 = @"-launcher D:\NGO\client\eide\exe\eclipse\eclipse.exe -name Eclipse";
			var launchlib_10 =	@"--launcher.library D:\NGO\client\eide\dist\eclipse\\plugins/org.eclipse.equinox.launcher.win32.win32.x86_1.1.401.v20161122-1740\eclipse_1617.dll";
			var startup_11 = @"-startup D:\NGO\client\eide\dist\eclipse\\plugins/org.eclipse.equinox.launcher_1.3.201.v20161025-1711.jar";
			var launcher_appVmarg_12 = @"--launcher.appendVmargs -exitdata 11ec_80 -product org.eclipse.epp.package.java.product";
			var vm_13 = @"-vm D:/ngo/client/jdk/bin/javaw.exe";
			
			
			var arguments = String.Format("{0} {1} {2} {3} {4} {5} {6} {7}  {8} {9} {10} {11} {12} {13}",
			                              osgiRequiredJavaVer_0,jvmXX_1,aether_2,jvmXX_3,jar_4,os_5,ws_6,arch_7,splash_8,launcher_9,launchlib_10,startup_11,launcher_appVmarg_12,vm_13);
			startInfo.Arguments = arguments;
			bool launched = false;
			//using (Process myProcess = Process.Start(startInfo)) {
				//myProcess.WaitForInputIdle();
				
				//string cmdText = "/C d:\\NGO\\client\\jdk\\bin\\javaw.exe "+arguments;
				//System.Diagnostics.Process.Start("cmd.exe", cmdText);
				
				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.StartInfo.FileName = @"D:\NGO\client\eide\dist\eclipse\eclipse.exe";
				proc.Start();
				
				while (!launched) {
					launched = IsRunning();
					if (launched)
					{
						break;
					}
				}
				
				Process[] processlist = Process.GetProcesses();
				foreach (Process theprocess in processlist) 
				{
					if (theprocess.MainWindowTitle.Contains("Eclipse")) {
					
						embedHandle = theprocess.MainWindowHandle;
						
						//Process explorerProcess = Process.GetProcessById(5060);
						//var result = SetParent(embedHandle, explorerProcess.Handle);
						//theprocess.Refresh();
						
						var result = SetParent(embedHandle, this.panel1.Handle);
						
						break;
					}
				}
			//}
		}

				
		
		
		private bool IsRunning()
		{
			Process[] processlist = Process.GetProcesses();
			foreach (Process theprocess in processlist) 
			{
				if (theprocess.MainWindowTitle.Contains("Eclipse")) {
					
					System.Diagnostics.Debug.WriteLine("MainWindowHandle="+theprocess.MainWindowHandle);
					return true;
				}
			}
			Thread.Sleep(300);
			return false;
		}
 
		public MainForm()
		{
		}
		void MainFormLoad(object sender, EventArgs e)
		{
		}
		
		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		private void ResizeEmebed()
		{
			const short SWP_NOMOVE = 0X2;
			const short SWP_NOSIZE = 1;
			const short SWP_NOZORDER = 0X4;
			const int SWP_SHOWWINDOW = 0x0040;
			SetWindowPos(embedHandle, 0, 0, 0, panel1.Width, panel1.Height, SWP_NOZORDER | SWP_SHOWWINDOW);            
		}

		
		/// <summary>
		/// https://stackoverflow.com/questions/1190423/using-setwindowpos-in-c-sharp-to-move-windows-around
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MainFormResizeEnd(object sender, EventArgs e)
		{			
		}

		void Panel1SizeChanged(object sender, EventArgs e)
		{
			ResizeEmebed();
		}
		void Button1Click(object sender, EventArgs e)
		{
			EmbedNotepad();
			//EmbedIE();
			//EmbedEIDE();
			ResizeEmebed();
			
		}
	}
}
