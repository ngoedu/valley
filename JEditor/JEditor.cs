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
        private Render render;
        public string Path {set; get;}
        
        /// <summary>
        /// invoke this for preventing screen from twinkling during rendering
        /// </summary>
 		[DllImport("user32")]  
        private static extern int SendMessage(HWND hwnd, int wMsg, int wParam, IntPtr lParam);  
        private const int WM_SETREDRAW = 0xB;  
        public static Font DEFAULT_FONT = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        
        public JEditor(Languages language)  
        {  
        	render = Render.Instance(language);
        	WordWrap = false;
            Font = DEFAULT_FONT;
            AcceptsTab = true;
            AutoWordSelection = false;
        	ImeMode = System.Windows.Forms.ImeMode.Alpha; //set defautl Ime
        	Cursor = System.Windows.Forms.Cursors.IBeam;
        	BackColor = render.BackColor();
        	ForeColor = render.ForeColor();
        	KeyPress += OnKeyPress;
        	
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
        /// force re-render keywords line by line
        /// </summary>
        private void RenderAll() {
    		SendMessage(base.Handle, WM_SETREDRAW, 0, IntPtr.Zero);  
    		render.ForceRenderAll(this);                          
            SendMessage(base.Handle, WM_SETREDRAW, 1, IntPtr.Zero);  
            base.Refresh();	
       	}
  
        protected override void OnTextChanged(EventArgs e)  
        {            
            if (base.Text.Length > 0){              
 	            SendMessage(base.Handle, WM_SETREDRAW, 0, IntPtr.Zero);  
                render.HandleTextChanged(this);
                SendMessage(base.Handle, WM_SETREDRAW, 1, IntPtr.Zero);  
                base.Refresh();  
            }  
            base.OnTextChanged(e);  
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
        
        public new bool WordWrap  
        {  
            get { return base.WordWrap; }  
            set { base.WordWrap = value; }  
        }  
  
        public enum Languages {  
            SQL, JAVA, HTML, CSS, JAVASCRIPT            
        }  
  
        private Languages language = Languages.CSS;  
  
        public Languages Language  
        {  
            get { return this.language; }  
            set { this.language = value; }  
        }  
    }  
}  
