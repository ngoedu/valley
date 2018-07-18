/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/2
 * Time: 21:28
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using App.Common;
using App.Common.Hook;
using App.Common.Proc;
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
	public class SimpleMediator : IMediator, ITileManager
	{
		private Dictionary<int, IAppTile> TILES = new Dictionary<int, IAppTile>();
		
		private Form mainForm;
		private string codeBase;
		
		private GifControl gif;
		
		private readonly ChromiumWebBrowser browser;
		private JToolbar jToolBar;
		private Profile jProfile;

		private CourseLib courseLib;
		private CoursePlay coursePlay;
		private JWebBrowser webBrowser;
		
		private AetherBridge bridge;
		private ManualResetEvent bridgeDone = new ManualResetEvent(false);
		
		private Endpoint aetherClient;
		private ManualResetEvent clientDone = new ManualResetEvent(false);
		
		public SimpleMediator(Form mf)
		{
			this.mainForm = mf;
			this.codeBase = CodeBase.GetCodePath();
			
			//hook keys
			AppTile tile1 = new AppTile("Guilder", 1, new Rectangle(50,130,1800,860), new Rectangle(100,250,300,260), this);
			HookKeyController.Instance.RegisterCallback(1, tile1);
			mainForm.Controls.Add(tile1);
			
			AppTile tile2 = new AppTile("Video", 2, new Rectangle(50,130,1800,860), new Rectangle(500,250,300,260), this);
			HookKeyController.Instance.RegisterCallback(2, tile2);
			mainForm.Controls.Add(tile2);
			
			TILES.Add(1, tile1);
			TILES.Add(2, tile2);
			
			
			//try clean all stale process. e.g. eide, bridge
			PidRecorder.Instance.CleanOldProcess();
			
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
			
			
		}
		
		#region form event
		public void FormLoaded()
		{
			jProfile.SetName("070718A001");
			jProfile.SetEnergy(85);
			
		}
		public void FormClosed()
		{
			
			//shutdown EIDE
			//aetherClient.SendData("$EXIT", 9);
			//clientDone.WaitOne();
			//System.Diagnostics.Debug.WriteLine("EIDE closed.");
			
			//disconnect endpoint
			aetherClient.Disconnect();
			
			//shutdown bridge
			bridge.Shutdown();
			
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
		}
		#endregion form events

		#region toolbar callback
		public void DisplayCourseLib()
		{

		}
		public void PlayCourseEntry()
		{

		}

		public void DisplayWebBrowser()
		{

		}

		#region ITileManager implementation
		public void ActiveTile(int index)
		{
			foreach(var tile in TILES)
			{	
				if (tile.Value.GetHotKeyId() != index) {
					tile.Value.Deactive();
					tile.Value.Minimized();
				}
				else {
					tile.Value.Active();
					tile.Value.Maxmized();
				}
					
			}
		}
		#endregion
		
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

	}
}
