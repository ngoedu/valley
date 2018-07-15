/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/12
 * Time: 21:14
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace App.Forms
{
	/// <summary>
	/// Description of WebBrowser.
	/// </summary>
	public partial class JWebBrowser : UserControl, IWebBrowser
	{
		private ChromiumWebBrowser cefBrowser;
		
		public JWebBrowser(ChromiumWebBrowser cb)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.cefBrowser = cb;
			this.Controls.Add(cefBrowser);
		}

		#region IWebBrowser implementation

		public void LoadPage(string content)
		{
			CefSharp.WebBrowserExtensions.LoadHtml(cefBrowser, content,"http://localhost/test.html");
		}

		public void GoToUrl(string url)
		{
			cefBrowser.Load(url);	
		}
		
		void WebBrowserSizeChanged(object sender, EventArgs e)
		{
			this.txtAddress.Width = this.ClientSize.Width;
			this.txtAddress.Left = 0;
			this.txtAddress.Top = 0;
			
			this.cefBrowser.Left = 0;
			this.cefBrowser.Top = this.txtAddress.Height;
			this.cefBrowser.Width = this.ClientSize.Width;
			this.cefBrowser.Height = this.ClientSize.Height - this.txtAddress.Height;
		}
		void TxtAddressTextChanged(object sender, EventArgs e)
		{
			GoToUrl(this.txtAddress.Text);
		}

		#endregion
	}
}
