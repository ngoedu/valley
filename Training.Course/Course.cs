/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/16
 * Time: 0:47
 * 
 * 
 */
using System;
using System.Collections.Generic;

namespace NGO.Train
{
	/// <summary>
	/// NGO training Course.
	/// </summary>
	public class Course
	{
		public const int STATUS_DEFAULT = 0;
		public const int STATUS_REFER = 1;
		public const int STATUS_CODE = 2;
		public const int STATUS_SAVE = 3;
		
		
		public Course(string cname)
		{
			Name = cname;
		}
		
		public string Name {set; get;}
		
		private List<Step> Steps = new List<Step>();
		
		public void AddMileStone(Step step) {
			Steps.Add(step);
		}
		
		public List<Step> GetMileStones() {
			return Steps;
		}
		
		public void ClearMileStones() {
			Steps.Clear();
		}
	}
	
	/// <summary>
	/// mile stone of the course
	/// </summary>
	public class Step {
		public Step(int id, string name, string refer, string code, int status) {
			Id = id;
			Name = name;
			Reference = refer;
			SourceCode = code;
			Status = status;
		}
		
		public int Id {set; get;}
		public string Name {set; get;}
		public string Reference {set; get;}
		public string SourceCode {set; get;}
		public int Status {set; get;}
	}
}
