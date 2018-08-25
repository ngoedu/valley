/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/23
 * Time: 22:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace NGO.Train.Entity
{
	/// <summary>
	/// Description of App.
	/// </summary>
	public class Tile
	{
		public string TileID {set; get;}
		
		public Tile(string tid)
		{
			TileID = tid;
		}
		
		public Tile()
		{
		}
	}
}
