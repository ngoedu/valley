/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/7
 * Time: 17:54
 * 
 * 
 */
using System;

namespace App.Common.Callback
{
	/// <summary>
	/// Description of IPidCleaner.
	/// </summary>
	public interface IPidCleaner
	{
		void KillProcessById(int pid);
		
		void CleanOldProcess();
	}
}
