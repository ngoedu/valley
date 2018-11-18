/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/4
 * Time: 10:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using App.Common.Signal;

namespace App.Common.Net
{
	/// <summary>
	/// Description of IClient.
	/// </summary>
	public interface IClient
	{
		void SendToRemote(string message, int target);
		string SendToRemoteSync(string message, int target);
		
		int RegistDataReceiver(IDatReceiver receiver);
	}
}
