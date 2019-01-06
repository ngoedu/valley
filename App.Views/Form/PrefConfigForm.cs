/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/12/15
 * Time: 20:45
 * 
 * 
 */
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace App.Views
{
	/// <summary>
	/// Description of PrefConfigForm.
	/// </summary>
	public partial class PrefConfigForm : Form
	{
		private string accountName;
		public PrefConfigForm(string account)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.accountName = account;
		}
		void PrefConfigFormSizeChanged(object sender, EventArgs e)
		{
			this.lbNavBox.Left = 0;
			this.lbNavBox.Top = 0;
			this.lbNavBox.Height = this.ClientSize.Height;
		}
		
		void LbCourseUpgradeConfClick(object sender, EventArgs e)
		{
			foreach (System.Windows.Forms.Control c in this.Controls) {
				if (c.Tag != null && c.Tag.ToString().StartsWith("ConfTab") ) {
					c.Visible = c.Tag.ToString().EndsWith("cup");
				}
		    }
		}
		void LbWorkspaceConfClick(object sender, EventArgs e)
		{
			foreach (System.Windows.Forms.Control c in this.Controls) {
				if (c.Tag != null && c.Tag.ToString().StartsWith("ConfTab") ) {
					c.Visible = c.Tag.ToString().EndsWith("ws");
				}
		    }
		}
	}
}
