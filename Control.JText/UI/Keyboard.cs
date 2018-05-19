/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/14
 * 时间: 20:44
 * 
 * 
 */
using System;
using System.Windows.Forms;


namespace NGO.Pad.JText.UI
{
	/// <summary>
	/// Keyboard is a key event sender, it can trigger below to its key callback 
	/// 	1. char input like Alpha, Symbols and Chinese;
	/// 	2. linefeed and return;
	/// 	3. backspace;
	/// 	4. tab;
	/// 	5. navigation key;
	/// 	6. shortcuts key combination - like ctrl+a, etc.
	/// </summary>
	public class Keyboard : System.Windows.Forms.TextBox, IFlicker
	{  
		private Context context;
		
		public Keyboard() {
			this.AcceptsReturn = true;
			this.AcceptsTab = true;
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			//The imeMode="Inherit" is working fine in win10, otherwise, the char accepted is double-sized
			this.ImeMode = System.Windows.Forms.ImeMode.Alpha; //set defautl Ime
			this.Location = new System.Drawing.Point(10, 28);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "Typer";
			this.Size = new System.Drawing.Size(1, 18);
			
			//register event handler
			this.TextChanged += OnTextChanged;
			this.KeyDown += OnKeyDown;
		}
		
		public Keyboard(Context c) : this() {
			this.context = c;
		}
		
		#region static key code
		public static char[] CHR_DELETE = 		{(char)6}; 				//06 ACK=>DELETE 
		public static char[] CHR_TAB = 			{(char)9}; 				//09 Horizontal tab
		public static char[] CHR_RETURN = 		{(char)13};				//13 Carriage return 
		public static char[] CHR_LINEFEED =		{(char)10};				//10 line feed, new line
		public static char[] CHR_BACK = 		{(char)8};
		public static char[] SHIFTIN_UP = 		{(char)15, 'U'};
		public static char[] SHIFTIN_DOWN = 	{(char)15, 'D'};
		public static char[] SHIFTIN_LEFT = 	{(char)15, 'L'};
		public static char[] SHIFTIN_RIGHT = 	{(char)15, 'R'};
		public static char[] SHIFTOUT_UP = 		{(char)14, 'U'};
		public static char[] SHIFTOUT_DOWN = 	{(char)14, 'D'};
		public static char[] SHIFTOUT_LEFT = 	{(char)14, 'L'};
		public static char[] SHIFTOUT_RIGHT = 	{(char)14, 'R'};
		public static char[] CTRL_A =		 	{(char)7, 'a'};
		public static char[] CTRL_C =		 	{(char)7, 'c'};
		public static char[] CTRL_V =		 	{(char)7, 'v'};
		public static char[] CTRL_S =		 	{(char)7, 's'};
		public static char[] CTRL_X =		 	{(char)7, 'x'};
		#endregion
		
		/// <summary>
		/// Trap WM_PASTE:
		/// https://stackoverflow.com/questions/7852509/override-paste-into-textbox
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
	    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {   
	    	var callback = this.context.GetKeyCallback();
	    	switch (keyData) {
			    case Keys.Tab:{
    				callback.CharIn(CHR_TAB);
			        WndProc(ref msg);
			        return true;
    			}
	    		case Keys.Delete:{
    				callback.CharIn(CHR_DELETE);
			        WndProc(ref msg);
			        return true;
    			}
			    case Keys.Back:{
    				callback.CharIn(CHR_BACK);
			        WndProc(ref msg);
			        return true;
    			}
	    		case Keys.Return: {
    				callback.CharIn(CHR_RETURN);
			    	WndProc(ref msg);
			        return true;
    			}
	    	}
	    	
		    return base.ProcessCmdKey(ref msg, keyData);
		}
	    
	    /// <summary>
	    /// Trap WM_PASTE, control will handle it
	    /// </summary>
	    /// <param name="m"></param>
	    protected override void WndProc(ref Message m) {
	        if (m.Msg == 0x302 && Clipboard.ContainsText()) {
	            //System.Diagnostics.Debug.Write("PAST IGNORED");
			    return;
	        }
	        base.WndProc(ref m);
	    }
	    
	    public void OnTextChanged(object sender, EventArgs e)
		{
	    	var cArray = this.Text.ToCharArray();
	    	if (cArray.Length == 0)
	    		return;
	    	var callback = this.context.GetKeyCallback();
	    	callback.CharIn(cArray);
			this.Clear();
		}
	    
	    public void OnKeyDown(object sender, KeyEventArgs key)
		{
			var callback = this.context.GetKeyCallback();
	    	//map shortcut
			if ( key.Control && !key.Alt && !key.Shift){
				
				switch (key.KeyCode)
				{
				    case Keys.C:
				        callback.CharIn(CTRL_C);
				        break;
				    case Keys.A:
				        callback.CharIn(CTRL_A);
				        break;
				    case Keys.X:
				        callback.CharIn(CTRL_X);
				        break;
				    case Keys.V:
				        callback.CharIn(CTRL_V);
				        break;
					case Keys.S:
				        callback.CharIn(CTRL_S);
				        break;	
				} 				
			}
			
			//map navigation 
			switch (key.KeyCode)
			{
				case Keys.Up: {
					callback.CharIn(key.Shift ? SHIFTIN_UP : SHIFTOUT_UP);	        
					break;
				}
				case Keys.Down: {
					callback.CharIn(key.Shift ? SHIFTIN_DOWN : SHIFTOUT_DOWN);	        
					break;
				}
				case Keys.Left: {
					callback.CharIn(key.Shift ? SHIFTIN_LEFT : SHIFTOUT_LEFT);	        
					break;
				}
				case Keys.Right: {
					callback.CharIn(key.Shift ? SHIFTIN_RIGHT : SHIFTOUT_RIGHT);	        
					break;
				}						
			}
		}

		
		public void SetLocation(int x, int y)
		{
			throw new NotImplementedException();
		}

		public void SetBackgound(System.Drawing.Color color)
		{
			throw new NotImplementedException();
		}
	}
}
