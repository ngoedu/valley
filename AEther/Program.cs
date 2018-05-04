/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/19
 * 时间: 23:54
 * 
 * 
 */
using System;
using System.Threading;
using System.Windows.Forms;

namespace NGO.Pad.AEther
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Thread.CurrentThread.Name = "Main Thread";
			Application.Run(new MainForm());
		}
		
	}
}
