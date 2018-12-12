/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/24
 * Time: 14:48
 * 
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using App.Common;
using App.Common.Dpi;
using Control.JBrowser;
using NGO.Train;
using log4net;
using App.Views;

namespace App.Views
{
	/// <summary>
	/// Description of CourseForm.
	/// </summary>
	public partial class CourseForm : Form
	{
		private JWebBrowser browser;
		private CouresJSCallback jsCallback;
		private bool isButtonTriggered = false;
		
		private const string path = "/cpack/";
		private const string site = "http://192.168.0.12/scup";
		private bool isDownloadInProgress = false;
		private string zipFile = CodeBase.GetCoursePackPath();
		private string downloadedCid;
		
		private string uid;
		
		private static readonly ILog logger = LogManager.GetLogger(typeof(CourseForm));  


		public CourseForm(string uid)
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.uid = uid;
			
			this.panelPreview.Visible = false;
			
			jsCallback = new CouresJSCallback(this);
			browser = new JWebBrowser(false, jsCallback);
 			browser.Dock = DockStyle.Fill;
 			this.Controls.Add(browser);
 			
 			double scale = DpiUtil.GetScale(this.CreateGraphics());
 			var newSize = new Size((int)(1200*scale), (int)(460*scale));
			this.ClientSize = newSize;
			
			var path = CodeBase.GetCodePath();
			#if (DIA_DEBUG)
			path =  @"C:/NGO/client/pad/src/valley";
			if (!Directory.Exists(path+@"\wui"))
            	path =  @"D:/NGO/client/pad/src/valley";
			#endif
			browser.GoToUrl(path +@"/wui/ui.html");
		}
		
		
		void CourseFormSizeChanged(object sender, EventArgs e)
		{
			
		}
		
		
		public void showCoursePreview(string cid) {
			this.Invoke((MethodInvoker)delegate() {
		       	this.rtbMetaInfo.Text = "Meta info for "+cid;
		       	var downloaded = this.GetDownloadedList();
		       	if (downloaded.Contains(cid)) {
		       		this.btnDownload.Visible = false;
		       		this.btnStart.Visible = true;
		       		this.pbDownload.Visible = false;
		       	} else {
		       		this.btnDownload.Visible = true;
		       		this.btnStart.Visible = false;
		       		this.pbDownload.Visible = true;
		       		this.pbDownload.Value = 0;
			
		       	}
		       	this.lvHistory.Visible = false;
		       	this.panelPreview.Visible = true;
				this.pbImage.Image = global::App.Views.Resource1.webdesign;
							
				this.browser.Visible = false;
		    });
			
		}
		
