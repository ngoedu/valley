/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/19
 * Time: 0:15
 * 
 * 
 */
using System;

namespace Component.Catalina
{
	/// <summary>
	/// Description of IOutputCallback.
	/// </summary>
	public interface IOutputCallback
	{
		void OutputArrived(string output);
	}
}
