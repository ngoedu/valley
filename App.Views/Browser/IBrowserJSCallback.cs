/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/24
 * Time: 15:11
 * 
 * 
 */
using System;
using CefSharp.WinForms;

namespace App.Views
{
	/// <summary>
	/// Description of IBrowserJSCallback.
	/// </summary>
	public interface IBrowserJSCallback
	{
		void SetCefBrowser(ChromiumWebBrowser cefBrowser);

		string GetJSCallbackName();	
	}
}
