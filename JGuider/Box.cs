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
			BackColor = Color.FromArgb(16,35,56);
			tbCourse.BackColor = Color.FromArgb(255,255,238);
		}
		
		void BoxSizeChanged(object sender, EventArgs e)
		{
			this.picBox.Top = 7;
			this.picBox.Left = 4;
			
			this.tbCourse.Left = this.picBox.Left + this.picBox.Width + 4;
			this.tbCourse.Top = (this.ClientSize.Height - tbCourse.Height) / 2;
			this.tbCourse.Width = this.ClientSize.Width - picBox.Width - 14;
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
