﻿/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/7
 * Time: 17:24
 * 
 * 
 */
using System;

namespace App.Common.Proc
{
	/// <summary>
	/// Description of IPidCallback.
	/// </summary>
	public interface IPidCallback : IPidCleaner
	{
		void PidCreated(string pName, int pid);
	}
}