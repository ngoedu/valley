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
	public class Select : ICommand
	{
		private IMediator media;
		public Select(IMediator m) {
			this.media = m;
		}
		
		public void Execute(Char chr)
		{
			System.Diagnostics.Debug.WriteLine("select");
		}	
	}
}
