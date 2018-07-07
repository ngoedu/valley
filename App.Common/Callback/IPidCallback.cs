/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/7
 * Time: 17:24
 * 
 * 
 */
using System;

namespace App.Common.Callback
{
	/// <summary>
	/// Description of IPidCallback.
	/// </summary>
	public interface IPidCallback
	{
		void PidCreated(string pName, int pid);
	}
}
