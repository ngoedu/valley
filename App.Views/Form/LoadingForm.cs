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

namespace App.Views
{
	/// <summary>
	/// Description of ExitForm.
	/// </summary>
	public partial class LoadingForm : Form
	{
		//Delegate for cross thread call to close
		private delegate void CloseDelegate();
		private static LoadingForm loadingForm;
		
		LabelProgressBar progressbar;
		
		public LoadingForm()
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
		
		static public void ShowLoadingScreen()
		{
			// Make sure it is only launched once.
			if (loadingForm != null)
				return;
			Thread thread = new Thread(new ThreadStart(LoadingForm.ShowForm));
			thread.IsBackground = true;
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();           
		}
	
		static private void ShowForm()
		{
			loadingForm = new LoadingForm();
			Application.Run(loadingForm);
		}
	
		static public void CloseForm()
		{
			loadingForm.Invoke(new CloseDelegate(LoadingForm.CloseFormInternal));
		}
	
		static private void CloseFormInternal()
		{
			loadingForm.Close();
		}
	}
}
