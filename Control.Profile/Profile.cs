/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/20
 * Time: 19:53
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using App.Common;

namespace Control.Profile
{
	/// <summary>
	/// Description of Profile.
	/// </summary>
	public partial class Profile : UserControl, IProfile
	{
		
		private Account account;
		private int energy;

		public Profile()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			BackColor = Color.FromArgb(60,60,60);
			//BackColor = Color.FromArgb(28,80,102);
			
			//init profile account
			InitAccount();
			
			//set account name
			this.SetName(this.GetAccountName());
		}
		
		private void InitAccount() {
			account = Account.ReadAccountFromFile(CodeBase.GetCodePath() + @"/conf/profile.xml");
			
			//first time gen account id
			if (account == null) {
				account = new Account();
				Account.WriteToFile(account, CodeBase.GetCodePath() + @"/conf/profile.xml");
			}
		}

		public int AddEnergy(int point)
		{
			this.energy += point;
			SetEnergy(this.energy);
			return this.energy;
		}
		
		public void SetEnergy(int progress) {
			this.energy = progress;
			this.jProgressBar1.SetValue(progress);
		}
		
		public void SetName(string name) {
			this.lblName.Text = name;
		}
		
		public string GetAccountName() {
			return account.ID;
		}
		
		void ProfileSizeChanged(object sender, EventArgs e)
		{
			jProgressBar1.Height = 16;
		}
		void ProfilePaint(object sender, PaintEventArgs e)
		{
			// Create font and brush.
			Font drawFont = new Font("微软雅黑", 9);
			SolidBrush drawBrush = new SolidBrush(Color.WhiteSmoke);
			
			// Create rectangle for drawing.
			float x = 0.0F;
			float y = 12.0F;
			float width = 200.0F;
			float height = 24.0F;
			RectangleF drawRect = new RectangleF(x, y, width, height);
			
			// Set format of string.
			StringFormat drawFormat = new StringFormat();
			drawFormat.Alignment = StringAlignment.Center;
			
			// Draw string to screen.
			e.Graphics.DrawString("账号：", drawFont, drawBrush, drawRect, drawFormat);
			
			
			// Create font and brush.
			Font drawFont2 = new Font("Arial", 9);
			
			// Create rectangle for drawing.
			x = 54.0F;
			y = 13.0F;
			width = 200.0F;
			height = 24.0F;
			RectangleF drawRect2 = new RectangleF(x, y, width, height);
			
			// Set format of string.
			StringFormat drawFormat2 = new StringFormat();
			drawFormat2.Alignment = StringAlignment.Center;
			
			// Draw string to screen.
			e.Graphics.DrawString(this.GetAccountName(), drawFont2, drawBrush, drawRect2, drawFormat2);
		}
	}
}
