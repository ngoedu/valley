/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/28
 * Time: 21:38
 * 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using App.Common.Proc;
using CefSharp.WinForms;
using CefSharp;
using Control.Eide;
using NGO.Train;

namespace App.Views
{
	/// <summary>
	/// Description of CourseView.
	/// </summary>
	public partial class CoursePlay : UserControl, ICoursePlay 
	{
		private ChromiumWebBrowser cefBrowser;
		private NJFLib.Controls.CollapsibleSplitter splitterPanelLeft;
		private System.Windows.Forms.Panel panelLeft;
		private JEide eide = null;
		
		public CoursePlay(ChromiumWebBrowser browser, string codeBase)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.
			eide = new JEide("NgoEclipse", codeBase, PidRecorder.Instance);
			
			this.cefBrowser = browser;
			
			#region splitter init
			this.splitterPanelLeft = new NJFLib.Controls.CollapsibleSplitter();
			this.panelLeft = new System.Windows.Forms.Panel();
			this.panelLeft.SuspendLayout();
			this.splitterPanelLeft.Click += new System.EventHandler(splitterPanelLeft_Click);
			// splitterPanelLeft
			this.splitterPanelLeft.AnimationDelay = 20;
			this.splitterPanelLeft.AnimationStep = 20;
			this.splitterPanelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitterPanelLeft.ControlToHide = this.panelLeft;
			//this.splitterPanelLeft.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitterPanelLeft.ExpandParentForm = false;
			this.splitterPanelLeft.Location = new System.Drawing.Point(720, 720);
			this.splitterPanelLeft.Name = "splitterPanelLeft";
			this.splitterPanelLeft.TabIndex = 20;
			this.splitterPanelLeft.TabStop = false;
			this.splitterPanelLeft.UseAnimations = false;
			this.splitterPanelLeft.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots; 
			// panelLeft
			this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.panelLeft.BackColor = Color.Black;
			this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelLeft.Location = new System.Drawing.Point(0, 80);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new System.Drawing.Size(720, 75);
			this.panelLeft.TabIndex = 19;
			this.panelLeft.SizeChanged += new System.EventHandler(this.LeftPanelSizeChanged);
			this.Controls.AddRange(new System.Windows.Forms.Control[] { this.splitterPanelLeft,
																		this.panelLeft});
			this.panelLeft.ResumeLayout(false);
			#endregion splitter init
			
			//add Guider
			this.panelLeft.Controls.Add(this.jGuider1);
			
			//add Video
			this.panelLeft.Controls.Add(this.jVideo1);
			
			//add eide
			this.Controls.Add(eide);
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
		}

		
		void LeftPanelSizeChanged(object sender, EventArgs e)
		{
			this.jGuider1.Top = 0;
			this.jGuider1.Left = 0;
			this.jGuider1.Height = 400;
			this.jGuider1.Width = 250;
			
			this.jVideo1.Left = 0;
			this.jVideo1.Top = 400 + 1;
			this.jVideo1.Width = this.panelLeft.Width;
			this.jVideo1.Height = this.panelLeft.Height - 400;
			
			this.eide.Left  = this.panelLeft.Width;
			this.eide.Top  = 0;
			this.eide.Width  = this.ClientSize.Width - this.panelLeft.Width;
			this.eide.Height  = this.ClientSize.Height;
			
			System.Diagnostics.Debug.WriteLine("LeftPanelSizeChanged:w="+this.panelLeft.Width + ",h="+this.panelLeft.Height);
			
			if (this.Width == Screen.PrimaryScreen.Bounds.Width) {
				if (!isCompomentLoaded)
					LoadCourse("1");
				isCompomentLoaded = true;
			}
				
		}

		private bool isCompomentLoaded = false;
		public void LoadCourse(string courseId)
		{
			//loading video
			int jVideoWidth = jVideo1.Width - 20;
			int jVideoHeight = jVideo1.Height - 20;
			var html = "<!DOCTYPE html><html><head>	<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' /><meta http-equiv='X-UA-Compatible' content='IE=Edge' />	<meta http-equiv='Content-Language' content='zh-CN'/><style type='text/css'>div { height:"+jVideoHeight+"px; width:"+jVideoWidth+"px;}body{background-color:black;}</style><script></script></head><body>	<div><iframe src='http://open.iqiyi.com/developer/player_js/coopPlayerIndex.html?vid=d52c9431203048a4986bba373d391525&tvId=1043319200&accessToken=2.f22860a2479ad60d8da7697274de9346&appKey=3955c3425820435e86d0f4cdfe56f5e7&appId=1368&height=100%&width=100%' frameborder='0' allowfullscreen='true' width='100%' height='100%'></iframe>	</div></body></html>";
			jVideo1.LoadHtml(html);
			
			//bind course milestons
			jGuider1.BindCourse(BuildCourse());
			
			//show eide
			eide.LoadEide(false,"");
			eide.EmbedIde();
			eide.WindowsReStyle();
		}
		
		
		private Course BuildCourse() {		
			var course = new Course("Web编程基础A001");
			course.AddMileStone(new Step(1, "添加一个页面","REF",0,"Code", Course.STATUS_DEFAULT));
			course.AddMileStone(new Step(2, "第一条文字","REF",0,"Code", Course.STATUS_DEFAULT));
			course.AddMileStone(new Step(3, "换行试试","REF",0,"Code", Course.STATUS_REFER));
			course.AddMileStone(new Step(4, "原来需要标签","REF",0,"Code", Course.STATUS_DEFAULT));
			course.AddMileStone(new Step(5, "样子丑陋","REF",0,"Code", Course.STATUS_DEFAULT));
			course.AddMileStone(new Step(6, "好多的样式","REF",0,"Code", Course.STATUS_CODE));
			course.AddMileStone(new Step(7, "好多的样式","REF",0,"Code", Course.STATUS_CODE));
			course.AddMileStone(new Step(8, "好多的样式","REF",0,"Code", Course.STATUS_CODE));
			course.AddMileStone(new Step(9, "好多的样式","REF",0,"Code", Course.STATUS_DEFAULT));
			course.AddMileStone(new Step(10, "结束","REF",0,"Code", Course.STATUS_CODE));
			return course;
		}
	}
}
