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
			var html = @"<!DOCTYPE html>
<html>
<head>
	<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
	<meta http-equiv='X-UA-Compatible' content='IE=Edge' />
	<meta http-equiv='Content-Language' content='zh-CN'/>
	<style type='text/css'>
	  div { height:600px; width:800px; }
	</style>
	<script>   
	</script>
</head>
<body>
	<div>
		<iframe src='http://open.iqiyi.com/developer/player_js/coopPlayerIndex.html?vid=d52c9431203048a4986bba373d391525&tvId=1043319200&accessToken=2.f22860a2479ad60d8da7697274de9346&appKey=3955c3425820435e86d0f4cdfe56f5e7&appId=1368&height=100%&width=100%' frameborder='0' allowfullscreen='true' width='100%' height='100%'></iframe>
	</div>
</body>
</html>";
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
