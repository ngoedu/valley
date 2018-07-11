/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/8
 * Time: 14:01
 * 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using App.Mediator;


namespace App.Dashboard
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{

		private readonly IMediator mediator;
		private SplashForm splash;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//show splash screen
			splash = new SplashForm();
			splash.ShowSplashScreen();
			
			//init mediator
			mediator = new SimpleMediator(this);
		}

		void MainFormResize(object sender, EventArgs e)
		{
			mediator.FormResized(this.ClientSize.Height, this.ClientSize.Width);
			splash.CloseForm();
			
			//prevent user from resize the main-form
			this.MinimumSize = new Size(this.Width, this.Height);
			this.Location = new Point(0, 0);
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			mediator.FormLoaded();
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			ExitForm.ShowExitScreen();
			mediator.FormClosed();
            ExitForm.CloseForm();

            Environment.Exit(0); //this works like a charm
		}
	}
}
