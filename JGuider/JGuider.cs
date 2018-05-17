/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/15
 * Time: 23:51
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NGO.Train;

namespace NGO.Pad.Guider
{
	/// <summary>
	/// Description of JGuider.
	/// </summary>
	public partial class JGuider : UserControl, IGuider
	{
		private Box boxCourse;
		private List<MileStone> mileStones = new List<MileStone>();
		public JGuider()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//BackColor = Color.FromArgb(0,138,227);
			
			boxCourse = new Box();
			this.Controls.Add(boxCourse);
			
		}
		
		private void RemoveMileStones() {
			foreach(Control ctl in mileStones) {
				this.Controls.Remove(ctl);
			}
			mileStones.Clear();
		}

		#region IGuider implementation
		public void BindCourse(NGO.Train.Course course)
		{
			boxCourse.SetName(course.Name);
			RemoveMileStones();
			List<Step> steps = course.GetMileStones();
			int index = 0;
			foreach (Step step in steps)
			{
				var stone = new MileStone(step.Name);
				stone.Top = (stone.Height - 2 ) * index++ + boxCourse.Top + boxCourse.Height;
				stone.Left = 20;
				mileStones.Add(stone);
				this.Controls.Add(stone);
			}
		}
		#endregion

		void JGuiderSizeChanged(object sender, EventArgs e)
		{
			boxCourse.Top = 2;
			boxCourse.Left = 2;
			boxCourse.Width = this.Width - 4;
			
			for (int i=0; i<mileStones.Count; i++)
			{
				var stone = new MileStone("Web页面设计 A0"+i);
				mileStones[i].Top = (stone.Height - 2 ) * i + boxCourse.Top + boxCourse.Height;
				stone.Left = 20;
				
			}
		}
	}
}
