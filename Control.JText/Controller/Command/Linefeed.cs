/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/21
 * 时间: 4:45
 * 
 * 
 */
using NGO.Pad.JText.Module;

namespace NGO.Pad.JText.Controller.Command
{
	/// <summary>
	/// Description of LineFeed.
	/// </summary>
	public class Linefeed: ICommand
	{
		
		private IMediator media;
		public Linefeed(IMediator m) {
			this.media = m;
		}
		
		public void Execute(Char chr)
		{
			var content = media.GetContent();
			var cursor = media.GetCursor();
			
			//1. append LF to current line
			var row = (Row)cursor.Line.Value;
			row.AddLast(Char.ValueOf((char)10));
			
			//line feed lead to a new line with LF char appended in the first position
			var newRow = new Row();
			Node line = cursor.Line == null ? content.AddLast(newRow) : content.AddAfter(cursor.Line, newRow);
			cursor.Reposition(line, newRow.First);
			
			System.Diagnostics.Debug.WriteLine("LineFeed");
		}	
	}
}
