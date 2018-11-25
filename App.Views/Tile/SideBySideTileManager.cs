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
	
	internal class SideTileHolder : IHookKeyCallback {
		public List<AppTile> TILES = new List<AppTile>();
		public int FUNKEY {get; set;}
		
		private ITileManager tileManager;
		
		public SideTileHolder(int key, ITileManager manager) {
			FUNKEY = key;
			this.tileManager = manager;
		}
		
		private int pointerIdx;
		
		public void OnHotKey(int keyCode)
		{
			var toBeActive = TILES[pointerIdx];
				
			foreach(var t in TILES) {				
				if (t != toBeActive) {
					this.tileManager.DeactiveTile(t.GetHotKeyId());
				}
				else
				{
					this.tileManager.ActiveTile(t.GetHotKeyId());
				}
			}
			
			pointerIdx += 1;
			pointerIdx = pointerIdx % (TILES.Count);
		}
		
		public void DiplayTile() {
			TILES = TILES.OrderBy(x => x.GetHotKeyId()).ToList();
			
			for(int i=1; i<TILES.Count;i++)
			{
				this.tileManager.DeactiveTile(TILES[i].GetHotKeyId());
			}
			
			this.tileManager.ActiveTile(TILES[0].GetHotKeyId());
			this.pointerIdx = 1;
		}		
	}
	
	/// <summary>
	/// Description of SideBySideTileManager.
	/// </summary>
	public class SideBySideTileManager : ITileManager
	{
		
		private TileLayout layout;
	    private Form mainForm;
	    
	    private SideTileHolder SideA1 ;
	    private SideTileHolder SideB2 ;
	    
		public SideBySideTileManager(Form mf)
		{
			this.mainForm = mf;
			SideA1 = new SideTileHolder(1, this);
	    	SideB2 = new SideTileHolder(2, this);
	    
		}

		#region ITileManager implementation

		public void BuildAppTiles(System.Collections.Generic.List<AppContext> context)
		{
			HookKeyController.Instance.RegisterCallback(SideA1.FUNKEY, SideA1);
			HookKeyController.Instance.RegisterCallback(SideB2.FUNKEY, SideB2);
			
			foreach(var app in context) {
				var tile = new AppTile(app.AppId, app.FuncKey, app.SideCode, app.Expandable, false, (System.Windows.Forms.Control)app.AppControl, this);
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

		public void OnHotkey(int keyCode,AppTile tile)
		{
			 if (tile.status == AppTile.TileStatus.Max) {
				DeactiveTile(keyCode);
			}  else if (tile.status == AppTile.TileStatus.Normal){
				ActiveTile(keyCode);
			}	
		}
		
		
		public void RebuildAppTiles(System.Collections.Generic.List<AppContext> context)
		{
			//clean up tiles
			for(int i=0; i<SideA1.TILES.Count;i++)
			{
				this.mainForm.Controls.Remove(SideA1.TILES[i]);
			}
			for(int i=0; i<SideB2.TILES.Count;i++)
			{
				this.mainForm.Controls.Remove(SideB2.TILES[i]);
			}
			SideA1.TILES.Clear();
			SideB2.TILES.Clear();			
			GetLayout().RemoveAllTiles();
			
			//renew tiles and init layout
			foreach(var app in context) {
				var tile = new AppTile(app.AppId, app.FuncKey, app.SideCode, app.Expandable, false, (System.Windows.Forms.Control)app.AppControl, this);
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
			return GetLayout().MaxmizedSize();
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
