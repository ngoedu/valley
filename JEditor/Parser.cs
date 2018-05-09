/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/13
 * 时间: 2:46
 * 
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;  
using System.IO;  
using System.Reflection;  
using System.Drawing;
using Snow.X.Algorithm; 

namespace NGO.Pad.Editor
{
	/// <summary>
	/// Description of Parser.
	/// </summary>
	public abstract class Parser  
    {  
        private static Dictionary<String, Parser> Parsers = new Dictionary<string, Parser>();
        protected XmlDocument xdoc;
 		
        public static Parser Instance(JEditor.Languages language){
 			return Parsers[language.ToString()];
		}  
		
		static Parser()   
        {         
        	Parsers[JEditor.Languages.HTML.ToString()] = new HTMLParser();
        	Parsers[JEditor.Languages.JAVASCRIPT.ToString()] = new JavascriptParser();
        	Parsers[JEditor.Languages.CSS.ToString()] = new CSSParser();
        } 

		protected void LoadConfig(JEditor.Languages language) {
            string filename = string.Empty;
            switch(language) 
            {  
                case JEditor.Languages.JAVA:  
                    filename="java.xml";  
                    break;  
                case JEditor.Languages.HTML:  
                    filename="html.xml";  
                    break;               
                case JEditor.Languages.CSS:  
                    filename="css.xml";  
                    break;  
                case JEditor.Languages.JAVASCRIPT:  
                    filename="javascript.xml";  
                    break;  
                case JEditor.Languages.SQL:  
               	 	filename="sql.xml";  
                	break;  
                default:  
                    break;  
            } 
            
            StreamReader reader= new StreamReader(filename,  System.Text.Encoding.UTF8); 
            xdoc = new XmlDocument();
            xdoc.Load(reader); 
        }
		
		protected Color ParseColor(string colorName) {
			if (colorName.StartsWith("#",StringComparison.Ordinal)) {
				string[] rgb = colorName.Remove(0,1).Split(',');
				Color rgbColor = Color.FromArgb(Int16.Parse(rgb[0]), Int16.Parse(rgb[1]), Int16.Parse(rgb[2]));
				return rgbColor;
			}			
			return Color.FromName(colorName); 
		}
				
        public abstract Color IsKeyword(string word, ref bool fuzzy);
        public abstract Color CommentColor();
        public abstract Color ForeColor();
        public abstract Color BackColor();
        public abstract string ToAutoComplete(string key);
        public abstract string ToAutoClose(string key);
    }
	
	/// <summary>
	/// HTML parser
	/// </summary>
	internal class HTMLParser : Parser
	{
		readonly TrieDict dict = new TrieDict();
		private int FUZZY = 0;
		protected ArrayList swords=null;
		protected ArrayList fwords=null;        
 		protected ArrayList scolors=null;	//static words
 		protected ArrayList fcolors=null; 	//fuzzy words
		protected Color comment; 	
		protected bool caseSensitive = false;
		protected Color backColor;
		protected Color foreColor;
		protected Color attKeyColor;
		protected Color attValueColor;
		protected Dictionary<string, string> completeDict;
		protected Dictionary<string, string> closeDict;
		
	
		public HTMLParser() {
			
			LoadConfig(JEditor.Languages.HTML);
			ParseConfig();
			
			for(int i=0; i<this.swords.Count; i++)
				dict.Insert((string)this.swords[i], i);
			FUZZY = swords.Count;
			string spart = null;
			for(int i=0; i<this.fwords.Count; i++) {
				spart = (string)this.fwords[i];
				//'*' represents HTML attributes 
				spart = spart.Split('*')[0]; 
				dict.Insert(spart, FUZZY + i);
			}		
		}
		
