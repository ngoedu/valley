/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-04-19
 * Time: 3:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Control.Files
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class FilesForm : Form, IFileHandler
	{
		JFiles browser;
		public FilesForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			browser = new JFiles(@"c:\windows", this);
			
			this.Controls.Add(browser);
			browser.Dock = DockStyle.Fill;
		}
		
		
		void MainFormResizeEnd(object sender, EventArgs e)
		{
			browser.Left = 3;
			browser.Top = 3;
			browser.Width = this.ClientSize.Width - 12;
			browser.Height = this.ClientSize.Height - 16;
		}

		#region IFileHandler implementation

		public void OpenFile(string path, string fileName)
		{
			System.Diagnostics.Debug.WriteLine("not impled.");
		}

		#endregion
	}
}
