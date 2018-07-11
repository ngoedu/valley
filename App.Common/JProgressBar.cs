/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/7
 * Time: 15:53
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
	/// Description of JProgressBar.
	/// </summary>
	public partial class JProgressBar : UserControl
	{
		
		private ToolTip toolTip1 = new ToolTip();
		
		public int MaxValue {set; get;}
					
		public JProgressBar()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.MaxValue = 100;
			lblBar.BackColor = Color.Transparent;
			lblBar.Visible = false;
		}
		
		public void SetValue(int value) {
			if (value > MaxValue)
				return;
			float w = (float)value / (float)MaxValue;
			float cw = (float)this.Width - 20;
			this.pictureBox2.Width = (int)(cw * w);
			this.lblBar.Text = value.ToString();
			
			toolTip1.RemoveAll();
			toolTip1.SetToolTip(this.pictureBox2, "能量："+ value.ToString());			
		}
		
		void JProgressBarSizeChanged(object sender, EventArgs e)
		{
			
			this.pictureBox1.Top = 0;
			this.pictureBox1.Left = 0;
			
			this.pictureBox2.Top = 4;
			this.pictureBox2.Left = 3;
			
			this.lblBar.Top = 0;
			this.lblBar.Left = 0;
			
							
			this.lblBar.Width = this.Width;
			this.lblBar.Height = this.Height;
			

			this.pictureBox1.Width = this.Width;
			this.pictureBox1.Height = this.Height ;
			
			this.pictureBox2.Width = 1;
			this.pictureBox2.Height = this.Height - 9 ;
		}
	}
}
