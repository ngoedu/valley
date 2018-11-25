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
			
			
		}

		#region IAppEntry implementation
		private BackgroundWorker tomcatBackgroundWorker;
		private BlockingCollection<string> tomcatQueue = new BlockingCollection<string>();
		private int pid = -1;
		private int inShutdown = -1;
		

		public void Init(AppRegistry reg)
		{
			var webapp = (string)reg[AppRegKeys.EIDE_PROJ];
			var context = reg[AppRegKeys.CATALINA_CONTEXT];
			catalina = new CatalinaServer(this, PidRecorder.Instance, "127.0.0.1",60080,  webapp, "/"+context);
			
			tomcatBackgroundWorker = new BackgroundWorker(); // 实例化后台对象webapp
 
            tomcatBackgroundWorker.WorkerReportsProgress = true; // 设置可以通告进度
            tomcatBackgroundWorker.WorkerSupportsCancellation = true; // 设置可以取消
 
            tomcatBackgroundWorker.DoWork += new DoWorkEventHandler(tomcatDoWork);
            tomcatBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(tomcatUpdateProgress);
            tomcatBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(tomcatCompletedWork);

            tomcatBackgroundWorker.RunWorkerAsync(this);
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
				this.rtbTomcatConsole.AppendText(e.UserState.ToString()+"\r\n");
			}
		}
		 
		void tomcatCompletedWork(object sender, RunWorkerCompletedEventArgs e)
        {	
		}
		

		public void Reload(AppRegistry reg)
		{
			
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

		void TabCatalinaSizeChanged(object sender, EventArgs e)
		{
			this.rtbTomcatConsole.Width = this.tabCatalina.ClientSize.Width - 2;
			this.rtbTomcatConsole.Left = 1;
			this.rtbTomcatConsole.Height = this.tabCatalina.ClientSize.Height - this.rtbTomcatConsole.Top - 20;
		}
		void BtTomcatStartClick(object sender, EventArgs e)
		{
			catalina.StartupSync();
			this.pbTomcatStatus.Image = global::Control.Server.Resource1.tomcat_logo_trans_vivid_48x48;
		}
		void BtTomcatStopClick(object sender, EventArgs e)
		{
			catalina.ShutdownSync();
			this.pbTomcatStatus.Image = global::Control.Server.Resource1.tomcat_logo_trans_grey_48x48;
		}
	}
}
