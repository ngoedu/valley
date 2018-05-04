/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/27
 * 时间: 21:31
 * 
 * 
 */

using NGO.Pad.JText.Module;
namespace NGO.Pad.JText.Controller
{
	/// <summary>
	/// Datahub is a dictionary which holding all related module infromnation for current control.
	/// </summary>
	public interface IDatahub
	{
		Cursor GetCursor();
		Block GetSelection();
		Block GetContent();
	}
}
