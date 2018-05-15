/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/1/30
 * 时间: 1:19
 * 
 * 
 */
using System;
using System.Windows.Forms;


namespace NGO.Pad.Editor
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
			#if (DIA_DEBUG)
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new MainForm());
			Application.Run(new JeditorForm());
			#endif
		}
		
	}
}
