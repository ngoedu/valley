/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/15
 * Time: 20:38
 * 
 * 
 */
using System;

namespace App.Common.Hook
{
	/// <summary>
	/// Description of IHookKeyCallback.
	/// </summary>
	public interface IHookKeyCallback
	{
		void OnKey(int keyCode);
	}
}
