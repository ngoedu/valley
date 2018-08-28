/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/8/28
 * Time: 20:48
 * 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using App.Common.Reg;

namespace App.Views.Browser
{
	/// <summary>
	/// Description of JBrowser.
	/// </summary>
	public class JDevBrowser : UserControl, IWebBrowser, IAppEntry
	{
		
		private NJFLib.Controls.CollapsibleSplitter splitterPanelLeft;
		private System.Windows.Forms.Panel panelLeft;
		private System.Windows.Forms.Panel panelRight;
		private JWebBrowser innerBrowser;
		private bool isDevToolEnabled = false;
		
		public JDevBrowser()
		{
			
			#region splitter init
			this.splitterPanelLeft = new NJFLib.Controls.CollapsibleSplitter();
			
			this.panelLeft = new System.Windows.Forms.Panel();
			this.panelLeft.SuspendLayout();
			this.splitterPanelLeft.Click += new System.EventHandler(splitterPanelLeft_Click);
			
			//create right panel
			panelRight = new System.Windows.Forms.Panel();
			this.innerBrowser = new JWebBrowser(true, null);
			//this.innerBrowser.ShowDevTools(panelRight);
			
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
			
			
			//add broswer
			this.panelLeft.Controls.Add(this.innerBrowser);
			
			//add devtool panel
			this.Controls.Add(this.panelRight);
		}

		public void LoadPage(string content)
		{
			innerBrowser.LoadPage(content);
		}

		public void GoToUrl(string url)
		{
			innerBrowser.GoToUrl(url);
		}
		
		
		#region IAppEntry implementation
		public void Init(AppRegistry reg)
		{
			
		}

		public void Dispose(AppRegistry reg)
		{

		}
		#endregion
		
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
			
			if (!this.isDevToolEnabled) {
				this.innerBrowser.ShowDevTools(panelRight);
				this.isDevToolEnabled = true;
			}
	
		}

		
		void LeftPanelSizeChanged(object sender, EventArgs e)
		{
			this.innerBrowser.Width = this.panelLeft.Width;
			this.innerBrowser.Height = this.panelLeft.Height;
			
			this.panelRight.Left  = this.panelLeft.Width;
			this.panelRight.Top  = 0;
			this.panelRight.Width  = this.ClientSize.Width - this.panelLeft.Width;
			this.panelRight.Height  = this.ClientSize.Height;
			
			this.splitterPanelLeft.Location = new System.Drawing.Point(this.ClientSize.Width - 20, this.ClientSize.Width - 20);
			
			System.Diagnostics.Debug.WriteLine("JDevBrowser.SpliterPanelSizeChanged w="+this.panelLeft.Width + ",h="+this.panelLeft.Height);
		}
	}
}
