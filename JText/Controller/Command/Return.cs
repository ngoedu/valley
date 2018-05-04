/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/21
 * 时间: 4:42
 * 
 * 
 */
using NGO.Pad.JText.Module;

namespace NGO.Pad.JText.Controller.Command
{
	/// <summary>
	/// Description of CarriageReturn.
	/// </summary>
	public class Return : ICommand
	{
		
		private IMediator media;
		public Return(IMediator m) {
			this.media = m;
		}
		
		public void Execute(Char chr)
		{
			var content = media.GetContent();
			var cursor = media.GetCursor();
			var row = (Row)cursor.Line.Value;
			
			//1.append CR + LF
			media.Dispatch(Const.LETTER).Execute(chr);
			
			var breaked = row.Break(cursor.Column);
			
			media.Dispatch(Const.LINEFEED).Execute(null);
			
			//2.append breaked
			row = (Row)cursor.Line.Value;
			if (breaked != null) 
				row.AddLast(breaked);			
			
			//5.cursor reposition
			cursor.Reposition(cursor.Line, row.First);
		}
	}
}
