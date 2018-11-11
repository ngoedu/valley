/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/15
 * Time: 23:57
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NGO.Pad.Guider
{
	/// <summary>
	/// Description of Box.
	/// </summary>
	public partial class Box : UserControl, IBox
	{
		public Box()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//BackColor = Color.FromArgb(109,125,143);
			BackColor = Color.FromArgb(93,93,93);
			tbCourse.BackColor = Color.FromArgb(255,255,238);
		}
		
		void BoxSizeChanged(object sender, EventArgs e)
		{
			
			this.tbCourse.Left =  6;
			this.tbCourse.Top = (this.ClientSize.Height - tbCourse.Height) / 2;
			this.tbCourse.Width = this.ClientSize.Width - 16;
		}

		#region ICourse implementation

		public void SetName(string course)
		{
			this.tbCourse.Text = course;
		}
		void BoxLoad(object sender, EventArgs e)
		{
	
		}

		#endregion
	}
}
