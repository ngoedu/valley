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
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using App.Common.Net;
using App.Common.Reg;
using Control.Eide;
using DiffMatchPatch;
using NGO.Pad.Editor;
using NGO.Train;
using NGO.Train.Entity;

namespace NGO.Pad.Guider
{
	/// <summary>
	/// Description of JGuider.
	/// </summary>
	public partial class JGuider : UserControl, IGuider
	{
		
		NGO.Train.Entity.Course course;
		private Box boxCourse;
		private List<MileStone> mileStones = new List<MileStone>();
		private System.Windows.Forms.Panel panelMileStone;
		private WebBrowser refBrowser;
		private TabControl codeTabs;
		private IClient eideClient;
		private AppStatus status;
		private TrainingSession session;
		
		public JGuider()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			BackColor = Color.FromArgb(173,198,227);
			
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

		public void SetAppTitleCallback(IAppTitleCallback callback)
		{
			//DO nothing
		}
		public void Init(App.Common.Reg.AppRegistry reg)
		{
			course = (Course)reg[AppRegKeys.COURSE_KEY];
			
			session = (TrainingSession)reg[AppRegKeys.COURSE_TRAINSESSION_OBJ];
			foreach (var ms in session.Progress) {
				course.Milestons[ms.MSID - 1].Status =  ms.Status;
			}
			
			this.BindCourse(course);
			
			eideClient = (IClient)reg[AppRegKeys.AETHER_CLIENT];
			this.status  = AppStatus.Inited;
		}
		
		public void Active()
		{
			
		}
		
		public void Inactive()
		{
			
		}
		
		public void Reload(AppRegistry reg)
		{
			this.Dispose(reg);
			if (this.status == AppStatus.Disposed) {
				this.Init(reg);
			}
		}
		
		public void Dispose(AppRegistry reg)
		{
			//TODO: copy milestone change
			session.Progress = new List<MsStatus>();
			foreach (var ms in course.Milestons) {
				session.Progress.Add(new MsStatus(ms.ID, ms.Status));
			}
			
			//persist training session into file
			TrainingSession.WriteSessionToFile(session, (string)reg[AppRegKeys.COURSE_TRAINSESSION]);
			
			this.status = AppStatus.Disposed;
		}

		public AppStatus Status()
		{
			return this.status;
		}
		public void ShowRef(int index)
		{
			var ms = course.GetMileStoneByID(index);
			ms.Status = Course.STATUS_REFER;
			var refInfo = course.GetReferByID(ms.RefID);
			LoadHtml(refInfo.Content);
			this.codeTabs.Visible = false;
			this.refBrowser.Visible = true;
		}
		public void ShowCode(int index)
		{
			var ms = course.GetMileStoneByID(index);
			ms.Status = Course.STATUS_CODE;
			
			List<NGO.Train.Entity.File> srcFiles = ms.Files;
			
			//clean all page
			foreach (TabPage tp in this.codeTabs.TabPages) {
				this.codeTabs.TabPages.Remove(tp);
			}
			
			//add new files 
			foreach(var file in srcFiles) {
				var page  = new TabPage();
				page.Dock = DockStyle.Fill;
				this.codeTabs.Controls.Add(page);
				page.Text = file.Name;
				JEditor.Languages lan = JEditor.CheckLanguage(file.Name.Split('.')[1]);
				var editor = new JEditor(lan, false);
				editor.BorderStyle = BorderStyle.None;
				//editor.Dock = DockStyle.Fill;
				//editor.Width = page.ClientSize.Width;
				//editor.Height = page.ClientSize.Height;
				editor.Name = "JEditor";
				editor.AcceptText(file.Code);
				
				//TODO: create diff and render in JEditor
				if (file.IsPatch()) {
		   			//System.Diagnostics.Debug.WriteLine("MarkupText for file={0}", file.Name);
		   			editor.MarkupText(file.Diff);
				}
				
				page.Controls.Add(editor);
				editor.Width = page.ClientSize.Width ;
				editor.Height = page.ClientSize.Height - 25;
			}
			
			this.codeTabs.Visible = true;
			this.refBrowser.Visible = false;
		}
		
		public void ReplicateCode(int index)
		{
			var revision = course.GetMileStoneByID(index);
			revision.Status = Course.STATUS_SAVE;
			
			string xmlRevision = revision.ToXml();
			
			string mileStoneCmd = "$MILESTONE="+xmlRevision;
			string response = eideClient.SendToRemoteSync(mileStoneCmd, JEide.ENDPOINT_ID);
			
			var eideResponse = EideResponse.Parse(response);
			if (eideResponse.status.Equals(EideResponse.STATUS_OK) && eideResponse.natid == ClientConst.NAT_GUIDER_ID)
				System.Diagnostics.Debug.WriteLine("[EIDE] rev"+index+" is sucessfully synced to eide workspace.");
			else {
				System.Diagnostics.Debug.WriteLine("[EIDE] rev"+index+" sync failed - " + response);
				MessageBox.Show("[EIDE] rev"+index+" sync failed - " + response);
			}
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
		public void BindCourse(NGO.Train.Entity.Course course)
		{
			boxCourse.SetName(course.Schema.Name);
			RemoveMileStones();
			List<Revision> steps = course.Milestons;
			int index = 0;
			foreach (var step in steps)
			{
				var stone = new MileStone(step.ID, step.ID+"."+step.Title, step.Status, this);
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
			boxCourse.Width = 220;
			
			panelMileStone.Top = boxCourse.Top + boxCourse.Height + 1;
			panelMileStone.Left = 0;
			panelMileStone.Width = boxCourse.Width;
			panelMileStone.Height = this.Height - boxCourse.Height - boxCourse.Top;
			
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
			
			foreach (TabPage tp in this.codeTabs.TabPages) {
				tp.Width = codeTabs.ClientSize.Width;
				tp.Height = codeTabs.ClientSize.Height;
				tp.Controls["JEditor"].Width = tp.ClientSize.Width ;
				tp.Controls["JEditor"].Height = tp.ClientSize.Height - 25;
				
			}
		}
	}
}
