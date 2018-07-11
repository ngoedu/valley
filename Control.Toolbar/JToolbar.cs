/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/23
 * Time: 0:00
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Control.Toolbar
{
	/// <summary>
	/// Description of JToolbar.
	/// </summary>
	public partial class JToolbar : UserControl, IToolbar
	{
		private IToolBarCallback callback;
		private int origWidth1,origHeight1,origWidth2,origHeight2;
		public JToolbar(IToolBarCallback cb)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.callback = cb;
			
			BackColor = Color.FromArgb(28,80,102);
			origWidth1 = pictureBox1.Width;
			origHeight1 = pictureBox1.Height;
			origWidth2 = pictureBox2.Width;
			origHeight2 = pictureBox2.Height;
		}
		
		
		void PictureBox1Click(object sender, EventArgs e)
		{
			this.callback.DisplayCourseLib();
		}
		
		void PictureBox2Click(object sender, EventArgs e)
		{
			this.callback.PlayCourseEntry();
		}
		
		
		#region Icon effect
		void PictureBox2MouseEnter(object sender, EventArgs e)
		{
			this.pictureBox2.Width -= 2;
			this.pictureBox2.Height -= 2;
			
		}
		void JToolbarMouseLeave(object sender, EventArgs e)
		{
			pictureBox2.Width =origWidth2 ;
			pictureBox2.Height =origHeight2;
		}
		void PictureBox1MouseEnter(object sender, EventArgs e)
		{
			this.pictureBox1.Width -= 2;
			this.pictureBox1.Height -= 2;
		}
		void PictureBox1MouseLeave(object sender, EventArgs e)
		{
			pictureBox1.Width =origWidth1 ;
			pictureBox1.Height =origHeight1;
	
		}
		#endregion Icon effect
		
	}
}
