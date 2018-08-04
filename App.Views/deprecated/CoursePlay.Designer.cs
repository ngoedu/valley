/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/28
 * Time: 21:38
 * 
 * 
 */
namespace App.Views
{
	partial class CoursePlay
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private NGO.Pad.Guider.JGuider jGuider1;
		private Control.Video.JVideo jVideo1;
		
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
			this.eide.Dispose();
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.jGuider1 = new NGO.Pad.Guider.JGuider();
			this.jVideo1 = new Control.Video.JVideo();
			this.SuspendLayout();
			// 
			// jGuider1
			// 
			this.jGuider1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.jGuider1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.jGuider1.Location = new System.Drawing.Point(0, 0);
			this.jGuider1.Name = "jGuider1";
			this.jGuider1.Size = new System.Drawing.Size(251, 200);
			this.jGuider1.TabIndex = 0;
			// 
			// jVideo1
			// 
			this.jVideo1.Location = new System.Drawing.Point(0, 200);
			this.jVideo1.Name = "jVideo1";
			this.jVideo1.Size = new System.Drawing.Size(248, 248);
			this.jVideo1.TabIndex = 1;
			// 
			// CourseView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Controls.Add(this.jVideo1);
			this.Controls.Add(this.jGuider1);
			this.Name = "CourseView";
			this.Size = new System.Drawing.Size(707, 450);
			this.ResumeLayout(false);

		}
	}
}
