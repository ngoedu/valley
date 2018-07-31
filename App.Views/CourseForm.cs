/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/24
 * Time: 14:48
 * 
 * 
 */
using System;
using System.Windows.Forms;
using App.Common;
using App.Views;

namespace App.Views
{
	/// <summary>
	/// Description of CourseForm.
	/// </summary>
	public partial class CourseForm : Form
	{
		public CourseForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			JWebBrowser browser = new JWebBrowser(false, new CouresJSCallback(this));
			browser.Dock = DockStyle.Fill;
			this.Controls.Add(browser);
			browser.GoToUrl(CodeBase.GetCodePath() +@"/ui-html/ui.html");
		}
	}
}
