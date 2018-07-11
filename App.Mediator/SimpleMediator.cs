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
using App.Common;
using App.Common.Impl;
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
		private string codeBase;
		
		private GifControl gif;
		
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
			this.codeBase = CodeBase.GetCodePath();
			
			//try clean all stale process. e.g. eide, bridge
			PidRecorder.Instance.CleanOldProcess();
			
			//add animation background gif
			gif = new GifControl(this.codeBase+@"\res\anim1.gif");
			this.mainForm.Controls.Add(gif);
			
			//startup the bridge first. block current thread until done.
			bridge = new AetherBridge(60001, this, PidRecorder.Instance, this.codeBase +@"\jre", this.codeBase+@"\aether\dist");
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

			//add profile
			jProfile = new Profile();
			jProfile.TabIndex = 0;
			mainForm.Controls.Add(jProfile);
			
			//add toolbar
			jToolBar = new JToolbar(this);
			jToolBar.TabIndex = 1;
			mainForm.Controls.Add(jToolBar);
			
			
			//init forms
			courseLib = new CourseLib(browser);
			mainForm.Controls.Add(courseLib);
			courseLib.Visible = false;
					
			coursePlay =  new CoursePlay(browser, this.codeBase);
			mainForm.Controls.Add(coursePlay);
			coursePlay.Visible = true;		
		}

		#region form event
		public void FormLoaded()
		{
			jProfile.SetName("070718A001");
			jProfile.SetEnergy(90);
			
			courseLib.InitCourseLib();
			courseLib.Visible = false;
			coursePlay.Visible = false;	
		}
		public void FormClosed()
		{
			//hide
			coursePlay.Visible = false;
			courseLib.Visible = false;
			gif.Visible = true;
			
			//shutdown EIDE
			aetherClient.SendData("$EXIT", 9);
			clientDone.WaitOne();
			System.Diagnostics.Debug.WriteLine("EIDE closed.");
			
			
			//disconnect endpoint
			aetherClient.Disconnect();
			
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
			coursePlay.Height = newHeight - jToolBar.Height -4;
			coursePlay.Top = 100;

			gif.Width = 277;
			gif.Height = 277;
			gif.Top = (newHeight - gif.Height) / 2;
			gif.Left = (newWidth  - gif.Width) / 2;
						
		}
		#endregion form events

		#region toolbar callback
		public void DisplayCourseLib()
		{
			coursePlay.Visible = false;
			courseLib.Visible = true;
			gif.Visible = false;
			courseLib.LoadCourseLib();
		}
		public void PlayCourseEntry()
		{
			coursePlay.Visible = true;
			courseLib.Visible = false;
			gif.Visible = false;
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
		
		//https://stackoverflow.com/questions/1199571/how-to-hide-file-in-c
		/*void loadVideo()
		{
			var html = @"<!DOCTYPE html>";
			CefSharp.WebBrowserExtensions.LoadHtml(browser,html,"http://localhost/test.html");
		}*/

	}
}
