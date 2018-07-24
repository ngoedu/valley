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
using CefSharp;
using CefSharp.WinForms;

namespace App.Views
{
	/// <summary>
	/// Description of WebBrowser.
	/// </summary>
	public partial class JWebBrowser : UserControl, IWebBrowser
	{
		private static ChromiumWebBrowser cefBrowser = new ChromiumWebBrowser("");
		private static string CEF_ACTIVE = "CEF_ACTIVE";
		private static string CEF_DEACTIVE = "CEF_DEACTIVE";
		private bool isNav = false;
		
		public JWebBrowser(bool isNavBar, IBrowserJSCallback callback)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			if (cefBrowser.Parent != null) {
				cefBrowser.Parent.Controls.Remove(cefBrowser);
				cefBrowser.Parent.Tag = CEF_DEACTIVE;
			}
			
			this.Controls.Add(cefBrowser);
			cefBrowser.RegisterJsObject(callback.GetJSCallbackName(), callback);
			isNav = isNavBar;
			if (!isNav) {
				cefBrowser.Dock = DockStyle.Fill;
				this.txtAddress.Visible = false;
			} else {
				this.txtAddress.Visible = true;
			}
			Tag = CEF_ACTIVE;
		}

		#region IWebBrowser implementation

		public void LoadPage(string content)
		{
			if (Tag == CEF_ACTIVE)
				CefSharp.WebBrowserExtensions.LoadHtml(cefBrowser, content,"http://localhost/test.html");
			else
				throw new InvalidOperationException("cef is not active");
		}

		public void GoToUrl(string url)
		{
			if (Tag == CEF_ACTIVE)
				cefBrowser.Load(url);	
			else
				throw new InvalidOperationException("cef is not active");
		}
		
		void WebBrowserSizeChanged(object sender, EventArgs e)
		{
			if (Tag == CEF_ACTIVE) {
				
				if (isNav)
				{
					this.txtAddress.Width = this.ClientSize.Width;
					this.txtAddress.Left = 0;
					this.txtAddress.Top = 0;
					
					cefBrowser.Left = 0;
					cefBrowser.Top = this.txtAddress.Height;
					cefBrowser.Width = this.ClientSize.Width;
					cefBrowser.Height = this.ClientSize.Height - this.txtAddress.Height;
				}
			}
		}
		void TxtAddressTextChanged(object sender, EventArgs e)
		{
			if (Tag == CEF_ACTIVE) {
				GoToUrl(this.txtAddress.Text);
			}
		}

		#endregion
		
		public static void Dispose()
		{
			cefBrowser.Dispose();
		}
	}
	
	/// <summary>
	/// this handler is for disabling context menu
	/// </summary>
	internal class MenuHandler : IContextMenuHandler
	   {
		#region IContextMenuHandler implementation
		public void OnBeforeContextMenu(CefSharp.IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
		{
			
		}
	
		public bool OnContextMenuCommand(CefSharp.IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
		{
			return true;  
		}
	
		public void OnContextMenuDismissed(CefSharp.IWebBrowser browserControl, IBrowser browser, IFrame frame)
		{
			
		}
	
		public bool RunContextMenu(CefSharp.IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
		{
			return true;  
		}
		#endregion
    }
}
