/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-04-20
 * Time: 7:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Control.Files
{
	/// <summary>
	/// https://stackoverflow.com/questions/2883289/c-sharp-input-dialog-as-a-function
	/// </summary>
	public partial class CreateDialog : Form
	{
		public CreateDialog()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			InitListView();

		}
		
		private void InitListView() {
			lvTypes.Items.Clear();
		    // Add columns
		    lvTypes.Columns.Add("Name", -2,HorizontalAlignment.Left);
		   
		    var imageList = new ImageList();
        	imageList.ImageSize = new Size(64, 64);  
        	imageList.ColorDepth = ColorDepth.Depth32Bit;
        
        	imageList.Images.Add("html", global::Control.JFiles.Resource1.html);
		    imageList.Images.Add("js", global::Control.JFiles.Resource1.js);
		    imageList.Images.Add("css", global::Control.JFiles.Resource1.css);
		    
		    //lvTypes.SmallImageList = imageList;
		    lvTypes.LargeImageList = imageList;
		    
		    string[] lvData = new string[1];
		    lvData[0] = "html";
		    ListViewItem lvItem = new ListViewItem(lvData, 0);
			//lvItem.Tag = "";
			lvItem.ImageKey="html"; 
			lvTypes.Items.Add(lvItem);
			
			lvData[0] = "javascript";
		    lvItem = new ListViewItem(lvData, 0);
			//lvItem.Tag = "";
			lvItem.ImageKey="js"; 
			lvTypes.Items.Add(lvItem);
			
			lvData[0] = "css";
		    lvItem = new ListViewItem(lvData, 0);
			//lvItem.Tag = "";
			lvItem.ImageKey="css"; 
			lvTypes.Items.Add(lvItem);
		}
		void Button1Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
        	Close();
		}

		void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			if (this.textBox1.Text.Length > 0)
				this.button1.Enabled = true;
		}
		void CreateDialogLoad(object sender, EventArgs e)
		{
	
		}
	}
}
