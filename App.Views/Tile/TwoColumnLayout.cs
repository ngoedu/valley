/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/9/8
 * Time: 21:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace App.Views.Tile
{
	/// <summary>
	/// Description of TwoColumnLayout.
	/// </summary>
	public class TwoColumnLayout : TileLayout
	{
		
		private Dictionary<int, Rectangle> MINS = new Dictionary<int, Rectangle>();
		
	    private Rectangle tileMaxSize;
	    private Rectangle tileNormalSize;
	    private Rectangle tileLockedSize;
	    
	    private IAppTile maxTile = null;
	    private IAppTile normalTile = null;
	    private IAppTile lockedTile = null;
	    
	    private double scale;
	    
		public TwoColumnLayout(double scale)
		{
			this.scale = scale;
			
			Rectangle resolution = Screen.PrimaryScreen.Bounds;
			Point maxPoint = new Point(20,100); 
			Point normalPoint = new Point(20,100); 
			Point lockedPoint = new Point(resolution.Width / 2 + 10,100); 
						
			MINS.Add(1,new Rectangle(new Point(100,220),new Size(300, 260)));
	    	MINS.Add(2,new Rectangle(new Point(500,220),new Size(300, 260)));
	    	MINS.Add(3,new Rectangle(new Point(900,220),new Size(300, 260)));
	    	MINS.Add(4,new Rectangle(new Point(100,520),new Size(300, 260)));
	    	
	    	tileMaxSize = new Rectangle(maxPoint, new Size(resolution.Width - 40, resolution.Height - 200));
	    	tileNormalSize = new Rectangle(normalPoint, new Size(resolution.Width / 2 - 40, resolution.Height - 200));
	    	tileLockedSize = new Rectangle(lockedPoint, new Size(resolution.Width / 2 - 40, resolution.Height - 200));
		}
		
		public override void ActiveTile(int index) {
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
		
		public override void DeactiveTile(int index) {
			foreach(var tile in TILES)
			{	
				if (tile.Value.GetHotKeyId() == index) {
					tile.Value.Deactive();
					tile.Value.Minimized();	
				}
			}
		}
		
		public override Rectangle MaxmizedSize()
		{
			return tileMaxSize;
		}

		public override Rectangle MinimizedSize(int tileId)
		{
			return MINS[tileId];
		}

		public override Rectangle NormalSize(int sc)
		{
			return tileNormalSize;
		}

		public override Rectangle LockedSize()
		{
			return tileLockedSize;
		}
	}
}
