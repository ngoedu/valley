﻿/*
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

namespace App.Views
{
	/// <summary>
	/// Description of AppTile.
	/// </summary>
	public partial class AppTile : UserControl, IAppTile
	{
		private string tileName;
		private int hotKey;
		private bool reactive = false;
		private int SGroup {set; get;}
		private ITileManager tileManager;
		public TileStatus status {private set; get;}
		private System.Windows.Forms.Control innerControl;
		
		public enum TileStatus { Min = 0, Max = 1, Lock = 2, Normal = 3}
	
		public AppTile(string name, int key, int sgroup, bool reactive, System.Windows.Forms.Control control, ITileManager tileManager)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.tileManager = tileManager;
			this.tileName = name;
			this.hotKey = key;
			this.reactive = reactive;
			this.lblName.Text = this.tileName;
			this.SGroup = sgroup;
			this.lblFK.Text = "F"+sgroup;
			this.innerControl = control;
			this.pContent.Controls.Add(control);
			this.innerControl.Dock = DockStyle.Fill;
			
				
		}

		public int GetHotKeyId()
		{
			return this.hotKey;
		}
		public string GetTileName()
		{
			return this.tileName;
		}
		
		public int GetSGroup() {
			return this.SGroup;
		}
		#region IHookKeyCallback implementation

		public void OnHotKey(int keyCode)
		{
			if (this.status == TileStatus.Lock) {
				this.Active();
				return;
			} else if (this.status == TileStatus.Max) {
				this.tileManager.DeactiveTile(keyCode);
			} else if (this.status == TileStatus.Min) {
				this.tileManager.ActiveTile(keyCode);
			} else if (this.status == TileStatus.Normal){
				this.tileManager.DeactiveTile(keyCode);
			}		
		}

		public void Active()
		{
			this.BringToFront();
			this.BackColor = Color.DeepSkyBlue;
		}

		public void Deactive()
		{
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		}

		public void Minimized()
		{
			var minSize = tileManager.MinimizedSize(this.hotKey);
			this.Top = minSize.Top;
			this.Left = minSize.Left;
			this.Width = minSize.Width;
			this.Height = minSize.Height;
			this.status = TileStatus.Min;
			cbLock.Visible = false;
		}

		public void Normal()
		{
			var normalSize = tileManager.NormalSize(this.SGroup);
			this.Top = normalSize.Top;
			this.Left = normalSize.Left;
			this.Width = normalSize.Width;
			this.Height = normalSize.Height;
			this.status = TileStatus.Normal;
		}
		
		public void Lock()
		{
			var lockSize = tileManager.LockedSize();
			this.Top = lockSize.Top;
			this.Left = lockSize.Left;
			this.Width = lockSize.Width;
			this.Height = lockSize.Height;
			this.status = TileStatus.Lock;
		}
		
		public void Maxmized()
		{
			var maxSize = tileManager.MaxmizedSize();
			this.Top = maxSize.Top;
			this.Left = maxSize.Left;
			this.Width = maxSize.Width;
			this.Height = maxSize.Height;
			this.status = TileStatus.Max;
			cbLock.Visible = true;
		}
		
		public bool IsLocked() {
			return this.status ==  TileStatus.Lock;
		}
		
		void AppTileSizeChanged(object sender, EventArgs e)
		{
			int titleHeight = 30;
			this.pContent.Top = titleHeight;
			this.pContent.Left = 0;
			this.pContent.Width = this.Width;
			this.pContent.Height = this.Height;
			 
			this.lblFK.Height = titleHeight;
			this.lblFK.Width = titleHeight;
			
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
			if (this.reactive)
			this.OnHotKey(this.hotKey);
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if (cbLock.Checked)
				this.status = TileStatus.Lock;
			else 
				this.status = TileStatus.Max;
		}
		#endregion
		
		public override string ToString()
		{
			return string.Format("[AppTile TileName={0}, HotKey={1}, Status={2}]", tileName, hotKey, status);
		}

	}
}
