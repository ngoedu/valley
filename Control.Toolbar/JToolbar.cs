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
		private int origWidth,origHeight;
		public JToolbar(IToolBarCallback cb)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.callback = cb;
			
			BackColor = Color.FromArgb(39, 113,143);
			origWidth = pictureBox2.Width;
			origHeight = pictureBox2.Height;
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
			pictureBox2.Width =origWidth ;
			pictureBox2.Height =origHeight;
		}
		void PictureBox1MouseEnter(object sender, EventArgs e)
		{
			this.pictureBox1.Width -= 2;
			this.pictureBox1.Height -= 2;
		}
		void PictureBox1MouseLeave(object sender, EventArgs e)
		{
			pictureBox1.Width =origWidth ;
			pictureBox1.Height =origHeight;
	
		}
		#endregion Icon effect
		
	}
}
