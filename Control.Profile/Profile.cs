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
			
			BackColor = Color.FromArgb(28,80,102);//39, 113,143);
			
		}
		
		public void SetEnergy(int progress) {
			
			this.jProgressBar1.SetValue(progress);
		}
		
		public void SetName(string name) {
			this.lblName.Text = name;
		}
		
		void ProfileSizeChanged(object sender, EventArgs e)
		{
			jProgressBar1.Height = 16;
		}
	}
}
