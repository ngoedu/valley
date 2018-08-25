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
		
		public int LinkID {set; get;}
		public int RefID {set; get;}
		
		public int Status {set;get;}
		
		public List<File> Files {set; get;}
		
		public Revision() {
	    	
	    }
		
		public override string ToString()
		{
			return string.Join("\r\n", this.Files);
		} 
	}
}
