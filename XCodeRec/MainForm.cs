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
using System.Windows.Forms;
using NGO.Pad.JEditor;
using NGO.Pad.JFiles;

namespace XCodeRec
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form, IFileHandler
	{
		JFiles fileBrowser;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
			//@"D:\NGO\course\xCodeRec\c1web"
			fileBrowser = new JFiles(path, this);
			this.Controls.Add(fileBrowser);
			ResizeControl();
		}
		
		private void ResizeControl() {
			fileBrowser.Left = 10;
			fileBrowser.Top = 10;
			fileBrowser.Height = this.Height - 120;
			fileBrowser.Width = 340;
			fileBrowser.Resize();
			
			this.tabControl1.Left = fileBrowser.Left + fileBrowser.Width + 10;
			this.tabControl1.Top = fileBrowser.Top;
			this.tabControl1.Height = fileBrowser.Height;
			this.tabControl1.Width = this.Width - fileBrowser.Width - 40;
		}

		#region IFileHandler implementation

		public void OpenFile(string path, string fileName)
		{
			System.Diagnostics.Debug.WriteLine("path={0}, file={1}", path, fileName);
			var editor = IsOpened(path);
			if (editor != null) {
				editor.LoadFromFile(path);		
				return;
			}
			JEditor.Languages lan = CheckLanguage(fileName.Split('.')[1]);
			editor = new JEditor(lan);
			editor.Path = path;
			editor.LoadFromFile(path);
			
			var page  = new TabPage();
			page.Width = this.tabControl1.ClientSize.Width;
			page.Height = this.tabControl1.ClientSize.Height;
			page.Text = fileName;
			//editor.Width = page.Width - 10;
			//editor.Height = page.Height - 10;
			editor.Dock = DockStyle.Fill;
			editor.Name = "JEditor";
			page.Controls.Add(editor);
			this.tabControl1.Controls.Add(page);
		}
		
		private JEditor IsOpened(string path) {
			foreach (TabPage page in this.tabControl1.Controls) {
				JEditor editor = (JEditor)page.Controls["JEditor"];
				if (editor.Path.Equals(path))
					return editor;
			}
			return null;
		}
		
		private JEditor.Languages CheckLanguage(string suffix) {
			if (suffix.Equals("js")) {
				return JEditor.Languages.JAVASCRIPT;
			} else if (suffix.Equals("html")) {
				return JEditor.Languages.HTML;
			} else if (suffix.Equals("css")) {
				return JEditor.Languages.CSS;
			}
			return JEditor.Languages.HTML;
		}
		void Button1Click(object sender, EventArgs e)
		{
			foreach (TabPage page in this.tabControl1.Controls) {
				JEditor editor = (JEditor)page.Controls["JEditor"];
				editor.SaveToFile();
			}
		}
		void Button2Click(object sender, EventArgs e)
		{
			
		}

		#endregion
	}
}
