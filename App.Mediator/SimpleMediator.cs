/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/2
 * Time: 21:28
 * 
 * 
 */
using System;
using System.Windows.Forms;
using App.Forms;
using CefSharp;
using CefSharp.WinForms;
using Control.Profile;
using Control.Toolbar;

namespace App.Mediator
{
	/// <summary>
	/// Description of SimpleMediator.
	/// </summary>
	public class SimpleMediator : IMediator
	{
		private Form mainForm;
		private readonly ChromiumWebBrowser browser;
		private JToolbar jToolBar;
		private Profile jProfile;

		private CourseLib courseLib;
		private CourseView courseView;
		
		public SimpleMediator(Form mf)
		{
			this.mainForm = mf;
			
			//the singleton instance of the CefSharp
			browser = new ChromiumWebBrowser("");

			jToolBar = new JToolbar(this);
			mainForm.Controls.Add(jToolBar);
			
			jProfile = new Profile();
			jProfile.SetName("N062018A001");
			jProfile.SetEnergy(90);
			mainForm.Controls.Add(jProfile);
			
			courseLib = new CourseLib(browser);
			mainForm.Controls.Add(courseLib);
			courseLib.Visible = false;
					
			courseView =  new CourseView(browser);
			mainForm.Controls.Add(courseView);
			courseView.Visible = true;
		}

		public void FormLoaded()
		{
			courseLib.ShowCourseLib();
		}

		public void FormClosed()
		{
			if (courseView != null)
				courseView.Dispose();
			if (courseLib != null)
				//courseLib.Dispose();	//this may break the app when exit, why?		
			browser.Dispose();
            Cef.Shutdown();
		}
		
		public void FormResized(int newHeight, int newWidth)
		{
			jProfile.Top = 0;
			jProfile.Left = 0;
			jProfile.Width = 300;
			jProfile.Height = 100;
			
			jToolBar.Top = 0;
			jToolBar.Left = 300;
			jToolBar.Width = newWidth - 300;
			jToolBar.Height = 100;
			
			courseLib.Width = newWidth;
			courseLib.Height = newHeight - jToolBar.Height - 4;
			courseLib.Top = 100;
			
			courseView.Width = newWidth;
			courseView.Height = newHeight - jToolBar.Height - 4;
			courseView.Top = 100;	
		}

		public void DisplayCourseLib()
		{
			courseView.Visible = false;
			courseLib.Visible = true;
		}
		
		/*void loadVideo()
		{
			var html = @"<!DOCTYPE html>";
			CefSharp.WebBrowserExtensions.LoadHtml(browser,html,"http://localhost/test.html");
		}*/
	}
}
