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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using App.Common.Reg;
using App.Common.Signal;
using CefSharp;
using CefSharp.WinForms;

namespace  Control.JBrowser
{
	/// <summary>
	/// Description of WebBrowser.
	/// </summary>
	public partial class JWebBrowser : UserControl, IWebBrowser, IAppEntry
	{
		private ChromiumWebBrowser cefBrowser ;
		private static string CEF_ACTIVE = "CEF_ACTIVE";
		private static string CEF_DEACTIVE = "CEF_DEACTIVE";
		private bool isNav = false;
		private AppStatus status;
		
		public JWebBrowser(bool isNavBar, IBrowserJSCallback callback)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			cefBrowser = new ChromiumWebBrowser("about:blank");
			cefBrowser.FrameLoadStart += (s, argsi) =>
	            {
	                var b = (ChromiumWebBrowser)s;
	                if (argsi.Frame.IsMain)
	                {
	                    
	                }
	                //TODO: add callback invoke here.
	                
	                //b.ExecuteScriptAsync("alert('css inject.');var node = document.createElement('style'); node.innerHTML = 'body {font-size: 22px !important; font-family: Consolas, Lucida Console, monospace;}'; document.body.appendChild(node);");
			};
				
			if (cefBrowser.Parent != null) {
				cefBrowser.Parent.Tag = CEF_DEACTIVE;
				cefBrowser.Parent.Controls.Remove(cefBrowser);
			}
			this.Controls.Add(cefBrowser);
            
			if (callback != null) {
				cefBrowser.RegisterJsObject(callback.GetJSCallbackName(), callback);
				callback.SetCefBrowser(cefBrowser);
			}

			isNav = isNavBar;
			if (!isNav) {
				cefBrowser.Dock = DockStyle.Fill;
				this.txtAddress.Visible = false;
			} else {
				this.txtAddress.Visible = true;
			}
			Tag = CEF_ACTIVE;
		}
		
		
		public string ShowDevTools(Panel panel) {
			/**/var windowInfo = new WindowInfo();
			var browser = cefBrowser.GetBrowser().GetHost();
			var rect = panel.ClientRectangle;
			windowInfo.SetAsChild(panel.Handle,rect.Left, rect.Top, rect.Right, rect.Bottom);
			browser.ShowDevTools(windowInfo);
			
			//cefBrowser.ShowDevTools();
			return "chrome-devtools://devtools/inspector.html";
		}

		public AppStatus Status()
		{
			return this.status;
		}
		#region IAppEntry implementation
		public void Init(AppRegistry reg)
		{
			this.status = AppStatus.Inited;
		}
		
		public void Reload(AppRegistry reg)
		{
			throw new NotImplementedException();
		}
		
		public void Dispose(AppRegistry reg)
		{
			//do nothing, as the static Dispose will handle the real dispostion of CEF instalces.
			this.status = 0;
		}
		#endregion
		#region IWebBrowser implementation

		public void LoadPage(string content)
		{
			if (Tag == CEF_ACTIVE) {
				//CefSharp.WebBrowserExtensions.LoadHtml(cefBrowser, content,"http://localhost/test.html");
				cefBrowser.LoadHtml(content, "about:blank");
			}			
			else
				throw new InvalidOperationException("cef is not active");
		}

		public void GoToUrl(string url)
		{
			if (Tag == CEF_ACTIVE) {
				cefBrowser.Load(url);	
			}	
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

				} else {
					
					
				}
			}
		}
		void TxtAddressTextChanged(object sender, EventArgs e)
		{
			if (Tag == CEF_ACTIVE) {
				GoToUrl(this.txtAddress.Text);
			}
		}
		
		public void Dispose() {
			cefBrowser.Dispose();
		}

		#endregion
		
		public static void CefDispose()
		{
			//cefBrowser.Dispose();
			System.Diagnostics.Debug.WriteLine("[JwebBrowser] ChromiumWebBrowser disposed.");
			CefSharp.Cef.Shutdown();
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
