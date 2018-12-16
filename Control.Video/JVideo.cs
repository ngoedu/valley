/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/20
 * Time: 0:52
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using App.Common.Reg;
using mshtml;

namespace Control.Video
{
	/// <summary>
	/// Description of JVideo.
	/// </summary>
	public partial class JVideo : UserControl, IAppEntry
	{
		/// <summary>
		/// -1: default
		/// 1: inited
		/// 0: disposed
		/// </summary>
		private AppStatus status;
		
		private AppRegistry reg;
		public JVideo()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.webBrowser1.AllowNavigation = true;
			
		}

		public void SetAppTitleCallback(IAppTitleCallback callback)
		{
			//DO nothing
		}
		#region IAppEntry implementation
		public void Init(AppRegistry reg)
		{
			/*
 			string html = (string)reg[AppRegKeys.VIDEO_LINK];
			LoadHtml(html);
			this.status = AppStatus.Inited;
			*/
			
			this.reg = reg;
			
		}

		public void Active()
		{
			if (reg.ContainsKey(AppRegKeys.VIDEO_OBJ)) {
				WebBrowser innerVideo = (WebBrowser)reg[AppRegKeys.VIDEO_OBJ];
				if (innerVideo != null ) {
					//remove from current parent
					var parent = (UserControl)reg[AppRegKeys.VIDEO_PARENT_OBJ];
					parent.Controls.Remove(innerVideo);
					
					//move to this
					var controls = this.Controls.Find("INNER_VIDEO", true); 
					if (controls.Length <= 0) 
					{
						this.Controls.Add(innerVideo);
						innerVideo.Parent = this;
						innerVideo.Width = this.ClientSize.Width;
						innerVideo.Height = this.ClientSize.Height;
						innerVideo.Top = 0;
						innerVideo.Left = 0;
						this.webBrowser1.Visible = false;
						innerVideo.BringToFront();
						
						var style= innerVideo.Document.GetElementsByTagName("style")[0];
						style.InnerText =  @"div { background-color: black; width:"+innerVideo.Width+"px; height: "+(innerVideo.Height-40)+"px;}";
					}				
				}
			}
		}
		
		public void Inactive()
		{
			var controls = this.Controls.Find("INNER_VIDEO", true); 
			if (controls.Length > 0) 
			{
				WebBrowser innerVideo = (WebBrowser)reg[AppRegKeys.VIDEO_OBJ];
				this.Controls.Remove(innerVideo);
				
				//remove from current parent
				var parent = (UserControl)reg[AppRegKeys.VIDEO_PARENT_OBJ];
				parent.Controls.Add(innerVideo);
				innerVideo.Parent = parent;
				var oriSize = (Size)reg[AppRegKeys.VIDEO_ORI_SIZE];
				innerVideo.Size = oriSize;
				
				var style= innerVideo.Document.GetElementsByTagName("style")[0];
				style.InnerText =  @"div { background-color: black; width:"+oriSize.Width+"px; height: "+(oriSize.Height-40)+"px;}";

			}
			
		}
		
		public void Reload(AppRegistry reg)
		{
			/*this.Dispose(reg);
			if (this.status == AppStatus.Disposed) {
				this.Init(reg);
			}
			*/
		}
		
		public void Dispose(AppRegistry reg)
		{
			this.status = AppStatus.Disposed;
		}
		
		public AppStatus Status () {
			return status;
		}
		
		#endregion		
		public void NavigatgeTo(string Url) {
			this.webBrowser1.DocumentText="";
			this.webBrowser1.Navigate(Url);//"http://localhost/s1web/iqiyi.html");
		}
		
		public void LoadHtml(string html) {		
			webBrowser1.DocumentText="";
			webBrowser1.Document.OpenNew(true);
			webBrowser1.Document.Write(html);
			webBrowser1.Refresh();
		}
	}
}
