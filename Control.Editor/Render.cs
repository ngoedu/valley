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
using DiffMatchPatch;

namespace NGO.Pad.Editor
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
			Renders[JEditor.Languages.JAVA.ToString()] = new JavaRender();
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

		public void HandleTextMarkup(RichTextBox rtb, my.utils.MyDiff.Item[] diffs)
		{
			//backup selection status
			int selectStartBak = rtb.SelectionStart;
            
			
            for(int i=0; i< diffs.Length; i++) {
            	my.utils.MyDiff.Item item = diffs[i];
            	
            	//locate line 
            	int lineNum = item.StartB;
	            string lineText = rtb.Lines[lineNum];  
	            int firstIdx = rtb.GetFirstCharIndexFromLine(lineNum);
            
	            //markup whole line
	            rtb.SelectionStart = firstIdx;
	            rtb.SelectionLength = lineText.Length;
	            rtb.SelectionBackColor = Color.Green;
				
				int insertLines = item.insertedB - 1;
				if ((insertLines) > 0) {
					
					for (int j=insertLines; j>0; j--)
						lineText += rtb.Lines[lineNum+j]+1;
					rtb.SelectionLength = lineText.Length;
					rtb.SelectionBackColor = Color.Green;
				}
            }

			/*
			//1.nav to first diff            
            rtb.SelectionStart = patch.start2;

			//2.render diffs
			int delete = 0;
			foreach (var df in patch.diffs)
			{
				if (df.operation == Operation.EQUAL) {
					rtb.SelectionStart += df.text.Length -1; //skip over by length of equal string
				} else if (df.operation ==Operation.DELETE) {
					delete = df.text.Length;
				} else if (df.operation ==Operation.INSERT) {
					rtb.SelectionLength = df.text.Length;
					rtb.SelectionBackColor = Color.Green;					
				}
			}
			*/
			
			//restore selection status
			rtb.SelectionStart = selectStartBak;
            rtb.SelectionLength = 0; 
		}
		
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
		private char LastChar;
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
				if (LastChar == '<') {
					return DoAutoClose(rtb);
				} else {
					return DoAutoComplete(rtb);
				}
			}
			LastChar = key;
			return false;
		}
		
		private bool DoAutoComplete(RichTextBox rtb) {
			string lastWord = GetLastWord(rtb);
			if (lastWord == null)
				return false;
			//2.backup candidate position
			int candidatePos = rtb.SelectionStart - lastWord.Length;
			//3.check auto-completion
			string toFill = parser.ToAutoComplete(lastWord);
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
		}
		
		private bool DoAutoClose(RichTextBox rtb) {
			string onString = GetOnString(rtb);
            List<Word> words = spliter.Split(onString.Substring(0, onString.Length - 1));
            if (words.Count <= 0)
            	return false;
            for(int i = words.Count - 1; i>=0; i--) {
            	if (words[i].IsHtmlTag()) {
            		string closed = parser.ToAutoClose(words[i].Inner);
					if (closed != null)	{
            			string[] splited = closed.Split(':');
            			InsertSelectedText(splited[1], rtb);
            			rtb.SelectionStart += Int16.Parse(splited[0]);
						return true;
					}
            	}
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
		
		private string GetOnString(RichTextBox rtb) {
			int ln = rtb.GetLineFromCharIndex(rtb.SelectionStart);
			int column = rtb.SelectionStart - rtb.GetFirstCharIndexFromLine(ln);
			string lineStr = rtb.Lines[ln];
			string onStr = lineStr.Substring(0, column);
			return onStr;
		}
		
		private string GetLastWord(RichTextBox rtb) {			
			string lastWord = string.Empty;
			if (rtb.Text.Length > 0) {
				string onStr = GetOnString(rtb);				
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
		
		private string MapAutoComplete(string candidate)
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
		
		private string MapAutoClose(string candidate)
		{
			//TODO: change to a efficient way for mapping fill Text
			if (candidate == "<html>") {
				return "0:/html>";
			}
			if (candidate == "<head>") {
				return "0:/head>";
			}
			if (candidate == "<body>") {
				return "0:/body>";
			}
			if (candidate == "<script>") {
				return "0:/script>";
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
		protected Parser parser = Parser.Instance(JEditor.Languages.JAVASCRIPT);
        
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
		
		protected string AutoComplete(string candidate)
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
	
	
	internal class JavaRender : Render
	{
		protected Parser parser = Parser.Instance(JEditor.Languages.JAVA);
        
      	public JavaRender() {
			 spliter = (JavaSplitor)Spliter.Instance(JEditor.Languages.JAVA);
		}
        
		public override Color BackColor()
		{
			return parser.BackColor();
		}
		
		public override Color ForeColor()
		{
			return parser.ForeColor();
		}
		
		protected string AutoComplete(string candidate)
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
