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
		private TileStatus status;
		
		public enum TileStatus { Min = 0, Max = 1}
	
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
			
			Minimized();	
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
			if (this.status == TileStatus.Max) {
				this.Minimized();
			} else {
				this.tileManager.ActiveTile(keyCode);
			}		
		}

		public void Active()
		{
			this.BringToFront();
			this.BackColor = Color.DeepSkyBlue;
		}

		public void Deactive()
		{
			this.SendToBack();
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		}

		public void Minimized()
		{
			this.Top = normalSize.Top;
			this.Left = normalSize.Left;
			this.Width = normalSize.Width;
			this.Height = normalSize.Height;
			this.status = TileStatus.Min;
		}

		public void Maxmized()
		{
			this.Top = maxSize.Top;
			this.Left = maxSize.Left;
			this.Width = maxSize.Width;
			this.Height = maxSize.Height;
			this.status = TileStatus.Max;
		}
		
		void AppTileSizeChanged(object sender, EventArgs e)
		{
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
		void AppTileLoad(object sender, EventArgs e)
		{
	
		}
		void AppTileDoubleClick(object sender, EventArgs e)
		{
			if (this.status == TileStatus.Min)
				this.Maxmized();
			else
				this.Minimized();
		}
		
		#endregion
	}
}
