/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/29
 * Time: 21:58
 * 
 * 
 */
using System;
using System.Threading;
using CefSharp.WinForms;
using CefSharp;

namespace App.Views
{
	/// <summary>
	/// Description of SyncLoad.
	/// </summary>
	public class SyncLoader
	{
		private ChromiumWebBrowser cefBrowser;
		ManualResetEvent loadingDone = new ManualResetEvent(false);
		public SyncLoader(ChromiumWebBrowser browser)
		{
			this.cefBrowser = browser;
			cefBrowser.LoadingStateChanged += OnLoadingStateChanged;
		}
		
		private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            //Wait for the Page to finish loading
			if (args.IsLoading == false)
			{
				loadingDone.Set();
				//loadingDone.Reset();				
			}
        }
		
		public void LoadUrl(string url) {
			cefBrowser.Load(url);
			//block until loading done.
			//loadingDone.WaitOne();
			//here loading done
		}
	}
}