		private void ParseConfig() {
			swords=new ArrayList();  
            fwords=new ArrayList();
			scolors=new ArrayList();  
            fcolors=new ArrayList();
           	completeDict = new Dictionary<string, string>();
            closeDict = new Dictionary<string, string>();
            
            XmlElement root=xdoc.DocumentElement;  
            
            string colorName = null, value = null;
            XmlNodeList xnl=root.SelectNodes("/definition/keywords/sword");  
            this.caseSensitive = bool.Parse(root.Attributes["caseSensitive"].Value);  
            
            for(int i=0;i<xnl.Count;i++)
            {   
            	value = xnl[i].ChildNodes[0].Value;
            	swords.Add(this.caseSensitive ? value : value.ToLower());
                colorName = xnl[i].Attributes["color"].Value;
                scolors.Add(ParseColor(colorName));
            }
            
            xnl=root.SelectNodes("/definition/keywords/fword");  
            for(int i=0;i<xnl.Count;i++)  
            {    
            	value = xnl[i].ChildNodes[0].Value;
            	fwords.Add(this.caseSensitive ? value : value.ToLower());
                colorName = xnl[i].Attributes["color"].Value;
                fcolors.Add(ParseColor(colorName));                  
            }
            
            xnl=root.SelectNodes("/definition/color-schema/comment");   
            colorName = xnl[0].Attributes["color"].Value;
            comment = ParseColor(colorName);                  

            xnl=root.SelectNodes("/definition/color-schema/background");  
            colorName = xnl[0].Attributes["color"].Value;
            backColor = ParseColor(colorName);
            
            xnl=root.SelectNodes("/definition/color-schema/foreground");  
            colorName = xnl[0].Attributes["color"].Value;
            foreColor = ParseColor(colorName);

			xnl=root.SelectNodes("/definition/color-schema/attribKey");  
          	if (xnl.Count > 0) {
            	colorName = xnl[0].Attributes["color"].Value;
            	attKeyColor = ParseColor(colorName); 
			} 

			xnl=root.SelectNodes("/definition/color-schema/attribValue");
			if (xnl.Count > 0) {
            	colorName = xnl[0].Attributes["color"].Value;
            	attValueColor = ParseColor(colorName); 
			}
			
			//https://stackoverflow.com/questions/514635/represent-space-and-tab-in-xml-tag
			xnl=root.SelectNodes("/definition/auto-complete/word");  
            for(int i=0;i<xnl.Count;i++)  
            {    
            	value = xnl[i].ChildNodes[0].Value;
            	completeDict.Add(xnl[i].Attributes["key"].Value, value.Replace(@"\r\n", "\r\n").Replace(@"\t", "\t"));
            }
            
            xnl=root.SelectNodes("/definition/auto-close/word");  
            for(int i=0;i<xnl.Count;i++)  
            {    
            	value = xnl[i].ChildNodes[0].Value;
            	closeDict.Add("<"+xnl[i].Attributes["key"].Value+">",  value.Replace(@"\r\n", "\r\n").Replace(@"\t", "\t"));               
            }
		}
		
		public override string ToAutoComplete(string key) {
			string tryValue = null;
			completeDict.TryGetValue(key, out tryValue);
			return tryValue;
		}
        
		public override string ToAutoClose(string key) {
			string tryValue = null;
			closeDict.TryGetValue(key, out tryValue);
			return tryValue;
		}
		
		public override Color IsKeyword(string word, ref bool fuzzy) {
			fuzzy = false;
			string key = this.caseSensitive ? word : word.ToLower();
			int idx = dict.Scan(key);
			if (idx == -1)
				return Color.Empty;
			if (idx < FUZZY)
				return (Color)scolors[idx];
			//TODO: remove this for a not restrict check
			if (key.EndsWith(">", StringComparison.Ordinal)) {
				fuzzy = true;
				return (Color)fcolors[idx - FUZZY];				
			}
			else
				return Color.Empty;				
		}
		
		public override Color CommentColor()
		{
			return comment;
		}
		
		public override Color BackColor()
		{
			return backColor;
		}
		
		public override Color ForeColor()
		{
			return foreColor;
		}
		
		public Color AttrKeyColor()
		{
			return attKeyColor;
		}
		
		public Color AttrValueColor()
		{
			return attValueColor;
		}
	}
	
	/// <summary>
	/// Javascropt parser
	/// </summary>
	internal class JavascriptParser : Parser
	{
		readonly TrieDict dict = new TrieDict();
		private int FUZZY = 0;
		protected ArrayList swords=null;
		protected ArrayList fwords=null;        
 		protected ArrayList scolors=null;	//static words
 		protected ArrayList fcolors=null; 	//fuzzy words
		protected Color comment; 	
		protected bool caseSensitive = false;  
		protected Color backColor;
		protected Color foreColor;
		
		public JavascriptParser() {
			LoadConfig(JEditor.Languages.JAVASCRIPT);
			ParseConfig();
			for(int i=0; i<this.swords.Count; i++)
				dict.Insert((string)this.swords[i], i);
			FUZZY = swords.Count;
			string spart = null;
			for(int i=0; i<this.fwords.Count; i++) {
				spart = (string)this.fwords[i];
				//'*' represents plain string 
				spart = spart.Split('*')[0]; 
				dict.Insert(spart, FUZZY + i);
			}			
		}
		
		private void ParseConfig() {
			swords=new ArrayList();  
            fwords=new ArrayList();
			scolors=new ArrayList();  
            fcolors=new ArrayList();
           
            XmlElement root=xdoc.DocumentElement;  
            
            string colorName = null, value = null;
            XmlNodeList xnl=root.SelectNodes("/definition/keywords/sword");  
            this.caseSensitive = bool.Parse(root.Attributes["caseSensitive"].Value);  
            
            for(int i=0;i<xnl.Count;i++)
            {   
            	value = xnl[i].ChildNodes[0].Value;
            	swords.Add(this.caseSensitive ? value : value.ToLower());
                colorName = xnl[i].Attributes["color"].Value;
                scolors.Add(ParseColor(colorName));
            }
            
            xnl=root.SelectNodes("/definition/keywords/fword");  
            for(int i=0;i<xnl.Count;i++)  
            {    
            	value = xnl[i].ChildNodes[0].Value;
            	fwords.Add(this.caseSensitive ? value : value.ToLower());
                colorName = xnl[i].Attributes["color"].Value;
                fcolors.Add(ParseColor(colorName));                  
            }
            
            xnl=root.SelectNodes("/definition/color-schema/comment");   
            colorName = xnl[0].Attributes["color"].Value;
            comment = ParseColor(colorName);                  

            xnl=root.SelectNodes("/definition/color-schema/background");  
            colorName = xnl[0].Attributes["color"].Value;
            backColor = ParseColor(colorName);
            
            xnl=root.SelectNodes("/definition/color-schema/foreground");  
            colorName = xnl[0].Attributes["color"].Value;
            foreColor = ParseColor(colorName);

		}
		
