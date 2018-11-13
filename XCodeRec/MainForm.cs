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
		
		private static string DefaultCodeFolder = @"D:\NGO\course\dist\";//@"c:\ngo\client\cdat\sweb-a01";
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
		
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
		    lvMileStones.Columns.Add("Title", -2, HorizontalAlignment.Left);			
			
		}
		
		private void ResizeControl() {
			
		}

		void MainFormLoad(object sender, EventArgs e)
		{
			ResizeControl();
		}
		
		
		void BtnPath1Click(object sender, EventArgs e)
		{
			using(var fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = DefaultCodeFolder;
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
				fbd.SelectedPath = DefaultCodeFolder;
			    DialogResult result = fbd.ShowDialog();
			
			    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
			    {
			        this.tbPackOutputFile.Text = fbd.SelectedPath + @"\pack.dat";
			    }
			}
		}
		
		void BtnMSoutPathClick(object sender, EventArgs e)
		{
			using(var fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = DefaultCodeFolder;
			    DialogResult result = fbd.ShowDialog();
			
			    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
			    {
			        this.tbPkgInPath.Text = fbd.SelectedPath;
			    }
			}
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

		void BtnRefAddClick(object sender, EventArgs e)
		{
			string[] lvData = new string[2];
			lvData[0] = tbRefID.Text;
			lvData[1] = rtbRefText.Text;
            ListViewItem lvItem = new ListViewItem(lvData, 0);
			lvRef.Items.Add(lvItem);
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
			Course course = CourseReader.Instance.ReadCourseFromFile(packFile);
			
			this.tbSchemaID.Text = course.Schema.ID.ToString();
			this.tbSchemaName.Text = course.Schema.Name;
			this.tbScheamWs.Text = course.Schema.Workspace;
			this.tbSchemaProj.Text = course.Schema.ProjName;
			this.tbSchemaDur.Text = course.Schema.Duration.ToString();
			this.tbSchemaSess.Text = course.Schema.Sessions.ToString();
			this.tbSchemaMS.Text = course.Schema.Milestones.ToString();
			this.tbSchemaLevel.Text = course.Schema.Level.ToString();
			
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
				string[] lvData = new string[4];
				lvData[0]= rev.ID.ToString();
				lvData[1] = rev.LinkID.ToString();
				lvData[2] = rev.RefID.ToString();
				lvData[3] = rev.Title;
				
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
		void BtnBuildMSFolderCopy(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbPkgInPath.Text) || string.IsNullOrEmpty(tbMSSrcPath.Text))
			{
				MessageBox.Show("MileStone src or output path empty！");
				return;
			}
			
			var directories = Directory.GetDirectories(tbPkgInPath.Text+@"\ms");
			var maxDir = directories.Length	 ==0 ? "0" : directories.Last();
			string[] sd = maxDir.Split('\\');
			maxDir = sd[sd.Length - 1];
			int curr = Int16.Parse(maxDir) + 1;
			
			string[] ignore1 = {".project", ".classpath"};
			string[] ignore2 = {"classes",  ".settings"};
			XTendLibs.FolderCopy.DirectoryCopy(tbMSSrcPath.Text,tbPkgInPath.Text+ @"\ms\"+curr, true, ignore1, ignore2);
			MessageBox.Show("MS folder copy done!");
		}
		
		
		
		//update ms
		void BtnUpdateMSClick(object sender, EventArgs e)
		{
			var item = lvMileStones.FindItemWithText(tbMSID.Text);
			Revision rev = (Revision)item.Tag;
			rev.RefID = Int16.Parse(tbMSRefID.Text);
			rev.LinkID = Int16.Parse(tbMSLinkID.Text);
			rev.Title = tbMSTitle.Text;
					
				
			item.SubItems[1].Text =tbMSLinkID.Text;
			item.SubItems[2].Text =tbMSRefID.Text;
			item.SubItems[3].Text =tbMSTitle.Text;
		}
		
		
		void BtnBuildMSClick(object sender, EventArgs e)
		{
			var directories = Directory.GetDirectories(tbPkgInPath.Text+"/ms");
			
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
			//lalalala
			var item = lvMileStones.FindItemWithText(rev.ID.ToString());
			if (item == null) {
				string[] lvData = new string[4];
				lvData[0] = rev.ID.ToString();
				lvData[1] = "";
				lvData[2] = "";
				lvData[3] = "";
				ListViewItem lvItem = new ListViewItem(lvData, 0);
	            lvItem.Tag = rev;
				lvMileStones.Items.Add(lvItem);
			} else {
				item.Tag = rev;
				
				rev.ID = Int16.Parse(tbMSID.Text);
				rev.LinkID = Int16.Parse(tbMSLinkID.Text);
				rev.RefID = Int16.Parse(tbMSRefID.Text) ;
				rev.Title = tbMSTitle.Text;
				
				
				tbMSID.Text = rev.ID.ToString();
				tbMSLinkID.Text = rev.LinkID.ToString();
				tbMSRefID.Text = rev.RefID.ToString();
				tbMSTitle.Text = rev.Title;
				
				rtbFiles.Text = rev.ToString();
			}
			
            
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
			tbMSTitle.Text = rev.Title;
			
			rtbFiles.Text = rev.ToString();
			
		}
		void BtnBuildVideosClick(object sender, EventArgs e)
		{
			var files =Directory.GetFiles(tbPkgInPath.Text+ @"\video\");
			for (int i = 0; i< files.Length; i++) {
				
				var id = Path.GetFileNameWithoutExtension(files[i]);
				var vlink = new VLink(Int16.Parse(id), "", System.IO.File.ReadAllText(files[i]));
				AddVideo(vlink);
			}
		}
		
		private void AddRefs(Refer refer) {
			string[] lvData = new string[2];
			lvData[0] = refer.ID.ToString();
			lvData[1] = refer.Content;
			
            ListViewItem lvItem = new ListViewItem(lvData, 0);
            lvItem.Tag = refer;
			lvRef.Items.Add(lvItem);
		}
		
		private void AddVideo(VLink vlink) {
			string[] lvData = new string[2];
			lvData[0] = vlink.ID.ToString();
			lvData[1] = vlink.Content;
			
            ListViewItem lvItem = new ListViewItem(lvData, 0);
            lvItem.Tag = vlink;
			lvVideo.Items.Add(lvItem);
		}
		void BtnBuildRefsClick(object sender, EventArgs e)
		{
			var files =Directory.GetFiles(tbPkgInPath.Text+ @"\ref\");
			lvRef.Items.Clear();
			for (int i = 0; i< files.Length; i++) {
				
				var id = Path.GetFileNameWithoutExtension(files[i]);
				var refer = new Refer(Int16.Parse(id), "", System.IO.File.ReadAllText(files[i]));
				AddRefs(refer);
			}
		}
		void BtnGenPkgClick(object sender, EventArgs e)
		{
			var pkg = new Course();
			var schema = new Schema();
			
			//schema
			schema.ID = tbSchemaID.Text;
			schema.Name = tbSchemaName.Text;
			schema.Duration = Int16.Parse(tbSchemaDur.Text);
			schema.Sessions = Int16.Parse(tbSchemaSess.Text);
			schema.Workspace = tbScheamWs.Text;
			schema.ProjName = this.tbSchemaProj.Text;
			schema.Milestones = Int16.Parse(tbSchemaMS.Text);
			schema.Level = Int16.Parse(tbSchemaLevel.Text);
			
			pkg.Schema = schema;
			
			//app
			pkg.Apps = new List<Tile>();
			
			for (int i = 0; i < clbApp.Items.Count; i++)
			{
				if (clbApp.GetItemChecked(i))
			    {
					pkg.Apps.Add(new Tile( clbApp.GetItemText(clbApp.Items[i])));
			    }
			}
			
			//video
			pkg.Videos = new List<VLink>();
			for (int i = 0; i < lvVideo.Items.Count; i++)
			{
				pkg.Videos.Add((VLink)lvVideo.Items[i].Tag);
			}
			
			//ref
			pkg.Refs = new List<Refer>();
			for (int i = 0; i < lvRef.Items.Count; i++)
			{
				pkg.Refs.Add((Refer)lvRef.Items[i].Tag);
			}
			
			//milestone
			pkg.Milestons = new List<Revision>();
			for (int i = 0; i < lvMileStones.Items.Count; i++)
			{
				pkg.Milestons.Add((Revision)lvMileStones.Items[i].Tag);
			}
			
			CourseWriter.Instance.WriteCourseToFile(pkg, tbPackOutputFile.Text);
			
			MessageBox.Show("pack.dat has been serilized!");
		}
	}
}
