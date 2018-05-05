/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/16
 * 时间: 22:22
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NGO.Pad.JEditor
{
	/// <summary>
	/// render keywords 
	/// key and Text Change event handler
	/// </summary>
	public abstract class Render
	{
		private static Dictionary<String, Render> Renders = new Dictionary<string, Render>();
		protected Spliter spliter = null;
		public static Render Instance(JEditor.Languages language)
		{	
			return Renders[language.ToString()];
		}
		
		static Render()
		{         
			Renders[JEditor.Languages.HTML.ToString()] = new HTMLRender();
			Renders[JEditor.Languages.JAVASCRIPT.ToString()] = new JavascriptRender();
			Renders[JEditor.Languages.CSS.ToString()] = new CSSRender();
		}
		
		public abstract Color BackColor();
		
		public abstract Color ForeColor();

		public virtual void ForceRenderAll(RichTextBox rtb){
			int selectStart = rtb.SelectionStart;
    		for(int line = 0; line < rtb.Lines.Length ; line++) {
	    		string lineStr = rtb.Lines[line];  
	            int lineStart = rtb.GetFirstCharIndexFromLine(line);  
	
	            
	            rtb.SelectionStart = lineStart;  
	            rtb.SelectionLength = lineStr.Length;  
	            rtb.SelectionColor = ForeColor();
				rtb.SelectionFont = JEditor.DEFAULT_FONT;                
	            rtb.SelectionStart = 0;  
	            rtb.SelectionLength = 0; 
	            //System.Diagnostics.Debug.WriteLine(base.SelectionFont);
	 			
	            if (spliter.IsComment(lineStr)) {
	            	Comment(lineStr, rtb, selectStart, lineStart);
	            	//reset the selection status
	            	rtb.SelectionStart = selectStart;  
	            	rtb.SelectionLength = 0;  
	            	rtb.SelectionColor = ForeColor(); 
	            } 
	            else
	            {
					List<Word> words = spliter.Split(lineStr);                 
	                for (int i = 0; i < words.Count; i++) {
						Coloring(words[i], rtb, selectStart, lineStart);
						//reset the selection status
	            		rtb.SelectionStart = selectStart;  
	            		rtb.SelectionLength = 0;  
	            		rtb.SelectionColor = ForeColor(); 
	                }                	
	            }
			}
		}
		
		public abstract bool HandleKey(char key, RichTextBox rtb);
		
		public virtual void HandleTextChanged(RichTextBox rtb) {
			int selectStart = rtb.SelectionStart;  
            int line = rtb.GetLineFromCharIndex(selectStart);  
            string lineStr = rtb.Lines[line];  
            int lineStart = rtb.GetFirstCharIndexFromLine(line);  
//System.Diagnostics.Debug.WriteLine("OnTextChanged - line={0},column={1}",line,rtb.SelectionStart - rtb.GetFirstCharIndexFromLine(line));
            rtb.SelectionStart = lineStart;  
            rtb.SelectionLength = lineStr.Length;  
            rtb.SelectionColor = ForeColor();
			rtb.SelectionFont = JEditor.DEFAULT_FONT;                
            rtb.SelectionStart = selectStart;  
            rtb.SelectionLength = 0; 
            //System.Diagnostics.Debug.WriteLine(base.SelectionFont);
 			
            if (spliter.IsComment(lineStr)) {
            	Comment(lineStr, rtb, selectStart, lineStart);
            	//reset the selection status
            	rtb.SelectionStart = selectStart;  
            	rtb.SelectionLength = 0;  
            	rtb.SelectionColor = ForeColor(); 
            } 
            else
            {
				List<Word> words = spliter.Split(lineStr);                 
                for (int i = 0; i < words.Count; i++) {
					Coloring(words[i], rtb, selectStart, lineStart);
					//reset the selection status
            		rtb.SelectionStart = selectStart;  
            		rtb.SelectionLength = 0;  
            		rtb.SelectionColor = ForeColor(); 
                }                	
            }
		}
		
		public abstract void Coloring(Word word, RichTextBox tbase, int selectStart, int lineStart);
		
		public abstract void Comment(string line, RichTextBox tbase, int selectStart, int lineStart);
		
	}
	
	/// //////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// HTML 
	/// </summary>
	internal class HTMLRender : Render
	{
		private HTMLParser parser = (HTMLParser)Parser.Instance(JEditor.Languages.HTML);
		
		public HTMLRender() {
			spliter = (HTMLSplitor)Spliter.Instance(JEditor.Languages.HTML);
		}
      
		public override Color BackColor()
		{
			return parser.BackColor();
		}
		
		public override Color ForeColor()
		{
			return parser.ForeColor();
		}
		
		public override bool HandleKey(char key, RichTextBox rtb)
		{
			if (key == '\t') {
				string lastWord = GetLastWord(rtb);
				if (lastWord == null)
					return false;
				//2.backup candidate position
				int candidatePos = rtb.SelectionStart - lastWord.Length;
				//3.check auto-completion
				string toFill = this.AutoComplete(lastWord);
				if (toFill == null)
					return false;
				string[] splited = toFill.Split(':');
				string prefix = splited[0];
				string suffix = splited[2];
				rtb.SelectionColor = this.ForeColor();
				//AC1. add prefix
				rtb.SelectionStart = candidatePos;
				//base.SelectedText=prefix;
				InsertSelectedText(prefix, rtb);
				//AC2. complete the candidate
				rtb.SelectionStart = candidatePos + lastWord.Length + prefix.Length;
				//base.SelectedText = suffix;
				InsertSelectedText(suffix, rtb);
				rtb.SelectedText = string.Empty;
				//AC3. reset cursor pos
				rtb.SelectionStart = candidatePos + lastWord.Length + prefix.Length + Int16.Parse(splited[1]);
				//AC4. ignore the tab
				return true;
			} else if (key == '<' ) {
				//TODO: auto-close does not sensitive now
				/*
				string lastWord = GetLastWord(rtb);
				if (lastWord != string.Empty) {
					string closed = AutoClose(lastWord);
					if (closed != null)	{	
						InsertSelectedText(closed, rtb);
						return true;
					}
				}
				*/
			}
			return false;
		}
	
		public override void Coloring(Word word, RichTextBox tbase, int selectStart, int lineStart)
		{
			//System.Diagnostics.Debug.WriteLine("ls={0},idx={1}",lineStart,index);
			bool fuzzy = false;
			var color = parser.IsKeyword(word.Inner, ref fuzzy);
			if (color == Color.Empty)
				return;
           	
			if (!fuzzy) {
				tbase.SelectionStart = lineStart + word.Index;
				tbase.SelectionLength = word.Inner.Length;  
				tbase.SelectionColor = color;
				return;
			}
           	
			//fuzzy matched, splite down the attributes
			// an example: <a href="/tags/tag_ul.asp" title="HTML">
			char[] cArray = word.Inner.ToCharArray();
			bool kwDone = false, quaOpen = false;
			int startIndex = lineStart + word.Index;
			int renderedPos = 0;
			for (int i = 0; i < cArray.Length; i++) {
				if (cArray[i] == ' ' || cArray[i] == '\t') {
					if (!kwDone) {
						tbase.SelectionStart = startIndex;
						tbase.SelectionLength = i; 
						renderedPos = i;
						tbase.SelectionColor = color;
						kwDone = true;
					} else if (!quaOpen) {
						renderedPos = i;
					}
           			
				} else if (cArray[i] == '=') {
					tbase.SelectionStart = startIndex + renderedPos;
					tbase.SelectionLength = i - renderedPos; 
					renderedPos = i;
					tbase.SelectionColor = parser.AttrKeyColor();
				} else if (cArray[i] == '\'' || cArray[i] == '"') {
					if (!quaOpen) {
						quaOpen = true;
						renderedPos = i;
					} else {
						tbase.SelectionStart = startIndex + renderedPos;
						tbase.SelectionLength = i - renderedPos + 1; 
						renderedPos = i;
						tbase.SelectionColor = parser.AttrValueColor();
						quaOpen = false;
					}
				} else if (cArray[i] == '>') {
					tbase.SelectionStart = startIndex + i;
					tbase.SelectionLength = 1; 
					tbase.SelectionColor = color;
				}
			}    	
		}
		
		public override void Comment(string line, RichTextBox tbase, int selectStart, int lineStart)
		{
			var color = parser.CommentColor();
			tbase.SelectionStart = lineStart + 0;
			tbase.SelectionLength = line.Length;  
			tbase.SelectionColor = color;
		}
		
		private string GetLastWord(RichTextBox rtb) {			
			string lastWord = string.Empty;
			if (rtb.Text.Length > 0) {
				int ln = rtb.GetLineFromCharIndex(rtb.SelectionStart);
				int column = rtb.SelectionStart - rtb.GetFirstCharIndexFromLine(ln);
	
				string lineStr = rtb.Lines[ln];
				string onStr = lineStr.Substring(0, column);
				if (onStr.Length <=0) 
					return null;
				int i = onStr.LastIndexOf(' ');
				i = i < 0 ? onStr.LastIndexOf('\t') : i;
				i = i < 0 ? onStr.Substring(0, onStr.Length - 1).LastIndexOf('>') : i;
				i = i < 0 ? 0 : i;
				//1. get candidate word
				lastWord = onStr.Substring(i == 0 ? 0 : i + 1).Trim();
			}
			System.Diagnostics.Debug.WriteLine(lastWord);
			return lastWord;
		}
		
		private void InsertSelectedText(string text, RichTextBox rtb)
		{
			string[] stringSeparators = new string[] { "\r\n" };
			string[] lines = text.Split(stringSeparators, StringSplitOptions.None);
			for (int i = 0; i < lines.Length; i++)
				if (i == 0)
					rtb.SelectedText = lines[0];
				else
					rtb.SelectedText = "\r\n" + lines[i];
		}
		
		private string AutoComplete(string candidate)
		{
			//TODO: change to a efficient way for mapping fill Text
			if (candidate == "html") {
				return "<!DOCTYPE html>\r\n<:16:>\r\n<head>\r\n\t<title></title>\r\n</head>\r\n<body>\r\n</body>\r\n</html>";
			}
			if (candidate == "head") {
				return "<:3:>\r\n\t\r\n</head>";
			}
			if (candidate == "body") {
				return "<:3:>\r\n\t\r\n</body>";
			}
			if (candidate == "script") {
				return "<:3:>\r\n\t\r\n</script>";
			}
			return null;
		}
		
		private string AutoClose(string candidate)
		{
			//TODO: change to a efficient way for mapping fill Text
			if (candidate == "<html>") {
				return "</html>";
			}
			if (candidate == "<head>") {
				return "</head>";
			}
			if (candidate == "<body>") {
				return "</body>";
			}
			if (candidate == "<script>") {
				return "</script>";
			}
			return null;
		}


	}
	
	/// //////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// JAVASCRIPT 
	/// </summary>
	internal class JavascriptRender : Render
	{
		private Parser parser = Parser.Instance(JEditor.Languages.JAVASCRIPT);
        
      	public JavascriptRender() {
			 spliter = (JavascriptSplitor)Spliter.Instance(JEditor.Languages.JAVASCRIPT);
		}
        
		public override Color BackColor()
		{
			return parser.BackColor();
		}
		
		public override Color ForeColor()
		{
			return parser.ForeColor();
		}
		
		private string AutoComplete(string candidate)
		{
			//TODO: change to a efficient way for mapping fill Text
			return "2:XXXXXXXXXX";	
		}

		public override bool HandleKey(char key, RichTextBox rtb)
		{	
			return false;
		}
		
		public override void Coloring(Word word, RichTextBox tbase, int selectStart, int lineStart)
		{
			//System.Diagnostics.Debug.WriteLine("ls={0},idx={1}",lineStart,index);
			bool fuzzy = false;
			var color = parser.IsKeyword(word.Inner, ref fuzzy);
			if (color == Color.Empty)
				return;
			tbase.SelectionStart = lineStart + word.Index;
			tbase.SelectionLength = word.Inner.Length;  
			tbase.SelectionColor = color;
		}
		
		public override void Comment(string line, RichTextBox tbase, int selectStart, int lineStart)
		{
			var color = parser.CommentColor();
			tbase.SelectionStart = lineStart + 0;
			tbase.SelectionLength = line.Length;  
			tbase.SelectionColor = color;
		}
	}
	
	/// //////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// CSS render
	/// </summary>
	internal class CSSRender : Render
	{
		private Parser parser = Parser.Instance(JEditor.Languages.CSS);
        
        public CSSRender() {
			 spliter = (CSSSplitor)Spliter.Instance(JEditor.Languages.CSS);
		}
        
		public  override Color BackColor()
		{
			return parser.BackColor();
		}
		
		public override Color ForeColor()
		{
			return parser.ForeColor();
		}
		
		private string AutoComplete(string candidate)
		{
			//TODO: change to a efficient way for mapping fill Text
			return "2:XXXXXXXXXX";	
		}
		
		public override bool HandleKey(char key, RichTextBox rtb)
		{
			return false;
		}
		
		public override void Coloring(Word word, RichTextBox tbase, int selectStart, int lineStart)
		{
			//System.Diagnostics.Debug.WriteLine("ls={0},idx={1}",lineStart,index);
			bool fuzzy = false;
			var color = parser.IsKeyword(word.Inner, ref fuzzy);
			if (color == Color.Empty)
				return;
			tbase.SelectionStart = lineStart + word.Index;
			tbase.SelectionLength = word.Inner.Length;  
			tbase.SelectionColor = color;
		}
		
		public override void Comment(string line, RichTextBox tbase, int selectStart, int lineStart)
		{
			var color = parser.CommentColor();
			tbase.SelectionStart = lineStart + 0;
			tbase.SelectionLength = line.Length;  
			tbase.SelectionColor = color;
		}
	}
}
