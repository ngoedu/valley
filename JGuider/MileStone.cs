/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-05-17
 * Time: 7:33 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NGO.Pad.Guider
{
	/// <summary>
	/// Description of MileStone.
	/// </summary>
	public partial class MileStone : UserControl
	{
		private ToolTip toolTip1 = new ToolTip();
		private SolidBrush myBrush = new SolidBrush(Color.FromArgb(109, 125, 143)); 
		private SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(34, 40, 42)); 
		public int STATUS {set; get;}
		public string Name {set; get;}
		
		private const int STONE_WIDTH = 40;
		private const int STONE_HEIGHT = 50;
		private Font font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		
		public MileStone(string name)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.Name = name;
			
			this.Size = new Size(STONE_WIDTH + 160, STONE_HEIGHT);
			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 8000;
			toolTip1.InitialDelay = 1000;
			toolTip1.ReshowDelay = 500;
			toolTip1.RemoveAll();
			toolTip1.SetToolTip(stone, "双击我查看讲解.");
			
			this.stone.BackColor = Color.FromArgb(255,255,238);
			
			STATUS = 1;
		}
		void MileStonePaint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			
			int s1w = 8, s1h = 10;
			int s1x = (STONE_WIDTH - s1w) / 2;
			
			int s2w = 30, s2h = 30;
			int s2x = (STONE_WIDTH - s2w) / 2;
			int s2y = s1h;
			
			int s3w = 8, s3h = 10;
			int s3x = (STONE_WIDTH - s3w) / 2;
			int s3y = s2y + s2h;
			
			e.Graphics.FillRectangle(myBrush, new Rectangle(s1x, 0, s1w, s1h));
			//e.Graphics.FillRectangle(Brushes.DarkGray, new Rectangle(s2x, s2y, s2w, s2h));
			FillRoundedRectangle(e.Graphics, myBrush, new Rectangle(s2x, s2y, s2w, s2h), 2);
			e.Graphics.FillRectangle(myBrush, new Rectangle(s3x, s3y, s3w, s3h));
			
			e.Graphics.DrawString(Name, font, myBrush,  STONE_WIDTH +1, (STONE_HEIGHT - font.Size )/ 2 - 4);
			e.Graphics.DrawString(Name, font, shadowBrush, STONE_WIDTH , (STONE_HEIGHT - font.Size )/ 2 - 4);
			
			int holeWidth = 18, holeHeight = 18;
			this.stone.Size = new Size(holeWidth, holeHeight);
			this.stone.Left = (STONE_WIDTH - holeWidth) / 2;
			this.stone.Top = (STONE_HEIGHT - holeHeight) / 2;
			this.stone.Show();
		}
		
		
		void StoneDoubleClick(object sender, EventArgs e)
		{
			// Create the ToolTip and associate with the Form container.
         
         
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;

			// Set up the ToolTip text for the Button and Checkbox.
         
			switch (STATUS) {
				
				case 1:
					{
						stone.BackColor = Color.SpringGreen;
						STATUS++;
						toolTip1.RemoveAll();
						toolTip1.SetToolTip(stone, "当前“讲解模式”，如果还有问题，继续双击进入代码演示模式.");
						break;
					}
				case 2:
					{
						stone.BackColor = Color.Yellow;
						STATUS++;
						toolTip1.RemoveAll();
						toolTip1.SetToolTip(stone, "当前“代码演示模式”，如果继续双击可以自动复制(覆盖)代码到你的工作区.");
						break;
					}
				case 3:
					{
						var confirmResult = MessageBox.Show("你确定执行此操作吗? 该操作将会覆盖你当前工作区的代码。",
							                    "覆盖确认!",
							                    MessageBoxButtons.YesNo);
						if (confirmResult == DialogResult.Yes) {
							stone.BackColor = Color.OrangeRed;
							STATUS++;
							toolTip1.RemoveAll();
							toolTip1.SetToolTip(stone, "已执行过自动覆盖代码操作！");
							
						} else {
							//do nothing
						}
						
						break;
					}
			}
		}
		
		public static void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
		{
			if (graphics == null)
				throw new ArgumentNullException("graphics");
			if (brush == null)
				throw new ArgumentNullException("brush");
	
			using (GraphicsPath path = RoundedRect(bounds, cornerRadius)) {
				graphics.FillPath(brush, path);
			}
		}
	
		public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
		{
			int diameter = radius * 2;
			Size size = new Size(diameter, diameter);
			Rectangle arc = new Rectangle(bounds.Location, size);
			GraphicsPath path = new GraphicsPath();
	
			if (radius == 0) {
				path.AddRectangle(bounds);
				return path;
			}
	
			// top left arc  
			path.AddArc(arc, 180, 90);
	
			// top right arc  
			arc.X = bounds.Right - diameter;
			path.AddArc(arc, 270, 90);
	
			// bottom right arc  
			arc.Y = bounds.Bottom - diameter;
			path.AddArc(arc, 0, 90);
	
			// bottom left arc 
			arc.X = bounds.Left;
			path.AddArc(arc, 90, 90);
	
			path.CloseFigure();
			return path;
		}
	}
}
