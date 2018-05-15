/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/30
 * 时间: 23:14
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

namespace NGO.Pad.Catalina
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			backgroundWorker = new BackgroundWorker(); // 实例化后台对象
 
            backgroundWorker.WorkerReportsProgress = true; // 设置可以通告进度
            backgroundWorker.WorkerSupportsCancellation = true; // 设置可以取消
 
            backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(UpdateProgress);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompletedWork);

            backgroundWorker.RunWorkerAsync(this);
		}
		
		
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
						System.Diagnostics.Debug.WriteLine("Tomcat shutted down!");
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
			if (inShutdown == 0 || pid != -1)
				return;
			//* Create your Process
		    Process process = new Process();
		    process.StartInfo.FileName = @"D:\NGO\client\embed\embed.bat";
		    process.StartInfo.UseShellExecute = false;
		    process.StartInfo.RedirectStandardOutput = true;
		    process.StartInfo.RedirectStandardError = true;
		    process.StartInfo.CreateNoWindow = true;
		    //* Set your output and error (asynchronous) handlers
		    process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
		    process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
		    //* Start process and handlers
		    
		    // try start process in a thread.
		    ThreadStart ths = new ThreadStart(
		    	delegate() { process.Start(); pid = process.Id;
		    						process.BeginOutputReadLine();
		    						process.BeginErrorReadLine();
		    						process.WaitForExit();}
		    					);
		    Thread th = new Thread(ths);
    		th.Start();

		}
		
		private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
		    //* Do your stuff with the output (write to console/log/StringBuilder)
		    //System.Diagnostics.Debug.WriteLine(outLine.Data);
		    queue.Add(outLine.Data);
		}
		/// <summary>
		/// initial status = -1
		/// in shutdown process = 0
		/// already shutted down = 1
		/// </summary>
		int inShutdown = -1;
		
		void Button2Click(object sender, EventArgs e)
		{
			if (inShutdown == -1 || inShutdown==1) {
				inShutdown = 0;
				Server.Instance.Shutdown();
			}
		}
	}
}
