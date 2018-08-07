﻿/*
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
using App.Common.Reg;
using NGO.Pad.Editor;
using NGO.Train;

namespace NGO.Pad.Guider
{
	/// <summary>
	/// Description of JGuider.
	/// </summary>
	public partial class JGuider : UserControl, IGuider
	{
		
		NGO.Train.Course course;
		private Box boxCourse;
		private List<MileStone> mileStones = new List<MileStone>();
		private System.Windows.Forms.Panel panelMileStone;
		private WebBrowser refBrowser;
		private TabControl codeTabs;
		public JGuider()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//BackColor = Color.FromArgb(0,138,227);
			
			boxCourse = new Box();
			this.Controls.Add(boxCourse);
			
			panelMileStone = new Panel();
			panelMileStone.AutoScroll = true;
			this.Controls.Add(panelMileStone);
			
			refBrowser = new WebBrowser();
			this.Controls.Add(refBrowser);
			
			codeTabs = new TabControl();
			this.Controls.Add(codeTabs);
		}

		public void Init(App.Common.Reg.AppRegistry reg)
		{
			course = (Course)reg[AppRegKeys.COURSE_KEY];
			this.BindCourse(course);
		}
		
		public void Dispose(AppRegistry reg)
		{
			
		}

		public void ShowRef(int index)
		{
			var ms = course.GetMileStoneByID(index);
			var refInfo = course.GetReferByID(ms.Reference);
			LoadHtml(refInfo.Text);
			this.codeTabs.Visible = false;
			this.refBrowser.Visible = true;
		}
		public void ShowCode(int index)
		{
			var ms = course.GetMileStoneByID(index);
			List<File> srcFiles = ms.SourceFiles;
			
			//clean all page
			foreach (TabPage tp in this.codeTabs.TabPages) {
				this.codeTabs.TabPages.Remove(tp);
			}
			
			//add new files 
			foreach(var file in srcFiles) {
				var page  = new TabPage();
				page.Width = this.codeTabs.ClientSize.Width;
				page.Height = this.codeTabs.ClientSize.Height;
				page.Text = file.Name;
				JEditor.Languages lan = JEditor.CheckLanguage(file.Name.Split('.')[1]);
				var editor = new JEditor(lan);
				editor.Dock = DockStyle.Fill;
				editor.Name = "JEditor";
				editor.AppendText(file.Src);
				page.Controls.Add(editor);
				this.codeTabs.Controls.Add(page);
			}
			
			this.codeTabs.Visible = true;
			this.refBrowser.Visible = false;
		}
		public void ReplicateCode(int index)
		{
			
		}

		private void LoadHtml(string html) {		
			refBrowser.DocumentText="";
			refBrowser.Document.OpenNew(true);
			refBrowser.Document.Write(html);
			refBrowser.Refresh();
		}		
		private void RemoveMileStones() {
			foreach(var ctl in mileStones) {
				this.panelMileStone.Controls.Remove(ctl);
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
				var stone = new MileStone(step.Id, step.Id+"."+step.Name, step.Status, this);
				stone.Top = (stone.Height - 2 ) * index++ ;
				stone.Left = 20;
				mileStones.Add(stone);
				this.panelMileStone.Controls.Add(stone);
			}
		}
		#endregion

		void JGuiderSizeChanged(object sender, EventArgs e)
		{
			boxCourse.Top = 0;
			boxCourse.Left = 0;
			boxCourse.Width = 260;
			
			panelMileStone.Top = boxCourse.Height + 1;
			panelMileStone.Left = 0;
			panelMileStone.Width = boxCourse.Width;
			panelMileStone.Height = this.Height - boxCourse.Height;
			
			for (int i=0; i<mileStones.Count; i++)
			{
				mileStones[i].Top = (mileStones[i].Height - 2 ) * i;
				mileStones[i].Left = 20;			
			}
			
			refBrowser.Top = boxCourse.Top;
			refBrowser.Left = boxCourse.Width;
			refBrowser.Width = this.Width - boxCourse.Width;
			refBrowser.Height = this.Height;
			
			codeTabs.Top = boxCourse.Top;
			codeTabs.Left = boxCourse.Width;
			codeTabs.Width = this.Width - boxCourse.Width;
			codeTabs.Height = this.Height;
		}
	}
}
