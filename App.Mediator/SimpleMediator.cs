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
using System.IO;
using System.Windows.Forms;
using App.Common;
using App.Common.Debug;
using App.Common.Proc;
using App.Common.Reg;
using App.Common.Tasks;
using App.Views;
using App.Views.Tile;
using CefSharp;
using Component.Bridge;
using Control.JBrowser;
using Control.Profile;
using NGO.Protocol.AEther;
using NGO.Train;


namespace App.Mediator
{
	/// <summary>
	/// Description of SimpleMediator.
	/// </summary>
	public class SimpleMediator : IMediator
	{
		private Form mainForm;
		private ITileManager tileManager;
		private Rectangle clientArea;
		private string codeBase;
		
		private List<App.Views.AppContext> appContexts = new List<App.Views.AppContext>();
		private AppRegistry appRegistry = new AppRegistry();
				
		private Profile jProfile;

		private AetherBridge aetherBridge;		
		private Endpoint aetherClient;
		
		public SimpleMediator(Form mf)
		{
			var settings = new CefSettings
            {
               	//By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
            	,Locale = "zh-CN"
            };
            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings,shutdownOnProcessExit:true, performDependencyCheck: true);
            
            App.Views.AppContext.AppContextsInitializer();
          
			
			//init depandencies
			this.clientArea = new Rectangle();
			this.mainForm = mf;
			this.tileManager = new SideBySideTileManager(mf);
			//this.tileManager = new SimpleTileManager(mf);
			this.codeBase = CodeBase.GetCodePath();

			//try clean all stale process. e.g. eide, bridge
			PidRecorder.Instance.CleanOldProcess();
			
			//startup the bridge first in sync mode - block current thread until done.
			aetherBridge = new AetherBridge(60001, this, PidRecorder.Instance, this.codeBase +@"\jre", this.codeBase+@"\aether\dist");
			aetherBridge.StartupSync();
			
			//startup aether client in sync mode - make sure it connect to bridge
			aetherClient = new Endpoint(this);
			aetherClient.ConnectSync("127.0.0.1", 60001);
			
			//add profile
			jProfile = new Profile();
			jProfile.Enabled = false;
			mainForm.Controls.Add(jProfile);	
		}
		
		private void LoadCoursePlayForm(string cid) {
			
			//1.load course content, prepare registry
			var courseName = cid;
			var cpath = CodeBase.GetCoursePackPath();
			var course = CourseReader.Instance.ReadCourseFrom(cpath, courseName, false);
			//var trSession = CourseReader.Instance.ReadTrainingSessionFrom(cpath, courseName, false);
			//TODO: setup training records in course
			
			course.RestoreRevFiles();
			
			appRegistry.Add(AppRegKeys.COURSE_KEY, course);
			
			var milestone = course.GetLatestMileStone();
			appRegistry.Add(AppRegKeys.VIDEO_LINK, course.GetVideoByID(milestone.LinkID).Content);
			appRegistry.Add(AppRegKeys.AETHER_CLIENT, aetherClient);
			appRegistry.Add(AppRegKeys.EIDE_WS, cpath+@"\"+courseName+@"\"+course.Schema.Workspace);
			appRegistry.Add(AppRegKeys.EIDE_RAW_WS, cpath+@"\rws");
			appRegistry.Add(AppRegKeys.EIDE_PROJ, cpath+@"\"+courseName+@"\"+course.Schema.ProjName);
			
			//2. prepare app controls
			appContexts = App.Views.AppContext.CreateAppContext(course.Apps);
			foreach(var app in appContexts) {
				app.AppControl.Init(appRegistry);		
			}
			Diagnostics.Debug("[app controls] initiated.");


			//3. build app tiles
	    	tileManager.BuildAppTiles(appContexts);
			Diagnostics.Debug("[app tiles] initiated.");

	    				
			//4.init profile
			jProfile.SetName("070718A001");
			jProfile.SetEnergy(85);//trSession.Point);
		}
		
		#region form event
		public void FormLoaded()
		{
			//launch Course Upgrade task
			UpgradeTask task = new UpgradeTask();
			task.LaunchTask();
			
			//TODO: uncoment below when go-prod
			if (true) {
				CourseForm form = new CourseForm();
				if (form.ShowDialog() == DialogResult.OK)
			    {
					var cid = (string)form.Tag;
					form.Close();
					Diagnostics.Debug(string.Format("course form closed with cid={0}", form.Tag.ToString()));
	
					//course selected
					LoadCoursePlayForm(cid);
			    }
			}
		}
		
		public void FormClosed()
		{
			//Hide all tiles
			tileManager.HideAppTiles(appContexts);
			
			foreach(var app in appContexts) {
				app.AppControl.Dispose(appRegistry);	
								
			}
			
			
			//disconnect endpoint
			aetherClient.Disconnect();
			
			//shutdown bridge
			aetherBridge.Shutdown();
			
			//cefSharp instances dispose explicitly				
			JWebBrowser.Dispose();
            
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
					
		}
		#endregion bridge callback

		#region aether endpoint callback	
		/// <summary>
		/// aether endpoint callback
		/// </summary>
		public void Connected()
		{

		}
		public void DataSent(string info)
		{
			
		}
		public void MessageReceived(string message)
		{
			
		}
		#endregion aether endpoint callback
	}
}
