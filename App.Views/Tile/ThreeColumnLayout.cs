/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/9/8
 * Time: 22:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace App.Views.Tile
{
	/// <summary>
	/// Description of ThreeColumnLayout.
	/// </summary>
	public class ThreeColumnLayout : TileLayout
	{
		public ThreeColumnLayout()
		{
		}
		
		public override void ActiveTile(int index) {
			
		}
		
		public override void DeactiveTile(int index) {
			
		}
		
		public override Rectangle MaxmizedSize()
		{
			return Rectangle.Empty;
		}

		public override Rectangle MinimizedSize(int tileId)
		{
			return Rectangle.Empty;
		}

		public override Rectangle NormalSize()
		{
			return Rectangle.Empty;
		}

		public override Rectangle LockedSize()
		{
			return Rectangle.Empty;
		}
	}
}
