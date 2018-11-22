/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/20
 * Time: 0:52
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using App.Common.Reg;

namespace Control.Video
{
	/// <summary>
	/// Description of JVideo.
	/// </summary>
	public partial class JVideo : UserControl, IAppEntry
	{
		/// <summary>
		/// -1: default
		/// 1: inited
		/// 0: disposed
		/// </summary>
		private AppStatus status;
		public JVideo()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.webBrowser1.AllowNavigation = true;
		}

		#region IAppEntry implementation
		public void Init(AppRegistry reg)
		{
			string html = (string)reg[AppRegKeys.VIDEO_LINK];
			LoadHtml(html);
			this.status = AppStatus.Inited;
		}

		public void Active()
		{
			
		}
		
		public void Inactive()
		{
			
		}
		
		public void Reload(AppRegistry reg)
		{
			this.Dispose(reg);
			if (this.status == AppStatus.Disposed) {
				this.Init(reg);
			}
		}
		
		public void Dispose(AppRegistry reg)
		{
			this.status = AppStatus.Disposed;
		}
		
		public AppStatus Status () {
			return status;
		}
		
		#endregion		
		public void NavigatgeTo(string Url) {
			this.webBrowser1.DocumentText="";
			this.webBrowser1.Navigate(Url);//"http://localhost/s1web/iqiyi.html");
		}
		
		public void LoadHtml(string html) {		
			webBrowser1.DocumentText="";
			webBrowser1.Document.OpenNew(true);
			webBrowser1.Document.Write(html);
			webBrowser1.Refresh();
		}
	}
}
