/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/17
 * Time: 21:07
 * 
 * 
 */
using System;
using App.Common.Hook;

namespace App.Forms
{
	/// <summary>
	/// Description of IAppTile.
	/// </summary>
	public interface IAppTile : IHookKeyCallback
	{
		int GetHotKeyId();
		string GetTileName();
		void Active();
		void Deactive();
		void Maxmized();
		void Normal();
		void Minimized();
		void Lock();
		bool IsLocked();
	}
}
