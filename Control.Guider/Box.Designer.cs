﻿/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/15
 * Time: 23:57
 * 
 * 
 */
namespace NGO.Pad.Guider
{
	partial class Box
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox picBox;
		private System.Windows.Forms.Label tbCourse;
		
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
			this.picBox = new System.Windows.Forms.PictureBox();
			this.tbCourse = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
			this.SuspendLayout();
			// 
			// picBox
			// 
			this.picBox.Image = global::Control.Guider.Resource2.course;
			this.picBox.Location = new System.Drawing.Point(8, 8);
			this.picBox.Name = "picBox";
			this.picBox.Size = new System.Drawing.Size(24, 24);
			this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picBox.TabIndex = 1;
			this.picBox.TabStop = false;
			// 
			// tbCourse
			// 
			this.tbCourse.BackColor = System.Drawing.SystemColors.Info;
			this.tbCourse.Font = new System.Drawing.Font("微软雅黑", 10F);
			this.tbCourse.Location = new System.Drawing.Point(72, 8);
			this.tbCourse.Name = "tbCourse";
			this.tbCourse.Size = new System.Drawing.Size(100, 23);
			this.tbCourse.TabIndex = 2;
			this.tbCourse.Text = "label1";
			// 
			// Box
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.HotTrack;
			this.Controls.Add(this.tbCourse);
			this.Controls.Add(this.picBox);
			this.Name = "Box";
			this.Size = new System.Drawing.Size(240, 40);
			this.Load += new System.EventHandler(this.BoxLoad);
			this.SizeChanged += new System.EventHandler(this.BoxSizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
			this.ResumeLayout(false);

		}
	}
}