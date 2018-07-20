/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/7
 * Time: 15:06
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace App.Common
{
	/// <summary>
	/// Description of GifControl.
	/// </summary>
	public partial class GifControl : UserControl
	{
		private GifImage gifImage = null;
		
		public GifControl(string filePath)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//a) Normal way
			pictureBox1.Image = global::App.Common.Resource1.animated_bg_2;//Image.FromFile(filePath);
	
	        //b) We control the animation
	        gifImage = new GifImage(filePath);
	        gifImage.ReverseAtEnd = false; //dont reverse at end
	        
	        timer1.Interval = 12000;
	        timer1.Enabled = true;
	        
	        this.TabStop = false;
		}
		
		
		
		void Timer1Tick(object sender, EventArgs e)
		{
			pictureBox1.Image = gifImage.GetNextFrame();
		}
		void GifControlSizeChanged(object sender, EventArgs e)
		{
			pictureBox1.Left = 0;
			pictureBox1.Top = 0;
			pictureBox1.Width = this.Width;
	        pictureBox1.Height = this.Height;
		}
	}
}
