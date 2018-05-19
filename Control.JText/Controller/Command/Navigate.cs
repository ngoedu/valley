/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/21
 * 时间: 4:48
 * 
 * 
 */
using NGO.Pad.JText.Module;

namespace NGO.Pad.JText.Controller.Command
{
	/// <summary>
	/// Description of Navigate.
	/// </summary>
	public class Navigate : ICommand
	{
		private IMediator media;
		public Navigate(IMediator m) {
			this.media = m;
		}
		
		public void Execute(Char chr)
		{
			switch (chr.Inter) {
			    case 'U':{
					Up(); break;
    			}
	    		case 'D':{
					Down();	break;	
				}
				case 'L':{
					Left();	break;
				}
				case 'R':{
					Right(); break;	
				}
			}
			System.Diagnostics.Debug.WriteLine("navigate {0}", chr);
		}

		private void Up() {
			Cursor cursor = media.GetCursor();
			if (cursor.Line.Previous == null)
				return;
			if (cursor.Column == null) {
				cursor.Reposition(cursor.Line.Previous, null);
				return;
			}
			var row = (Row)cursor.Line.Previous.Value;
			var column = cursor.Column.Index >= row.Last.Index - 2 ? row.Last.Previous.Previous : row.IndexOf(cursor.Column.Index);
			cursor.Reposition(cursor.Line.Previous, column);
		}
		
		private void Down() {
			Cursor cursor = media.GetCursor();
			if (cursor.Line.Next == null)
				return;
			if (cursor.Column == null) {
				cursor.Reposition(cursor.Line.Previous, null);
				return;
			}
			var row = (Row)cursor.Line.Next.Value;
			var column = cursor.Column.Index >= row.Last.Index - 2 ? row.Last.Previous.Previous : row.IndexOf(cursor.Column.Index);
			cursor.Reposition(cursor.Line.Next, column);
		}
		
		private void Left() {
			Cursor cursor = media.GetCursor();
			if (cursor.Column == null)
				return;
			cursor.Reposition(cursor.Line, cursor.Column.Previous);
		}
		
		private void Right() {
			Cursor cursor = media.GetCursor();
			var row = (Row)cursor.Line.Value;
			if (cursor.Column == null ) {
				if (row.First.Value != Char.CR)
					cursor.Reposition(cursor.Line, row.First);
				return;
			}
			if (cursor.Column.Next == null || cursor.Column.Next.Value == Char.CR)
				return;		
			cursor.Reposition(cursor.Line, cursor.Column.Next);
		}
	}
}
