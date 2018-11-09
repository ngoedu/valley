/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/19
 * Time: 5:35
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using App.Common;
using App.Common.Dpi;
using App.Common.Hook;
using App.Common.Proc;
using Control.Eide;
using App.Views.Tile;

namespace App.Views
{
	/// <summary>
	/// Description of SimpleTileManager.
	/// </summary>
	public class SimpleTileManager : ITileManager
	{
		
	    private TileLayout layout;
	    private Form mainForm;
	       
		public SimpleTileManager(Form mf)
		{
			this.mainForm = mf;
		}

		public void BuildAppTiles(List<AppContext> context)
		{
			foreach(var app in context) {
				var tile = new AppTile(app.AppId, app.FuncKey,app.SideCode, true, true, (System.Windows.Forms.Control)app.AppControl, this);
				HookKeyController.Instance.RegisterCallback(app.FuncKey, tile);
				mainForm.Controls.Add(tile);
				GetLayout().AddTile(app.FuncKey, tile);
				tile.Minimized();
			}
		}
		
		private TileLayout GetLayout() {
			if (layout == null )
			{
				double scale = DpiUtil.GetScale(this.mainForm.CreateGraphics());
				layout = new TwoColumnLayout(scale);
				if (scale.Equals(DPI.SMALL)) {
					//layout = new ThreeColumnLayout();
				} 
			}		
			return layout;
		}

		public void HideAppTiles(List<AppContext> appContexts)
		{
			foreach(var app in appContexts) {		
				GetLayout().HideTile(app.FuncKey);
			}
		}
		
		#region ITileManager implementation
		public void ActiveTile(int index)
		{
			GetLayout().ActiveTile(index);
		}
		#endregion
		
		#region ITileManager implementation
		public void DeactiveTile(int index)
		{
			GetLayout().DeactiveTile(index);
		}
		#endregion

		public Rectangle MaxmizedSize()
		{
			return GetLayout().MaxmizedSize();
		}

		public Rectangle MinimizedSize(int tileId)
		{
			return GetLayout().MinimizedSize(tileId);
		}

		public Rectangle NormalSize(int sideCode)
		{
			return GetLayout().NormalSize(sideCode);
		}

		public Rectangle LockedSize()
		{
			return GetLayout().LockedSize();
		}
	}
}
