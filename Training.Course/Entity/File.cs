/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/23
 * Time: 22:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml.Serialization;

namespace NGO.Train.Entity
{
	/// <summary>
	/// Description of File.
	/// </summary>
	public class File
	{
		public string Name {set; get;}
		public string Path {set;get;}
		
		[XmlIgnore]
		public string Content {set; get;}
		
		[XmlElement("Content")]
	    public System.Xml.XmlCDataSection ContentCDATA
	    {
	        get
	        {
	            return new System.Xml.XmlDocument().CreateCDataSection(Content);
	        }
	        set
	        {
	            Content = value.Value;
	        }
	    }
	    
		public override string ToString()
		{
			return string.Format("name=[{0}],path=[{1}],content=[{2}]", Name, Path, Content);;
		} 
	}
}
