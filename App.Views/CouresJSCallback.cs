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

namespace App.Views
{
	/// <summary>
	/// Description of CouresJSCallback.
	/// </summary>
	public class CouresJSCallback : IBrowserJSCallback
	{
		private Form courseForm; 
		public CouresJSCallback(Form form) {
			courseForm = form;
		}
		
		#region IBrowserJSCallback implementation
		public string GetJSCallbackName()
		{
			return "callbackObj";
		}
		#endregion
	    public void startDownload(string cid){
	        //MessageBox.Show("start download "+cid);
	        //browser.ExecuteScriptAsync("alert('["+cid+"] downloaded, please refresh ui');");
	        courseForm.DialogResult = DialogResult.OK;
	        courseForm.Close();
	    }
		public string getPreviewSrc(string cid){
			return "D:/neverstop/tutorial/webClient/test2.html";
	        //browser.ExecuteScriptAsync("alert('["+cid+"] downloaded, please refresh ui');");
	    }
		
		public string getDownloaded() {
			return "cweb-A01";
		}
	}
}
