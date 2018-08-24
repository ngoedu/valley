/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/23
 * Time: 22:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace NGO.Train.Entity
{
	/// <summary>
	/// Description of Course.
	/// </summary>
	public class Course
	{
		public Schema Schema {set; get;}
		public List<Tile> Apps {set; get;}
		public List<VLink> Videos {set; get;}
		public List<Refer> Refs {set; get;}
		public List<Revision> Milestons {set; get;}
		
		public Course()
		{
		}
	}
}
