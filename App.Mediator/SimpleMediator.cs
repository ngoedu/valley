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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using App.Common;
using App.Common.Debug;
using App.Common.Net;
using App.Common.Proc;
using App.Common.Reg;
using App.Common.Tasks;
using App.Views;
using App.Views.Tile;
using CefSharp;
using Component.Bridge;
using Component.Catalina;
using Control.Eide;
using Control.JBrowser;
using Control.Profile;
using Control.Toolbar;
using NGO.Protocol.AEther;
using NGO.Train;
using NGO.Train.Entity;
using log4net;


namespace App.Mediator
{
	/// <summary>
	/// Description of SimpleMediator.
	/// </summary>
	public class SimpleMediator : IMediator, IToolBarCallback
	{
		private Form mainForm;
		private ITileManager tileManager;
		private Rectangle clientArea;
		private string codeBase;
		
		private List<App.Views.AppContext> appContexts = new List<App.Views.AppContext>();
		private AppRegistry appRegistry = new AppRegistry();
				
		private Profile jProfile;
		private JToolbar jToolbar;

		

		private AetherBridge aetherBridge;		
		private Endpoint aetherClient;
		
		private CatalinaServer catalina;
		
		private static readonly ILog logger = LogManager.GetLogger(typeof(SimpleMediator));  

		public SimpleMediator(Form mf)
		{
			logger.Debug("SimpleMediator cotr");
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

			//add toolbar
			jToolbar = new JToolbar(this);
			mainForm.Controls.Add(jToolbar);

		}
		
		private void LoadCoursePlayForm(string cid) {
			logger.Debug("LoadCoursePlayForm cid="+cid);
			
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
			appRegistry.Add(AppRegKeys.EIDE_CDAT, cpath);
			appRegistry.Add(AppRegKeys.EIDE_PROJ, cpath+@"\"+courseName+@"\"+course.Schema.ProjName);
			
			//2. prepare app controls
			appContexts = App.Views.AppContext.CreateAppContext(course.Apps);
			foreach(var app in appContexts) {
				app.AppControl.Init(appRegistry);		
			}
			logger.Debug("app controls inited");
			

			//3. build app tiles
	    	tileManager.BuildAppTiles(appContexts);
			logger.Debug("app tiles inited");
			
			//load catalina if required.
			if (Int16.Parse(course.Schema.Category) == (int)CourseCategory.Web){
				int port = 60080;
				catalina = new CatalinaServer(null, PidRecorder.Instance, "127.0.0.1", 60080, cpath+@"\"+courseName+@"\"+course.Schema.ProjName,course.Schema.ProjName);
				catalina.StartupSync();
				logger.Debug("catalina started up on port - " + port);
			
			}
	    				
			//4.init profile
			jProfile.SetName("070718A001");
			jProfile.SetEnergy(85);//TODO
	
		}
		
		private void ReLoadCoursePlayForm(string cid){
			logger.Debug("ReLoadCoursePlayForm cid="+cid);
			//1.load course content, prepare registry
			var courseName = cid;
			var cpath = CodeBase.GetCoursePackPath();
			var course = CourseReader.Instance.ReadCourseFrom(cpath, courseName, false);
		
			//2.restore raw code, parepare registry info
			course.RestoreRevFiles();			
			appRegistry[AppRegKeys.COURSE_KEY]= course;
					
			var milestone = course.GetLatestMileStone();
			appRegistry[AppRegKeys.VIDEO_LINK]= course.GetVideoByID(milestone.LinkID).Content;
			appRegistry[AppRegKeys.AETHER_CLIENT]= aetherClient;
			appRegistry[AppRegKeys.EIDE_CDAT]= cpath;
			appRegistry[AppRegKeys.EIDE_PROJ]= cpath+@"\"+courseName+@"\"+course.Schema.ProjName;
			
			//TODO: any change here? 3.reload app control
			appContexts = App.Views.AppContext.CreateAppContext(course.Apps);		
			foreach(var app in appContexts) {
				app.AppControl.Reload(appRegistry);		
			}
			logger.Debug("app controls reloaded");
			
			
			//4.rebuid tiles
			tileManager.RebuildAppTiles(appContexts);
			logger.Debug("app tiles reloaded");
			
			//load catalina if required.
			if (Int16.Parse(course.Schema.Category) == (int)CourseCategory.Web ){
				if (catalina != null) {
					catalina.ShutdownSync();
					logger.Debug("catalina shuted down.");
				}				
			
				int port = 60080;
				catalina = new CatalinaServer(null, PidRecorder.Instance, "127.0.0.1", port, cpath+@"\"+courseName+@"\"+course.Schema.ProjName,course.Schema.ProjName);
				catalina.StartupSync();
				logger.Debug("catalina re-started up.");
			
			}
		}
		
		#region form event
		public void FormShown()
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
						
					//course selected
					LoadCoursePlayForm(cid);
			    }
			} else {
				//course selected
				LoadCoursePlayForm("sweb-a01-proj1");
			}
		}
		
		public void FormClosed()
		{
			//Hide all tiles
			tileManager.HideAppTiles(appContexts);
			
			foreach(var app in appContexts) {
				app.AppControl.Dispose(appRegistry);					
			}
			
			//shutdown catalina
			if (catalina != null) {
				catalina.ShutdownSync();
				logger.Debug("catalina shuted down.");
			}
			
			//disconnect endpoint
			aetherClient.Disconnect();
			
			//shutdown bridge
			aetherBridge.Shutdown();
			
			//cefSharp instances dispose explicitly				
			JWebBrowser.CefDispose();
            
		}	
		
		public void FormResized(int newHeight, int newWidth)
		{
			int headHeight = 78;
			
			clientArea.Width = newWidth;
			clientArea.Height = newHeight;
			clientArea.X = 0; 
			clientArea.Y = 78;
			
			jProfile.Top = 0;
			jProfile.Left = 0;
			jProfile.Width = 300;
			jProfile.Height = headHeight;
			
			jToolbar.Top = jProfile.Top;
			jToolbar.Left = jProfile.Width;
			jToolbar.Width = clientArea.Width - jProfile.Width;
			jToolbar.Height = jProfile.Height;
			
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
			EideResponse response = EideResponse.Parse(message);
			if (response.natid == ClientConst.NAT_SWEB_ID) {
				MessageBox.Show("SWEB");
			}
		}
		#endregion aether endpoint callback

		#region IToolBarCallback implementation

		public void PlayCourseEntry()
		{
			throw new NotImplementedException();
		}

		public void DisplayCourseLib()
		{
			tileManager.HideAppTiles(appContexts);
		
			CourseForm form = new CourseForm();
			if (form.ShowDialog() == DialogResult.OK)
		    {
				var cid = (string)form.Tag;
				form.Close();
					
				//course selected
				ReLoadCoursePlayForm(cid);
				//MessageBox.Show("reload course play form");
		    }
		}

		public void DisplayWebBrowser()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
