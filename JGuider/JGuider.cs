/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/15
 * Time: 23:51
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
	/// Description of JGuider.
	/// </summary>
	public partial class JGuider : UserControl, IGuider
	{
		private Box boxCourse;
		private MileStone mileStone;
		public JGuider()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			BackColor = Color.FromArgb(0,138,227);
			
			boxCourse = new Box();
			mileStone = new MileStone();
			this.Controls.Add(boxCourse);
			this.Controls.Add(mileStone);
		}

		#region IGuider implementation
		public void BindCourse(NGO.Train.Course course)
		{
			boxCourse.SetName(course.Name);
		}
		#endregion

		void JGuiderSizeChanged(object sender, EventArgs e)
		{
			boxCourse.Top = 2;
			boxCourse.Left = 2;
			boxCourse.Width = this.Width - 4;
			
			mileStone.Top = boxCourse.Height + boxCourse.Top + 4;
			mileStone.Left = boxCourse.Left;
			mileStone.Width = boxCourse.Width - 2;
			mileStone.Height = this.Height - 10 - boxCourse.Height;
			
		
		}
	}
}
