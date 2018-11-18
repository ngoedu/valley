/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/18
 * Time: 13:47
 * 
 * 
 */
using System;

namespace App.Common.Net
{
	/// <summary>
	/// Description of IDatReceiver.
	/// </summary>
	public interface IDatReceiver
	{
		void DataArrived(string message);
		int NatId();
	}
}
