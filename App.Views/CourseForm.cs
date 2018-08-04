/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/24
 * Time: 14:48
 * 
 * 
 */
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using App.Common;
using App.Common.Dpi;
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
			
			double scale = DpiUtil.GetScale(this.CreateGraphics());
			
			var newSize = new Size((int)(1200*scale), (int)(460*scale));
			this.ClientSize = newSize;
			
			var path = CodeBase.GetCodePath();
			#if (DIA_DEBUG)
			path =  @"C:/NGO/client/pad/src/valley";
			#endif
			browser.GoToUrl(path +@"/wui/ui.html");
		}
	}
}
