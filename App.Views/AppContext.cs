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
using App.Common.Reg;

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
		public Dictionary<string, object> Registry = new Dictionary<string, object>();
		public AppContext(string id, int key, IAppEntry ctl)
		{
			this.AppId = id;
			this.FuncKey = key;
			this.AppControl = ctl;
		}
	}
}