		private string FindCourseName(string cid, string courseJson) {
			DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<CourseEntry>));
			MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(courseJson));
			List<CourseEntry> allCourses = (List<CourseEntry>)js.ReadObject(stream);
			
			foreach(var ce in allCourses) {
				if (ce.cid == cid)
					return ce.name;
			}
			
			return string.Empty;
		}

		public void showTrainingHistory(string coursesJson)
		{
			
			this.Invoke((MethodInvoker)delegate() {
			   
			   
				this.lvHistory.Clear(); 
				ColumnHeader  ch1= new ColumnHeader();
				ch1.Text = "课程代码";
				ch1.Width = 190;
				ch1.TextAlign = HorizontalAlignment.Center;
				ColumnHeader ch2 = new ColumnHeader();
				ch2.Text = "课程名称";
				ch2.Width = 280;
				ch2.TextAlign = HorizontalAlignment.Center;
				this.lvHistory.Columns.Add(ch1);
				this.lvHistory.Columns.Add(ch2);
				
			   
				var history = this.GetTrainHistoryList();
				foreach(string h in history) {
					ListViewItem lvi = new ListViewItem();
					lvi.Text = h;
					lvi.SubItems.Add(FindCourseName(h, coursesJson));
					this.lvHistory.Items.Add(lvi);
				}
				 
			    
				this.btnDownload.Visible = false;
	       		this.btnStart.Visible = true;
	       		this.pbDownload.Visible = false;
				this.lvHistory.Visible = true;
				this.panelPreview.Visible = true;
				this.pbImage.Image = global::App.Views.Resource1.train_history;
				this.browser.Visible = false;
			});
		}
		
		
		public void NavigateBackToCourseLib() {
			this.panelPreview.Visible = false;
			this.jsCallback.setSelectedCourseId(string.Empty);
			this.browser.Visible = true;
		}
		
		public IList GetDownloadedList() {
			string extractPath = CodeBase.GetCoursePackPath();
			var list = new ArrayList();
			foreach (var d in System.IO.Directory.GetDirectories(extractPath)) {
			    var dirName = new DirectoryInfo(d).Name;
			    list.Add(dirName);
			 }
			return list;
		}
		
		public IList GetTrainHistoryList() {
			string extractPath = CodeBase.GetCoursePackPath();
			var list = new ArrayList();
			foreach (var d in System.IO.Directory.GetDirectories(extractPath)) {
			    var dirName = new DirectoryInfo(d).Name;
			    var tr = new DirectoryInfo(d).FullName + "/tr.dat";
			    if (new FileInfo(tr).Exists) {
			    	list.Add(dirName);
			    }
			 }
			return list;
		}
		
		void BtnGoBackClick(object sender, EventArgs e)
		{
			NavigateBackToCourseLib();
		}
		
		
		void BtnStartClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(jsCallback.CourseId))
			{
				MessageBox.Show("请先选择课程！","提示", MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
				return;
			}
			
			if (!isButtonTriggered) {
				isButtonTriggered = true;
				this.btnGoBack.Enabled = false;
				this.btnStart.Enabled = false;
	       	 	this.DialogResult = DialogResult.OK;
	        	this.Tag = jsCallback.CourseId;
	        	this.browser.Dispose();
			}
		}
		
		
		        
	    public void StartDownload(string cid){
			
			if (isDownloadInProgress == true)
				return;
			
			isDownloadInProgress = true;
			
			//Directory.Delete(zipFile + "\\"+cid, true);
			
			using (WebClient wc = new WebClient())
		    {
				wc.Headers.Add ("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
		       
				wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
       			wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);


				//wc.DownloadProgressChanged += wc_DownloadProgressChanged;
		        //wc.DownloadFileCompleted += wc_DownloadFileCompleted;
		       	string fileUrl  = "http://192.168.0.13/scup/cpack/"+cid+".zip";
		       	wc.QueryString.Add("token", "NGO");

		       	downloadedCid = cid;
		       	logger.Info(string.Format("url={0} download to {1}", fileUrl, zipFile ));
		        wc.DownloadFileAsync(new System.Uri(fileUrl), zipFile +"\\"+cid +".zip");
		    }
	    }
		
		private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			
			// In case you don't have a progressBar Log the value instead 
		    // Console.WriteLine(e.ProgressPercentage);
		   	int value  = e.ProgressPercentage;
		   	this.pbDownload.Value = value;
		}
		
		private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{		   
		    if (e.Cancelled)   {
		        MessageBox.Show("The download has been cancelled");
		        return;
		    }
		
		    if (e.Error != null) // We have an error! Retry a few times, then abort.
		    {
		    	logger.Error("download error - "+e.Error.StackTrace);
		    	MessageBox.Show("下载文件错误 - "+e.Error.InnerException);
		    	this.btnGoBack.Enabled = true;
		        return;
		    }
		    
		    //unzip file to folder
		    string zipFilePath = zipFile+ "//"+downloadedCid + ".zip";
      		string extractPath = CodeBase.GetCoursePackPath();
      		System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, extractPath);
		
      		isDownloadInProgress = false;
      		this.btnGoBack.Enabled = true;
      		
      		//delete zip file
      		File.Delete(zipFilePath);
      		
      		//generate TR file 
      		TrainingSession.InitSessionWithSecurity(this.downloadedCid, this.uid,  100, zipFilePath.Replace(@".zip",""));
      		
      		//show 
      		showCoursePreview(jsCallback.CourseId);
		}
		
		void BtnDownloadClick(object sender, EventArgs e)
		{
			this.btnGoBack.Enabled = false;
			StartDownload(jsCallback.CourseId);
		}
		
		void LvHistorySelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvHistory.SelectedItems.Count == 0)
				return;
			
			var cid = lvHistory.SelectedItems[0].Text;
			//System.Diagnostics.Debug.WriteLine("history selected course id="+cid);
			this.jsCallback.setSelectedCourseId(cid);
		}
		
	}
	
	/**
	 *
* "mid" : "cweb",
* "cid":"sweb-a01-proj1", 
* "name" : "web客户端基础",
* "target":"中级",
* "duration":"18分钟", 
* "content":"HTML,css3,javascript构造页面",
* "type":"免费"
	 */
	[DataContract]
	class CourseEntry
	{
		[DataMember]
		public string mid;
		[DataMember]
		public string cid;
		[DataMember]
		public string name;
		[DataMember]
		public string target;
		[DataMember]
		public string duration;
		[DataMember]
		public string content;
		[DataMember]
		public string type;
	}
}
