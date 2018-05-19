/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/21
 * 时间: 4:16
 * 
 * 
 */
using NGO.Pad.JText.Module;

namespace NGO.Pad.JText.Controller.Command
{
	/// <summary>
	/// Horizental Table Char, the width of tab is varies base on width of precedings.
	/// </summary>
	public class Tab : ICommand
	{
	
		private IMediator media;
		public Tab(IMediator m) {
			this.media = m;	
		}
		
		public void Execute(Char chr)
		{
			var cursor = media.GetCursor();
			var row = (Row)cursor.Line.Value;
			
			if (cursor.Column == null) {
				chr.Width *= 4;
			} else {
				chr.Width = 40 - cursor.Column.End % 40;
			}
			
			media.Dispatch(Const.LETTER).Execute(chr);
			
			System.Diagnostics.Debug.WriteLine("tab");
		}	
	}
}
