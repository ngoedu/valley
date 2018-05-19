/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/27
 * 时间: 20:01
 * 
 * 
 */
using System;

namespace NGO.Pad.JText.UI
{
	/// <summary>
	/// A keyboard input callback interface, the implentation needs handle the input
	/// </summary>
	public interface IKeyCallback
	{
		void CharIn(char[] chrs);
	}
}
