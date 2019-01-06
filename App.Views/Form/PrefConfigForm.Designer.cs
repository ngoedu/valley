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
		private System.Windows.Forms.Label lbCourseUpgradeConf;
		private System.Windows.Forms.Label lbWorkspaceConf;
		
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
			this.lbCourseUpgradeConf = new System.Windows.Forms.Label();
			this.lbWorkspaceConf = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbNavBox
			// 
			this.lbNavBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.lbNavBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbNavBox.Location = new System.Drawing.Point(0, 0);
			this.lbNavBox.Name = "lbNavBox";
			this.lbNavBox.Size = new System.Drawing.Size(176, 568);
			this.lbNavBox.TabIndex = 0;
			// 
			// lbCourseUpgradeConf
			// 
			this.lbCourseUpgradeConf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbCourseUpgradeConf.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lbCourseUpgradeConf.Location = new System.Drawing.Point(0, 24);
			this.lbCourseUpgradeConf.Name = "lbCourseUpgradeConf";
			this.lbCourseUpgradeConf.Size = new System.Drawing.Size(176, 40);
			this.lbCourseUpgradeConf.TabIndex = 1;
			this.lbCourseUpgradeConf.Tag = "NavItem-cup";
			this.lbCourseUpgradeConf.Text = "课程更新";
			this.lbCourseUpgradeConf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbCourseUpgradeConf.Click += new System.EventHandler(this.LbCourseUpgradeConfClick);
			// 
			// lbWorkspaceConf
			// 
			this.lbWorkspaceConf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbWorkspaceConf.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lbWorkspaceConf.Location = new System.Drawing.Point(0, 64);
			this.lbWorkspaceConf.Name = "lbWorkspaceConf";
			this.lbWorkspaceConf.Size = new System.Drawing.Size(176, 40);
			this.lbWorkspaceConf.TabIndex = 3;
			this.lbWorkspaceConf.Tag = "NavItem-ws";
			this.lbWorkspaceConf.Text = "工作区设置";
			this.lbWorkspaceConf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbWorkspaceConf.Click += new System.EventHandler(this.LbWorkspaceConfClick);
			// 
			// PrefConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.lbWorkspaceConf);
			this.Controls.Add(this.lbCourseUpgradeConf);
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
