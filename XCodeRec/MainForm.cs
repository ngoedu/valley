/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/5/2
 * 时间: 21:19
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using NGO.Train;
using NGO.Train.Entity;


namespace XCodeRec
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		private static string CodeFolder = @"D:\NGO\client\cdat\sweb-a01";//@"c:\ngo\client\cdat\sweb-a01";
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
			
			
		}
		
		private void ResizeControl() {
			
		}

		
		void Button2Click(object sender, EventArgs e)
		{
			
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			ResizeControl();
			lvVideo.Items.Clear();
		    lvVideo.Columns.Add("ID", -2,HorizontalAlignment.Left);
		    lvVideo.Columns.Add("Link", -2, HorizontalAlignment.Left);
		    
		    lvRef.Items.Clear();
		    lvRef.Columns.Add("ID", -2,HorizontalAlignment.Left);
		    lvRef.Columns.Add("Text", -2, HorizontalAlignment.Left);
		    
		    lvMileStones.Items.Clear();
		    lvMileStones.Columns.Add("ID", -2,HorizontalAlignment.Left);
		    lvMileStones.Columns.Add("LinkID", -2, HorizontalAlignment.Left);
		   	lvMileStones.Columns.Add("RefID", -2, HorizontalAlignment.Left);
		   
		}
		
		void BtVideoAddClick(object sender, EventArgs e)
		{
			string[] lvData = new string[2];
			lvData[0] = tbVideoID.Text;
			lvData[1] = rtbVideoLink.Text;
            ListViewItem lvItem = new ListViewItem(lvData, 0);
            
            var vlink = new VLink(Int16.Parse(lvData[0] ), "", lvData[1] );
            lvItem.Tag = vlink;
            
            lvVideo.Items.Add(lvItem);
	
		}
		void TabPage4Click(object sender, EventArgs e)
		{
	
		}
		void BtnRefAddClick(object sender, EventArgs e)
		{
			string[] lvData = new string[2];
			lvData[0] = tbRefID.Text;
			lvData[1] = rtbRefText.Text;
            ListViewItem lvItem = new ListViewItem(lvData, 0);
			lvRef.Items.Add(lvItem);
		}
		void BtnPath1Click(object sender, EventArgs e)
		{
			using(var fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = CodeFolder;
			    DialogResult result = fbd.ShowDialog();
			
			    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
			    {
			        this.tbMSSrcPath.Text = fbd.SelectedPath;
			    }
			}
		}
		void BtnPath2Click(object sender, EventArgs e)
		{
			using(var fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = CodeFolder;
			    DialogResult result = fbd.ShowDialog();
			
			    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
			    {
			        this.tbPackOutputFile.Text = fbd.SelectedPath + @"\pack.dat";
			    }
			}
		}
		void BtnLoadPackageClick(object sender, EventArgs e)
		{
			if (!System.IO.File.Exists(this.tbPackOutputFile.Text))
	    	{
				MessageBox.Show("file "+this.tbPackOutputFile.Text + " does not exit.");
	    		return;
	    	}
			
			this.LoadPackage(this.tbPackOutputFile.Text);
			
		}
		
		private void LoadPackage(string packFile) {
			Course course = CourseReader.Instance.ReadFromFile(packFile);
			
			this.tbSchemaID.Text = course.Schema.ID.ToString();
			this.tbSchemaName.Text = course.Schema.Name;
			this.tbScheamWs.Text = course.Schema.Workspace;
			
			
			//this.clbApp.Items
			for (int i = 0; i < clbApp.Items.Count; i++)
			{
				var aid = clbApp.GetItemText(clbApp.Items[i]);
				if (course.Apps.Any(x => x.TileID == aid))
			    {
			       clbApp.SetItemChecked(i, true); 
			    }
			}
			
			//add video items
			lvVideo.Items.Clear();
			foreach (var v in course.Videos) {
				string[] lvData = new string[2];
				lvData[0]= v.ID.ToString();
				lvData[1] = v.Content;
				ListViewItem lvItem = new ListViewItem(lvData, 0);
				lvItem.Tag = v;
				lvVideo.Items.Add(lvItem);
			}
			
			//add refer items
			lvRef.Items.Clear();
			foreach (var r in course.Refs) {
				string[] lvData = new string[2];
				lvData[0]= r.ID.ToString();
				lvData[1] = r.Content;
				ListViewItem lvItem = new ListViewItem(lvData, 0);
				lvItem.Tag = r;
				lvRef.Items.Add(lvItem);
			}
			
			
			//add refer items
			lvMileStones.Items.Clear();
			foreach (var rev in course.Milestons) {
				string[] lvData = new string[3];
				lvData[0]= rev.ID.ToString();
				lvData[1] = rev.LinkID.ToString();
				lvData[2] = rev.RefID.ToString();
				
				ListViewItem lvItem = new ListViewItem(lvData, 0);
				lvItem.Tag = rev;
				lvMileStones.Items.Add(lvItem);
			}
			
		}
		void BtnVideoSaveClick(object sender, EventArgs e)
		{
			VLink v = new VLink(Int16.Parse(tbVideoID.Text),"", rtbVideoLink.Text);
			var item = lvVideo.FindItemWithText(tbVideoID.Text);
			item.SubItems[1].Text = rtbVideoLink.Text;
			item.Tag = v;
		}
		void LvVideoItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			VLink v = (VLink)e.Item.Tag;
			rtbVideoLink.Text = v.Content;
			tbVideoID.Text = v.ID.ToString();
		}
		void BtnRefSaveClick(object sender, EventArgs e)
		{
			Refer r = new Refer(Int16.Parse(tbRefID.Text), "", rtbRefText.Text);
			var item = lvRef.FindItemWithText(tbRefID.Text);
			item.SubItems[1].Text = rtbRefText.Text;
			item.Tag = r;
		}
		void LvRefItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			Refer r = (Refer)e.Item.Tag;
			rtbRefText.Text = r.Content;
			tbRefID.Text = r.ID.ToString();
		}
		
		//create MS folders
		void Button1Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbMSoutPath.Text) || string.IsNullOrEmpty(tbMSSrcPath.Text))
			{
				MessageBox.Show("MileStone src or output path empty！");
				return;
			}
			
			var directories = Directory.GetDirectories(tbMSoutPath.Text);
			var maxDir = directories.Length	 ==0 ? "0" : directories.Last();
			string[] sd = maxDir.Split('\\');
			maxDir = sd[sd.Length - 1];
			int curr = Int16.Parse(maxDir) + 1;
			
			string[] ignore1 = {".project", ".classpath"};
			string[] ignore2 = {"classes",  ".settings"};
			FolderCopy.DirectoryCopy(tbMSSrcPath.Text,tbMSoutPath.Text+ @"\"+curr, true, ignore1, ignore2);
			MessageBox.Show("MS folder copy done!");
		}
		
		void BtnMSoutPathClick(object sender, EventArgs e)
		{
			using(var fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = CodeFolder;
			    DialogResult result = fbd.ShowDialog();
			
			    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
			    {
			        this.tbMSoutPath.Text = fbd.SelectedPath;
			    }
			}
		}
		void BtnGenMSClick(object sender, EventArgs e)
		{
			//treeViewMS
		}
		void BtnBuildMSClick(object sender, EventArgs e)
		{
			var directories = Directory.GetDirectories(tbMSoutPath.Text);
			
			if (directories.Length == 0)
			{	
				MessageBox.Show("Milestone src folder empty!");
				return;
			}
			
			var firstRev = RevBuilder.FirstRevision(directories.First());
			AddMileStoneRev(firstRev);
			if (directories.Length == 1)
			{
				return;
			}
			
			for (int i = 1; i< directories.Length; i++) {
				var rev = RevBuilder.NextRevision(i+1, directories[i-1], directories[i]);
				AddMileStoneRev(rev);	
			}
		}
		
		private void AddMileStoneRev(Revision rev) {
			string[] lvData = new string[3];
			lvData[0] = rev.ID.ToString();
			lvData[1] = "";
			lvData[2] = "";
			
            ListViewItem lvItem = new ListViewItem(lvData, 0);
            lvItem.Tag = rev;
			lvMileStones.Items.Add(lvItem);
		}
		void LvMileStonesSelectedIndexChanged(object sender, EventArgs e)
		{
			
		}
		void LvMileStonesItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			Revision rev = (Revision)e.Item.Tag;
			
			tbMSID.Text = rev.ID.ToString();
			tbMSLinkID.Text = rev.LinkID.ToString();
			tbMSRefID.Text = rev.RefID.ToString();
			tbMSTitle.Text = rev.Tile;
			
			rtbFiles.Text = rev.ToString();
			
		}
	}
}
