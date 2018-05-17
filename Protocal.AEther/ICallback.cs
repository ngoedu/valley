/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/20
 * 时间: 0:17
 * 
 * 
 */
using System;

namespace NGO.Protocol.AEther
{
	/// <summary>
	/// Description of ICallback.
	/// </summary>
	public interface ICallback
	{
		void Connected();
		void DataSent(string info);
		void MessageReceived(string message);
	}
}
