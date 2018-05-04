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
	/// New Char to be added into row.
	/// </summary>
	public class Letter : ICommand
	{
	
		private IMediator media;
		public Letter(IMediator m) {
			this.media = m;
		}
		
		public void Execute(Char chr)
		{
			var cursor = media.GetCursor();
			var row = (Row)cursor.Line.Value;
			Node column = null;
			if (cursor.Column == null) {
				if (row.First !=null)
					column = row.AddBefore(row.First, chr);
				else
					column = row.AddLast(chr);
			} 
			else 
			{
				column = row.AddAfter(cursor.Column, chr);
			}
			
			//cursor reposition
			cursor.Reposition(cursor.Line, column);
			System.Diagnostics.Debug.WriteLine("{0}", chr);
		}	
	}
}
