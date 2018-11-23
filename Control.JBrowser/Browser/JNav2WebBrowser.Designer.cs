/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/23
 * Time: 18:27
 * 
 * 
 */
namespace Control.JBrowser.Browser
{
	partial class JNav2WebBrowser
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panelBrowser;
		private System.Windows.Forms.TextBox urlTextBox;
		private System.Windows.Forms.PictureBox goBotton;
		private System.Windows.Forms.PictureBox goBackButton;
		private System.Windows.Forms.PictureBox forwardButton;
		private System.Windows.Forms.Label statusLabel;
		
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
			this.panelBrowser = new System.Windows.Forms.Panel();
			this.urlTextBox = new System.Windows.Forms.TextBox();
			this.goBotton = new System.Windows.Forms.PictureBox();
			this.goBackButton = new System.Windows.Forms.PictureBox();
			this.forwardButton = new System.Windows.Forms.PictureBox();
			this.statusLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// panelBrowser
			// 
			this.panelBrowser.Location = new System.Drawing.Point(8, 32);
			this.panelBrowser.Name = "panelBrowser";
			this.panelBrowser.Size = new System.Drawing.Size(200, 100);
			this.panelBrowser.TabIndex = 0;
			// 
			// urlTextBox
			// 
			this.urlTextBox.Location = new System.Drawing.Point(56, 0);
			this.urlTextBox.Name = "urlTextBox";
			this.urlTextBox.Size = new System.Drawing.Size(100, 21);
			this.urlTextBox.TabIndex = 2;
			// 
			// goBotton
			// 
			this.goBotton.Image = global::Control.JBrowser.Resource1.nav_plain_green;
			this.goBotton.Location = new System.Drawing.Point(296, 0);
			this.goBotton.Name = "goBotton";
			this.goBotton.Size = new System.Drawing.Size(20, 18);
			this.goBotton.TabIndex = 3;
			this.goBotton.TabStop = false;
			// 
			// goBackButton
			// 
			this.goBackButton.Image = global::Control.JBrowser.Resource1.nav_left_green;
			this.goBackButton.Location = new System.Drawing.Point(8, 0);
			this.goBackButton.Name = "goBackButton";
			this.goBackButton.Size = new System.Drawing.Size(18, 18);
			this.goBackButton.TabIndex = 4;
			this.goBackButton.TabStop = false;
			// 
			// forwardButton
			// 
			this.forwardButton.Image = global::Control.JBrowser.Resource1.nav_right_green;
			this.forwardButton.Location = new System.Drawing.Point(32, 0);
			this.forwardButton.Name = "forwardButton";
			this.forwardButton.Size = new System.Drawing.Size(18, 18);
			this.forwardButton.TabIndex = 5;
			this.forwardButton.TabStop = false;
			// 
			// statusLabel
			// 
			this.statusLabel.Location = new System.Drawing.Point(8, 240);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(328, 23);
			this.statusLabel.TabIndex = 6;
			// 
			// JNav2WebBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.forwardButton);
			this.Controls.Add(this.goBackButton);
			this.Controls.Add(this.goBotton);
			this.Controls.Add(this.urlTextBox);
			this.Controls.Add(this.panelBrowser);
			this.Name = "JNav2WebBrowser";
			this.Size = new System.Drawing.Size(334, 259);
			this.SizeChanged += new System.EventHandler(this.JNav2WebBrowserSizeChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		    this.Controls.Add(this.urlTextBox);
			this.Controls.Add(this.panelBrowser);
			this.Name = "JNav2WebBrowser";
			this.Size = new System.Drawing.Size(334, 259);
			this.SizeChanged += new System.EventHandler(this.JNav2WebBrowserSizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.goBotton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.goBackButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.forwardButton)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
