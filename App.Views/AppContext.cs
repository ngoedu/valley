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
		public IAppEntry AppControl {private set; get;}
		
		private static Dictionary<string, AppContext> APPREG = new Dictionary<string, AppContext>();
		static AppContext() {
			APPREG.Add("guider", new App.Views.AppContext("导航", 1, new JGuider()));
			APPREG.Add("video", new App.Views.AppContext("视频", 2, new JVideo()));
			APPREG.Add("jeide", new App.Views.AppContext("编码", 3, new JEide("NgoEclipse",  CodeBase.GetCodePath(), PidRecorder.Instance)));
		}
		
		public AppContext(string id, int key, IAppEntry ctl)
		{
			this.AppId = id;
			this.FuncKey = key;
			this.AppControl = ctl;
		}

		public static List<App.Views.AppContext> CreateAppContext(List<NGO.Train.App> apps)
		{
			List<App.Views.AppContext> appContexts = new List<App.Views.AppContext>();
			foreach(var app in apps) {
				appContexts.Add(APPREG[app.ID]);	
			}
			return appContexts;
		}
	}
}
