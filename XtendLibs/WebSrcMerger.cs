/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/8/20
 * Time: 21:45
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace XtendLibs
{
	/// <summary>
	/// A class which have the ability to merge js and css which linked by HTML page into that html file.
	/// </summary>
	public class WebSrcMerger
	{
		public bool IsRemoveJsComment = false;
		public bool IsRemoveNewLine = false;
		
		public void MergeAllHtmls(string srcPath, string destPath, string suffix) {
			string[] htmlFiles = Directory.GetFiles(srcPath, suffix, SearchOption.TopDirectoryOnly);
			foreach(var f in htmlFiles)
				this.MergeHtml(System.IO.File.ReadAllText(f), f, destPath, srcPath);
			
		}
		
		private void MergeHtml(string theHTML, string fileName, string destPath, string srcPath) {
			var browser = new WebBrowser();
			browser.ScriptErrorsSuppressed = true;
			browser.DocumentText="0";
			browser.Document.OpenNew(true);
			browser.Document.Write(theHTML);
			browser.Refresh();
			
    		//get document
			var doc = browser.Document;
			
			MergeCssManually(doc, srcPath);
			MergeJS(doc, srcPath);
			
			var finalContent = doc.Body.Parent.OuterHtml;
			if (pendingCSS != string.Empty) {
				int idx = finalContent.IndexOf("</head>",StringComparison.OrdinalIgnoreCase);
				if (idx <=0)
					idx = finalContent.IndexOf("<body>",StringComparison.OrdinalIgnoreCase);
				if (idx > 0)
				
				finalContent = finalContent.Insert(idx, pendingCSS);
			}
			
			if (IsRemoveNewLine)
				finalContent = this.RemoveCRLF(finalContent);
			
     		System.Diagnostics.Debug.WriteLine(finalContent);
     		
     		var destFile = destPath+"/" + Path.GetFileName(fileName);
     		DumpToFile(finalContent, destFile);
		}
		
		private void DumpToFile(string htmlContent,string fileName) {
			var newFileName = fileName.Replace(@".html", ".html");
			System.IO.File.WriteAllText(newFileName, htmlContent);
		}
		
		private void MergeCss(HtmlDocument doc, string srcPath) {
			var linkCollection = doc.GetElementsByTagName("link");
     		foreach(HtmlElement css in linkCollection) {
     			string cssFile = css.GetAttribute("href");
     			//TODO:verify the type
     			css.OuterHtml = string.Empty;

				var doc2 = doc.DomDocument as IHTMLDocument2;
				//mshtml.HTMLBodyClass body = (mshtml.HTMLBodyClass)doc2.body;
				IHTMLStyleSheet  ss = doc2.createStyleSheet("",0);
				ss.cssText = LoadSrc(srcPath + @"\"+cssFile);
     		}
		}
		
		private string pendingCSS = string.Empty;
		private void MergeCssManually(HtmlDocument doc, string srcPath) {
			var linkCollection = doc.GetElementsByTagName("link");
     		foreach(HtmlElement css in linkCollection) {
     			string cssFile = css.GetAttribute("href");
     			//TODO:verify the type
     			css.OuterHtml = string.Empty;

     			pendingCSS += string.Format("<STYLE>{0}</STYLE>",LoadSrc(srcPath + @"\"+cssFile));
				
     		}
		}
		
		
		private void MergeJS(HtmlDocument doc, string srcPath) {
			var jsCollection = doc.GetElementsByTagName("script");
     		foreach(HtmlElement js in jsCollection) {
     			string jsFile = js.GetAttribute("src");
     			string skip = js.GetAttribute("skip");
     			
     			if (skip.Equals("true"))
     				continue;
     			
     			js.OuterHtml = string.Empty;
     			
     			HtmlElement newJs = doc.CreateElement("script");
				//newJs.SetAttribute("type", "text/javascript"); newJs.SetAttribute("src", ""); newJs.Id = "xxx";
        		
				var jselement = (IHTMLScriptElement)newJs.DomElement;
				
				var jsSrc = LoadSrc(srcPath + @"\"+jsFile);
				if (IsRemoveJsComment)
					jsSrc = this.RemoveCommentFromJS(jsSrc);
				
     			jselement.text = jsSrc;
     			doc.Body.AppendChild(newJs);
     		}
		}
		
		private string RemoveCommentFromJS(string jsContent) {
			if  (string.IsNullOrEmpty(jsContent))
				return null;
			string[] lines = jsContent.Split(Environment.NewLine.ToCharArray());
			var sb = new StringBuilder();
			foreach (var l in lines) {
				if (l.TrimStart().StartsWith("//")) {
					//sb.Append(l.TrimStart().Replace("//", string.Empty));
				} else {
					sb.Append(l);
				}
			}
			return sb.ToString();
		}
		
		private string RemoveCRLF(string htmlContent) {
			if  (string.IsNullOrEmpty(htmlContent))
				return null;
			string nlines = htmlContent.Replace(Environment.NewLine, string.Empty);
			return nlines;
		}
		
		private string LoadSrc(string fileName) {
			return System.IO.File.ReadAllText(fileName);
		}
	}
}