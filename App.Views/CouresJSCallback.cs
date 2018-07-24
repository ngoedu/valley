/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/24
 * Time: 15:13
 * 
 * 
 */
using System;
using System.Windows.Forms;
using CefSharp;

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
		
	    public void startDownload(string cid){
			internalBrowser.ExecuteScriptAsync("MAINUI.modalDialog();");
	        courseForm.DialogResult = DialogResult.OK;
	        courseForm.Close();
	    }
		
		public void startPlayCourse(string cid){
	        courseForm.DialogResult = DialogResult.OK;
	        courseForm.Close();
	    }
		
		public string getPreviewSrc(string cid){
			return "D:/neverstop/tutorial/webClient/test2.html";
	        
	    }
		
		public string getDownloaded() {
			return "cweb-A01";
		}
	}
}
