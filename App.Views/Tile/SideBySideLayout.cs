/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/9
 * Time: 13:12
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace App.Views.Tile
{
	/// <summary>
	/// Description of SideBySideLayout.
	/// </summary>
	public class SideBySideLayout	: TileLayout
	{
		
		private Rectangle sideBNormalSize;
	    private Rectangle sideANormalSize;
	    
	  	private Rectangle tileMaxSize;
	  	private Point maxPoint = new Point(20,100);
			
	    
	    private Dictionary<int, Rectangle> MINS = new Dictionary<int, Rectangle>();  
	    private double scale;
	    
		public SideBySideLayout(double scale)
		{
			this.scale = scale;
			
			Rectangle resolution = Screen.PrimaryScreen.Bounds;
			Point normalAPoint = new Point(20,100); 
	    	sideANormalSize = new Rectangle(normalAPoint, new Size(resolution.Width / 2 - 40, resolution.Height - 190));
			
	    	Point normalBPoint = new Point(resolution.Width / 2 ,100);
	    	sideBNormalSize = new Rectangle(normalBPoint, new Size(resolution.Width / 2 -20 , resolution.Height - 190));
			
	    	MINS.Add(1,new Rectangle(new Point(100,220),new Size(3, 2)));
	    	MINS.Add(2,new Rectangle(new Point(120,220),new Size(3, 2)));
	    	MINS.Add(3,new Rectangle(new Point(140,220),new Size(3, 2)));
	    	MINS.Add(4,new Rectangle(new Point(160,220),new Size(3, 2)));
	    	
	    	tileMaxSize = new Rectangle(maxPoint, new Size(resolution.Width - 40, resolution.Height - 180));
	    	
		}
		
		public override void ActiveTile(int keyCode) {
			foreach(var tile in TILES)
			{	
				if (tile.Value.GetHotKeyId() == keyCode) {
					System.Diagnostics.Debug.WriteLine("=====>ActiveTIle, status="+tile.Value.status);
						
					if (tile.Value.Reactive) {
						if (tile.Value.status == AppTile.TileStatus.Min) {
							tile.Value.Normal();
						} else if (tile.Value.status == AppTile.TileStatus.Normal) {
							tile.Value.Maxmized();
						} else if (tile.Value.status == AppTile.TileStatus.Max) {
							tile.Value.Normal();
						}
					} else {
						tile.Value.Normal();
					}
					tile.Value.Active();
					tile.Value.Visible = true;
					break;
				} 
			}
		}

		#region implemented abstract members of TileLayout
		public override void HideTile(int tileId)
		{
			foreach(var tile in TILES)
			{	
				if (tile.Value.GetHotKeyId() == tileId) {
					tile.Value.Visible = false;
					break;
				} 
			}
		}
		#endregion		
		
		
		public override void DeactiveTile(int index) {
			foreach(var tile in TILES)
			{	
				if (tile.Value.GetHotKeyId() == index) {
					
					if (tile.Value.Reactive) {
						if (tile.Value.status == AppTile.TileStatus.Max) {
							tile.Value.Visible = true;
							tile.Value.Normal();
						} else if (tile.Value.status == AppTile.TileStatus.Normal) {
							tile.Value.Visible = false;
							tile.Value.Deactive();
							tile.Value.Minimized();	
						} else if (tile.Value.status == AppTile.TileStatus.Min) {
							tile.Value.Visible = false;
							tile.Value.Deactive();
							tile.Value.Minimized();	
						}
					} else {
					
						tile.Value.Visible = false;
						tile.Value.Deactive();
						tile.Value.Minimized();	
					}
				}
			}
		}
		
		public override Rectangle MaxmizedSize()
		{
			return this.tileMaxSize;
		}

		public override Rectangle MinimizedSize(int tileId)
		{
			return MINS[tileId];
		}

		public override Rectangle NormalSize(int sideCode)
		{
			return sideCode ==1 ? sideANormalSize : sideBNormalSize ;
		}

		public override Rectangle LockedSize()
		{
			throw new InvalidOperationException();
		}
	}
}
