/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/18
 * Time: 2:39
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Control.Files;
using NGO.Pad.Editor;
using NGO.Pad.Guider;
using NGO.Pad.Catalina;

namespace AppTestForms
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			AetherForm form = new AetherForm();
			form.Show();
		}
		void Button2Click(object sender, EventArgs e)
		{
			GuiderForm form = new GuiderForm();
			form.Show();
		}
		void Button3Click(object sender, EventArgs e)
		{
			JeditorForm form =  new JeditorForm();
			form.Show();
		}
		void Button4Click(object sender, EventArgs e)
		{
			CatalinaForm form = new CatalinaForm();
			form.Show();
		}
		void Button5Click(object sender, EventArgs e)
		{
			EideForm eide = new EideForm();
			eide.Show();
		}
		void Button6Click(object sender, EventArgs e)
		{
			FilesForm form = new FilesForm();
			form.Show();
		}
	}
}
