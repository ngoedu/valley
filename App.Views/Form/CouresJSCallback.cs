/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/24
 * Time: 15:13
 * 
 * 
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
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
		private CourseForm courseForm; 
		private CefSharp.WinForms.ChromiumWebBrowser internalBrowser;
		public string CourseId {private set; get;}
		
		public CouresJSCallback(CourseForm form) {
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

		
		public void setSelectedCourseId(string cid) {
			CourseId = cid;
		}
		
		public void showCoursePreview(string cid) {
			setSelectedCourseId(cid);
			courseForm.showCoursePreview(cid);
		}
		
	}
}
