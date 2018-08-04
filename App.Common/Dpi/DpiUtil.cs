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

namespace App.Common.Dpi
{
	/// <summary>
	/// Description of DpiUtil.
	/// </summary>
	public class DpiUtil
	{
		public static double GetScale(Graphics g)
		{
			float dpiX = g.DpiX;
			double scale = 1;
			if (dpiX == 120) {
				scale = 1.25;
			} else if (dpiX==144) {
				scale = 1.5;
			}
			
			return scale;
		}
	}
}
