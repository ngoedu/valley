﻿/*
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
			var allstring = "Starting ProtocolHandler [\"http-nio-60080\"]";
			bool match = allstring.Contains("60080");
			System.Diagnostics.Debug.WriteLine(match);
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
		void Button7Click(object sender, EventArgs e)
		{
			VideoForm1 form = new VideoForm1();
			form.Show();
		}
		void Button8Click(object sender, EventArgs e)
		{
			TinyServerForm1 form = new TinyServerForm1();
			form.Show();
		}
		void Button9Click(object sender, EventArgs e)
		{
			BridgeForm1 form = new BridgeForm1();
			form.Show();
		}
		void BtnJvmUtilClick(object sender, EventArgs e)
		{
			var result = new App.Common.Java.JvmUtil().Execute(tbJar.Text, tbParameter.Text);
			MessageBox.Show(result);
		}
	}
}
