/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/2/17
 * 时间: 10:18
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using NGO.Pad.JText.Controller;

namespace NGO.Pad.JText.UI
{
	/// <summary>
	/// This is the stage of
	/// </summary>
	public partial class MainControl : UserControl
	{
		private Graphic graphic; 
		
		private Context context;
		
		private TextController controller;
		
		private Keyboard keyboard;
		
		public MainControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			// Activates double buffering
			this.SetStyle(ControlStyles.DoubleBuffer |
			              ControlStyles.OptimizedDoubleBuffer |
			              ControlStyles.UserPaint |
			              ControlStyles.AllPaintingInWmPaint, true);
			this.UpdateStyles();
			
			this.graphic = new Graphic(this.CreateGraphics());
			
			this.context = new Context();
			
			//since controller is eagerly needs flicker but keyboard lazily,
			//so keyboard needs to be instantiated firstly
			this.keyboard = new Keyboard(this.context);
			this.context.SetFlicker(this.keyboard);
			this.controller = new TextController(this.context);
			this.context.SetKeyCallback(this.controller);
				
			this.Controls.Add(keyboard);
			
			//initiate adding a new line feed for current document
			//this.controller.CharIn(Keyboard.CHR_LINEFEED);
		}

		
		/// <summary>
		/// Get Focus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void TextControl_Enter(object sender, EventArgs e)
		{
			this.BackColor = Color.AliceBlue;
		}
		
		/// <summary>
		/// Lost focus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void TextControl_Leave(object sender, EventArgs e)
		{
			this.BackColor = Color.Gray;
		}	
		
		/// <summary>
		/// control needs repaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void TextControl_Paint(object sender, PaintEventArgs e){
			System.Diagnostics.Debug.Write("=OnPaint=");
			var stopWatch = Stopwatch.StartNew();

			this.controller.Repaint(graphic);	
			
			stopWatch.Stop();
			this.DebugTips.Text = string.Format("RePaint: {0}ms", stopWatch.Elapsed.TotalMilliseconds);
		}
	}
}