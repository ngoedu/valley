/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/4
 * Time: 18:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace App.Common.Dpi
{
	/// <summary>
	/// https://stackoverflow.com/questions/5977445/how-to-get-windows-display-settings
	/// </summary>
	public class DpiUtil
	{
		public static double GetScale(Graphics g)
		{
			double scale = DPI.SMALL; //1;
			using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                float dpiX = graphics.DpiX;
                float dpiY = graphics.DpiY;
			
				if (dpiX.Equals(120)) {
					scale = DPI.MIDDLE; //1.25;
				} else if (dpiX.Equals(144)) {
					scale = DPI.LARGE; //1.5
				}
                MessageBox.Show(dpiX.ToString());
			
			}
			return scale;
		}
	}
}
