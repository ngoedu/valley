/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/17
 * Time: 21:36
 * 
 * 
 */
using System;
using System.Drawing;

namespace App.Views
{
	/// <summary>
	/// Description of ITileManager.
	/// </summary>
	public interface ITileManager
	{
		void BuildAppTiles(System.Windows.Forms.Form form);
		void ActiveTile(int index);
		void DeactiveTile(int index);
		Rectangle MaxmizedSize();
		Rectangle MinimizedSize(int tileId);
		Rectangle NormalSize();
		Rectangle LockedSize();
		
	}
}
