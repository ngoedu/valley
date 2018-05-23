﻿/*
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

namespace Control.Video
{
	/// <summary>
	/// Description of JVideo.
	/// </summary>
	public partial class JVideo : UserControl
	{
		public JVideo()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.webBrowser1.AllowNavigation = true;
		}
		
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
