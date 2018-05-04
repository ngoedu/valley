/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/27
 * 时间: 20:00
 * 
 * 
 */

using System.Diagnostics;
using NGO.Pad.JText.Controller.Command;
using NGO.Pad.JText.Module;
using NGO.Pad.JText.UI;
namespace NGO.Pad.JText.Controller
{
	/// <summary>
	/// TextController is the center of the control which is handling
	/// 	1. dispatch key board event to specific command ;
	/// 	2. UI repaint event;
	/// it is also acting as a multi-parties mediator between actions and module data
	/// </summary>
	public class TextController : IKeyCallback, IMediator
	{
		private Context uicontext;
		
		private readonly ICommand[] CommandMap;
		
		private readonly Cursor cursor;
		private readonly Block selection;
		private readonly Block content;
		
		public TextController(Context c)
		{
			this.uicontext = c; 
						
			cursor = new Cursor(uicontext.GetFlicker());
			selection = new Block();
			content = new Block();
			
			this.Initiate();
			
			CommandMap = new ICommand[16];
			CommandMap[Const.LETTER] 	= new Letter(this);
			CommandMap[Const.DELETE] 	= new Delete(this);
			CommandMap[Const.SHORTCUT] 	= new Shortcuts(this);
			CommandMap[Const.BACKSPACE] = new Backspace(this);
			CommandMap[Const.TAB] 		= new Tab(this);
			CommandMap[Const.RETURN] 	= new Return(this);
			CommandMap[Const.LINEFEED] 	= new Linefeed(this);
			CommandMap[Const.NAVIGATE] 	= new Navigate(this);
			CommandMap[Const.SELECT] 	= new Select(this);	
		}
		
		private void Initiate() {
			//add first row
			var line = GetContent().AddLast(new Row());
			GetCursor().Reposition(line, null);
		}

		public ICommand Dispatch(int chr)
		{
			int index = chr > 15 ? 0 : chr;
			return CommandMap[index];
		}
		
		public void CharIn(char[] cArray)
		{
			var cmd = this.Dispatch(cArray[0]);
			Char chr = Char.ValueOf(cArray.Length == 2 ? cArray[1] : cArray[0]);
			cmd.Execute(chr);
			
			//TODO:
			//OnPaint(g, false);
		}
		
		public void Repaint(Graphic g)
		{
			//OnPaint(g, false);
		}

		public Cursor GetCursor()
		{
			return this.cursor;
		}

		public Block GetSelection()
		{
			return this.selection;
		}

		public Block GetContent()
		{
			return this.content;
		}
	}
}
