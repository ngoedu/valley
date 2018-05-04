/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/2/17
 * 时间: 10:18
 * 
 * 
 */
namespace NGO.Pad.JText.UI
{
	partial class MainControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label DebugTips;
		private System.Windows.Forms.TextBox output;
		
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
			this.DebugTips = new System.Windows.Forms.Label();
			this.output = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// DebugTips
			// 
			this.DebugTips.Location = new System.Drawing.Point(0, 0);
			this.DebugTips.Name = "DebugTips";
			this.DebugTips.Size = new System.Drawing.Size(200, 15);
			this.DebugTips.TabIndex = 0;
			// 
			// output
			// 
			this.output.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.output.Enabled = false;
			this.output.Font = new System.Drawing.Font("宋体", 9F);
			this.output.Location = new System.Drawing.Point(0, 376);
			this.output.Multiline = true;
			this.output.Name = "output";
			this.output.Size = new System.Drawing.Size(800, 224);
			this.output.TabIndex = 1;
			// 
			// TextControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.output);
			this.Controls.Add(this.DebugTips);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Name = "TextControl";
			this.Size = new System.Drawing.Size(800, 600);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.TextControl_Paint);
			this.Enter += new System.EventHandler(this.TextControl_Enter);
			this.Leave += new System.EventHandler(this.TextControl_Leave);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
