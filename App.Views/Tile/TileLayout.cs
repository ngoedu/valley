/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/9/8
 * Time: 21:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace App.Views.Tile
{
	/// <summary>
	/// Description of ITileLayout.
	/// </summary>
	public abstract class TileLayout
	{
		protected Dictionary<int, AppTile> TILES = new Dictionary<int, AppTile>();
		
		public  virtual void AddTile(int key, AppTile tile) {
			TILES.Add(key, tile);
		}
		
		public abstract void ActiveTile(int index);
		
		public abstract void DeactiveTile(int index);
		
		public abstract Rectangle MaxmizedSize();

		public abstract Rectangle MinimizedSize(int tileId);

		public abstract Rectangle NormalSize(int sideCode);

		public abstract Rectangle LockedSize();
		
		public abstract void HideTile(int tileId);
	}
}
