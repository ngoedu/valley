/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/24
 * Time: 14:48
 * 
 * 
 */
namespace App.Views
{
	partial class CourseForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panelPreview;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.PictureBox pbImage;
		private System.Windows.Forms.RichTextBox rtbMetaInfo;
		private System.Windows.Forms.Button btnGoBack;
		private System.Windows.Forms.Button btnDownload;
		private System.Windows.Forms.ProgressBar pbDownload;
		private System.Windows.Forms.ListBox lbHistory;
		
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
			this.panelPreview = new System.Windows.Forms.Panel();
			this.lbHistory = new System.Windows.Forms.ListBox();
			this.pbDownload = new System.Windows.Forms.ProgressBar();
			this.btnDownload = new System.Windows.Forms.Button();
			this.btnGoBack = new System.Windows.Forms.Button();
			this.rtbMetaInfo = new System.Windows.Forms.RichTextBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.pbImage = new System.Windows.Forms.PictureBox();
			this.panelPreview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
			this.SuspendLayout();
			// 
			// panelPreview
			// 
			this.panelPreview.Controls.Add(this.lbHistory);
			this.panelPreview.Controls.Add(this.pbDownload);
			this.panelPreview.Controls.Add(this.btnDownload);
			this.panelPreview.Controls.Add(this.btnGoBack);
			this.panelPreview.Controls.Add(this.rtbMetaInfo);
			this.panelPreview.Controls.Add(this.btnStart);
			this.panelPreview.Controls.Add(this.pbImage);
			this.panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelPreview.Location = new System.Drawing.Point(0, 0);
			this.panelPreview.Name = "panelPreview";
			this.panelPreview.Size = new System.Drawing.Size(1184, 460);
			this.panelPreview.TabIndex = 0;
			// 
			// lbHistory
			// 
			this.lbHistory.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lbHistory.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lbHistory.FormattingEnabled = true;
			this.lbHistory.ItemHeight = 21;
			this.lbHistory.Location = new System.Drawing.Point(632, 0);
			this.lbHistory.Name = "lbHistory";
			this.lbHistory.Size = new System.Drawing.Size(569, 319);
			this.lbHistory.TabIndex = 6;
			this.lbHistory.SelectedIndexChanged += new System.EventHandler(this.LbHistorySelectedIndexChanged);
			// 
			// pbDownload
			// 
			this.pbDownload.Location = new System.Drawing.Point(648, 336);
			this.pbDownload.Name = "pbDownload";
			this.pbDownload.Size = new System.Drawing.Size(536, 23);
			this.pbDownload.TabIndex = 5;
			// 
			// btnDownload
			// 
			this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDownload.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnDownload.Location = new System.Drawing.Point(1064, 384);
			this.btnDownload.Name = "btnDownload";
			this.btnDownload.Size = new System.Drawing.Size(80, 50);
			this.btnDownload.TabIndex = 4;
			this.btnDownload.Text = "下载";
			this.btnDownload.UseVisualStyleBackColor = false;
			this.btnDownload.Click += new System.EventHandler(this.BtnDownloadClick);
			// 
			// btnGoBack
			// 
			this.btnGoBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnGoBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnGoBack.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnGoBack.Location = new System.Drawing.Point(960, 384);
			this.btnGoBack.Name = "btnGoBack";
			this.btnGoBack.Size = new System.Drawing.Size(80, 50);
			this.btnGoBack.TabIndex = 3;
			this.btnGoBack.Text = "返回";
			this.btnGoBack.UseVisualStyleBackColor = false;
			this.btnGoBack.Click += new System.EventHandler(this.BtnGoBackClick);
			// 
			// rtbMetaInfo
			// 
			this.rtbMetaInfo.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.rtbMetaInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbMetaInfo.Enabled = false;
			this.rtbMetaInfo.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.rtbMetaInfo.Location = new System.Drawing.Point(640, 8);
			this.rtbMetaInfo.Name = "rtbMetaInfo";
			this.rtbMetaInfo.Size = new System.Drawing.Size(536, 328);
			this.rtbMetaInfo.TabIndex = 2;
			this.rtbMetaInfo.Text = "";
			// 
			// btnStart
			// 
			this.btnStart.BackColor = System.Drawing.Color.Lime;
			this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnStart.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnStart.Location = new System.Drawing.Point(1064, 384);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(80, 50);
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "开始";
			this.btnStart.UseVisualStyleBackColor = false;
			this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
			// 
			// pbImage
			// 
			this.pbImage.Image = global::App.Views.Resource1.webdesign;
			this.pbImage.InitialImage = null;
			this.pbImage.Location = new System.Drawing.Point(0, 0);
			this.pbImage.Name = "pbImage";
			this.pbImage.Size = new System.Drawing.Size(632, 460);
			this.pbImage.TabIndex = 0;
			this.pbImage.TabStop = false;
			// 
			// CourseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1184, 460);
			this.ControlBox = false;
			this.Controls.Add(this.panelPreview);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "CourseForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "选择课程";
			this.SizeChanged += new System.EventHandler(this.CourseFormSizeChanged);
			this.panelPreview.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
