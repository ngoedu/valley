/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/3
 * Time: 21:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace App.Common.Reg
{
	/// <summary>
	/// Description of IAppEntry.
	/// </summary>
	public interface IAppEntry
	{
		void Init(AppRegistry reg);
		void Dispose(AppRegistry reg);
		int Status();
	}
}
