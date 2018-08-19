/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/5/2
 * 时间: 21:19
 * 
 * 
 */
namespace XCodeRec
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox txtSrcPath;
		private System.Windows.Forms.Button btnPath1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtOutputFile;
		private System.Windows.Forms.Button btnPath2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox tbSchemaMS;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbScheamWs;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbSchemaSess;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbSchemaDur;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbSchemaName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbSchemaID;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckedListBox clbApp;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button btVideoAdd;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.RichTextBox rtbVideoLink;
		private System.Windows.Forms.TextBox txtVideoID;
		private System.Windows.Forms.ListView lvVideo;
		private System.Windows.Forms.Button btnLoadPackage;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button btnRefAdd;
		private System.Windows.Forms.RichTextBox rtbRefText;
		private System.Windows.Forms.TextBox txtRefID;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ListView lvRef;
		private System.Windows.Forms.TextBox txtLevel;
		private System.Windows.Forms.Label label13;
		
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
			this.button2 = new System.Windows.Forms.Button();
			this.txtSrcPath = new System.Windows.Forms.TextBox();
			this.btnPath1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtOutputFile = new System.Windows.Forms.TextBox();
			this.btnPath2 = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.txtLevel = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.tbSchemaMS = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tbScheamWs = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tbSchemaSess = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tbSchemaDur = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbSchemaName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tbSchemaID = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.clbApp = new System.Windows.Forms.CheckedListBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.btVideoAdd = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.rtbVideoLink = new System.Windows.Forms.RichTextBox();
			this.txtVideoID = new System.Windows.Forms.TextBox();
			this.lvVideo = new System.Windows.Forms.ListView();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.btnRefAdd = new System.Windows.Forms.Button();
			this.rtbRefText = new System.Windows.Forms.RichTextBox();
			this.txtRefID = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.lvRef = new System.Windows.Forms.ListView();
			this.btnLoadPackage = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(542, 657);
			this.button2.Margin = new System.Windows.Forms.Padding(4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(187, 29);
			this.button2.TabIndex = 2;
			this.button2.Text = "Generate Package";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// txtSrcPath
			// 
			this.txtSrcPath.Location = new System.Drawing.Point(133, 12);
			this.txtSrcPath.Name = "txtSrcPath";
			this.txtSrcPath.Size = new System.Drawing.Size(402, 25);
			this.txtSrcPath.TabIndex = 3;
			// 
			// btnPath1
			// 
			this.btnPath1.Location = new System.Drawing.Point(542, 12);
			this.btnPath1.Name = "btnPath1";
			this.btnPath1.Size = new System.Drawing.Size(42, 23);
			this.btnPath1.TabIndex = 4;
			this.btnPath1.Text = "...";
			this.btnPath1.UseVisualStyleBackColor = true;
			this.btnPath1.Click += new System.EventHandler(this.BtnPath1Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 23);
			this.label1.TabIndex = 5;
			this.label1.Text = "Src Path";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "MS output Path";
			// 
			// txtOutputFile
			// 
			this.txtOutputFile.Location = new System.Drawing.Point(133, 44);
			this.txtOutputFile.Name = "txtOutputFile";
			this.txtOutputFile.Size = new System.Drawing.Size(402, 25);
			this.txtOutputFile.TabIndex = 6;
			// 
			// btnPath2
			// 
			this.btnPath2.Location = new System.Drawing.Point(542, 46);
			this.btnPath2.Name = "btnPath2";
			this.btnPath2.Size = new System.Drawing.Size(42, 23);
			this.btnPath2.TabIndex = 7;
			this.btnPath2.Text = "...";
			this.btnPath2.UseVisualStyleBackColor = true;
			this.btnPath2.Click += new System.EventHandler(this.BtnPath2Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(2, 94);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(736, 556);
			this.tabControl1.TabIndex = 8;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.txtLevel);
			this.tabPage1.Controls.Add(this.label13);
			this.tabPage1.Controls.Add(this.tbSchemaMS);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.tbScheamWs);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.tbSchemaSess);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.tbSchemaDur);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.tbSchemaName);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.tbSchemaID);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(728, 527);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Schema";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// txtLevel
			// 
			this.txtLevel.Location = new System.Drawing.Point(112, 254);
			this.txtLevel.Name = "txtLevel";
			this.txtLevel.Size = new System.Drawing.Size(186, 25);
			this.txtLevel.TabIndex = 13;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(34, 257);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(100, 23);
			this.label13.TabIndex = 12;
			this.label13.Text = "Level";
			// 
			// tbSchemaMS
			// 
			this.tbSchemaMS.Location = new System.Drawing.Point(112, 217);
			this.tbSchemaMS.Name = "tbSchemaMS";
			this.tbSchemaMS.Size = new System.Drawing.Size(186, 25);
			this.tbSchemaMS.TabIndex = 11;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(34, 217);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 23);
			this.label8.TabIndex = 10;
			this.label8.Text = "MileStone";
			// 
			// tbScheamWs
			// 
			this.tbScheamWs.Location = new System.Drawing.Point(112, 174);
			this.tbScheamWs.Name = "tbScheamWs";
			this.tbScheamWs.Size = new System.Drawing.Size(186, 25);
			this.tbScheamWs.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(34, 177);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 23);
			this.label7.TabIndex = 8;
			this.label7.Text = "Workspace";
			// 
			// tbSchemaSess
			// 
			this.tbSchemaSess.Location = new System.Drawing.Point(112, 134);
			this.tbSchemaSess.Name = "tbSchemaSess";
			this.tbSchemaSess.Size = new System.Drawing.Size(186, 25);
			this.tbSchemaSess.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(34, 137);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 23);
			this.label6.TabIndex = 6;
			this.label6.Text = "Sessions";
			// 
			// tbSchemaDur
			// 
			this.tbSchemaDur.Location = new System.Drawing.Point(112, 100);
			this.tbSchemaDur.Name = "tbSchemaDur";
			this.tbSchemaDur.Size = new System.Drawing.Size(186, 25);
			this.tbSchemaDur.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(34, 103);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 4;
			this.label5.Text = "Duration";
			// 
			// tbSchemaName
			// 
			this.tbSchemaName.Location = new System.Drawing.Point(112, 66);
			this.tbSchemaName.Name = "tbSchemaName";
			this.tbSchemaName.Size = new System.Drawing.Size(186, 25);
			this.tbSchemaName.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(34, 69);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 2;
			this.label4.Text = "Name";
			// 
			// tbSchemaID
			// 
			this.tbSchemaID.Location = new System.Drawing.Point(112, 32);
			this.tbSchemaID.Name = "tbSchemaID";
			this.tbSchemaID.Size = new System.Drawing.Size(186, 25);
			this.tbSchemaID.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(34, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "ID";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.clbApp);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(728, 527);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "App";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// clbApp
			// 
			this.clbApp.FormattingEnabled = true;
			this.clbApp.Items.AddRange(new object[] {
			"guider",
			"video",
			"jeide",
			"browser",
			"devtool"});
			this.clbApp.Location = new System.Drawing.Point(29, 39);
			this.clbApp.Name = "clbApp";
			this.clbApp.Size = new System.Drawing.Size(674, 244);
			this.clbApp.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.btVideoAdd);
			this.tabPage3.Controls.Add(this.label10);
			this.tabPage3.Controls.Add(this.label9);
			this.tabPage3.Controls.Add(this.rtbVideoLink);
			this.tabPage3.Controls.Add(this.txtVideoID);
			this.tabPage3.Controls.Add(this.lvVideo);
			this.tabPage3.Location = new System.Drawing.Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(728, 527);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Video";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// btVideoAdd
			// 
			this.btVideoAdd.Location = new System.Drawing.Point(572, 430);
			this.btVideoAdd.Name = "btVideoAdd";
			this.btVideoAdd.Size = new System.Drawing.Size(75, 23);
			this.btVideoAdd.TabIndex = 5;
			this.btVideoAdd.Text = "Add";
			this.btVideoAdd.UseVisualStyleBackColor = true;
			this.btVideoAdd.Click += new System.EventHandler(this.BtVideoAddClick);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(18, 430);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(44, 23);
			this.label10.TabIndex = 4;
			this.label10.Text = "Link";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(18, 400);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(44, 23);
			this.label9.TabIndex = 3;
			this.label9.Text = "ID";
			// 
			// rtbVideoLink
			// 
			this.rtbVideoLink.Location = new System.Drawing.Point(68, 430);
			this.rtbVideoLink.Name = "rtbVideoLink";
			this.rtbVideoLink.Size = new System.Drawing.Size(497, 60);
			this.rtbVideoLink.TabIndex = 2;
			this.rtbVideoLink.Text = "";
			// 
			// txtVideoID
			// 
			this.txtVideoID.Location = new System.Drawing.Point(68, 399);
			this.txtVideoID.Name = "txtVideoID";
			this.txtVideoID.Size = new System.Drawing.Size(157, 25);
			this.txtVideoID.TabIndex = 1;
			// 
			// lvVideo
			// 
			this.lvVideo.GridLines = true;
			this.lvVideo.Location = new System.Drawing.Point(7, 7);
			this.lvVideo.Name = "lvVideo";
			this.lvVideo.Size = new System.Drawing.Size(718, 386);
			this.lvVideo.TabIndex = 0;
			this.lvVideo.UseCompatibleStateImageBehavior = false;
			this.lvVideo.View = System.Windows.Forms.View.Details;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.btnRefAdd);
			this.tabPage4.Controls.Add(this.rtbRefText);
			this.tabPage4.Controls.Add(this.txtRefID);
			this.tabPage4.Controls.Add(this.label12);
			this.tabPage4.Controls.Add(this.label11);
			this.tabPage4.Controls.Add(this.lvRef);
			this.tabPage4.Location = new System.Drawing.Point(4, 25);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(728, 527);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Ref";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.tabPage4.Click += new System.EventHandler(this.TabPage4Click);
			// 
			// btnRefAdd
			// 
			this.btnRefAdd.Location = new System.Drawing.Point(585, 430);
			this.btnRefAdd.Name = "btnRefAdd";
			this.btnRefAdd.Size = new System.Drawing.Size(75, 23);
			this.btnRefAdd.TabIndex = 5;
			this.btnRefAdd.Text = "Add";
			this.btnRefAdd.UseVisualStyleBackColor = true;
			this.btnRefAdd.Click += new System.EventHandler(this.BtnRefAddClick);
			// 
			// rtbRefText
			// 
			this.rtbRefText.Location = new System.Drawing.Point(75, 430);
			this.rtbRefText.Name = "rtbRefText";
			this.rtbRefText.Size = new System.Drawing.Size(503, 50);
			this.rtbRefText.TabIndex = 4;
			this.rtbRefText.Text = "";
			// 
			// txtRefID
			// 
			this.txtRefID.Location = new System.Drawing.Point(75, 399);
			this.txtRefID.Name = "txtRefID";
			this.txtRefID.Size = new System.Drawing.Size(218, 25);
			this.txtRefID.TabIndex = 3;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(7, 429);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 23);
			this.label12.TabIndex = 2;
			this.label12.Text = "Text";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(7, 402);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 23);
			this.label11.TabIndex = 1;
			this.label11.Text = "ID";
			// 
			// lvRef
			// 
			this.lvRef.GridLines = true;
			this.lvRef.Location = new System.Drawing.Point(7, 7);
			this.lvRef.Name = "lvRef";
			this.lvRef.Size = new System.Drawing.Size(715, 388);
			this.lvRef.TabIndex = 0;
			this.lvRef.UseCompatibleStateImageBehavior = false;
			this.lvRef.View = System.Windows.Forms.View.Details;
			// 
			// btnLoadPackage
			// 
			this.btnLoadPackage.Location = new System.Drawing.Point(591, 46);
			this.btnLoadPackage.Name = "btnLoadPackage";
			this.btnLoadPackage.Size = new System.Drawing.Size(75, 23);
			this.btnLoadPackage.TabIndex = 9;
			this.btnLoadPackage.Text = "Load";
			this.btnLoadPackage.UseVisualStyleBackColor = true;
			this.btnLoadPackage.Click += new System.EventHandler(this.BtnLoadPackageClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(742, 699);
			this.Controls.Add(this.btnLoadPackage);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnPath2);
			this.Controls.Add(this.txtOutputFile);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnPath1);
			this.Controls.Add(this.txtSrcPath);
			this.Controls.Add(this.button2);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "XCodeRec";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