		public override Color IsKeyword(string word, ref bool fuzzy) {
			string key = this.caseSensitive ? word : word.ToLower();
			var stopWatch = Stopwatch.StartNew();
			int idx = dict.Scan(key);
			stopWatch.Stop();
			//System.Diagnostics.Debug.WriteLine(string.Format("javascript scan: {0}ms", stopWatch.Elapsed.TotalMilliseconds));
			if (idx == -1)
				return Color.Empty;
			if (idx < FUZZY)
				return (Color)scolors[idx];
			return (Color)fcolors[idx - FUZZY];
		}
		
		public override string ToAutoComplete(string key) {
			return null;
		}
        
		public override string ToAutoClose(string key) {
			return null;
		}
		
		public override Color CommentColor()
		{
			return comment;
		}
		
		public override Color BackColor()
		{
			return backColor;
		}
		
		public override Color ForeColor()
		{
			return foreColor;
		}
	}
	
	/// <summary>
	/// CSS parser
	/// </summary>
	internal class CSSParser : Parser
	{
		readonly TrieDict dict = new TrieDict();
		private int FUZZY = 0;
		protected ArrayList swords=null;
		protected ArrayList fwords=null;        
 		protected ArrayList scolors=null;	//static words
 		protected ArrayList fcolors=null; 	//fuzzy words
		protected Color comment; 		//comment
		protected bool caseSensitive = false;  
		protected Color backColor;
		protected Color foreColor;
		
		public CSSParser() {
			LoadConfig(JEditor.Languages.CSS);
			ParseConfig();
			for(int i=0; i<this.swords.Count; i++)
				dict.Insert((string)this.swords[i], i);
			FUZZY = swords.Count;
			string spart = null;
			for(int i=0; i<this.fwords.Count; i++) {
				spart = (string)this.fwords[i];
				//'*' represents plain string 
				spart = spart.Split('*')[0]; 
				dict.Insert(spart, FUZZY + i);
			}			
		}
		
		private void ParseConfig() {
			swords=new ArrayList();  
            fwords=new ArrayList();
			scolors=new ArrayList();  
            fcolors=new ArrayList();
            XmlElement root=xdoc.DocumentElement;  
            
            string colorName = null, value = null;
            XmlNodeList xnl=root.SelectNodes("/definition/keywords/sword");  
            this.caseSensitive = bool.Parse(root.Attributes["caseSensitive"].Value);  
            
            for(int i=0;i<xnl.Count;i++)
            {   
            	value = xnl[i].ChildNodes[0].Value;
            	swords.Add(this.caseSensitive ? value : value.ToLower());
                colorName = xnl[i].Attributes["color"].Value;
                scolors.Add(ParseColor(colorName));
            }
            
            xnl=root.SelectNodes("/definition/keywords/fword");  
            for(int i=0;i<xnl.Count;i++)  
            {    
            	value = xnl[i].ChildNodes[0].Value;
            	fwords.Add(this.caseSensitive ? value : value.ToLower());
                colorName = xnl[i].Attributes["color"].Value;
                fcolors.Add(ParseColor(colorName));                  
            }
            
            xnl=root.SelectNodes("/definition/color-schema/comment");   
            colorName = xnl[0].Attributes["color"].Value;
            comment = ParseColor(colorName);                  

            xnl=root.SelectNodes("/definition/color-schema/background");  
            colorName = xnl[0].Attributes["color"].Value;
            backColor = ParseColor(colorName);
            
            xnl=root.SelectNodes("/definition/color-schema/foreground");  
            colorName = xnl[0].Attributes["color"].Value;
            foreColor = ParseColor(colorName);
		}
		
		public override Color IsKeyword(string word, ref bool fuzzy) {
			string key = this.caseSensitive ? word : word.ToLower();
			var stopWatch = Stopwatch.StartNew();
			int idx = dict.Scan(key);
			stopWatch.Stop();
			System.Diagnostics.Debug.WriteLine(string.Format("css scan: {0}ms", stopWatch.Elapsed.TotalMilliseconds));
			if (idx == -1)
				return Color.Empty;
			if (idx < FUZZY)
				return (Color)scolors[idx];
			return (Color)fcolors[idx - FUZZY];
		}
		
		public override string ToAutoComplete(string key) {
			return null;
		}
        
		public override string ToAutoClose(string key) {
			return null;
		}
		
		public override Color CommentColor()
		{
			return comment;
		}
		
		public override Color BackColor()
		{
			return backColor;
		}
		
		public override Color ForeColor()
		{
			return foreColor;
		}
	}
}
