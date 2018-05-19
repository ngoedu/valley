/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/20
 * Time: 0:55
 * 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Control.Video;

namespace AppTestForms
{
	/// <summary>
	/// Description of VideoForm1.
	/// </summary>
	public partial class VideoForm1 : Form
	{
		JVideo video = new JVideo();
		public VideoForm1()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.Controls.Add(video);
			video.Top = button1.Top + button1.Height + 4;
			video.Left = 0;
			video.Width = this.ClientSize.Width;
			video.Height = this.ClientSize.Height - this.Top;
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			video.NavigatgeTo("http://localhost/s1web/iqiyi.html");
		}
		void Button2Click(object sender, EventArgs e)
		{
			var html = @"<embed src='http://player.video.qiyi.com/5ef9f94c3023d2d839688acdae373314/0/0/v_19rrm6n7c4.swf-albumId=204588701-tvId=529118600-isPurchase=0-cnId=12' allowFullScreen='true' quality='high' width='480' height='350' align='middle' allowScriptAccess='always' type='application/x-shockwave-flash'></embed>";
			video.LoadHtml(html);
		}
		void VideoForm1SizeChanged(object sender, EventArgs e)
		{
			video.Top = button1.Top + button1.Height + 4;
			video.Left = 0;
			video.Width = this.ClientSize.Width;
			video.Height = this.ClientSize.Height - this.Top;
		}
	}
}
