/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/2/9
 * 时间: 23:05
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace NGO.Pad.CefSharp
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private readonly ChromiumWebBrowser browser;

		public MainForm()
		{
		
            InitializeComponent();

            Text = "CefSharp";
            WindowState = FormWindowState.Maximized;

            browser = new ChromiumWebBrowser("www.baidu.com")
            {
                Dock = DockStyle.Fill,
            };
            
            
            this.Controls.Add(browser);
		}
	}
}
