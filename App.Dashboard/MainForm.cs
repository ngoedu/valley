/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/8
 * Time: 14:01
 * 
 * 
 */
using System;
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
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//show splash screen
			SplashForm.ShowSplashScreen();
			
			//init mediator
			mediator = new SimpleMediator(this);
		}

		void MainFormResize(object sender, EventArgs e)
		{
			mediator.FormResized(this.ClientSize.Height, this.ClientSize.Width);
			SplashForm.CloseForm();
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
