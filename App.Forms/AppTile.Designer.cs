/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/15
 * Time: 21:00
 * 
 * 
 */
namespace App.Forms
{
	partial class AppTile
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel pContent;
		private System.Windows.Forms.Label lblName;
		
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
			this.lblName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lblName.Location = new System.Drawing.Point(8, 8);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(72, 16);
			this.lblName.TabIndex = 1;
			// 
			// AppTile
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.lblName);
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
