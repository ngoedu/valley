/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-04-20
 * Time: 7:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace NGO.Pad.JFiles
{
	/// <summary>
	/// Description of INavigator.
	/// </summary>
	public interface INavigator
	{
		Graphics GetGraphic();
		void ChangePath(TextNode node);
		int GetHeight();
		int GetWidth();
		int GetTop();
		int GetLeft();
	}
}
