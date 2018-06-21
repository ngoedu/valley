/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/22
 * Time: 2:13
 * 
 * 
 */
using System;

namespace Component.Bridge
{
	/// <summary>
	/// Description of Interface1.
	/// </summary>
	public interface IOutputCallback
	{
		void OutputArrived(string output);
	}
}
