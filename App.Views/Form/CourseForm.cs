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
using System.Threading;
using System.Windows.Forms;
using App.Common;
using App.Common.Dpi;
using Control.JBrowser;
using App.Views;

namespace App.Views
{
	/// <summary>
	/// Description of CourseForm.
	/// </summary>
	public partial class CourseForm : Form
	{
		JWebBrowser browser;
		CouresJSCallback jsCallback;
		
		public CourseForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.panelPreview.Visible = false;
			
			jsCallback = new CouresJSCallback(this);
			browser = new JWebBrowser(false, jsCallback);
			browser.Dock = DockStyle.Fill;
			this.Controls.Add(browser);
			
			double scale = DpiUtil.GetScale(this.CreateGraphics());
			
			var newSize = new Size((int)(1200*scale), (int)(460*scale));
			this.ClientSize = newSize;
			
			var path = CodeBase.GetCodePath();
			#if (DIA_DEBUG)
			path =  @"C:/NGO/client/pad/src/valley";
			if (!Directory.Exists(path+@"\wui"))
            	path =  @"D:/NGO/client/pad/src/valley";
			#endif
			browser.GoToUrl(path +@"/wui/ui.html");
		}
		
		
		void CourseFormSizeChanged(object sender, EventArgs e)
		{
			
		}
		
		
		public void showCoursePreview(string cid) {
			this.Invoke((MethodInvoker)delegate() {
		       	this.rtbMetaInfo.Text = "Meta info for "+cid;
				this.panelPreview.Visible = true;
				this.browser.Visible = false;
		    });
			
		}
		
		public void navigateBackToCourseLib() {
			this.panelPreview.Visible = false;
			this.browser.Visible = true;
		}
		
		void BtnGoBackClick(object sender, EventArgs e)
		{
			navigateBackToCourseLib();
		}
		
		bool isStartTriggered = false;
		
		void BtnStartClick(object sender, EventArgs e)
		{
			if (!isStartTriggered) {
				isStartTriggered = true;
	       	 	this.DialogResult = DialogResult.OK;
	        	this.Tag = jsCallback.CourseId;
	        	this.browser.Dispose();
			}
		}
	}
}
