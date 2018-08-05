/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/15
 * Time: 23:44
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NGO.Train;

namespace NGO.Pad.Guider
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class GuiderForm : Form
	{
		private JGuider guider;
		public GuiderForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			guider = new JGuider();
			this.Controls.Add(guider);
			
			guider.BindCourse(BuildCourse());
			this.Size = new Size(250,400);
		}
		
		private Course BuildCourse() {
			
			var course = new Course("Web编程基础A001");
			course.AddMileStone(new Step(1, "添加一个页面","REF",0,"Code", Course.STATUS_DEFAULT));
			course.AddMileStone(new Step(2, "第一条文字","REF",0,"Code", Course.STATUS_DEFAULT));
			course.AddMileStone(new Step(3, "换行试试","REF",0,"Code", Course.STATUS_REFER));
			course.AddMileStone(new Step(4, "原来需要标签","REF",0,"Code", Course.STATUS_DEFAULT));
			course.AddMileStone(new Step(5, "样子丑陋","REF",0,"Code", Course.STATUS_DEFAULT));
			course.AddMileStone(new Step(6, "好多的样式","REF",0,"Code", Course.STATUS_CODE));
			
			return course;
		}
		
		void MainFormSizeChanged(object sender, EventArgs e)
		{
			guider.Top = 2;
			guider.Left = 2;
			guider.Width = this.ClientSize.Width - 4;
			guider.Height = this.ClientSize.Height - 4;
		}
		
		
	}
}
