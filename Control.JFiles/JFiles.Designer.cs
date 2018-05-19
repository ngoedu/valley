/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-04-19
 * Time: 3:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Control.Files
{
	partial class JFiles
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView lvFiles;
		private System.Windows.Forms.PictureBox pbPlus;
		
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
			this.lvFiles = new System.Windows.Forms.ListView();
			this.pbPlus = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbPlus)).BeginInit();
			this.SuspendLayout();
			// 
			// lvFiles
			// 
			this.lvFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvFiles.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.lvFiles.Location = new System.Drawing.Point(3, 35);
			this.lvFiles.Name = "lvFiles";
			this.lvFiles.Size = new System.Drawing.Size(344, 380);
			this.lvFiles.TabIndex = 0;
			this.lvFiles.UseCompatibleStateImageBehavior = false;
			this.lvFiles.View = System.Windows.Forms.View.Details;
			this.lvFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvFilesMouseDoubleClick);
			// 
			// pbPlus
			// 
			this.pbPlus.Image = global::Control.JFiles.Resource1.plus;
			this.pbPlus.Location = new System.Drawing.Point(316, 4);
			this.pbPlus.Name = "pbPlus";
			this.pbPlus.Size = new System.Drawing.Size(23, 23);
			this.pbPlus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbPlus.TabIndex = 1;
			this.pbPlus.TabStop = false;
			this.pbPlus.Click += new System.EventHandler(this.PbPlusClick);
			// 
			// JBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.pbPlus);
			this.Controls.Add(this.lvFiles);
			this.Name = "JBrowser";
			this.Size = new System.Drawing.Size(350, 419);
			((System.ComponentModel.ISupportInitialize)(this.pbPlus)).EndInit();
			this.ResumeLayout(false);

		}
		
	}
}
