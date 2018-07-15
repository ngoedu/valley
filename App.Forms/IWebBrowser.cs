/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/12
 * Time: 21:12
 * 
 * 
 */
using System;

namespace App.Forms
{
	/// <summary>
	/// Description of IWebBrowser.
	/// </summary>
	public interface IWebBrowser
	{
		void LoadPage(string content);
		void GoToUrl(string url);
	}
}
