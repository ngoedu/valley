/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/20
 * Time: 19:53
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Control.Profile
{
	/// <summary>
	/// Description of Profile.
	/// </summary>
	public partial class Profile : UserControl, IProfile
	{
		public Profile()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			BackColor = Color.FromArgb(0, 119,198);
			progressBar1.Height = 18;
		}
		
		public void SetEnergy(int progress) {
			
			this.progressBar1.Value = progress;
		}
		
		public void SetName(string name) {
			this.lblName.Text = name;
		}
	}
}
