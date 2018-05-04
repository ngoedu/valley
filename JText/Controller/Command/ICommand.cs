/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/21
 * 时间: 4:14
 * 
 * 
 */

using NGO.Pad.JText.Module;

namespace NGO.Pad.JText.Controller.Command
{
	/// <summary>
	/// Refer to Command design pattern.
	/// </summary>
	public interface ICommand
	{
		void Execute(Char chr);
	}
}
