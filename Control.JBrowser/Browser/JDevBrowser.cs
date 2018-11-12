/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/8/28
 * Time: 20:48
 * 
 * 
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using App.Common.Reg;
using App.Common.Win32;

namespace  Control.JBrowser
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
		private AppStatus status;
		
		public JDevBrowser()
		{
			this.SizeChanged += new System.EventHandler(this.JDevBrowserSizeChanged);

			#region splitter init
			this.splitterPanelLeft = new NJFLib.Controls.CollapsibleSplitter();
			
			this.panelLeft = new System.Windows.Forms.Panel();
			this.panelLeft.SuspendLayout();
			this.splitterPanelLeft.Click += new System.EventHandler(splitterPanelLeft_Click);
			
			//create right panel
			panelRight = new System.Windows.Forms.Panel();
			this.innerBrowser = new JWebBrowser(true, null);
			
			// splitterPanelLeft
			this.splitterPanelLeft.AnimationDelay = 20;
			this.splitterPanelLeft.AnimationStep = 20;
			this.splitterPanelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitterPanelLeft.ControlToHide = this.panelLeft;
			this.splitterPanelLeft.ExpandParentForm = false;
			this.splitterPanelLeft.Location = new System.Drawing.Point(0, 80);
			this.splitterPanelLeft.Name = "splitterPanelLeft";
			this.splitterPanelLeft.TabIndex = 20;
			this.splitterPanelLeft.TabStop = false;
			this.splitterPanelLeft.UseAnimations = false;
			this.splitterPanelLeft.VisualStyle = NJFLib.Controls.VisualStyles.Mozilla;
			this.splitterPanelLeft.BackColor = Color.LightBlue;
			this.splitterPanelLeft.Dock = DockStyle.Left;
			
			// panelLeft
			this.panelLeft.Visible = false;
			this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.panelLeft.BackColor = Color.Black;
			this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelLeft.Location = new System.Drawing.Point(0, 80);
			this.panelLeft.Name = "panelLeft";
			//this.panelLeft.Size = new System.Drawing.Size(720, 75);
			this.panelLeft.TabIndex = 19;
			this.panelLeft.SizeChanged += new System.EventHandler(this.LeftPanelSizeChanged);
			this.Controls.AddRange(new System.Windows.Forms.Control[] { this.splitterPanelLeft,
																		this.panelLeft});
			this.panelLeft.ResumeLayout(false);
			#endregion splitter init
			
			
			//add broswer
			this.innerBrowser.Dock = DockStyle.Fill;
			this.panelRight.Controls.Add(this.innerBrowser);
			
			//add devtool panel
			this.Controls.Add(this.panelRight);
		}
		
		void JDevBrowserSizeChanged(object sender, EventArgs e)
		{
			if (defaultPanelSize == 0)
			{
				this.panelLeft.Size = new System.Drawing.Size(0, this.ClientSize.Height);
			}
		}
		
		
		public void LoadPage(string content)
		{
			innerBrowser.LoadPage(content);
		}

		public void GoToUrl(string url)
		{
			innerBrowser.GoToUrl(url);
		}

		public AppStatus Status()
		{
			throw new NotImplementedException();
		}
		
		#region IAppEntry implementation
		public void Init(AppRegistry reg)
		{
			this.status = AppStatus.Inited;
		}
		
		public void Reload(AppRegistry reg)
		{
			this.Dispose(reg);
			if (this.status == AppStatus.Disposed) {
				this.Init(reg);
			}
		}

		public void Dispose(AppRegistry reg)
		{
			this.status = AppStatus.Disposed;
		}
		#endregion
		
		private int defaultPanelSize = 0;
		private void splitterPanelLeft_Click(object sender, System.EventArgs e)
		{
			if (splitterPanelLeft.IsCollapsed)
			{
				defaultPanelSize = this.panelLeft.Width;
				this.panelLeft.Width = 0;		
			} else {
				if (defaultPanelSize == 0) {
					defaultPanelSize = this.ClientSize.Width / 2;
				}
				this.panelLeft.Width = defaultPanelSize;
			}
			
			if (!this.isDevToolEnabled) {
				string winTitle = this.innerBrowser.ShowDevTools(panelLeft);
				isDevToolEnabled = true;
			}
		}
		
		private IntPtr FindDevToolHandle(IntPtr handle) {
			var childWindows = Win32Api.GetAllChildHandles(handle);
			if (childWindows.Count > 0) {
				System.Diagnostics.Debug.WriteLine("child window.size > 0");
				return childWindows[0];
			}
			return IntPtr.Zero;
		}
		
		/// <summary>
		/// resize the windwo
		/// </summary>
		private void ResizeDevTool(IntPtr handle, int width, int height)
		{
			Win32Api.SetWindowPos(handle, 0, 2, 2, width, height, Win32Api.SWP_NOZORDER | Win32Api.SWP_SHOWWINDOW);   
			
			//try set window no border
	        int style = Win32Api.GetWindowLong(handle, Win32Api.GWL_STYLE);
			Win32Api.SetWindowLong(handle, Win32Api.GWL_STYLE, (style & ~ Win32Api.WS_THICKFRAME )); 
		}

		
		void LeftPanelSizeChanged(object sender, EventArgs e)
		{
			//this.innerBrowser.Width = this.panelLeft.Width -10;
			//this.innerBrowser.Height = this.panelLeft.Height;
			
			
			this.panelRight.Left  = this.panelLeft.Width + 5 ;
			this.panelRight.Top  = 0;
			this.panelRight.Width  = this.ClientSize.Width - this.panelLeft.Width -5;
			this.panelRight.Height  = this.ClientSize.Height;
			
			IntPtr dtoolPtr = FindDevToolHandle(panelLeft.Handle);
			if (dtoolPtr != IntPtr.Zero)
				ResizeDevTool(dtoolPtr, this.panelLeft.Width, this.panelLeft.Height);
						
			//System.Diagnostics.Debug.WriteLine("JDevBrowser.SpliterPanelSizeChanged w="+this.panelLeft.Width + ",h="+this.panelLeft.Height);
		}
	}
}
