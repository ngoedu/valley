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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using App.Common;
using App.Common.Dpi;
using Control.JBrowser;
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

		
		public CourseForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
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
		       	}
		       	this.panelPreview.Visible = true;
							
				this.browser.Visible = false;
		    });
			
		}
		
		public void NavigateBackToCourseLib() {
			this.panelPreview.Visible = false;
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
		
		void BtnGoBackClick(object sender, EventArgs e)
		{
			NavigateBackToCourseLib();
		}
		
		
		void BtnStartClick(object sender, EventArgs e)
		{
			if (!isButtonTriggered) {
				isButtonTriggered = true;
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
		        wc.DownloadProgressChanged += wc_DownloadProgressChanged;
		        wc.DownloadFileCompleted += wc_DownloadFileCompleted;
		       string fileUrl  = "http://192.168.0.12/scup/cpack/"+cid+".zip";
		       
		       zipFile += "\\"+cid +".zip";
				
		        try {
					wc.DownloadFileAsync(new System.Uri(fileUrl), zipFile);
				} catch (WebException webex) {
					HttpWebResponse webResp = (HttpWebResponse) webex.Response;
					MessageBox.Show(webResp.ToString());
		        }
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
		        MessageBox.Show("An error ocurred while trying to download file - "+e.Error.Message); 
		        return;
		    }
		    
		    string zipPath = zipFile;
      		string extractPath = CodeBase.GetCoursePackPath();

      		System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
		
      		isDownloadInProgress = false;
      		MessageBox.Show("课程下载完成,开始按钮播放该课程","提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
      		
      		
      		showCoursePreview(jsCallback.CourseId);
		}
		
		void BtnDownloadClick(object sender, EventArgs e)
		{
			StartDownload(jsCallback.CourseId);
		}
	}
}
