/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/20
 * Time: 19:53
 * 
 * 
 */
namespace Control.Profile
{
	partial class Profile
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblName;
		private App.Common.JProgressBar jProgressBar1;
		
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
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblName = new System.Windows.Forms.Label();
			this.jProgressBar1 = new App.Common.JProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("微软雅黑", 8F);
			this.label1.ForeColor = System.Drawing.SystemColors.Menu;
			this.label1.Location = new System.Drawing.Point(64, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "账号：";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::Control.Profile.Resource1.profile_t3;
			this.pictureBox1.Location = new System.Drawing.Point(16, 13);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(45, 45);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// lblName
			// 
			this.lblName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lblName.Location = new System.Drawing.Point(104, 14);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(100, 16);
			this.lblName.TabIndex = 3;
			// 
			// jProgressBar1
			// 
			this.jProgressBar1.Location = new System.Drawing.Point(68, 34);
			this.jProgressBar1.MaxValue = 100;
			this.jProgressBar1.Name = "jProgressBar1";
			this.jProgressBar1.Size = new System.Drawing.Size(145, 16);
			this.jProgressBar1.TabIndex = 4;
			// 
			// Profile
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Controls.Add(this.jProgressBar1);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.Name = "Profile";
			this.Size = new System.Drawing.Size(300, 62);
			this.SizeChanged += new System.EventHandler(this.ProfileSizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
