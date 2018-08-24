/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/23
 * Time: 22:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace NGO.Train.Entity
{
	/// <summary>
	/// Description of Schema.
	/// </summary>
	public class Schema
	{
		public int ID {set; get;}
		public string Name {set; get;}
		public int Duration {set; get;}
		public int Sessions {set; get;}
		public int Milestones {set; get;}
		public int Level {set; get;}
		public string Workspace {set; get;}
		
		public Schema()
		{
		}
	}
}
