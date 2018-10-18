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
using NGO.Train;
using NGO.Train.Entity;

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
		
		private Color COLOR_REFER = Color.SpringGreen;
		private Color COLOR_CODE = Color.Yellow;
		private Color COLOR_SAVE = Color.OrangeRed;
		private Color COLOR_DEF = Color.FromArgb(255,255,238);
		
		private Color[] COLOR_INDEX = new Color[4];
		private IGuider guider;
		private int index;
		
		public MileStone(int idx, string name, int status, IGuider guider)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//initilize color index
			COLOR_INDEX[0] = COLOR_DEF;
			COLOR_INDEX[1] = COLOR_REFER;
			COLOR_INDEX[2] = COLOR_CODE;
			COLOR_INDEX[3] = COLOR_SAVE;
			
			//show given stone color
			this.stone.BackColor = COLOR_INDEX[status];
			
			//set current status
			STATUS = status;
			
			//set name
			this.Name = name;
			
			this.index = idx;
			
			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 5000;
			toolTip1.InitialDelay = 500;
			toolTip1.ReshowDelay = 500;
			toolTip1.RemoveAll();
			toolTip1.SetToolTip(stone, "双击我查看讲解.");
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;
		
			//trigger resizes
			this.Size = new Size(STONE_WIDTH + 160, STONE_HEIGHT);
			
			this.guider = guider;
		}
		
		/// <summary>
		/// repaint the control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
			switch (STATUS) {
				case 0:
					{
						STATUS = Course.STATUS_REFER;
						stone.BackColor = COLOR_INDEX[STATUS];
						toolTip1.RemoveAll();
						toolTip1.SetToolTip(stone, "回顾知识点");
						this.guider.ShowRef(this.index);
						break;
					}
				case 1:
					{
						STATUS = Course.STATUS_CODE;
						stone.BackColor = COLOR_INDEX[STATUS];
						toolTip1.RemoveAll();
						toolTip1.SetToolTip(stone, "参考示例代码");
						this.guider.ShowCode(this.index);
						break;
					}
				case 2:
					{
						var confirmResult = MessageBox.Show("将示例代码覆盖工作区，确定执行此操作吗? ",
							                    "覆盖确认!",
							                    MessageBoxButtons.YesNo);
						if (confirmResult == DialogResult.Yes) {
							STATUS = Course.STATUS_SAVE;
							stone.BackColor = COLOR_INDEX[STATUS];
							toolTip1.RemoveAll();
							this.guider.ReplicateCode(this.index);
							toolTip1.SetToolTip(stone, "已执行覆盖代码操作！");
							
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
		void StoneClick(object sender, EventArgs e)
		{
			switch (STATUS) {
				case 1:
					{
						this.guider.ShowRef(this.index);
						break;
					}
				case 2:
					{
						this.guider.ShowCode(this.index);
						break;
					}
				case 3:
					{
						this.guider.ShowCode(this.index);
						break;
					}
			}
		}
	}
}
