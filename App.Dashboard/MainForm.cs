/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/8
 * Time: 14:01
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Component.TinyServer;
using Control.Profile;
using Control.Toolbar;

namespace CefSharp49NuGet
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		private readonly ChromiumWebBrowser browser;
		private JToolbar jToolBar;
		private Profile jProfile;
		
		private SimpleHTTPServer jTinyserver;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			var webRoot = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			//var webRoot = @"D:/NGO/client/pad/src/valley/ui-html";
			//jTinyserver = new SimpleHTTPServer(webRoot,60002);
			//jTinyserver.Startup();
			
			
			jToolBar = new JToolbar();
			this.Controls.Add(jToolBar);
			
			jProfile = new Profile();
			jProfile.SetName("N062018A001");
			jProfile.SetEnergy(90);
			this.Controls.Add(jProfile);
			
			// sample of how to embed devtool into a  panel
			//D:\NGO\client\pad\demo\CefSharp\CefSharp-master\CefSharp.WinForms.Example.BrowserForm
			var uiRoot = webRoot.Replace(@"\App.Dashboard\bin\Debug","") + @"/ui-html/ui.html";
			browser = new ChromiumWebBrowser("file:///"+ uiRoot);//http://localhost:60002/select.html");
			browser.Dock = DockStyle.None;
			browser.MenuHandler = new MenuHandler();
			browser.RegisterJsObject("callbackObj", new CallbackObjectForJs(browser));
			this.Controls.Add(browser);
			//loadVideo();
		}
		
		

		void MainFormResize(object sender, EventArgs e)
		{
			jProfile.Top = 0;
			jProfile.Left = 0;
			jProfile.Width = 300;
			jProfile.Height = 100;
			
			jToolBar.Top = 0;
			jToolBar.Left = 300;
			jToolBar.Width = this.ClientSize.Width - 300;
			jToolBar.Height = 100;
			
			browser.Width = this.Width;
			browser.Height = this.ClientSize.Height - jToolBar.Height - 4;
			browser.Top = 100;
		}
		
		void loadVideo()
		{
			var html = @"<!DOCTYPE html>
<html>
<head>
	<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
	<meta http-equiv='X-UA-Compatible' content='IE=Edge' />
	<meta http-equiv='Content-Language' content='zh-CN'/>
	<style type='text/css'>
	  div { height:600px; width:800px; }
	</style>
	<script>   
	</script>
</head>
<body>
	<div> Hi, there, this is a inner html page.
	
		<iframe src='http://open.iqiyi.com/developer/player_js/coopPlayerIndex.html?vid=d52c9431203048a4986bba373d391525&tvId=1043319200&accessToken=2.f22860a2479ad60d8da7697274de9346&appKey=3955c3425820435e86d0f4cdfe56f5e7&appId=1368&height=100%&width=100%' frameborder='0' allowfullscreen='true' width='100%' height='100%'></iframe>
		
</div>
</body>
</html>";
			CefSharp.WebBrowserExtensions.LoadHtml(browser,html,"http://localhost/test.html");
		}
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			browser.Dispose();
            Cef.Shutdown();
            //Application.Exit();
            Environment.Exit(0); //this works like a charm
		}
	}
	
	public class CallbackObjectForJs{
		ChromiumWebBrowser browser;
		public CallbackObjectForJs(ChromiumWebBrowser b) {
			this.browser = b;
		}
	    public void startDownload(string cid){
	        MessageBox.Show("start download "+cid);
	        //browser.ExecuteScriptAsync("alert('["+cid+"] downloaded, please refresh ui');");
	    }
		public void startPreview(string cid){
	        MessageBox.Show("start preview "+cid);
	        //browser.ExecuteScriptAsync("alert('["+cid+"] downloaded, please refresh ui');");
	    }
		
		public string getDownloaded() {
			return "cweb-A01";
		}
	}
	
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
