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
		
		public const int STATUS_DEFAULT = 0;
		public const int STATUS_REFER = 1;
		public const int STATUS_CODE = 2;
		public const int STATUS_SAVE = 3;
		
		
		
		public Schema Schema {set; get;}
		public List<Tile> Apps {set; get;}
		public List<VLink> Videos {set; get;}
		public List<Refer> Refs {set; get;}
		public List<Revision> Milestons {set; get;}
		
		public Course()
		{
		}

		public Revision GetMileStoneByID(int index)
		{
			if (index <0 || index >= Milestons.Count)
				return null;
			
			return Milestons[index - 1];
		}

		public VLink GetVideoByID(int index)
		{
			if (index <0 || index >= Videos.Count)
				return null;
			
			return Videos[index - 1];
		}
		
		public Refer GetReferByID(int reference)
		{
			if (reference <0 || reference >= Refs.Count)
				return null;
			
			return Refs[reference - 1];
		}

		public Revision GetLatestMileStone()
		{
			return Milestons[Milestons.Count - 1];
		}
	}
}
