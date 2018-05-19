/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/2/18
 * 时间: 21:05
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NGO.Pad.Dashboard
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public static int DASHBOARD_HEIGHT = 200;
		public static Rectangle SCREEN_RES;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			SCREEN_RES = DetectScreenResolution();
			this.Width = SCREEN_RES.Width;
			this.Height = DASHBOARD_HEIGHT;
			
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			
		}
		
		private Rectangle DetectScreenResolution() {
			Rectangle resolution = Screen.PrimaryScreen.Bounds;
			return resolution;
			
		}
		
		/// <summary>
		/// prevent console from moving
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MainFormLocationChanged(object sender, EventArgs e)
		{
			this.Location = new Point(0,0);	
		}
		void MainFormSizeChanged(object sender, EventArgs e)
		{
			this.Width = SCREEN_RES.Width;
			this.Height = DASHBOARD_HEIGHT;
		}
	
	}
}
