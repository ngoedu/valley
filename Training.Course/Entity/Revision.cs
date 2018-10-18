/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/23
 * Time: 22:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NGO.Train.Entity
{
	/// <summary>
	/// Description of Revision.
	/// </summary>
	public class Revision
	{
		public int ID {set; get;}
		public string Title {set;get;}
		
		//this field is duplicated with parent objec course's, but useful for remote peer - eide perfome milestone action
		public string Project{set; get;} 
		
		public int LinkID {set; get;}
		public int RefID {set; get;}
		
		public int Status {set;get;}
		
		public List<File> Files {set; get;}
		
		public Revision() {
	    	
	    }

		public string ToXml()
		{
			//save content temperaly
			var dicTemp = new Dictionary<string, string>();
			foreach(File f in Files) {
				dicTemp[f.Name+f.Path] = f.Content;
				f.Content = f.Code;
			}
			
			//build xml string
			XmlSerializer xsSubmit = new XmlSerializer(typeof(Revision));
			var xmlRevision = string.Empty;
			using(var sww = new StringWriter())
			{
			    using(XmlWriter writer = XmlWriter.Create(sww))
			    {
			        xsSubmit.Serialize(writer, this);
			        xmlRevision = sww.ToString(); 
			    }
			}
			
			//revert the content back
			foreach(File f in Files) {
				f.Content = dicTemp[f.Name+f.Path] ;
			}
			
			return xmlRevision;
		}
		public override string ToString()
		{
			return string.Join("\r\n", this.Files);
		} 
	}
}
