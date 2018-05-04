/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-04-19
 * Time: 3:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace NGO.Pad.JFiles
{
	/// <summary>
	/// Description of JBrowser.
	/// </summary>
	public partial class JFiles : UserControl, IFiles
	{
		private Navigator navigator;
		
		public string RootPath;
		public string CurrentPath;
		private static string ROOT_NAME = "Root";
		private IFileHandler fileHandler;
		public JFiles(string root, IFileHandler handler)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			RootPath = root;
			CurrentPath = RootPath;
			fileHandler = handler;
			
			navigator = new Navigator(this);
			Controls.Add(navigator);
			
			Resize();
			InitListView();
			
			NavigateTo(ROOT_NAME, RootPath);
		}
		
		public void NavigateTo(string name, string path) {
			Refresh(path);
			navigator.AddPathNode(name, path);
		}
		
		public void Refresh(string path) {
			CurrentPath = path;
			PopulateFiles(path);
		}
		
		public void RefreshCurrent() {
			PopulateFiles(CurrentPath);
		}
		
		public string GetRootName() {
			return ROOT_NAME;
		}
		
		public void Resize() {
			navigator.Top = 3;
			navigator.Left = 3;
			navigator.Width = this.Width - 47;
			//pbPlus.Width = navigator.Height - 3;
			//pbPlus.Height = navigator.Height - 3;
			pbPlus.Left = navigator.Left + navigator.Width + 5;
			pbPlus.Top = navigator.Top + 3;
			lvFiles.Top = navigator.Top + navigator.Height + 3;
			lvFiles.Left = navigator.Left;
			lvFiles.Width = this.Width - 7;
			lvFiles.Height = this.Height - navigator.Height - 10;
		}
		
		private void InitListView()
		{
			lvFiles.Items.Clear();
		    // Add columns
		    lvFiles.Columns.Add("Name", -2,HorizontalAlignment.Left);
		    lvFiles.Columns.Add("Size", -2, HorizontalAlignment.Left);
		    lvFiles.Columns.Add("Create", -2, HorizontalAlignment.Left);
		    lvFiles.Columns.Add("Modify", -2, HorizontalAlignment.Left);
		    ImageList imageList = new ImageList();
		    imageList.Images.Add("Folder", global::NGO.Pad.JFiles.Resource1.icon1);
		    imageList.Images.Add("File", global::NGO.Pad.JFiles.Resource1.file);
		    
		    lvFiles.SmallImageList = imageList;
		    lvFiles.LargeImageList = imageList;   
		}
		
		private string getFullPath(string stringPath)
		{ 
			//Get Full path string 
			stringPath = ""; 
		    
			//remove My Computer from path. 
			stringPath = stringPath.Replace("My Computer\\", ""); 
		    
			return stringPath; 
		}
		
		private string GetPathName(string stringPath)
		{ 
			//Get Name of folder 
			string[] stringSplit = stringPath.Split('\\'); 
		    
			int _maxIndex = stringSplit.Length; 
		    
			return stringSplit[_maxIndex - 1]; 
		}
		
		private string formatDate(DateTime dtDate)
		{ 
			//Get date and time in short format 
			string stringDate = ""; 
			stringDate = dtDate.ToShortDateString().ToString()
			+ " " + dtDate.ToShortTimeString().ToString();
    
			return stringDate; 
		}

		private string formatSize(Int64 lSize)
		{
			//Format number to KB 
			string stringSize = ""; 
			NumberFormatInfo myNfi = new NumberFormatInfo();
			Int64 lKBSize = 0; 
    
			if (lSize < 1024) { 
				if (lSize == 0) { 
					//zero byte 
					stringSize = "0"; 
				} else { 
					//less than 1K but not zero byte 
					stringSize = "1"; 
				} 
			} else { 
				//convert to KB 
				lKBSize = lSize / 1024; 
        
				//format number with default format 
				stringSize = lKBSize.ToString("n", myNfi); 
        
				//remove decimal 
				stringSize = stringSize.Replace(".00", ""); 
			} 
    
			return stringSize + " KB"; 
		}
		
		private void PopulateFolders(string path) {
			string[] stringDirectories =   Directory.GetDirectories(path); 
                
            string stringFullPath = ""; 
            string stringPathName = ""; 
            string[] lvData = new string[4];

            foreach (string stringDir in stringDirectories) 
            { 
                stringFullPath = stringDir; 
                stringPathName = GetPathName(stringFullPath); 
                lvData[0] = stringPathName;
                //Create actual list item
				ListViewItem lvItem = new ListViewItem(lvData, 0);
				lvItem.Tag = stringFullPath;
					
				lvItem.ImageKey="Folder"; //TODO: chooice image
				lvFiles.Items.Add(lvItem);
                
            } 
		}
				
		private void PopulateFiles(string filePath)
		{
			try {
				string[] stringFiles = Directory.GetFiles(filePath);
				string stringFileName = "";
				DateTime dtCreateDate, dtModifyDate;
				Int64 lFileSize = 0;
				string[] lvData = new string[4];

				lvFiles.Items.Clear();
				
				//populate folder first
				PopulateFolders(filePath);
				
				//loop throught all files
				foreach (string stringFile in stringFiles) {
					stringFileName = stringFile;
					FileInfo objFileSize = new 
                        FileInfo(stringFileName);
					lFileSize = objFileSize.Length;
					//GetCreationTime(stringFileName);
					dtCreateDate = objFileSize.CreationTime; 
					//GetLastWriteTime(stringFileName);
					dtModifyDate = objFileSize.LastWriteTime; 

					//create listview data
					lvData[0] = GetPathName(stringFileName);
					lvData[1] = formatSize(lFileSize);
                            
					//check if file is in local current 
					//day light saving time
					if (TimeZone.CurrentTimeZone.
                        IsDaylightSavingTime(dtCreateDate) == false) {
						//not in day light saving time adjust time
						lvData[2] = formatDate(dtCreateDate.AddHours(1));
					} else {
						//is in day light saving time adjust time
						lvData[2] = formatDate(dtCreateDate);
					}

					//check if file is in local current day 
					//light saving time
					if (TimeZone.CurrentTimeZone.
                        IsDaylightSavingTime(dtModifyDate) == false) {
						//not in day light saving time adjust time
						lvData[3] = formatDate(dtModifyDate.AddHours(1));
					} else {
						//not in day light saving time adjust time
						lvData[3] = formatDate(dtModifyDate);
					}

					//Create actual list item
					ListViewItem lvItem = new ListViewItem(lvData, 0);
					lvItem.Tag = stringFileName;
					lvItem.ImageKey = "File";
					lvFiles.Items.Add(lvItem);
				}
				
				
			} catch (IOException e) {
				MessageBox.Show("Error: Drive not ready or directory does not exist.");
			} catch (UnauthorizedAccessException e) {
				MessageBox.Show("Error: Drive or directory access denided.");
			} catch (Exception e) {
				MessageBox.Show("Error: " + e);
			}
		}
		
		void LvFilesMouseDoubleClick(object sender, MouseEventArgs e)
		{
			var senderList  = (ListView) sender;
	        var clickedItem = senderList.HitTest(e.Location).Item;
	        if (clickedItem != null)
	        {
	            string fullPath = (string)clickedItem.Tag;
	            if (clickedItem.ImageKey == "Folder") {
	            	if (fullPath !=null) {
	            		NavigateTo(clickedItem.Text, fullPath);
	            	}
	            } else if (clickedItem.ImageKey == "File") {
	            	fileHandler.OpenFile(fullPath, clickedItem.Text);
	            }
	            
	        }   
		}
		void PbPlusClick(object sender, EventArgs e)
		{
			var createDialog  = new CreateDialog();
		    if (createDialog.ShowDialog() == DialogResult.OK)
		    {
		    	//
		    }
		}
	}
}
