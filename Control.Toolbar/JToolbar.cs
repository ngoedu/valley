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
		public JToolbar(IToolBarCallback cb)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.callback = cb;
			
			BackColor = Color.FromArgb(0, 119,198);
		}
		
		
		void PictureBox1Click(object sender, EventArgs e)
		{
			this.callback.DisplayCourseLib();
		}
	}
}
