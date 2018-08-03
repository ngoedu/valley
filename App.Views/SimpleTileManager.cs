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
using App.Common.Hook;
using App.Common.Proc;
using Control.Eide;

namespace App.Views
{
	/// <summary>
	/// Description of SimpleTileManager.
	/// </summary>
	public class SimpleTileManager : ITileManager
	{
		private Dictionary<int, IAppTile> TILES = new Dictionary<int, IAppTile>();
		private static readonly Lazy<SimpleTileManager> lazy =
	        new Lazy<SimpleTileManager>(() => new SimpleTileManager());
	    
	    public static SimpleTileManager Instance { get { return lazy.Value; } }

	    private Dictionary<int, Rectangle> MINS = new Dictionary<int, Rectangle>();
	    private Rectangle tileMaxSize;
	    private Rectangle tileNormalSize;
	    private Rectangle tileLockedSize;
	    
	    private IAppTile maxTile = null;
	    private IAppTile normalTile = null;
	    private IAppTile lockedTile = null;
	       
		private SimpleTileManager()
		{
			Rectangle resolution = Screen.PrimaryScreen.Bounds;
			Point maxPoint = new Point(20,100); 
			Point normalPoint = new Point(20,100); 
			Point lockedPoint = new Point(resolution.Width / 2 + 10,100); 
						
			MINS.Add(1,new Rectangle(new Point(100,220),new Size(300, 260)));
	    	MINS.Add(2,new Rectangle(new Point(500,220),new Size(300, 260)));
	    	MINS.Add(3,new Rectangle(new Point(900,220),new Size(300, 260)));
	    	
	    	tileMaxSize = new Rectangle(maxPoint, new Size(resolution.Width - 40, resolution.Height - 200));
	    	tileNormalSize = new Rectangle(normalPoint, new Size(resolution.Width / 2 - 40, resolution.Height - 200));
	    	tileLockedSize = new Rectangle(lockedPoint, new Size(resolution.Width / 2 - 40, resolution.Height - 200));
		}

		public void BuildAppTiles(System.Windows.Forms.Form mainForm, List<AppContext> context)
		{	
			
			/*
 			AppTile tile1 = new AppTile("Guilder", 1, new NGO.Pad.Guider.JGuider(), this);
			HookKeyController.Instance.RegisterCallback(1, tile1);
			mainForm.Controls.Add(tile1);
			
			AppTile tile2 = new AppTile("Video", 2, new Control.Video.JVideo(), this);
			HookKeyController.Instance.RegisterCallback(2, tile2);
			mainForm.Controls.Add(tile2);
			
			AppTile tile3 = new AppTile("EIDE", 3, new JEide("NgoEclipse",  CodeBase.GetCodePath(), PidRecorder.Instance),  this);
			HookKeyController.Instance.RegisterCallback(3, tile3);
			mainForm.Controls.Add(tile3);
					
			TILES.Add(1, tile1);
			TILES.Add(2, tile2);
			TILES.Add(3, tile3);
			*/
			
			foreach(var app in context) {
				var tile = new AppTile(app.AppId, app.FuncKey, (System.Windows.Forms.Control)app.AppControl, this);
				HookKeyController.Instance.RegisterCallback(app.FuncKey, tile);
				mainForm.Controls.Add(tile);
				TILES.Add(app.FuncKey, tile);
			}
		}
		
		#region ITileManager implementation
		public void ActiveTile(int index)
		{
			foreach(var tile in TILES)
			{	
				if (tile.Value.GetHotKeyId() == index) {
					if (maxTile == null) {
						tile.Value.Maxmized();
						tile.Value.Active();
						maxTile = tile.Value;
					} else {
						if (maxTile.IsLocked())
						{
							maxTile.Lock();
							maxTile.Deactive();
							lockedTile = maxTile;
							
							tile.Value.Normal();
							tile.Value.Active();
							normalTile = tile.Value;
						} else {
							maxTile.Minimized();
							maxTile.Deactive();
							tile.Value.Maxmized();
							tile.Value.Active();
							maxTile = tile.Value;
						}
					}
				} else {
					if (!tile.Value.IsLocked()) {
						tile.Value.Minimized();
						tile.Value.Deactive();
					} 
					else
					{
						tile.Value.Active();
					}
				}
			}
		}
		#endregion
		
		#region ITileManager implementation
		public void DeactiveTile(int index)
		{
			foreach(var tile in TILES)
			{	
				if (tile.Value.GetHotKeyId() == index) {
					tile.Value.Deactive();
					tile.Value.Minimized();	
				}
			}
		}
		#endregion

		public Rectangle MaxmizedSize()
		{
			return tileMaxSize;
		}

		public Rectangle MinimizedSize(int tileId)
		{
			return MINS[tileId];
		}

		public Rectangle NormalSize()
		{
			return tileNormalSize;
		}

		public Rectangle LockedSize()
		{
			return tileLockedSize;
		}
	}
}
