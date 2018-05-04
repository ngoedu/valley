/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/23
 * 时间: 5:31
 * 
 * 
 */
using NGO.Pad.JText.Module;

namespace NGO.Pad.JText.Controller.Command
{
	/// <summary>
	/// handling shortcut action triggered by combination of key Ctrl + a,c,v,x,z etc
	/// </summary>
	public class Shortcuts: ICommand
	{
	
		private IMediator media;
		public Shortcuts(IMediator m) {
			this.media = m;
		}
		
		public void Execute(Char chr)
		{
			System.Diagnostics.Debug.WriteLine("shortcut {0}", chr);
		}	
	}
}
