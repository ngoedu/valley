/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/12/15
 * Time: 20:45
 * 
 * 
 */
namespace App.Views
{
	partial class PrefConfigForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label lbNavBox;
		
		/// <summary>
		/// Disposes resources used by the form.
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrefConfigForm));
			this.lbNavBox = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbNavBox
			// 
			this.lbNavBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.lbNavBox.Location = new System.Drawing.Point(0, 0);
			this.lbNavBox.Name = "lbNavBox";
			this.lbNavBox.Size = new System.Drawing.Size(176, 568);
			this.lbNavBox.TabIndex = 0;
			// 
			// PrefConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.lbNavBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PrefConfigForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "系统设置";
			this.SizeChanged += new System.EventHandler(this.PrefConfigFormSizeChanged);
			this.ResumeLayout(false);

		}
	}
}
