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
		private System.Windows.Forms.Button btnGenPkg;
		private System.Windows.Forms.TextBox tbMSSrcPath;
		private System.Windows.Forms.Button btnPath1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbPackOutputFile;
		private System.Windows.Forms.Button btnPkgPath;
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
		private System.Windows.Forms.ListView lvVideo;
		private System.Windows.Forms.Button btnLoadPackage;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button btnRefAdd;
		private System.Windows.Forms.RichTextBox rtbRefText;
		private System.Windows.Forms.TextBox tbRefID;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ListView lvRef;
		private System.Windows.Forms.TextBox tbSchemaLevel;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Button btnVideoSave;
		private System.Windows.Forms.TextBox tbVideoID;
		private System.Windows.Forms.Button btnRefSave;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Button btnCreateMSF;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox tbPkgInPath;
		private System.Windows.Forms.Button btnMSoutPath;
		private System.Windows.Forms.Button btnUpdateMS;
		private System.Windows.Forms.TextBox tbMSRefID;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox tbMSLinkID;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox tbMSTitle;
		private System.Windows.Forms.ListView lvMileStones;
		private System.Windows.Forms.Button btnBuildMS;
		private System.Windows.Forms.TextBox tbMSID;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.RichTextBox rtbFiles;
		private System.Windows.Forms.Button btnBuildRefs;
		private System.Windows.Forms.Button btnBuildVideos;
		private System.Windows.Forms.TextBox tbSchemaProj;
		private System.Windows.Forms.Label label19;
		
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
		private void InitializeComponent()
		{
			this.btnGenPkg = new System.Windows.Forms.Button();
			this.tbMSSrcPath = new System.Windows.Forms.TextBox();
			this.btnPath1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbPackOutputFile = new System.Windows.Forms.TextBox();
			this.btnPkgPath = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tbSchemaLevel = new System.Windows.Forms.TextBox();
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
			this.btnBuildVideos = new System.Windows.Forms.Button();
			this.btnVideoSave = new System.Windows.Forms.Button();
			this.btVideoAdd = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.rtbVideoLink = new System.Windows.Forms.RichTextBox();
			this.tbVideoID = new System.Windows.Forms.TextBox();
			this.lvVideo = new System.Windows.Forms.ListView();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.btnBuildRefs = new System.Windows.Forms.Button();
			this.btnRefSave = new System.Windows.Forms.Button();
			this.btnRefAdd = new System.Windows.Forms.Button();
			this.rtbRefText = new System.Windows.Forms.RichTextBox();
			this.tbRefID = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.lvRef = new System.Windows.Forms.ListView();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.tbMSTitle = new System.Windows.Forms.TextBox();
			this.rtbFiles = new System.Windows.Forms.RichTextBox();
			this.tbMSID = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.btnBuildMS = new System.Windows.Forms.Button();
			this.tbMSRefID = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.tbMSLinkID = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.lvMileStones = new System.Windows.Forms.ListView();
			this.btnUpdateMS = new System.Windows.Forms.Button();
			this.btnLoadPackage = new System.Windows.Forms.Button();
			this.btnCreateMSF = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.tbPkgInPath = new System.Windows.Forms.TextBox();
			this.btnMSoutPath = new System.Windows.Forms.Button();
			this.label19 = new System.Windows.Forms.Label();
			this.tbSchemaProj = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnGenPkg
			// 
			this.btnGenPkg.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenPkg.Location = new System.Drawing.Point(406, 526);
			this.btnGenPkg.Name = "btnGenPkg";
			this.btnGenPkg.Size = new System.Drawing.Size(140, 23);
			this.btnGenPkg.TabIndex = 2;
			this.btnGenPkg.Text = "Generate Package";
			this.btnGenPkg.UseVisualStyleBackColor = true;
			this.btnGenPkg.Click += new System.EventHandler(this.BtnGenPkgClick);
			// 
			// tbMSSrcPath
			// 
			this.tbMSSrcPath.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbMSSrcPath.Location = new System.Drawing.Point(100, 10);
			this.tbMSSrcPath.Margin = new System.Windows.Forms.Padding(2);
			this.tbMSSrcPath.Name = "tbMSSrcPath";
			this.tbMSSrcPath.Size = new System.Drawing.Size(302, 22);
			this.tbMSSrcPath.TabIndex = 3;
			// 
			// btnPath1
			// 
			this.btnPath1.Location = new System.Drawing.Point(406, 10);
			this.btnPath1.Margin = new System.Windows.Forms.Padding(2);
			this.btnPath1.Name = "btnPath1";
			this.btnPath1.Size = new System.Drawing.Size(32, 18);
			this.btnPath1.TabIndex = 4;
			this.btnPath1.Text = "...";
			this.btnPath1.UseVisualStyleBackColor = true;
			this.btnPath1.Click += new System.EventHandler(this.BtnPath1Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(9, 13);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 18);
			this.label1.TabIndex = 5;
			this.label1.Text = "MS src Path";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(9, 38);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(95, 18);
			this.label2.TabIndex = 5;
			this.label2.Text = "Pkg in Path";
			// 
			// tbPackOutputFile
			// 
			this.tbPackOutputFile.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbPackOutputFile.Location = new System.Drawing.Point(131, 58);
			this.tbPackOutputFile.Margin = new System.Windows.Forms.Padding(2);
			this.tbPackOutputFile.Name = "tbPackOutputFile";
			this.tbPackOutputFile.Size = new System.Drawing.Size(271, 22);
			this.tbPackOutputFile.TabIndex = 6;
			// 
			// btnPkgPath
			// 
			this.btnPkgPath.Location = new System.Drawing.Point(407, 61);
			this.btnPkgPath.Margin = new System.Windows.Forms.Padding(2);
			this.btnPkgPath.Name = "btnPkgPath";
			this.btnPkgPath.Size = new System.Drawing.Size(32, 18);
			this.btnPkgPath.TabIndex = 7;
			this.btnPkgPath.Text = "...";
			this.btnPkgPath.UseVisualStyleBackColor = true;
			this.btnPkgPath.Click += new System.EventHandler(this.BtnPath2Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabControl1.Location = new System.Drawing.Point(0, 95);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(552, 425);
			this.tabControl1.TabIndex = 8;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.tbSchemaProj);
			this.tabPage1.Controls.Add(this.label19);
			this.tabPage1.Controls.Add(this.tbSchemaLevel);
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
			this.tabPage1.Location = new System.Drawing.Point(4, 23);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage1.Size = new System.Drawing.Size(544, 398);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Schema";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tbSchemaLevel
			// 
			this.tbSchemaLevel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbSchemaLevel.Location = new System.Drawing.Point(88, 248);
			this.tbSchemaLevel.Margin = new System.Windows.Forms.Padding(2);
			this.tbSchemaLevel.Name = "tbSchemaLevel";
			this.tbSchemaLevel.Size = new System.Drawing.Size(140, 26);
			this.tbSchemaLevel.TabIndex = 13;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(8, 248);
			this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(75, 18);
			this.label13.TabIndex = 12;
			this.label13.Text = "Level";
			// 
			// tbSchemaMS
			// 
			this.tbSchemaMS.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbSchemaMS.Location = new System.Drawing.Point(88, 216);
			this.tbSchemaMS.Margin = new System.Windows.Forms.Padding(2);
			this.tbSchemaMS.Name = "tbSchemaMS";
			this.tbSchemaMS.Size = new System.Drawing.Size(140, 26);
			this.tbSchemaMS.TabIndex = 11;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(8, 216);
			this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(75, 18);
			this.label8.TabIndex = 10;
			this.label8.Text = "MileStone";
			// 
			// tbScheamWs
			// 
			this.tbScheamWs.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbScheamWs.Location = new System.Drawing.Point(88, 152);
			this.tbScheamWs.Margin = new System.Windows.Forms.Padding(2);
			this.tbScheamWs.Name = "tbScheamWs";
			this.tbScheamWs.Size = new System.Drawing.Size(140, 26);
			this.tbScheamWs.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(8, 152);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(75, 18);
			this.label7.TabIndex = 8;
			this.label7.Text = "Workspace";
			// 
			// tbSchemaSess
			// 
			this.tbSchemaSess.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbSchemaSess.Location = new System.Drawing.Point(88, 120);
			this.tbSchemaSess.Margin = new System.Windows.Forms.Padding(2);
			this.tbSchemaSess.Name = "tbSchemaSess";
			this.tbSchemaSess.Size = new System.Drawing.Size(140, 26);
			this.tbSchemaSess.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 120);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 18);
			this.label6.TabIndex = 6;
			this.label6.Text = "Sessions";
			// 
			// tbSchemaDur
			// 
			this.tbSchemaDur.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbSchemaDur.Location = new System.Drawing.Point(88, 88);
			this.tbSchemaDur.Margin = new System.Windows.Forms.Padding(2);
			this.tbSchemaDur.Name = "tbSchemaDur";
			this.tbSchemaDur.Size = new System.Drawing.Size(140, 26);
			this.tbSchemaDur.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 88);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(75, 18);
			this.label5.TabIndex = 4;
			this.label5.Text = "Duration";
			// 
			// tbSchemaName
			// 
			this.tbSchemaName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbSchemaName.Location = new System.Drawing.Point(88, 56);
			this.tbSchemaName.Margin = new System.Windows.Forms.Padding(2);
			this.tbSchemaName.Name = "tbSchemaName";
			this.tbSchemaName.Size = new System.Drawing.Size(140, 26);
			this.tbSchemaName.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 64);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 18);
			this.label4.TabIndex = 2;
			this.label4.Text = "Name";
			// 
			// tbSchemaID
			// 
			this.tbSchemaID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbSchemaID.Location = new System.Drawing.Point(88, 24);
			this.tbSchemaID.Margin = new System.Windows.Forms.Padding(2);
			this.tbSchemaID.Name = "tbSchemaID";
			this.tbSchemaID.Size = new System.Drawing.Size(140, 26);
			this.tbSchemaID.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 32);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 18);
			this.label3.TabIndex = 0;
			this.label3.Text = "ID";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.clbApp);
			this.tabPage2.Location = new System.Drawing.Point(4, 23);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage2.Size = new System.Drawing.Size(544, 398);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "App";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// clbApp
			// 
			this.clbApp.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.clbApp.FormattingEnabled = true;
			this.clbApp.Items.AddRange(new object[] {
			"guider",
			"video",
			"jeide",
			"browser",
			"devtool"});
			this.clbApp.Location = new System.Drawing.Point(8, 8);
			this.clbApp.Margin = new System.Windows.Forms.Padding(2);
			this.clbApp.Name = "clbApp";
			this.clbApp.Size = new System.Drawing.Size(520, 137);
			this.clbApp.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.btnBuildVideos);
			this.tabPage3.Controls.Add(this.btnVideoSave);
			this.tabPage3.Controls.Add(this.btVideoAdd);
			this.tabPage3.Controls.Add(this.label10);
			this.tabPage3.Controls.Add(this.label9);
			this.tabPage3.Controls.Add(this.rtbVideoLink);
			this.tabPage3.Controls.Add(this.tbVideoID);
			this.tabPage3.Controls.Add(this.lvVideo);
			this.tabPage3.Location = new System.Drawing.Point(4, 23);
			this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage3.Size = new System.Drawing.Size(544, 398);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Video";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// btnBuildVideos
			// 
			this.btnBuildVideos.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.btnBuildVideos.Location = new System.Drawing.Point(432, 360);
			this.btnBuildVideos.Name = "btnBuildVideos";
			this.btnBuildVideos.Size = new System.Drawing.Size(75, 23);
			this.btnBuildVideos.TabIndex = 7;
			this.btnBuildVideos.Text = "Build Videos";
			this.btnBuildVideos.UseVisualStyleBackColor = false;
			this.btnBuildVideos.Click += new System.EventHandler(this.BtnBuildVideosClick);
			// 
			// btnVideoSave
			// 
			this.btnVideoSave.Location = new System.Drawing.Point(432, 328);
			this.btnVideoSave.Margin = new System.Windows.Forms.Padding(2);
			this.btnVideoSave.Name = "btnVideoSave";
			this.btnVideoSave.Size = new System.Drawing.Size(56, 24);
			this.btnVideoSave.TabIndex = 6;
			this.btnVideoSave.Text = "Save";
			this.btnVideoSave.UseVisualStyleBackColor = true;
			this.btnVideoSave.Click += new System.EventHandler(this.BtnVideoSaveClick);
			// 
			// btVideoAdd
			// 
			this.btVideoAdd.Location = new System.Drawing.Point(432, 304);
			this.btVideoAdd.Margin = new System.Windows.Forms.Padding(2);
			this.btVideoAdd.Name = "btVideoAdd";
			this.btVideoAdd.Size = new System.Drawing.Size(56, 24);
			this.btVideoAdd.TabIndex = 5;
			this.btVideoAdd.Text = "Add";
			this.btVideoAdd.UseVisualStyleBackColor = true;
			this.btVideoAdd.Click += new System.EventHandler(this.BtVideoAddClick);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(16, 296);
			this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 20);
			this.label10.TabIndex = 4;
			this.label10.Text = "Link";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 272);
			this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(33, 13);
			this.label9.TabIndex = 3;
			this.label9.Text = "ID";
			// 
			// rtbVideoLink
			// 
			this.rtbVideoLink.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.rtbVideoLink.Location = new System.Drawing.Point(56, 304);
			this.rtbVideoLink.Margin = new System.Windows.Forms.Padding(2);
			this.rtbVideoLink.Name = "rtbVideoLink";
			this.rtbVideoLink.Size = new System.Drawing.Size(366, 79);
			this.rtbVideoLink.TabIndex = 2;
			this.rtbVideoLink.Text = "";
			// 
			// tbVideoID
			// 
			this.tbVideoID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbVideoID.Location = new System.Drawing.Point(56, 272);
			this.tbVideoID.Margin = new System.Windows.Forms.Padding(2);
			this.tbVideoID.Name = "tbVideoID";
			this.tbVideoID.Size = new System.Drawing.Size(111, 26);
			this.tbVideoID.TabIndex = 1;
			// 
			// lvVideo
			// 
			this.lvVideo.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvVideo.GridLines = true;
			this.lvVideo.Location = new System.Drawing.Point(5, 6);
			this.lvVideo.Margin = new System.Windows.Forms.Padding(2);
			this.lvVideo.Name = "lvVideo";
			this.lvVideo.Size = new System.Drawing.Size(540, 250);
			this.lvVideo.TabIndex = 0;
			this.lvVideo.UseCompatibleStateImageBehavior = false;
			this.lvVideo.View = System.Windows.Forms.View.Details;
			this.lvVideo.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LvVideoItemSelectionChanged);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.btnBuildRefs);
			this.tabPage4.Controls.Add(this.btnRefSave);
			this.tabPage4.Controls.Add(this.btnRefAdd);
			this.tabPage4.Controls.Add(this.rtbRefText);
			this.tabPage4.Controls.Add(this.tbRefID);
			this.tabPage4.Controls.Add(this.label12);
			this.tabPage4.Controls.Add(this.label11);
			this.tabPage4.Controls.Add(this.lvRef);
			this.tabPage4.Location = new System.Drawing.Point(4, 23);
			this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage4.Size = new System.Drawing.Size(544, 398);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Ref";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// btnBuildRefs
			// 
			this.btnBuildRefs.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.btnBuildRefs.Location = new System.Drawing.Point(432, 336);
			this.btnBuildRefs.Name = "btnBuildRefs";
			this.btnBuildRefs.Size = new System.Drawing.Size(75, 23);
			this.btnBuildRefs.TabIndex = 7;
			this.btnBuildRefs.Text = "Build Refs";
			this.btnBuildRefs.UseVisualStyleBackColor = false;
			this.btnBuildRefs.Click += new System.EventHandler(this.BtnBuildRefsClick);
			// 
			// btnRefSave
			// 
			this.btnRefSave.Location = new System.Drawing.Point(432, 304);
			this.btnRefSave.Margin = new System.Windows.Forms.Padding(2);
			this.btnRefSave.Name = "btnRefSave";
			this.btnRefSave.Size = new System.Drawing.Size(64, 24);
			this.btnRefSave.TabIndex = 6;
			this.btnRefSave.Text = "Save";
			this.btnRefSave.UseVisualStyleBackColor = true;
			this.btnRefSave.Click += new System.EventHandler(this.BtnRefSaveClick);
			// 
			// btnRefAdd
			// 
			this.btnRefAdd.Location = new System.Drawing.Point(432, 272);
			this.btnRefAdd.Margin = new System.Windows.Forms.Padding(2);
			this.btnRefAdd.Name = "btnRefAdd";
			this.btnRefAdd.Size = new System.Drawing.Size(64, 26);
			this.btnRefAdd.TabIndex = 5;
			this.btnRefAdd.Text = "Add";
			this.btnRefAdd.UseVisualStyleBackColor = true;
			this.btnRefAdd.Click += new System.EventHandler(this.BtnRefAddClick);
			// 
			// rtbRefText
			// 
			this.rtbRefText.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.rtbRefText.Location = new System.Drawing.Point(56, 280);
			this.rtbRefText.Margin = new System.Windows.Forms.Padding(2);
			this.rtbRefText.Name = "rtbRefText";
			this.rtbRefText.Size = new System.Drawing.Size(368, 80);
			this.rtbRefText.TabIndex = 4;
			this.rtbRefText.Text = "";
			// 
			// tbRefID
			// 
			this.tbRefID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbRefID.Location = new System.Drawing.Point(56, 248);
			this.tbRefID.Margin = new System.Windows.Forms.Padding(2);
			this.tbRefID.Name = "tbRefID";
			this.tbRefID.Size = new System.Drawing.Size(164, 26);
			this.tbRefID.TabIndex = 3;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 275);
			this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(75, 18);
			this.label12.TabIndex = 2;
			this.label12.Text = "Text";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 248);
			this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(75, 18);
			this.label11.TabIndex = 1;
			this.label11.Text = "ID";
			// 
			// lvRef
			// 
			this.lvRef.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvRef.GridLines = true;
			this.lvRef.Location = new System.Drawing.Point(5, 6);
			this.lvRef.Margin = new System.Windows.Forms.Padding(2);
			this.lvRef.Name = "lvRef";
			this.lvRef.Size = new System.Drawing.Size(537, 234);
			this.lvRef.TabIndex = 0;
			this.lvRef.UseCompatibleStateImageBehavior = false;
			this.lvRef.View = System.Windows.Forms.View.Details;
			this.lvRef.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LvRefItemSelectionChanged);
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.tbMSTitle);
			this.tabPage5.Controls.Add(this.rtbFiles);
			this.tabPage5.Controls.Add(this.tbMSID);
			this.tabPage5.Controls.Add(this.label18);
			this.tabPage5.Controls.Add(this.btnBuildMS);
			this.tabPage5.Controls.Add(this.tbMSRefID);
			this.tabPage5.Controls.Add(this.label17);
			this.tabPage5.Controls.Add(this.tbMSLinkID);
			this.tabPage5.Controls.Add(this.label16);
			this.tabPage5.Controls.Add(this.label15);
			this.tabPage5.Controls.Add(this.lvMileStones);
			this.tabPage5.Controls.Add(this.btnUpdateMS);
			this.tabPage5.Location = new System.Drawing.Point(4, 23);
			this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage5.Size = new System.Drawing.Size(544, 398);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "MileStone";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// tbMSTitle
			// 
			this.tbMSTitle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbMSTitle.Location = new System.Drawing.Point(56, 256);
			this.tbMSTitle.Name = "tbMSTitle";
			this.tbMSTitle.Size = new System.Drawing.Size(176, 26);
			this.tbMSTitle.TabIndex = 3;
			// 
			// rtbFiles
			// 
			this.rtbFiles.Location = new System.Drawing.Point(56, 288);
			this.rtbFiles.Name = "rtbFiles";
			this.rtbFiles.Size = new System.Drawing.Size(448, 64);
			this.rtbFiles.TabIndex = 12;
			this.rtbFiles.Text = "";
			// 
			// tbMSID
			// 
			this.tbMSID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbMSID.Location = new System.Drawing.Point(56, 224);
			this.tbMSID.Name = "tbMSID";
			this.tbMSID.Size = new System.Drawing.Size(176, 26);
			this.tbMSID.TabIndex = 11;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(8, 224);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(100, 23);
			this.label18.TabIndex = 10;
			this.label18.Text = "ID";
			// 
			// btnBuildMS
			// 
			this.btnBuildMS.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.btnBuildMS.Location = new System.Drawing.Point(384, 360);
			this.btnBuildMS.Name = "btnBuildMS";
			this.btnBuildMS.Size = new System.Drawing.Size(120, 23);
			this.btnBuildMS.TabIndex = 9;
			this.btnBuildMS.Text = "Build MS";
			this.btnBuildMS.UseVisualStyleBackColor = false;
			this.btnBuildMS.Click += new System.EventHandler(this.BtnBuildMSClick);
			// 
			// tbMSRefID
			// 
			this.tbMSRefID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbMSRefID.Location = new System.Drawing.Point(328, 256);
			this.tbMSRefID.Name = "tbMSRefID";
			this.tbMSRefID.Size = new System.Drawing.Size(176, 26);
			this.tbMSRefID.TabIndex = 8;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(264, 256);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(100, 23);
			this.label17.TabIndex = 7;
			this.label17.Text = "Ref ID";
			// 
			// tbMSLinkID
			// 
			this.tbMSLinkID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbMSLinkID.Location = new System.Drawing.Point(328, 224);
			this.tbMSLinkID.Name = "tbMSLinkID";
			this.tbMSLinkID.Size = new System.Drawing.Size(176, 26);
			this.tbMSLinkID.TabIndex = 6;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(256, 224);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(100, 23);
			this.label16.TabIndex = 5;
			this.label16.Text = "Link ID";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 248);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(64, 23);
			this.label15.TabIndex = 4;
			this.label15.Text = "Title";
			// 
			// lvMileStones
			// 
			this.lvMileStones.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvMileStones.GridLines = true;
			this.lvMileStones.Location = new System.Drawing.Point(8, 8);
			this.lvMileStones.Name = "lvMileStones";
			this.lvMileStones.Size = new System.Drawing.Size(528, 208);
			this.lvMileStones.TabIndex = 2;
			this.lvMileStones.UseCompatibleStateImageBehavior = false;
			this.lvMileStones.View = System.Windows.Forms.View.Details;
			this.lvMileStones.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LvMileStonesItemSelectionChanged);
			// 
			// btnUpdateMS
			// 
			this.btnUpdateMS.Location = new System.Drawing.Point(48, 360);
			this.btnUpdateMS.Name = "btnUpdateMS";
			this.btnUpdateMS.Size = new System.Drawing.Size(128, 23);
			this.btnUpdateMS.TabIndex = 1;
			this.btnUpdateMS.Text = "Update";
			this.btnUpdateMS.UseVisualStyleBackColor = true;
			this.btnUpdateMS.Click += new System.EventHandler(this.BtnUpdateMSClick);
			// 
			// btnLoadPackage
			// 
			this.btnLoadPackage.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLoadPackage.Location = new System.Drawing.Point(448, 56);
			this.btnLoadPackage.Margin = new System.Windows.Forms.Padding(2);
			this.btnLoadPackage.Name = "btnLoadPackage";
			this.btnLoadPackage.Size = new System.Drawing.Size(96, 24);
			this.btnLoadPackage.TabIndex = 9;
			this.btnLoadPackage.Text = "Load Pack";
			this.btnLoadPackage.UseVisualStyleBackColor = true;
			this.btnLoadPackage.Click += new System.EventHandler(this.BtnLoadPackageClick);
			// 
			// btnCreateMSF
			// 
			this.btnCreateMSF.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.btnCreateMSF.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCreateMSF.Location = new System.Drawing.Point(448, 8);
			this.btnCreateMSF.Margin = new System.Windows.Forms.Padding(2);
			this.btnCreateMSF.Name = "btnCreateMSF";
			this.btnCreateMSF.Size = new System.Drawing.Size(96, 40);
			this.btnCreateMSF.TabIndex = 10;
			this.btnCreateMSF.Text = "Create MSF";
			this.btnCreateMSF.UseVisualStyleBackColor = false;
			this.btnCreateMSF.Click += new System.EventHandler(this.BtnBuildMSFolderCopy);
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(9, 66);
			this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(128, 18);
			this.label14.TabIndex = 5;
			this.label14.Text = "Package output Path";
			// 
			// tbPkgInPath
			// 
			this.tbPkgInPath.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbPkgInPath.Location = new System.Drawing.Point(100, 34);
			this.tbPkgInPath.Margin = new System.Windows.Forms.Padding(2);
			this.tbPkgInPath.Name = "tbPkgInPath";
			this.tbPkgInPath.Size = new System.Drawing.Size(302, 22);
			this.tbPkgInPath.TabIndex = 11;
			// 
			// btnMSoutPath
			// 
			this.btnMSoutPath.Location = new System.Drawing.Point(407, 38);
			this.btnMSoutPath.Margin = new System.Windows.Forms.Padding(2);
			this.btnMSoutPath.Name = "btnMSoutPath";
			this.btnMSoutPath.Size = new System.Drawing.Size(32, 18);
			this.btnMSoutPath.TabIndex = 12;
			this.btnMSoutPath.Text = "...";
			this.btnMSoutPath.UseVisualStyleBackColor = true;
			this.btnMSoutPath.Click += new System.EventHandler(this.BtnMSoutPathClick);
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(8, 184);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(72, 23);
			this.label19.TabIndex = 14;
			this.label19.Text = "ProjName";
			// 
			// tbSchemaProj
			// 
			this.tbSchemaProj.Location = new System.Drawing.Point(88, 184);
			this.tbSchemaProj.Name = "tbSchemaProj";
			this.tbSchemaProj.Size = new System.Drawing.Size(200, 22);
			this.tbSchemaProj.TabIndex = 15;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(556, 559);
			this.Controls.Add(this.btnMSoutPath);
			this.Controls.Add(this.tbPkgInPath);
			this.Controls.Add(this.btnCreateMSF);
			this.Controls.Add(this.btnLoadPackage);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnPkgPath);
			this.Controls.Add(this.tbPackOutputFile);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnPath1);
			this.Controls.Add(this.tbMSSrcPath);
			this.Controls.Add(this.btnGenPkg);
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
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
