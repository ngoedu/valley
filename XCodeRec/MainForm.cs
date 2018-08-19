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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using NGO.Train;


namespace XCodeRec
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		private static string CodeFolder = @"c:\ngo\client\cdat";
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
		   
		}
		
		void BtVideoAddClick(object sender, EventArgs e)
		{
			string[] lvData = new string[2];
			lvData[0] = txtVideoID.Text;
			lvData[1] = rtbVideoLink.Text;
            ListViewItem lvItem = new ListViewItem(lvData, 0);
			lvVideo.Items.Add(lvItem);
	
		}
		void TabPage4Click(object sender, EventArgs e)
		{
	
		}
		void BtnRefAddClick(object sender, EventArgs e)
		{
			string[] lvData = new string[2];
			lvData[0] = txtRefID.Text;
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
			        //string[] files = Directory.GetFiles(fbd.SelectedPath);
			        this.txtSrcPath.Text = fbd.SelectedPath;
			        //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
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
			        this.txtOutputFile.Text = fbd.SelectedPath + @"\pack.dat";
			    }
			}
		}
		void BtnLoadPackageClick(object sender, EventArgs e)
		{
			if (!System.IO.File.Exists(this.txtOutputFile.Text))
	    	{
	    		System.IO.File.Create(this.txtOutputFile.Text).Dispose();
	    		return;
	    	}
			
			this.LoadPackage(this.txtOutputFile.Text);
			
		}
		
		private void LoadPackage(string pack) {
			string packDate = System.IO.File.ReadAllText(pack);
			Course course = CourseLoader.Instance.Load(packDate, "");
			
			this.tbSchemaID.Text = course.ID;
			this.tbSchemaName.Text = course.Name;
			this.tbScheamWs.Text = course.Workspace;
			
			//this.clbApp.Items
			for (int i = 0; i < clbApp.Items.Count; i++)
			{
				var aid = clbApp.GetItemText(clbApp.Items[i]);
				if (course.GetApps().Any(x => x.ID == aid))
			    {
			       clbApp.SetItemChecked(i, true); 
			    }
			}
		}
	}
}
