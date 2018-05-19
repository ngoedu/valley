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
	public class Delete : ICommand
	{
	
		private IMediator media;
		public Delete(IMediator m) {
			this.media = m;
		}
		
		public void Execute(Char chr)
		{
			var cursor = media.GetCursor();
			var row = (Row)cursor.Line.Value;
			if (row.Count <= 0)
				return;
			Node node = null;
			if (cursor.Column == null) {
				row.Remove(row.First);
				cursor.Reposition(cursor.Line, null);
			} else {
				node = row.Remove(cursor.Column);
				cursor.Reposition(cursor.Line, node);
			}
			System.Diagnostics.Debug.WriteLine("delete");
		}	
	}
}
