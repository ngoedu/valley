/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/24
 * Time: 15:13
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using App.Common;
using App.Common.Debug;
using CefSharp;
using Control.JBrowser;

namespace App.Views
{
	/// <summary>
	/// Description of CouresJSCallback.
	/// </summary>
	public class CouresJSCallback : IBrowserJSCallback
	{
		private Form courseForm; 
		private CefSharp.WinForms.ChromiumWebBrowser internalBrowser;
		public CouresJSCallback(Form form) {
			courseForm = form;
		}

		public void SetCefBrowser(CefSharp.WinForms.ChromiumWebBrowser cefBrowser)
		{
			internalBrowser = cefBrowser;
		}
		#region IBrowserJSCallback implementation
		public string GetJSCallbackName()
		{
			return "CourseJScallback";
		}
		#endregion
		
		private const string path = "/cpack/";
		private const string site = "http://192.168.0.12/scup";
		private bool isDownloadInProgress = false;
		private string zipFile = CodeBase.GetCoursePackPath();
		        
	    public void startDownload(string cid){
			
			if (isDownloadInProgress == true)
				return;
			
			using (WebClient wc = new WebClient())
		    {
		        wc.DownloadProgressChanged += wc_DownloadProgressChanged;
		        wc.DownloadFileCompleted += wc_DownloadFileCompleted;
		       string fileUrl  = "http://192.168.0.12/scup/cpack/"+cid+".zip?token=NGO";
		       
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
			isDownloadInProgress = true;
			
			// In case you don't have a progressBar Log the value instead 
		    // Console.WriteLine(e.ProgressPercentage);
		   int value  = e.ProgressPercentage;
		   internalBrowser.ExecuteScriptAsync("MAINUI.downloadStatusChanged("+value+");");
	       
		}
		
		private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
		   
		    if (e.Cancelled)
		    {
		        MessageBox.Show("The download has been cancelled");
		        return;
		    }
		
		    if (e.Error != null) // We have an error! Retry a few times, then abort.
		    {
		        MessageBox.Show("An error ocurred while trying to download file - "+e.Error.Message); 
		        return;
		    }
		    
		    string zipPath = zipFile;
      		string extractPath = CodeBase.GetCoursePath();

      		System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
		
		    isDownloadInProgress = false;
		    internalBrowser.ExecuteScriptAsync("MAINUI.downloadCompleted();");        
		}
			
		public void startPlayCourse(string cid){
	        courseForm.DialogResult = DialogResult.OK;
	        courseForm.Tag = cid;
	        courseForm.Close();
	    }
		
		public string getPreviewSrc(string cid){
			return "D:/neverstop/tutorial/webClient/test2.html";
	        
	    }
		
		public string getDownloadedList() {
			return "cweb-A01";
		}
	}
}
