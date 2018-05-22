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
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			browser = new ChromiumWebBrowser("http://www.bing.com");
			browser.Dock = DockStyle.None;
			
			this.Controls.Add(browser);
			
			jToolBar = new JToolbar();
			this.Controls.Add(jToolBar);
			
			
		}

		void MainFormResize(object sender, EventArgs e)
		{
			jToolBar.Top = 0;
			jToolBar.Left = 0;
			jToolBar.Width = this.ClientSize.Width;
			jToolBar.Height = 120;
			
			browser.Width = this.Width;
			browser.Height = this.ClientSize.Height - jToolBar.Height - 4;
			browser.Top = 124;
		}
	}
}
