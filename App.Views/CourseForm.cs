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
			
			float dpiX = this.CreateGraphics().DpiX;
			double scale = 1;
			if (dpiX == 120) {
				scale = 1.25;
			} else if (dpiX==144) {
				scale = 1.5;
			}
			
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
