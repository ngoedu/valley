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

namespace Control.Video
{
	/// <summary>
	/// Description of JVideo.
	/// </summary>
	public partial class JVideo : UserControl
	{
		public JVideo()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.webBrowser1.AllowNavigation = true;
		}
		
		public void NavigatgeTo(string Url) {
			this.webBrowser1.DocumentText="";
			this.webBrowser1.Navigate(Url);//"http://localhost/s1web/iqiyi.html");
		}
		
		public void LoadHtml(string html) {
		
			//string theHTML = @"<html><head></head><body><p1>The paragraph</p1><ul><li>1</li><li>2</li><ul></body></html>";
			//string iqiyi = @"<embed src='http://player.video.qiyi.com/5ef9f94c3023d2d839688acdae373314/0/0/v_19rrm6n7c4.swf-albumId=204588701-tvId=529118600-isPurchase=0-cnId=12' allowFullScreen='true' quality='high' width='480' height='350' align='middle' allowScriptAccess='always' type='application/x-shockwave-flash'></embed>";
		
			webBrowser1.DocumentText="";
			webBrowser1.Document.OpenNew(true);
			webBrowser1.Document.Write(html);
			webBrowser1.Refresh();
		}
	}
}
