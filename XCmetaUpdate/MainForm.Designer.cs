/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/12/26
 * Time: 19:50
 * 
 * 
 */
namespace XCmetaUpdate
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.DataGridView dgvCatagory;
		private System.Windows.Forms.DataGridViewTextBoxColumn mid;
		private System.Windows.Forms.DataGridViewTextBoxColumn name;
		private System.Windows.Forms.Button btnCategoryLoad;
		private System.Windows.Forms.Button btnCategoryWrite;
		private System.Windows.Forms.DataGridView dgvActivity;
		private System.Windows.Forms.DataGridViewTextBoxColumn amid;
		private System.Windows.Forms.DataGridViewTextBoxColumn cid;
		private System.Windows.Forms.DataGridViewTextBoxColumn aname;
		private System.Windows.Forms.DataGridViewTextBoxColumn target;
		private System.Windows.Forms.DataGridViewTextBoxColumn duration;
		private System.Windows.Forms.DataGridViewTextBoxColumn content;
		private System.Windows.Forms.DataGridViewTextBoxColumn desc;
		private System.Windows.Forms.Button btnActivityWrite;
		private System.Windows.Forms.Button btnActivityLoad;
		private System.Windows.Forms.DataGridView dgvTopic;
		private System.Windows.Forms.DataGridViewTextBoxColumn tid;
		private System.Windows.Forms.DataGridViewTextBoxColumn tname;
		private System.Windows.Forms.Button btnTopicWrite;
		private System.Windows.Forms.Button btnGenDatjs;
		private System.Windows.Forms.TextBox tbxJsonDatOutputPath;
		private System.Windows.Forms.Button btnUploadToNOC;
		private System.Windows.Forms.TextBox tbUploadURI;
		private System.Windows.Forms.DataGridViewTextBoxColumn type;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbCmetaRegist;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		
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
		private void InitializeComponent()
		{
			System.Windows.Forms.Button btnTopicLoad;
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btnCategoryWrite = new System.Windows.Forms.Button();
			this.btnCategoryLoad = new System.Windows.Forms.Button();
			this.dgvCatagory = new System.Windows.Forms.DataGridView();
			this.mid = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.btnActivityWrite = new System.Windows.Forms.Button();
			this.btnActivityLoad = new System.Windows.Forms.Button();
			this.dgvActivity = new System.Windows.Forms.DataGridView();
			this.amid = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cid = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.aname = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.target = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.content = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.btnTopicWrite = new System.Windows.Forms.Button();
			this.dgvTopic = new System.Windows.Forms.DataGridView();
			this.tid = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tname = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnGenDatjs = new System.Windows.Forms.Button();
			this.tbxJsonDatOutputPath = new System.Windows.Forms.TextBox();
			this.btnUploadToNOC = new System.Windows.Forms.Button();
			this.tbUploadURI = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.tbCmetaRegist = new System.Windows.Forms.TextBox();
			btnTopicLoad = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvCatagory)).BeginInit();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvActivity)).BeginInit();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvTopic)).BeginInit();
			this.SuspendLayout();
			// 
			// btnTopicLoad
			// 
			btnTopicLoad.Location = new System.Drawing.Point(864, 56);
			btnTopicLoad.Name = "btnTopicLoad";
			btnTopicLoad.Size = new System.Drawing.Size(136, 32);
			btnTopicLoad.TabIndex = 1;
			btnTopicLoad.Text = "读入";
			btnTopicLoad.UseVisualStyleBackColor = true;
			btnTopicLoad.Click += new System.EventHandler(this.BtnTopicLoadClick);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(24, 16);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1024, 416);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.btnCategoryWrite);
			this.tabPage1.Controls.Add(this.btnCategoryLoad);
			this.tabPage1.Controls.Add(this.dgvCatagory);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1016, 390);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Category";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// btnCategoryWrite
			// 
			this.btnCategoryWrite.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnCategoryWrite.Location = new System.Drawing.Point(896, 80);
			this.btnCategoryWrite.Name = "btnCategoryWrite";
			this.btnCategoryWrite.Size = new System.Drawing.Size(75, 32);
			this.btnCategoryWrite.TabIndex = 3;
			this.btnCategoryWrite.Text = "写入";
			this.btnCategoryWrite.UseVisualStyleBackColor = true;
			this.btnCategoryWrite.Click += new System.EventHandler(this.BtnCategoryWriteClick);
			// 
			// btnCategoryLoad
			// 
			this.btnCategoryLoad.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnCategoryLoad.Location = new System.Drawing.Point(896, 24);
			this.btnCategoryLoad.Name = "btnCategoryLoad";
			this.btnCategoryLoad.Size = new System.Drawing.Size(75, 32);
			this.btnCategoryLoad.TabIndex = 1;
			this.btnCategoryLoad.Text = "读入";
			this.btnCategoryLoad.UseVisualStyleBackColor = true;
			this.btnCategoryLoad.Click += new System.EventHandler(this.BtnCategoryLoadClick);
			// 
			// dgvCatagory
			// 
			this.dgvCatagory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvCatagory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.mid,
			this.name});
			this.dgvCatagory.Location = new System.Drawing.Point(8, 8);
			this.dgvCatagory.Name = "dgvCatagory";
			this.dgvCatagory.RowTemplate.Height = 23;
			this.dgvCatagory.Size = new System.Drawing.Size(824, 376);
			this.dgvCatagory.TabIndex = 0;
			// 
			// mid
			// 
			this.mid.HeaderText = "Module ID";
			this.mid.Name = "mid";
			// 
			// name
			// 
			this.name.HeaderText = "Module Name";
			this.name.Name = "name";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.btnActivityWrite);
			this.tabPage2.Controls.Add(this.btnActivityLoad);
			this.tabPage2.Controls.Add(this.dgvActivity);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1016, 390);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Activity";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.tabPage2.Click += new System.EventHandler(this.TabPage2Click);
			// 
			// btnActivityWrite
			// 
			this.btnActivityWrite.Location = new System.Drawing.Point(928, 120);
			this.btnActivityWrite.Name = "btnActivityWrite";
			this.btnActivityWrite.Size = new System.Drawing.Size(56, 24);
			this.btnActivityWrite.TabIndex = 2;
			this.btnActivityWrite.Text = "写入";
			this.btnActivityWrite.UseVisualStyleBackColor = true;
			this.btnActivityWrite.Click += new System.EventHandler(this.BtnActivityWriteClick);
			// 
			// btnActivityLoad
			// 
			this.btnActivityLoad.Location = new System.Drawing.Point(928, 56);
			this.btnActivityLoad.Name = "btnActivityLoad";
			this.btnActivityLoad.Size = new System.Drawing.Size(56, 24);
			this.btnActivityLoad.TabIndex = 1;
			this.btnActivityLoad.Text = "读入";
			this.btnActivityLoad.UseVisualStyleBackColor = true;
			this.btnActivityLoad.Click += new System.EventHandler(this.BtnActivityLoadClick);
			// 
			// dgvActivity
			// 
			this.dgvActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvActivity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.amid,
			this.cid,
			this.aname,
			this.target,
			this.duration,
			this.content,
			this.type,
			this.desc});
			this.dgvActivity.Location = new System.Drawing.Point(8, 8);
			this.dgvActivity.Name = "dgvActivity";
			this.dgvActivity.RowTemplate.Height = 23;
			this.dgvActivity.Size = new System.Drawing.Size(872, 376);
			this.dgvActivity.TabIndex = 0;
			// 
			// amid
			// 
			this.amid.HeaderText = "Module ID";
			this.amid.Name = "amid";
			this.amid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.amid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// cid
			// 
			this.cid.HeaderText = "Activity ID";
			this.cid.Name = "cid";
			this.cid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.cid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// aname
			// 
			this.aname.HeaderText = "Activity Name";
			this.aname.Name = "aname";
			// 
			// target
			// 
			this.target.HeaderText = "Target";
			this.target.Name = "target";
			// 
			// duration
			// 
			this.duration.HeaderText = "Duration";
			this.duration.Name = "duration";
			// 
			// content
			// 
			this.content.HeaderText = "Content";
			this.content.Name = "content";
			// 
			// type
			// 
			this.type.HeaderText = "type";
			this.type.Name = "type";
			// 
			// desc
			// 
			this.desc.HeaderText = "Description";
			this.desc.Name = "desc";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.btnTopicWrite);
			this.tabPage3.Controls.Add(btnTopicLoad);
			this.tabPage3.Controls.Add(this.dgvTopic);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(1016, 390);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Topic";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// btnTopicWrite
			// 
			this.btnTopicWrite.Location = new System.Drawing.Point(864, 96);
			this.btnTopicWrite.Name = "btnTopicWrite";
			this.btnTopicWrite.Size = new System.Drawing.Size(136, 32);
			this.btnTopicWrite.TabIndex = 2;
			this.btnTopicWrite.Text = "写入";
			this.btnTopicWrite.UseVisualStyleBackColor = true;
			this.btnTopicWrite.Click += new System.EventHandler(this.BtnTopicWriteClick);
			// 
			// dgvTopic
			// 
			this.dgvTopic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTopic.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.tid,
			this.tname});
			this.dgvTopic.Location = new System.Drawing.Point(8, 8);
			this.dgvTopic.Name = "dgvTopic";
			this.dgvTopic.RowTemplate.Height = 23;
			this.dgvTopic.Size = new System.Drawing.Size(816, 376);
			this.dgvTopic.TabIndex = 0;
			// 
			// tid
			// 
			this.tid.HeaderText = "Topic ID";
			this.tid.Name = "tid";
			// 
			// tname
			// 
			this.tname.HeaderText = "Name";
			this.tname.Name = "tname";
			// 
			// btnGenDatjs
			// 
			this.btnGenDatjs.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenDatjs.Location = new System.Drawing.Point(24, 440);
			this.btnGenDatjs.Name = "btnGenDatjs";
			this.btnGenDatjs.Size = new System.Drawing.Size(136, 32);
			this.btnGenDatjs.TabIndex = 1;
			this.btnGenDatjs.Text = "Gen Json JS";
			this.btnGenDatjs.UseVisualStyleBackColor = true;
			this.btnGenDatjs.Click += new System.EventHandler(this.BtnGenDatjsClick);
			// 
			// tbxJsonDatOutputPath
			// 
			this.tbxJsonDatOutputPath.Font = new System.Drawing.Font("Lucida Console", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbxJsonDatOutputPath.Location = new System.Drawing.Point(168, 448);
			this.tbxJsonDatOutputPath.Name = "tbxJsonDatOutputPath";
			this.tbxJsonDatOutputPath.Size = new System.Drawing.Size(832, 21);
			this.tbxJsonDatOutputPath.TabIndex = 2;
			this.tbxJsonDatOutputPath.Text = "d:\\ngo\\client\\pad\\src\\valley\\wui\\dat.js";
			// 
			// btnUploadToNOC
			// 
			this.btnUploadToNOC.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUploadToNOC.Location = new System.Drawing.Point(24, 488);
			this.btnUploadToNOC.Name = "btnUploadToNOC";
			this.btnUploadToNOC.Size = new System.Drawing.Size(136, 48);
			this.btnUploadToNOC.TabIndex = 3;
			this.btnUploadToNOC.Text = "Upload/Regist to NOC";
			this.btnUploadToNOC.UseVisualStyleBackColor = true;
			this.btnUploadToNOC.Click += new System.EventHandler(this.BtnUploadToNOCClick);
			// 
			// tbUploadURI
			// 
			this.tbUploadURI.Font = new System.Drawing.Font("Lucida Console", 10.5F);
			this.tbUploadURI.Location = new System.Drawing.Point(168, 488);
			this.tbUploadURI.Name = "tbUploadURI";
			this.tbUploadURI.Size = new System.Drawing.Size(408, 21);
			this.tbUploadURI.TabIndex = 4;
			this.tbUploadURI.Text = "http://192.168.0.13/scup/admin/cmupload";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.textBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(168, 552);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(408, 22);
			this.textBox1.TabIndex = 6;
			this.textBox1.Text = "ngobased2:U#fv3rK#0w!t2";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(72, 552);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Authentication";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(592, 512);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Path";
			// 
			// textBox2
			// 
			this.textBox2.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox2.Location = new System.Drawing.Point(624, 512);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(216, 24);
			this.textBox2.TabIndex = 9;
			this.textBox2.Text = "/cpack/dat.ngjs";
			// 
			// textBox3
			// 
			this.textBox3.Font = new System.Drawing.Font("Lucida Console", 10.5F);
			this.textBox3.Location = new System.Drawing.Point(168, 512);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(336, 21);
			this.textBox3.TabIndex = 10;
			this.textBox3.Text = "http://192.168.0.13/scup/admin/cmregist";
			// 
			// tbCmetaRegist
			// 
			this.tbCmetaRegist.Font = new System.Drawing.Font("Lucida Console", 10.5F);
			this.tbCmetaRegist.Location = new System.Drawing.Point(168, 512);
			this.tbCmetaRegist.Name = "tbCmetaRegist";
			this.tbCmetaRegist.Size = new System.Drawing.Size(408, 21);
			this.tbCmetaRegist.TabIndex = 10;
			this.tbCmetaRegist.Text = "http://192.168.0.13/scup/admin/cmregist";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1198, 669);
			this.Controls.Add(this.tbCmetaRegist);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.tbUploadURI);
			this.Controls.Add(this.btnUploadToNOC);
			this.Controls.Add(this.tbxJsonDatOutputPath);
			this.Controls.Add(this.btnGenDatjs);
			this.Controls.Add(this.tabControl1);
			this.Name = "MainForm";
			this.Text = "NGO CMeta Util";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvCatagory)).EndInit();
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvActivity)).EndInit();
			this.tabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvTopic)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
