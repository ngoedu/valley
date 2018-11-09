/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/15
 * Time: 21:00
 * 
 * 
 */
namespace App.Views
{
	partial class AppTile
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel pContent;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.CheckBox cbLock;
		private System.Windows.Forms.Label lblFK;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.pContent = new System.Windows.Forms.Panel();
			this.lblName = new System.Windows.Forms.Label();
			this.cbLock = new System.Windows.Forms.CheckBox();
			this.lblFK = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pContent
			// 
			this.pContent.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.pContent.Location = new System.Drawing.Point(0, 32);
			this.pContent.Name = "pContent";
			this.pContent.Size = new System.Drawing.Size(360, 200);
			this.pContent.TabIndex = 0;
			// 
			// lblName
			// 
			this.lblName.Font = new System.Drawing.Font("微软雅黑", 13.25F);
			this.lblName.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lblName.Location = new System.Drawing.Point(40, 2);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(88, 28);
			this.lblName.TabIndex = 1;
			// 
			// cbLock
			// 
			this.cbLock.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbLock.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.cbLock.Location = new System.Drawing.Point(144, 4);
			this.cbLock.Name = "cbLock";
			this.cbLock.Size = new System.Drawing.Size(80, 24);
			this.cbLock.TabIndex = 2;
			this.cbLock.Text = "锁";
			this.cbLock.UseVisualStyleBackColor = true;
			this.cbLock.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// lblFK
			// 
			this.lblFK.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.lblFK.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
			this.lblFK.Location = new System.Drawing.Point(0, 0);
			this.lblFK.Margin = new System.Windows.Forms.Padding(0);
			this.lblFK.Name = "lblFK";
			this.lblFK.Size = new System.Drawing.Size(33, 27);
			this.lblFK.TabIndex = 3;
			this.lblFK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AppTile
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.lblFK);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.cbLock);
			this.Controls.Add(this.pContent);
			this.Name = "AppTile";
			this.Size = new System.Drawing.Size(360, 228);
			this.SizeChanged += new System.EventHandler(this.AppTileSizeChanged);
			this.DoubleClick += new System.EventHandler(this.AppTileDoubleClick);
			this.Enter += new System.EventHandler(this.AppTileEnter);
			this.Leave += new System.EventHandler(this.AppTileLeave);
			this.ResumeLayout(false);

		}
	}
}
