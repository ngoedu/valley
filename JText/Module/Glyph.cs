/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/10
 * 时间: 10:15
 * 
 * 
 */

namespace NGO.Pad.JText.Module
{
	/// <summary>
	/// Description of Drawing.
	/// </summary>
	public abstract class Glyph
	{
		public int Width {set; get;}
		
		public int Position {set; get;}
		
		public bool IsDirty {set; get;}
	}
}
