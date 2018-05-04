/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/27
 * 时间: 20:11
 * 
 * 
 */
using System;
using System.Drawing;

namespace NGO.Pad.JText.UI
{
	/// <summary>
	/// This is a flick indicator or cursor which indicating the location of current row and column.
	/// </summary>
	public interface IFlicker
	{
		void SetLocation(int x, int y);
		
		void SetBackgound(Color color);
	}
}
