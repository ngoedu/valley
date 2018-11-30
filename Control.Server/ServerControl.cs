/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/25
 * Time: 17:31
 * 
 * 
 */
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using App.Common;
using App.Common.Proc;
using App.Common.Reg;
using Component.Catalina;

namespace Control.Server
{
	/// <summary>
	/// Description of ServerControl.
	/// </summary>
	public partial class ServerControl : UserControl , ICatalinaOutputCallback, IAppEntry
	{
		
		private CatalinaServer catalina ;
		
		public ServerControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
			tomcatBackgroundWorker = new BackgroundWorker(); // 实例化后台对象webapp 
            tomcatBackgroundWorker.WorkerReportsProgress = true; // 设置可以通告进度
            tomcatBackgroundWorker.WorkerSupportsCancellation = true; // 设置可以取消
            tomcatBackgroundWorker.DoWork += new DoWorkEventHandler(tomcatDoWork);
            tomcatBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(tomcatUpdateProgress);
            tomcatBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(tomcatCompletedWork);
            tomcatBackgroundWorker.RunWorkerAsync(this);
		}

		#region IAppEntry implementation
		private BackgroundWorker tomcatBackgroundWorker;
		private BlockingCollection<string> tomcatQueue = new BlockingCollection<string>();
		private int pid = -1;
		private int inShutdown = -1;
		private int port = 60080;
		private string context = string.Empty;


		public void SetAppTitleCallback(IAppTitleCallback callback)
		{
			//DO nothing
		}
		public void Init(AppRegistry reg)
		{
			var webapp = (string)reg[AppRegKeys.EIDE_PROJ];
			context = (string)reg[AppRegKeys.CATALINA_CONTEXT];
			catalina = new CatalinaServer(this, PidRecorder.Instance, "127.0.0.1", port,  webapp, "/"+context);
			
			//reg push value.
			reg[AppRegKeys.BROWSER_URL] = "http://127.0.0.1:"+port+"/"+context+"/";
			
		}

		#region ICatalinaOutputCallback implementation
		public void OutputArrived(string output)
		{
			tomcatQueue.Add(output);
		}
		#endregion
		
		void tomcatDoWork(object sender, DoWorkEventArgs e)
        {
			while (true)
			{
				var item = tomcatQueue.Take();
				//System.Diagnostics.Debug.WriteLine("item Taken");
				tomcatBackgroundWorker.ReportProgress(1,item);
			}		
		}
		
		void tomcatUpdateProgress(object sender, ProgressChangedEventArgs e)
        {
			//System.Diagnostics.Debug.WriteLine("updateProgress");
			if (e != null && e.UserState != null) {
				if (inShutdown == 0) {
					if (e.UserState.ToString().EndsWith("A valid shutdown command was received via the shutdown port. Stopping the Server instance.", StringComparison.Ordinal)) {
						inShutdown = 1;
						System.Diagnostics.Debug.WriteLine("Tomcat shutted down!");
					} else {
						if (pid > 0 && Process.GetProcessById(pid) !=null)
						{
							Process.GetProcessById(pid).Kill();
							pid = -1;
						}
							
					}
				}
				string message = e.UserState.ToString();
				if (message.StartsWith("$MARK=")) {
					int idx1 = message.IndexOf('[')+1;
					int idx2 = message.IndexOf(']');
					var color = message.Substring(idx1, idx2 - idx1);
					this.rtbTomcatConsole.AppendText(message.Substring(idx2+ 1));
					MarkupText(message.Substring(idx2+ 1), color);
					
				} else {
					this.rtbTomcatConsole.AppendText(message+"\r\n");
				
				}
			}
		}
		 
		void tomcatCompletedWork(object sender, RunWorkerCompletedEventArgs e)
        {	
		}
		

		public void Reload(AppRegistry reg)
		{
			if (catalina != null) {
				catalina.ShutdownSync();
			}
			Init(reg);
			
			this.btTomcatStart.Enabled = true;
			this.btTomcatStop.Enabled = false;
			this.pbTomcatStatus.Image = global::Control.Server.Resource1.tomcat_logo_trans_grey_48x48;
			
		}


		public void Active()
		{
			
		}


		public void Inactive()
		{
			
		}


		public void Dispose(AppRegistry reg)
		{
			catalina.ShutdownSync();
			
		}


		public AppStatus Status()
		{
			throw new NotImplementedException();
		}


		#endregion
		private void MarkupText(string text, string color) {
			rtbTomcatConsole.SelectionStart = rtbTomcatConsole.TextLength - (text.Length -1);
	        rtbTomcatConsole.SelectionLength = text.Length;  
	        rtbTomcatConsole.SelectionColor = Color.FromName(color);
			rtbTomcatConsole.SelectionStart = 0;  
	        rtbTomcatConsole.SelectionLength = 0; 
		}
		private void AppendColorText (string text, string color) {
			tomcatQueue.Add("$MARK=["+color+"]"+text);
		}

		void TabCatalinaSizeChanged(object sender, EventArgs e)
		{
			this.rtbTomcatConsole.Width = this.tabCatalina.ClientSize.Width - 2;
			this.rtbTomcatConsole.Left = 1;
			this.rtbTomcatConsole.Height = this.tabCatalina.ClientSize.Height - 100;
		}
		void BtTomcatStartClick(object sender, EventArgs e)
		{
			if (catalina.IsStartedUp())
			{
				MessageBox.Show("Tomcat服务器已经启动了。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			catalina.StartupSync();
			AppendColorText("Tomcat服务器已经启动。端口="+port+",应用="+context+"\r\n", "green");
			this.pbTomcatStatus.Image = global::Control.Server.Resource1.tomcat_logo_trans_vivid_48x48;
			this.btTomcatStart.Enabled = false;
			this.btTomcatStop.Enabled = true;
		}
		void BtTomcatStopClick(object sender, EventArgs e)
		{
			if (!catalina.IsStartedUp())
			{
				MessageBox.Show("Tomcat服务器还没启动。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			catalina.ShutdownSync();
			AppendColorText("Tomcat服务器已经停止"+"\r\n", "green");
			
			this.pbTomcatStatus.Image = global::Control.Server.Resource1.tomcat_logo_trans_grey_48x48;
			this.btTomcatStart.Enabled = true;
			this.btTomcatStop.Enabled = false;
		}
		
		
	}
}
