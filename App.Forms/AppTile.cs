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
	public partial class AppTile : UserControl, IAppTile
	{
		private string tileName;
		private int hotKey;
		private Rectangle maxSize, normalSize;
		private ITileManager tileManager;
		
		public AppTile(string name, int key, Rectangle max, Rectangle normal, ITileManager tileManager)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.tileManager = tileManager;
			this.tileName = name;
			this.hotKey = key;
			this.maxSize = max;
			this.normalSize = normal;
			
			this.lblName.Text = this.tileName;
			
			Maxmized();	
		}

		public int GetHotKeyId()
		{
			return this.hotKey;
		}
		public string GetTileName()
		{
			return this.tileName;
		}
		#region IHookKeyCallback implementation

		public void OnHotKey(int keyCode)
		{
			//MessageBox.Show(tileName + " hot key trigger "+keyCode.ToString());
			this.tileManager.ActiveTile(keyCode);
		}

		public void Active()
		{
			this.BackColor = Color.DeepSkyBlue;
		}

		public void Deactive()
		{
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		}

		public void Maxmized()
		{
			this.Width = normalSize.Width;
			this.Height = normalSize.Height;
		}

		public void Minimized()
		{
			this.Width = maxSize.Width;
			this.Height = maxSize.Height;
		}
		void AppTileSizeChanged(object sender, EventArgs e)
		{
			this.Left = normalSize.Left;
			this.Top = normalSize.Top;
			
			int titleHeight = 30;
			this.pContent.Top = titleHeight;
			this.pContent.Left = 0;
			this.pContent.Width = this.Width;
			this.pContent.Height = this.Height;
			
		}
		void AppTileEnter(object sender, EventArgs e)
		{
			this.Active();
		}
		void AppTileLeave(object sender, EventArgs e)
		{
			this.Deactive();
		}
		
		#endregion
	}
}
