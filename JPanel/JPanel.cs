/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/7
 * Time: 23:33
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NGO.Pad.View
{
	/// <summary>
	/// Description of JPanel.
	/// </summary>
	public partial class JPanel : UserControl, IView
	{
		private Panel innerPanel;
		private Label title;
		
		public JPanel()
		{
			
			InitializeComponent();
			
			title = new Label();
			title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			title.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			innerPanel = new Panel();
			this.Controls.Add(title);
			this.Controls.Add(innerPanel);
			this.BackColor = Color.Gray;
		}
		
		public void ResizeControls() {
			this.title.Top = 0;
			this.title.Left = 0;
			this.title.Width = this.Width;
			this.title.Height = 30;
			innerPanel.Top = title.Top + title.Height + 4;
			innerPanel.Left = title.Left;
			innerPanel.Width = title.Width;
			innerPanel.Height = this.Height - title.Height - 4;
			
			innerPanel.Controls[0].Width = innerPanel.Width;
			innerPanel.Controls[0].Height = innerPanel.Height;
			
		}
		
		public void SetTitle(string text) {
			title.Text = text;
		}
		
		public void Add(Control control) {
			innerPanel.Controls.Add(control);
		}
			
	}
}
