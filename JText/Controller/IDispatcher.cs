/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/27
 * 时间: 21:08
 * 
 * 
 */

using NGO.Pad.JText.Module;
using NGO.Pad.JText.Controller.Command;

namespace NGO.Pad.JText.Controller
{
	/// <summary>
	/// Dispatcher is routing user action to specific command object.
	/// </summary>
	public interface IDispatcher
	{
		ICommand Dispatch(int chr);
	}
}
