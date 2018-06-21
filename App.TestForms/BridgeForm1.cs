/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/22
 * Time: 2:08
 * 
 * 
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Component.Bridge;

namespace AppTestForms
{
	/// <summary>
	/// Description of BridgeForm1.
	/// </summary>
	public partial class BridgeForm1 : Form, IOutputCallback
	{
		AetherBridge bridge;
		public BridgeForm1()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			bridge = new AetherBridge(60001, this);
			
			backgroundWorker = new BackgroundWorker(); // 实例化后台对象
 
            backgroundWorker.WorkerReportsProgress = true; // 设置可以通告进度
            backgroundWorker.WorkerSupportsCancellation = true; // 设置可以取消
 
            backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(UpdateProgress);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompletedWork);

            backgroundWorker.RunWorkerAsync(this);
		}
		
		#region IOutputCallback implementation
		public void OutputArrived(string output)
		{
			queue.Add(output);
		}
		#endregion		
		
		// https://stackoverflow.com/questions/4291912/process-start-how-to-get-the-output
		//
		//http://localhost/ns1/lb.jsp
		//
		//
		private BackgroundWorker backgroundWorker;
		
		BlockingCollection<string> queue = new BlockingCollection<string>();
		
		private int pid = -1;
		
		void DoWork(object sender, DoWorkEventArgs e)
        {
			while (true)
			{
				var item = queue.Take();
				//System.Diagnostics.Debug.WriteLine("item Taken");
				backgroundWorker.ReportProgress(1,item);
			}		
		}
		
		void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
			//System.Diagnostics.Debug.WriteLine("updateProgress");
			if (e != null && e.UserState != null) {
				if (inShutdown == 0) {
					if (e.UserState.ToString().EndsWith("A valid shutdown command was received via the shutdown port. Stopping the Server instance.", StringComparison.Ordinal)) {
						inShutdown = 1;
						System.Diagnostics.Debug.WriteLine("shutted down!");
					} else {
						if (pid > 0 && Process.GetProcessById(pid) !=null)
						{
							Process.GetProcessById(pid).Kill();
							pid = -1;
						}
							
					}
				}
				this.richTextBox1.AppendText(e.UserState.ToString()+"\r\n");
			}
		}
		 
		void CompletedWork(object sender, RunWorkerCompletedEventArgs e)
        {	
		}
		
		/// <summary>
		/// https://stackoverflow.com/questions/14455510/how-to-start-a-process-in-a-thread
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Button1Click(object sender, EventArgs e)
		{
			bridge.Startup();
		}
		
		
		/// <summary>
		/// initial status = -1
		/// in shutdown process = 0
		/// already shutted down = 1
		/// </summary>
		int inShutdown = -1;
		
		void Button2Click(object sender, EventArgs e)
		{
			bridge.Shutdown();
		}
	}
}
