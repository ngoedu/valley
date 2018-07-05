/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/2
 * Time: 21:28
 * 
 * 
 */
using System;
using System.Windows.Forms;
using System.Threading;
using App.Forms;
using CefSharp;
using CefSharp.WinForms;
using Component.Bridge;
using Control.Profile;
using Control.Toolbar;
using NGO.Protocol.AEther;

namespace App.Mediator
{
	/// <summary>
	/// Description of SimpleMediator.
	/// </summary>
	public class SimpleMediator : IMediator
	{
		private Form mainForm;
		private readonly ChromiumWebBrowser browser;
		private JToolbar jToolBar;
		private Profile jProfile;

		private CourseLib courseLib;
		private CoursePlay coursePlay;
		
		private AetherBridge bridge;
		private ManualResetEvent bridgeDone = new ManualResetEvent(false);
		
		private Endpoint aetherClient;
		private ManualResetEvent clientDone = new ManualResetEvent(false);
		
		public SimpleMediator(Form mf)
		{
			this.mainForm = mf;
			
			//startup the bridge first. block current thread until done.
			bridge = new AetherBridge(60001, this, @"D:\NGO\client\jre", @"D:\NGO\client\aether\dist");
			bridge.Startup();
			bridgeDone.WaitOne();
			System.Diagnostics.Debug.WriteLine("bridge initialized.");
			
			//startup aether client. make sure it connect to bridge
			aetherClient = new Endpoint(this);
			aetherClient.Connect("127.0.0.1", 60001);
			clientDone.WaitOne();
			System.Diagnostics.Debug.WriteLine("aether client initialized.");
			
			//the singleton instance of the CefSharp
			browser = new ChromiumWebBrowser("");

			jToolBar = new JToolbar(this);
			mainForm.Controls.Add(jToolBar);
			
			jProfile = new Profile();
			jProfile.SetName("N062018A001");
			jProfile.SetEnergy(90);
			mainForm.Controls.Add(jProfile);
			
			courseLib = new CourseLib(browser);
			mainForm.Controls.Add(courseLib);
			courseLib.Visible = false;
					
			coursePlay =  new CoursePlay(browser);
			mainForm.Controls.Add(coursePlay);
			coursePlay.Visible = true;
			
			
		}

		#region form event
		public void FormLoaded()
		{
			courseLib.ShowCourseLib();
		}
		public void FormClosed()
		{
			//shutdown EIDE
			aetherClient.SendData("$EXIT", 9);
			clientDone.WaitOne();
			System.Diagnostics.Debug.WriteLine("EIDE closed.");
			
			//shutdown bridge
			bridge.Shutdown();

			//form dispose
			if (coursePlay != null)
				coursePlay.Dispose();
			if (courseLib != null)
				/*courseLib.Dispose();*/	//this may break the app when exit, why?
			
			//cefSharp dispose				
			browser.Dispose();
            Cef.Shutdown();
		}	
		public void FormResized(int newHeight, int newWidth)
		{
			jProfile.Top = 0;
			jProfile.Left = 0;
			jProfile.Width = 300;
			jProfile.Height = 100;
			
			jToolBar.Top = 0;
			jToolBar.Left = 300;
			jToolBar.Width = newWidth - 300;
			jToolBar.Height = 100;
			
			courseLib.Width = newWidth;
			courseLib.Height = newHeight - jToolBar.Height - 4;
			courseLib.Top = 100;
			
			coursePlay.Width = newWidth;
			coursePlay.Height = newHeight - jToolBar.Height - 4;
			coursePlay.Top = 100;	
		}
		#endregion form events

		#region toolbar callback
		public void DisplayCourseLib()
		{
			coursePlay.Visible = false;
			courseLib.Visible = true;
		}
		public void PlayCourseEntry()
		{
			coursePlay.Visible = true;
			courseLib.Visible = false;
		}
		#endregion toolbar callback
		
		#region bridge callback
		/// <summary>
		/// bridge callback
		/// </summary>
		/// <param name="output"></param>
		public void OutputArrived(string output)
		{
			System.Diagnostics.Debug.WriteLine(output);
			if (output !=null && output.Contains("[aether bridge v1.1] launched")) {
				bridgeDone.Set();
				bridgeDone.Reset();
			}			
		}
		#endregion bridge callback

		#region aether endpoint callback	
		/// <summary>
		/// aether endpoint callback
		/// </summary>
		public void Connected()
		{
			clientDone.Set();
			clientDone.Reset();
		}
		public void DataSent(string info)
		{
			
		}
		public void MessageReceived(string message)
		{
			if (message.Equals("<EIDE status='closed'/>"))
				clientDone.Set();
		}
		#endregion aether endpoint callback
		
		
		/*void loadVideo()
		{
			var html = @"<!DOCTYPE html>";
			CefSharp.WebBrowserExtensions.LoadHtml(browser,html,"http://localhost/test.html");
		}*/

	}
}
