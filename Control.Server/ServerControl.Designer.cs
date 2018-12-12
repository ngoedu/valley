/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/25
 * Time: 17:31
 * 
 * 
 */
namespace Control.Server
{
	partial class ServerControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TabControl tabServers;
		private System.Windows.Forms.TabPage tabCatalina;
		private System.Windows.Forms.TabPage tabMysql;
		private System.Windows.Forms.PictureBox pbTomcatStatus;
		private System.Windows.Forms.Button btTomcatStop;
		private System.Windows.Forms.Button btTomcatStart;
		private System.Windows.Forms.RichTextBox rtbTomcatConsole;
		private System.Windows.Forms.Button btnMySQLStop;
		private System.Windows.Forms.Button btnMySQLStart;
		private System.Windows.Forms.RichTextBox rtbMySqlConsole;
		private System.Windows.Forms.PictureBox pbMySQL;
		
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
			this.tabServers = new System.Windows.Forms.TabControl();
			this.tabCatalina = new System.Windows.Forms.TabPage();
			this.rtbTomcatConsole = new System.Windows.Forms.RichTextBox();
			this.btTomcatStop = new System.Windows.Forms.Button();
			this.btTomcatStart = new System.Windows.Forms.Button();
			this.pbTomcatStatus = new System.Windows.Forms.PictureBox();
			this.tabMysql = new System.Windows.Forms.TabPage();
			this.pbMySQL = new System.Windows.Forms.PictureBox();
			this.rtbMySqlConsole = new System.Windows.Forms.RichTextBox();
			this.btnMySQLStop = new System.Windows.Forms.Button();
			this.btnMySQLStart = new System.Windows.Forms.Button();
			this.tabServers.SuspendLayout();
			this.tabCatalina.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbTomcatStatus)).BeginInit();
			this.tabMysql.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbMySQL)).BeginInit();
			this.SuspendLayout();
			// 
			// tabServers
			// 
			this.tabServers.Controls.Add(this.tabCatalina);
			this.tabServers.Controls.Add(this.tabMysql);
			this.tabServers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabServers.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabServers.Location = new System.Drawing.Point(0, 0);
			this.tabServers.Name = "tabServers";
			this.tabServers.SelectedIndex = 0;
			this.tabServers.Size = new System.Drawing.Size(426, 359);
			this.tabServers.TabIndex = 0;
			// 
			// tabCatalina
			// 
			this.tabCatalina.Controls.Add(this.rtbTomcatConsole);
			this.tabCatalina.Controls.Add(this.btTomcatStop);
			this.tabCatalina.Controls.Add(this.btTomcatStart);
			this.tabCatalina.Controls.Add(this.pbTomcatStatus);
			this.tabCatalina.Location = new System.Drawing.Point(4, 28);
			this.tabCatalina.Name = "tabCatalina";
			this.tabCatalina.Padding = new System.Windows.Forms.Padding(3);
			this.tabCatalina.Size = new System.Drawing.Size(418, 327);
			this.tabCatalina.TabIndex = 0;
			this.tabCatalina.Text = "Tomcat";
			this.tabCatalina.UseVisualStyleBackColor = true;
			this.tabCatalina.SizeChanged += new System.EventHandler(this.TabCatalinaSizeChanged);
			// 
			// rtbTomcatConsole
			// 
			this.rtbTomcatConsole.BackColor = System.Drawing.SystemColors.MenuText;
			this.rtbTomcatConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtbTomcatConsole.ForeColor = System.Drawing.SystemColors.ScrollBar;
			this.rtbTomcatConsole.Location = new System.Drawing.Point(0, 80);
			this.rtbTomcatConsole.Name = "rtbTomcatConsole";
			this.rtbTomcatConsole.ReadOnly = true;
			this.rtbTomcatConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbTomcatConsole.Size = new System.Drawing.Size(408, 224);
			this.rtbTomcatConsole.TabIndex = 3;
			this.rtbTomcatConsole.Text = "";
			// 
			// btTomcatStop
			// 
			this.btTomcatStop.Enabled = false;
			this.btTomcatStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btTomcatStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btTomcatStop.Location = new System.Drawing.Point(176, 32);
			this.btTomcatStop.Name = "btTomcatStop";
			this.btTomcatStop.Size = new System.Drawing.Size(75, 31);
			this.btTomcatStop.TabIndex = 2;
			this.btTomcatStop.Text = "停止";
			this.btTomcatStop.UseVisualStyleBackColor = true;
			this.btTomcatStop.Click += new System.EventHandler(this.BtTomcatStopClick);
			// 
			// btTomcatStart
			// 
			this.btTomcatStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btTomcatStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btTomcatStart.Location = new System.Drawing.Point(88, 32);
			this.btTomcatStart.Name = "btTomcatStart";
			this.btTomcatStart.Size = new System.Drawing.Size(75, 31);
			this.btTomcatStart.TabIndex = 1;
			this.btTomcatStart.Text = "启动";
			this.btTomcatStart.UseVisualStyleBackColor = true;
			this.btTomcatStart.Click += new System.EventHandler(this.BtTomcatStartClick);
			// 
			// pbTomcatStatus
			// 
			this.pbTomcatStatus.Image = global::Control.Server.Resource1.tomcat_logo_trans_grey_48x48;
			this.pbTomcatStatus.Location = new System.Drawing.Point(16, 24);
			this.pbTomcatStatus.Name = "pbTomcatStatus";
			this.pbTomcatStatus.Size = new System.Drawing.Size(48, 48);
			this.pbTomcatStatus.TabIndex = 0;
			this.pbTomcatStatus.TabStop = false;
			// 
			// tabMysql
			// 
			this.tabMysql.Controls.Add(this.pbMySQL);
			this.tabMysql.Controls.Add(this.rtbMySqlConsole);
			this.tabMysql.Controls.Add(this.btnMySQLStop);
			this.tabMysql.Controls.Add(this.btnMySQLStart);
			this.tabMysql.Location = new System.Drawing.Point(4, 28);
			this.tabMysql.Name = "tabMysql";
			this.tabMysql.Padding = new System.Windows.Forms.Padding(3);
			this.tabMysql.Size = new System.Drawing.Size(418, 327);
			this.tabMysql.TabIndex = 1;
			this.tabMysql.Text = "MySQL";
			this.tabMysql.UseVisualStyleBackColor = true;
			this.tabMysql.SizeChanged += new System.EventHandler(this.TabMysqlSizeChanged);
			// 
			// pbMySQL
			// 
			this.pbMySQL.Image = global::Control.Server.Resource1.mysql_d;
			this.pbMySQL.Location = new System.Drawing.Point(32, 24);
			this.pbMySQL.Name = "pbMySQL";
			this.pbMySQL.Size = new System.Drawing.Size(64, 56);
			this.pbMySQL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbMySQL.TabIndex = 3;
			this.pbMySQL.TabStop = false;
			// 
			// rtbMySqlConsole
			// 
			this.rtbMySqlConsole.BackColor = System.Drawing.SystemColors.InfoText;
			this.rtbMySqlConsole.ForeColor = System.Drawing.SystemColors.Info;
			this.rtbMySqlConsole.Location = new System.Drawing.Point(32, 96);
			this.rtbMySqlConsole.Name = "rtbMySqlConsole";
			this.rtbMySqlConsole.Size = new System.Drawing.Size(360, 184);
			this.rtbMySqlConsole.TabIndex = 2;
			this.rtbMySqlConsole.Text = "";
			// 
			// btnMySQLStop
			// 
			this.btnMySQLStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMySQLStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnMySQLStop.Location = new System.Drawing.Point(200, 40);
			this.btnMySQLStop.Name = "btnMySQLStop";
			this.btnMySQLStop.Size = new System.Drawing.Size(75, 32);
			this.btnMySQLStop.TabIndex = 1;
			this.btnMySQLStop.Text = "停止";
			this.btnMySQLStop.UseVisualStyleBackColor = true;
			this.btnMySQLStop.Click += new System.EventHandler(this.BtnMySQLStopClick);
			// 
			// btnMySQLStart
			// 
			this.btnMySQLStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMySQLStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnMySQLStart.Location = new System.Drawing.Point(120, 40);
			this.btnMySQLStart.Name = "btnMySQLStart";
			this.btnMySQLStart.Size = new System.Drawing.Size(75, 32);
			this.btnMySQLStart.TabIndex = 0;
			this.btnMySQLStart.Text = "开始";
			this.btnMySQLStart.UseVisualStyleBackColor = true;
			this.btnMySQLStart.Click += new System.EventHandler(this.BtnMySQLStartClick);
			// 
			// ServerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabServers);
			this.Name = "ServerControl";
			this.Size = new System.Drawing.Size(426, 359);
			this.tabServers.ResumeLayout(false);
			this.tabCatalina.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbTomcatStatus)).EndInit();
			this.tabMysql.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbMySQL)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
