/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/19
 * Time: 1:21
 * 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using App.Common.Callback;
using App.Common.Impl;
using Control.Eide;

namespace AppTestForms
{
	/// <summary>
	/// Description of EideForm.
	/// </summary>
	public partial class EideForm : Form
	{
		
		JEide ide = new JEide("NgoEclipse", @"d:\NGO\client", PidRecorder.Instance);
		
		public EideForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			ide.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 40);
			
			button1.Top = this.ClientSize.Height - 30;
			button2.Top = button1.Top;
			button3.Top = button1.Top;
			this.Controls.Add(ide);
			
		}

		void Button1Click(object sender, EventArgs e)
		{
			ide.LoadEide(false);
		}
		void Button2Click(object sender, EventArgs e)
		{
			ide.EmbedIde();
		}
		void Button3Click(object sender, EventArgs e)
		{
			ide.WindowsReStyle();
		}
	}
}
