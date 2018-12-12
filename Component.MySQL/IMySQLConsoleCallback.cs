/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/12/12
 * Time: 13:08
 * 
 * 
 */
using System;

namespace Component.MySQL
{
	/// <summary>
	/// Description of IMySQLConsoleCallback.
	/// </summary>
	public interface IMySQLConsoleCallback
	{
		void MySQLOutputArrived(string message);
	}
}
