/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-05-17
 * Time: 7:33 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace NGO.Pad.Guider
{
	partial class MileStone
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label stone;
		private System.Windows.Forms.PictureBox pbPlayVideo;
		
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
			this.stone = new System.Windows.Forms.Label();
			this.pbPlayVideo = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbPlayVideo)).BeginInit();
			this.SuspendLayout();
			// 
			// stone
			// 
			this.stone.BackColor = System.Drawing.SystemColors.Info;
			this.stone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.stone.Location = new System.Drawing.Point(28, 19);
			this.stone.Name = "stone";
			this.stone.Size = new System.Drawing.Size(17, 14);
			this.stone.TabIndex = 0;
			this.stone.Click += new System.EventHandler(this.StoneClick);
			this.stone.DoubleClick += new System.EventHandler(this.StoneDoubleClick);
			// 
			// pbPlayVideo
			// 
			this.pbPlayVideo.Image = global::Control.Guider.Resource2.play1;
			this.pbPlayVideo.Location = new System.Drawing.Point(56, 16);
			this.pbPlayVideo.Name = "pbPlayVideo";
			this.pbPlayVideo.Size = new System.Drawing.Size(22, 22);
			this.pbPlayVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbPlayVideo.TabIndex = 1;
			this.pbPlayVideo.TabStop = false;
			this.pbPlayVideo.Click += new System.EventHandler(this.PbPlayVideoClick);
			// 
			// MileStone
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pbPlayVideo);
			this.Controls.Add(this.stone);
			this.Name = "MileStone";
			this.Size = new System.Drawing.Size(76, 55);
			this.SizeChanged += new System.EventHandler(this.MileStoneSizeChanged);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MileStonePaint);
			((System.ComponentModel.ISupportInitialize)(this.pbPlayVideo)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
