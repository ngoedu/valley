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
using App.Common;
using CefSharp.WinForms;
using CefSharp;

namespace App.Views
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
			//cefBrowser.RegisterJsObject("callbackObj", new CallbackObjectForJs(cefBrowser));
			//disabel context menu
			cefBrowser.MenuHandler = new MenuHandler();
			
			cefBrowser.Dock = DockStyle.Fill;
			this.Controls.Add(cefBrowser);
			
			var webRoot = CodeBase.GetCodePath();
			
			#if (DIA_DEBUG)
            webRoot =  @"D:/NGO/client/pad/src/valley";;
			#endif
			
			//D:\NGO\client\pad\demo\CefSharp\CefSharp-master\CefSharp.WinForms.Example.BrowserForm
			uiRoot = webRoot + @"/ui-html/ui.html";
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
	
}
