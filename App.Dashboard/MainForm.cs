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
using System.Threading;
using System.Windows.Forms;
using App.Forms;
using App.Mediator;
using CefSharp;
using CefSharp.WinForms;
using Component.TinyServer;
using Control.Profile;
using Control.Toolbar;

namespace CefSharp49NuGet
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		
		
		private IMediator mediator;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//init mediator
			mediator = new SimpleMediator(this);
		}

		void MainFormResize(object sender, EventArgs e)
		{
			mediator.FormResized(this.ClientSize.Height, this.ClientSize.Width);
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			mediator.FormLoaded();
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			mediator.FormClosed();
            //Application.Exit();
            Environment.Exit(0); //this works like a charm
		}
	}
}
