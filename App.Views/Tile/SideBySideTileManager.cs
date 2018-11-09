/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/9
 * Time: 13:02
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using App.Common.Dpi;
using App.Common.Hook;

namespace App.Views.Tile
{
	
	internal class TileSideArray : IHookKeyCallback {
		public List<AppTile> TILES = new List<AppTile>();
		public int FUNKEY {get; set;}
		
		private ITileManager tileManager;
		
		public TileSideArray(int key, ITileManager manager) {
			FUNKEY = key;
			this.tileManager = manager;
		}
		
		public void OnHotKey(int keyCode)
		{
			foreach(var t in TILES) {
				if (t.status == AppTile.TileStatus.Normal) {
					this.tileManager.DeactiveTile(t.GetHotKeyId());
				} else if (t.status == AppTile.TileStatus.Min) {
					this.tileManager.ActiveTile(t.GetHotKeyId());
				}
			}
		}
		
		public void DiplayTile() {
			TILES = TILES.OrderBy(x => x.GetHotKeyId()).ToList();
			
			for(int i=1; i<TILES.Count;i++)
			{
				this.tileManager.DeactiveTile(TILES[i].GetHotKeyId());
			}
			
			this.tileManager.ActiveTile(TILES[0].GetHotKeyId());
		}		
	}
	
	/// <summary>
	/// Description of SideBySideTileManager.
	/// </summary>
	public class SideBySideTileManager : ITileManager
	{
		
		private TileLayout layout;
	    private Form mainForm;
	    
	    private TileSideArray SideA1 ;
	    private TileSideArray SideB2 ;
	    
		public SideBySideTileManager(Form mf)
		{
			this.mainForm = mf;
			SideA1 = new TileSideArray(1, this);
	    	SideB2 = new TileSideArray(2, this);
	    
		}

		#region ITileManager implementation

		public void BuildAppTiles(System.Collections.Generic.List<AppContext> context)
		{
			HookKeyController.Instance.RegisterCallback(SideA1.FUNKEY, SideA1);
			HookKeyController.Instance.RegisterCallback(SideB2.FUNKEY, SideB2);
			
			foreach(var app in context) {
				var tile = new AppTile(app.AppId, app.FuncKey, app.SideCode, false, false, (System.Windows.Forms.Control)app.AppControl, this);
				mainForm.Controls.Add(tile);
				if (app.SideCode == SideA1.FUNKEY) {
					SideA1.TILES.Add(tile);
				}
				else if (app.SideCode == SideB2.FUNKEY) {
					SideB2.TILES.Add(tile);
				}
				else
					MessageBox.Show("app don't have defined side by side hotkey");
					
				GetLayout().AddTile(app.FuncKey, tile);
			}
			SideA1.DiplayTile();
			SideB2.DiplayTile();
		}
		
		private TileLayout GetLayout() {
			if (layout == null )
			{
				double scale = DpiUtil.GetScale(this.mainForm.CreateGraphics());
				layout = new SideBySideLayout(scale);
				if (scale.Equals(DPI.SMALL)) {
					//layout = new ThreeColumnLayout();
				} 
			}
			
			return layout;
		}

		public void HideAppTiles(List<AppContext> appContexts)
		{
			for(int i=0; i<SideA1.TILES.Count;i++)
			{
				SideA1.TILES[i].Visible = false;
			}
			for(int i=0; i<SideB2.TILES.Count;i++)
			{
				SideB2.TILES[i].Visible = false;
			}
		}
		public void ActiveTile(int index)
		{
			GetLayout().ActiveTile(index);
		}

		public void DeactiveTile(int index)
		{
			GetLayout().DeactiveTile(index);
		}

		public System.Drawing.Rectangle MaxmizedSize()
		{
			throw new InvalidOperationException();
		}

		public System.Drawing.Rectangle MinimizedSize(int tileId)
		{
			return GetLayout().MinimizedSize(tileId);
		}

		public System.Drawing.Rectangle NormalSize(int sideCode)
		{
			return GetLayout().NormalSize(sideCode);
		}

		public System.Drawing.Rectangle LockedSize()
		{
			throw new InvalidOperationException();
		}

		#endregion
	}
}
