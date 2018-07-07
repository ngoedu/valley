/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/28
 * Time: 21:08
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;

namespace App.Forms
{
	/// <summary>
	/// Description of CourseLib.
	/// </summary>
	public partial class CourseLib : UserControl,ICourseLib
	{
		private ChromiumWebBrowser cefBrowser;
		private string uiRoot;
		
		public CourseLib(ChromiumWebBrowser browser)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			cefBrowser = browser;
			//only do RegisterJSObj before Dock can works
			cefBrowser.RegisterJsObject("callbackObj", new CallbackObjectForJs(cefBrowser));
			//disabel context menu
			cefBrowser.MenuHandler = new MenuHandler();
			
			cefBrowser.Dock = DockStyle.Fill;
			this.Controls.Add(cefBrowser);
			
			var webRoot = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			//var webRoot = @"D:/NGO/client/pad/src/valley/ui-html";
	
			//D:\NGO\client\pad\demo\CefSharp\CefSharp-master\CefSharp.WinForms.Example.BrowserForm
			uiRoot = webRoot.Replace(@"\App.Dashboard\bin\Debug","") + @"/ui-html/ui.html";
		}

		public void InitCourseLib()
		{
			//prepare course lib data here
			
			cefBrowser.Load("file:///"+uiRoot);	
		}
		
		public void LoadCourseLib()
		{		
			cefBrowser.Load("file:///"+uiRoot);	
		}
		
		void CourseLibSizeChanged(object sender, EventArgs e)
		{
			cefBrowser.Top = 0;
			cefBrowser.Left = 0;
			cefBrowser.Width = this.ClientSize.Width;
			cefBrowser.Height = this.ClientSize.Height;
		}
	}
	
	/// <summary>
	/// This object interact with JS code
	/// </summary>
	public class CallbackObjectForJs{
		ChromiumWebBrowser browser;
		public CallbackObjectForJs(ChromiumWebBrowser b) {
			this.browser = b;
		}
	    public void startDownload(string cid){
	        MessageBox.Show("start download "+cid);
	        //browser.ExecuteScriptAsync("alert('["+cid+"] downloaded, please refresh ui');");
	    }
		public string getPreviewSrc(string cid){
			return "D:/neverstop/tutorial/webClient/test2.html";
	        //browser.ExecuteScriptAsync("alert('["+cid+"] downloaded, please refresh ui');");
	    }
		
		public string getDownloaded() {
			return "cweb-A01";
		}
	}
	
	/// <summary>
	/// this handler is for disabling context menu
	/// </summary>
	internal class MenuHandler : IContextMenuHandler
	   {
		#region IContextMenuHandler implementation

		public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
		{
			;
		}
	
		public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
		{
			return true;  
		}
	
		public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
		{
			;
		}
	
		public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
		{
			return true;
		}
	
		#endregion
    }
}
