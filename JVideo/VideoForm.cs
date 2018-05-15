/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/26
 * 时间: 23:05
 * 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NGO.Pad.JVideo
{
	/// <summary>
	/// http://open.iqiyi.com/lib/play.html
	/// Description of VideoForm.
	/// </summary>
	public partial class VideoForm : Form
	{
		public VideoForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//https://stackoverflow.com/questions/502199/how-to-open-a-web-page-from-my-application
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.Width = 940;
			this.Height = 760;
		}
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			//System.Diagnostics.Process.Start(@"C:\Users\Administrator\Desktop\share.html");
			//System.Diagnostics.Process.Start("iexplore.exe", "http://localhost:8080/share.html");
			
			this.webBrowser1.Navigate("http://192.168.0.5/share.html");
		}
		void Button1Click(object sender, EventArgs e)
		{
			//string ver = (new WebBrowser()).Version.ToString();
			//MessageBox.Show(ver);
			
			this.webBrowser1.Navigate("file:///D:/NGO/course/Demo/3dLable/miaov_demo.html");
		}
		void VideoFormSizeChanged(object sender, EventArgs e)
		{
			this.webBrowser1.Top = 30;
			this.webBrowser1.Left = 10;
			this.webBrowser1.Width = this.Width - 40;
			this.webBrowser1.Height = this.Height - 80;
		}
	}
}
