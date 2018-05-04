/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/21
 * 时间: 3:50
 * 
 * 
 */
using System;
using NGO.Pad.JText.Module;
using NGO.Pad.JText.UI;

namespace NGO.Pad.JText.Module
{
	/// <summary>
	/// Cursor is an indicator of location and current row and column.
	/// </summary>
	public class Cursor
	{
		private IFlicker flicker;
		public Cursor(IFlicker f) {
			this.flicker = f;
		}
		
		public Node Line {set; get;}
		
		public Node Column {set; get;}
		
		public void Reposition(Node line, Node column)
		{
			this.Line = line;
			this.Column = column;
		}

	}
}
