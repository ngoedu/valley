/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/23
 * Time: 22:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml.Serialization;

namespace NGO.Train.Entity
{
	/// <summary>
	/// Description of VLink.
	/// </summary>
	public class VLink
	{
		public int ID {set; get;}
		public string Title {set;get;}
		
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
	    
	    public VLink() {
	    	
	    }
	    	
		public VLink(int id, string title, string content)
		{
			ID = id;
			Title = title;
			Content = content;
		}
	}
}
