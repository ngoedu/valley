/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/7
 * Time: 11:40
 * 
 * 
 */
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using App.Common.Controls;

namespace App.Dashboard
{
	/// <summary>
	/// Description of ExitForm.
	/// </summary>
	public partial class ExitForm : Form
	{
		//Delegate for cross thread call to close
		private delegate void CloseDelegate();
		private static ExitForm exitForm;
		
		LabelProgressBar progressbar;
		
		public ExitForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			progressBar1.Height = this.Height;
			//this.Width = progressBar1.Width;
			timer1.Enabled = true;
			timer1.Start();
			timer1.Interval = 100;
			progressBar1.Maximum = 100;
			timer1.Tick += new EventHandler(timer1_Tick);
		}
		
		
		void timer1_Tick(object sender, EventArgs e)
		{
			if (progressBar1.Value != 100) {
				progressBar1.Value++;
			} else {
				timer1.Stop();
			}
		}
		
		static public void ShowExitScreen()
		{
			// Make sure it is only launched once.
			if (exitForm != null)
				return;
			Thread thread = new Thread(new ThreadStart(ExitForm.ShowForm));
			thread.IsBackground = true;
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();           
		}
	
		static private void ShowForm()
		{
			exitForm = new ExitForm();
			Application.Run(exitForm);
		}
	
		static public void CloseForm()
		{
			exitForm.Invoke(new CloseDelegate(ExitForm.CloseFormInternal));
		}
	
		static private void CloseFormInternal()
		{
			exitForm.Close();
		}
	}
}
