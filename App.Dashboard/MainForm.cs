/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/8
 * Time: 14:01
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CefSharp.WinForms;
using Control.Profile;
using Control.Toolbar;

namespace CefSharp49NuGet
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		private readonly ChromiumWebBrowser browser;
		private JToolbar jToolBar;
		private Profile jProfile;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			browser = new ChromiumWebBrowser("http://localhost:8080/ngoui/select.html");
			browser.Dock = DockStyle.None;
			
			this.Controls.Add(browser);
			
			jToolBar = new JToolbar();
			this.Controls.Add(jToolBar);
			
			jProfile = new Profile();
			jProfile.SetName("ngo062018a01");
			jProfile.SetEnergy(90);
			this.Controls.Add(jProfile);
			
			
			
		}

		void MainFormResize(object sender, EventArgs e)
		{
			jProfile.Top = 0;
			jProfile.Left = 0;
			jProfile.Width = 300;
			jProfile.Height = 100;
			
			jToolBar.Top = 0;
			jToolBar.Left = 300;
			jToolBar.Width = this.ClientSize.Width - 300;
			jToolBar.Height = 100;
			
			browser.Width = this.Width;
			browser.Height = this.ClientSize.Height - jToolBar.Height - 4;
			browser.Top = 100;
		}
	}
}
