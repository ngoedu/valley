/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/7
 * Time: 10:38
 * 
 * 
 */
using System;
using System.Threading;
using System.Windows.Forms;

namespace App.Dashboard
{
	/// <summary>
	/// Description of SplashForm.
	/// </summary>
	class SplashForm : Form
	{
	    //Delegate for cross thread call to close
	    private delegate void CloseDelegate();
	
	    //The type of form to be displayed as the splash screen.
	    private static SplashForm splashForm;
		private System.Windows.Forms.PictureBox pic;
		
	    public SplashForm()
		{
	    	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
	    	this.Width = 452;
	    	this.Height = 302;
	    	this.pic = new System.Windows.Forms.PictureBox();
	    	this.pic.Image = global::App.Dashboard.Resource1.splash;
			this.pic.Location = new System.Drawing.Point(0, 0);
			this.pic.Name = "Splash";
			this.pic.Size = new System.Drawing.Size(452, 302);
			this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pic.TabIndex = 0;
			this.pic.TabStop = false;
			this.Controls.Add(this.pic);
			this.StartPosition = FormStartPosition.CenterScreen;
	    }
	    
	    static public void ShowSplashScreen()
	    {
	        // Make sure it is only launched once.
	
	        if (splashForm != null)
	            return;
	        Thread thread = new Thread(new ThreadStart(SplashForm.ShowForm));
	        thread.IsBackground = true;
	        thread.SetApartmentState(ApartmentState.STA);
	        thread.Start();           
	    }
	
	    static private void ShowForm()
	    {
	        splashForm = new SplashForm();
	        Application.Run(splashForm);
	    }
	
	    static public void CloseForm()
	    {
	        splashForm.Invoke(new CloseDelegate(SplashForm.CloseFormInternal));
	    }
	
	    static private void CloseFormInternal()
	    {
	        splashForm.Close();
	    }
	}
}
