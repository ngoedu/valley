/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/3
 * Time: 20:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using App.Common;
using App.Common.Proc;
using App.Common.Reg;
using Control.Eide;
using Control.JBrowser;
using Control.Server;
using Control.Video;
using NGO.Pad.Guider;
using NGO.Train;


namespace App.Views
{
	/// <summary>
	/// Description of AppContext.
	/// </summary>
	public class AppContext
	{
		public string AppId {private set; get;}
		public int FuncKey {private set; get;}
		public int SideCode {private set; get;} //group or side code of layout
		public IAppEntry AppControl {private set; get;}
		public bool Expandable{set; get;}
		
		private static readonly Dictionary<string, AppContext> APPREG = new Dictionary<string, AppContext>();
		
		public static void AppContextsInitializer() {
			
		}
		
		static AppContext() {
			APPREG.Add("video", new App.Views.AppContext("视频", 1, 1,new JVideo(), false));
			APPREG.Add("guider", new App.Views.AppContext("导航", 2, 1,new JGuider(),false));
			APPREG.Add("jeide", new App.Views.AppContext("编码", 3, 2,new JEide("NgoEclipse",  CodeBase.GetCodePath(), PidRecorder.Instance), false));
			APPREG.Add("browser", new App.Views.AppContext("浏览器", 4, 2, new JDevBrowser(),true));
			APPREG.Add("server", new App.Views.AppContext("服务器", 5, 3, new ServerControl(),false));

		}
		
		public AppContext(string id, int key, int sideCode, IAppEntry ctl, bool expandable)
		{
			this.AppId = id;
			this.FuncKey = key;
			this.AppControl = ctl;
			this.SideCode = sideCode;
			this.Expandable = expandable;
		}

		public static List<App.Views.AppContext> CreateAppContext(List<NGO.Train.Entity.Tile> apps)
		{
			List<App.Views.AppContext> appContexts = new List<App.Views.AppContext>();
			foreach(var app in apps) {
				appContexts.Add(APPREG[app.TileID]);	
			}
			return appContexts;
		}
	}
}
