/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/13
 * 时间: 2:49
 * 
 * 
 */
using System;  
using System.Collections.Generic;
using System.Drawing;  
using System.Runtime.InteropServices;  
using System.Windows.Forms;
using HWND = System.IntPtr;  

namespace NGO.Pad.JEditor
{
	/// <summary>
	/// RichTextBox based ngo Editor which used for code demostration.
	/// </summary>
	public partial class JEditor : System.Windows.Forms.RichTextBox  
    {  
        private int line;  
        private Render render;
        private Spliter spliter;
       
        public string Path {set; get;}
        
        /// <summary>
        /// invoke this for preventing screen from twinkling during rendering
        /// </summary>
 		[DllImport("user32")]  
        private static extern int SendMessage(HWND hwnd, int wMsg, int wParam, IntPtr lParam);  
        private const int WM_SETREDRAW = 0xB;  
        private static Font DEFAULT_ASCII_FONT = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));		
        private static Font DEFAULT_FONT = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        
        public JEditor(Languages language)  
        {  
        	render = Render.Instance(language);
        	spliter = Spliter.Instance(language);
            WordWrap = false;
            Font = DEFAULT_FONT;
            AcceptsTab = true;
            AutoWordSelection = false;
        	ImeMode = System.Windows.Forms.ImeMode.Alpha; //set defautl Ime
        	Cursor = System.Windows.Forms.Cursors.IBeam;
        	BackColor = render.BackColor();
        	ForeColor = render.ForeColor();
        	this.KeyPress += OnKeyPress;
        	
        }
        
        /// <summary>
        /// special key handling by render
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e){
        	e.Handled = render.HandleKey(e.KeyChar, this);
        }
        
        
        /// <summary>
        /// persist to file
        /// </summary>
        public void SaveToFile() {
        	base.SaveFile(Path, RichTextBoxStreamType.UnicodePlainText);
        }
        
        public void LoadFromFile(string path) {
        	base.LoadFile(path, RichTextBoxStreamType.UnicodePlainText);
        	RenderAll();
        }
        
        private void RenderAll() {
        	int selectStart = base.SelectionStart; 
        	for(int line = 0; line <base.Lines.Length ; line++) {
        		string lineStr = base.Lines[line];  
                int lineStart = base.GetFirstCharIndexFromLine(line);  
  
                SendMessage(base.Handle, WM_SETREDRAW, 0, IntPtr.Zero);  
  
                base.SelectionStart = lineStart;  
                base.SelectionLength = lineStr.Length;  
                base.SelectionColor = render.ForeColor();
				base.SelectionFont = DEFAULT_FONT;                
                base.SelectionStart = 0;  
                base.SelectionLength = 0; 
                //System.Diagnostics.Debug.WriteLine(base.SelectionFont);
     			
                if (spliter.IsComment(lineStr)) {
                	render.Comment(lineStr, this, selectStart, lineStart);
                	//reset the selection status
	            	base.SelectionStart = selectStart;  
	            	base.SelectionLength = 0;  
	            	base.SelectionColor = render.ForeColor(); 
                } 
                else
                {
					List<Word> words = spliter.Split(lineStr);                 
	                for (int i = 0; i < words.Count; i++) {
						render.Coloring(words[i], this, selectStart, lineStart);
						//reset the selection status
	            		base.SelectionStart = selectStart;  
	            		base.SelectionLength = 0;  
	            		base.SelectionColor = render.ForeColor(); 
	                }                	
                }
                                
                SendMessage(base.Handle, WM_SETREDRAW, 1, IntPtr.Zero);  
                base.Refresh();
        	}
       	}
  
        protected override void OnTextChanged(EventArgs e)  
        {            
            if (base.Text.Length > 0)  
            {  
                int selectStart = base.SelectionStart;  
                line = base.GetLineFromCharIndex(selectStart);  
                string lineStr = base.Lines[line];  
                int lineStart = base.GetFirstCharIndexFromLine(line);  
  System.Diagnostics.Debug.WriteLine("OnTextChanged - line={0},column={1}",line,base.SelectionStart - base.GetFirstCharIndexFromLine(line));
                SendMessage(base.Handle, WM_SETREDRAW, 0, IntPtr.Zero);  
  
                base.SelectionStart = lineStart;  
                base.SelectionLength = lineStr.Length;  
                base.SelectionColor = render.ForeColor();
				base.SelectionFont = DEFAULT_FONT;                
                base.SelectionStart = selectStart;  
                base.SelectionLength = 0; 
                //System.Diagnostics.Debug.WriteLine(base.SelectionFont);
     			
                if (spliter.IsComment(lineStr)) {
                	render.Comment(lineStr, this, selectStart, lineStart);
                	//reset the selection status
	            	base.SelectionStart = selectStart;  
	            	base.SelectionLength = 0;  
	            	base.SelectionColor = render.ForeColor(); 
                } 
                else
                {
					List<Word> words = spliter.Split(lineStr);                 
	                for (int i = 0; i < words.Count; i++) {
						render.Coloring(words[i], this, selectStart, lineStart);
						//reset the selection status
	            		base.SelectionStart = selectStart;  
	            		base.SelectionLength = 0;  
	            		base.SelectionColor = render.ForeColor(); 
	                }                	
                }
                                
                SendMessage(base.Handle, WM_SETREDRAW, 1, IntPtr.Zero);  
                base.Refresh();  
            }  
            base.OnTextChanged(e);  
        }  
  
        public new bool WordWrap  
        {  
            get { return base.WordWrap; }  
            set { base.WordWrap = value; }  
        }  
  
        public enum Languages  
        {  
            SQL,  
            JAVA,  
            HTML,  
            CSS,
			JAVASCRIPT            
        }  
  
        private Languages language = Languages.CSS;  
  
        public Languages Language  
        {  
            get { return this.language; }  
            set { this.language = value; }  
        }  
    }  
}  
