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
using App.Common.Proc;
using CefSharp;
using App.Views;
using Component.Bridge;
using Control.Eide;
using Control.Profile;
using Control.Video;
using NGO.Pad.Guider;
using NGO.Protocol.AEther;

namespace App.Mediator
{
	/// <summary>
	/// Description of SimpleMediator.
	/// </summary>
	public class SimpleMediator : IMediator
	{
		private Dictionary<int, IAppTile> TILES = new Dictionary<int, IAppTile>();
		
		private Form mainForm;
		private Rectangle clientArea;
		private string codeBase;
		
		private GifControl gif;
		private Profile jProfile;

		private AetherBridge bridge;
		private ManualResetEvent bridgeDone = new ManualResetEvent(false);
		
		private Endpoint aetherClient;
		private ManualResetEvent clientDone = new ManualResetEvent(false);
		
		public SimpleMediator(Form mf)
		{
			//init depandencies
			this.clientArea = new Rectangle();
			this.mainForm = mf;
			this.codeBase = CodeBase.GetCodePath();

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

			
			//init required UI components
			//background gif
			//gif = new GifControl(this.codeBase + @"/res/anim-bg2.gif");
			//this.mainForm.Controls.Add(gif);

			//add profile
			jProfile = new Profile();
			jProfile.Enabled = false;
			mainForm.Controls.Add(jProfile);	
		}
		
		private void LoadCoursePlayForm(string cid) {
			//1.load course content
			var context = new List<App.Views.AppContext>();
			var app1 = new App.Views.AppContext("导航", 1, new JGuider());
			context.Add(app1);
			var app2 = new App.Views.AppContext("视频", 2, new JVideo());
			context.Add(app2);
			var app3 = new App.Views.AppContext("编码", 3, new JEide("NgoEclipse",  CodeBase.GetCodePath(), PidRecorder.Instance));
			context.Add(app3);

			//2. build app tiles
	    	SimpleTileManager.Instance.BuildAppTiles(this.mainForm,context);
	    	
	    				
			//4.init profile
			jProfile.SetName("070718A001");
			jProfile.SetEnergy(85);
		}
		
		#region form event
		public void FormLoaded()
		{
			CourseForm form = new CourseForm();
			if (form.ShowDialog() == DialogResult.OK)
		    {
				//course selected
				LoadCoursePlayForm(form.Tag.ToString());
		    }
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
			
			//cefSharp instances dispose explicitly				
			JWebBrowser.Dispose();
            Cef.Shutdown();
		}	
		
		public void FormResized(int newHeight, int newWidth)
		{
			int headHeight = 62;
			
			clientArea.Width = newWidth;
			clientArea.Height = newHeight;
			clientArea.X = 0; 
			clientArea.Y = 62;
			
			jProfile.Top = 0;
			jProfile.Left = 0;
			jProfile.Width = newWidth;
			jProfile.Height = headHeight;
		}
		#endregion form events

		
		
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
