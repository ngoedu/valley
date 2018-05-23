/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/23
 * Time: 20:49
 * 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Component.TinyServer;

namespace AppTestForms
{
	/// <summary>
	/// Description of TinyServerForm1.
	/// </summary>
	public partial class TinyServerForm1 : Form
	{
		private SimpleHTTPServer httpServer;
		public TinyServerForm1()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			httpServer = new SimpleHTTPServer(@"D:\Km\NOC\apache-tomcat-8.0.46\webapps\fnp",60002);
		}
		void Button1Click(object sender, EventArgs e)
		{
			httpServer.Startup();
		}
		void Button2Click(object sender, EventArgs e)
		{
			httpServer.Stop();
		}
	}
}
