/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/15
 * Time: 21:00
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using App.Common.Hook;

namespace App.Forms
{
	/// <summary>
	/// Description of AppTile.
	/// </summary>
	public partial class AppTile : UserControl, IHookKeyCallback
	{
		private string tileName;
		private Keys hotKey;
		public AppTile(string name, Keys key)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.tileName = name;
			this.hotKey = key;
			
		}

		#region IHookKeyCallback implementation

		public void OnKey(int keyCode)
		{
			MessageBox.Show(tileName + " hot key trigger "+keyCode.ToString());
		}

		#endregion
	}
}
