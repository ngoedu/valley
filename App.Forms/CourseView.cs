/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/28
 * Time: 21:38
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;

namespace App.Forms
{
	/// <summary>
	/// Description of CourseView.
	/// </summary>
	public partial class CourseView : UserControl, ICourseView
	{
		private ChromiumWebBrowser cefBrowser;
		private NJFLib.Controls.CollapsibleSplitter splitterPanelLeft;
		private System.Windows.Forms.Panel panelLeft;
			
		public CourseView(ChromiumWebBrowser browser)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.cefBrowser = browser;
			
			this.splitterPanelLeft = new NJFLib.Controls.CollapsibleSplitter();
			this.panelLeft = new System.Windows.Forms.Panel();
			this.panelLeft.SuspendLayout();
			this.splitterPanelLeft.Click += new System.EventHandler(splitterPanelLeft_Click);
			// 
			// splitterPanelLeft
			// 
			this.splitterPanelLeft.AnimationDelay = 20;
			this.splitterPanelLeft.AnimationStep = 20;
			this.splitterPanelLeft.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
			this.splitterPanelLeft.ControlToHide = this.panelLeft;
			//this.splitterPanelLeft.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitterPanelLeft.ExpandParentForm = false;
			this.splitterPanelLeft.Location = new System.Drawing.Point(720, 720);
			this.splitterPanelLeft.Name = "splitterPanelLeft";
			this.splitterPanelLeft.TabIndex = 20;
			this.splitterPanelLeft.TabStop = false;
			this.splitterPanelLeft.UseAnimations = false;
			this.splitterPanelLeft.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
			
			// 
			// panelLeft
			// 
			this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelLeft.BackColor = System.Drawing.SystemColors.GrayText;
			
			this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelLeft.Location = new System.Drawing.Point(0, 80);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new System.Drawing.Size(720, 75);
			this.panelLeft.TabIndex = 19;
			this.panelLeft.SizeChanged += new System.EventHandler(this.LeftPanelSizeChanged);
			
			
			this.Controls.AddRange(new System.Windows.Forms.Control[] { this.splitterPanelLeft,
																		this.panelLeft
																		  });
			
			this.panelLeft.ResumeLayout(false);
		}
		
		private int leftPanelSize = 0;
		private void splitterPanelLeft_Click(object sender, System.EventArgs e)
		{
			if (splitterPanelLeft.IsCollapsed)
			{
				leftPanelSize = this.panelLeft.Width;
				this.panelLeft.Width = 0;
			} else {
				this.panelLeft.Width = leftPanelSize;	
			}
			//System.Diagnostics.Debug.WriteLine("left panel size Changed to:"+this.panelLeft.Width);
		
		}

		
		void LeftPanelSizeChanged(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("LeftPanelSizeChanged:"+this.panelLeft.Width);
		}

		#region ICourseView implementation

		public void LoadCourse(string courseId)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
