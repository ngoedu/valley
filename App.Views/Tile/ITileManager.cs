/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/17
 * Time: 21:36
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace App.Views
{
	/// <summary>
	/// Description of ITileManager.
	/// </summary>
	public interface ITileManager
	{
		void BuildAppTiles(List<AppContext> context);
		void ActiveTile(int index);
		void DeactiveTile(int index);

		void HideAppTiles(List<App.Views.AppContext> appContexts);

		Rectangle MaxmizedSize();
		Rectangle MinimizedSize(int tileId);
		Rectangle NormalSize(int sideCode);
		Rectangle LockedSize();
		
	}
}
