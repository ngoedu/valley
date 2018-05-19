/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-04-19
 * Time: 9:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Control.Files
{
	/// <summary>
	/// Description of IBrowser.
	/// </summary>
	public interface IFiles
	{
		string GetRootName();
		void Refresh(string path);
		void RefreshCurrent();
	}
}
