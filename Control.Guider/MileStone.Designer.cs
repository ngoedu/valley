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
			this.SuspendLayout();
			// 
			// stone
			// 
			this.stone.BackColor = System.Drawing.SystemColors.Info;
			this.stone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.stone.Location = new System.Drawing.Point(37, 24);
			this.stone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.stone.Name = "stone";
			this.stone.Size = new System.Drawing.Size(22, 17);
			this.stone.TabIndex = 0;
			this.stone.Click += new System.EventHandler(this.StoneClick);
			this.stone.DoubleClick += new System.EventHandler(this.StoneDoubleClick);
			// 
			// MileStone
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.stone);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "MileStone";
			this.Size = new System.Drawing.Size(101, 69);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MileStonePaint);
			this.ResumeLayout(false);

		}
	}
}
