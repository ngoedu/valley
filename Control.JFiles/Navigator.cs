/*
 * Created by SharpDevelop.
 * User: xho
 * Date: 2018-04-19
 * Time: 3:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Control.Files
{
	/// <summary>
	/// Navigator.
	/// </summary>
	public partial class Navigator : UserControl, INavigator
	{
		public static Color NAV_SPLIT_BG = Color.FromArgb(248, 250, 243);
		public static Color REFRESH_BG = Color.FromArgb(236, 242, 249);
		public static Color FOLDER_BG = Color.FromArgb(236, 242, 249);
		
		private RefreshNode refresh;
		private ArrayList NodeStack = new ArrayList();
		private IFiles browser ;
		
		private static Graphics g;
		public Navigator(IFiles browser)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			g = this.CreateGraphics();
			this.SizeChanged += new System.EventHandler(this.NavigateResize);
			this.browser = browser;
			refresh = new RefreshNode(global::Control.JFiles.Resource1.icon2, this, browser);
		
			this.Controls.Add(refresh);
		}
		
		public Graphics GetGraphic() {
			return g;
		}
			
		
		public void AddPathNode(string name, string path) {
			var newNode = new TextNode(name, this);
			newNode.Tag = path;
			var newSplitor = new ImageNode(global::Control.JFiles.Resource1.icon3, this);
			newNode.Splitor = newSplitor;
			NodeStack.Add(newNode);
			
			this.Controls.Add(newNode);
			this.Controls.Add(newSplitor);
			
			if (name.Equals(browser.GetRootName())) {
				var ToolTip1 = new System.Windows.Forms.ToolTip();
				ToolTip1.SetToolTip( newNode, (string)newNode.Tag);
			}
			
			Resize();
		}
		
		public void ChangePath(TextNode node) {
			for (int i=NodeStack.Count - 1; i>=0; i--) {
				if (NodeStack[i] !=node) {
					this.Controls.Remove((TextNode)NodeStack[i]);
					this.Controls.Remove(((TextNode)NodeStack[i]).Splitor);
					NodeStack.RemoveAt(i);
				} else {
					TextNode to = (TextNode)NodeStack[i];
					this.browser.Refresh((string)to.Tag);
					return;
				}
			}
		}
		
		public int GetHeight() {
			return this.Height;
		}
		public int GetWidth() {
			return this.Width;
		}
		public int GetTop() {
			return this.Top;
		}
		public int GetLeft(){
			return this.Left;
		}
		
		private void Resize() {
			Height=  26;
			refresh.NodeResize();
			int nodeLeft = 0;
			for (int i=0; i< NodeStack.Count; i++) {
				TextNode node = (TextNode)NodeStack[i];
				node.NodeResize();
				
				var splitor = (ImageNode)node.Splitor;
				splitor.NodeResize();
				
				node.Left = nodeLeft;
				nodeLeft+=node.Width;
				
				splitor.Left = nodeLeft;
				nodeLeft+=splitor.Width;
			}
		}
		
		void NavigateResize(object sender, EventArgs e)
		{
			Resize();
		}
			
	}
	
	interface INode {
		void NodeResize();
	}
	
	public class TextNode : Label, INode
	{
		public ImageNode Splitor {set; get;}
		private INavigator parent;
		private static Color HOVER = System.Drawing.SystemColors.GradientActiveCaption;
		private static Font DEFAULT_FONT = new System.Drawing.Font("Lucida Console", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		private static Font CAL_WIDTH_FONT = new System.Drawing.Font("Lucida Console", 10F);
		
		public TextNode(string name, Navigator parent)
		{
			BackColor = Navigator.NAV_SPLIT_BG;
			BorderStyle = System.Windows.Forms.BorderStyle.None;
			Font = DEFAULT_FONT;
			TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			MouseEnter += new System.EventHandler(this.LabelMouseEnter);
			MouseLeave += new System.EventHandler(this.LabelMouseLeave);

			Text = name;
			this.parent = parent;
		}
		
		public void NodeResize() {
			this.Top = parent.GetTop() - 3 ;
			this.Height = parent.GetHeight();
			//this.Width = Text.Length <=3 ? Text.Length * 11 : Text.Length * 10;
			SizeF size = parent.GetGraphic().MeasureString(Text, CAL_WIDTH_FONT);
			this.Width = (int)size.Width + 3;
		}
			
		
		protected override void OnClick(EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("text click! ");
			parent.ChangePath(this);
		}
		
		private void LabelMouseEnter(object sender, System.EventArgs e)
		{
			Label lbl = (Label)sender;
			lbl.BackColor = HOVER;
		}

		private void LabelMouseLeave(object sender, System.EventArgs e)
		{
			Label lbl = (Label)sender;
			lbl.BackColor = Navigator.NAV_SPLIT_BG;
		}
	}
	
	/// <summary>
	/// Image node
	/// </summary>
	public class ImageNode : PictureBox, INode
	{
		protected INavigator parent;
		public ImageNode(Image image, Navigator parent)
		{
			BackColor = Navigator.NAV_SPLIT_BG;
			BorderStyle = System.Windows.Forms.BorderStyle.None;
			SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			MouseEnter += new System.EventHandler(this.PictureBoxMouseEnter);
			MouseLeave += new System.EventHandler(this.PictureBoxMouseLeave);

			Image = image;
			this.parent = parent;
		}
		
		public  void NodeResize() {
			this.Top = parent.GetTop() - 3;
			this.Height = parent.GetHeight();
			this.Width = 18;
		}
		
		protected override void OnClick(EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine(" click! ");
		}
		
		private void PictureBoxMouseEnter(object sender, System.EventArgs e)
		{
			PictureBox lbl = (PictureBox)sender;
			lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;	
		}

		private void PictureBoxMouseLeave(object sender, System.EventArgs e)
		{
			PictureBox lbl = (PictureBox)sender;
			lbl.BorderStyle = System.Windows.Forms.BorderStyle.None;	
		}
	}
	
	/// <summary>
	/// refresh node
	/// </summary>
	public class RefreshNode : ImageNode
	{
		private IFiles browser;
		public RefreshNode(Image image, Navigator parent, IFiles browser): base(image, parent)
		{
			BackColor = Navigator.REFRESH_BG;
			BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.browser = browser;		
		}
		
		public  void NodeResize() {
			this.Width = 26;
			this.Height = parent.GetHeight();
			this.Top = parent.GetTop() - 3;
			this.Left = parent.GetLeft() + parent.GetWidth() - this.Width - 2;
		}
		
		protected override void OnClick(EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("refresh! ");
			this.browser.RefreshCurrent();
		}
	}
}
