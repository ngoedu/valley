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
		
		public Course(string cid, string cname, string ews)
		{
			ID = cid;
			Name = cname;
			Workspace = ews;
		}
		
		public string ID {private set; get;}
		
		public string Name {set; get;}
		
		public string Workspace {set; get;}
		
		private List<Step> Steps = new List<Step>();
		private List<Video> Videos = new List<Video>();
		private List<App> Apps = new List<App>();
		private List<Refer> Refs = new List<Refer>();

		public void AddRef(Refer refer)
		{
			Refs.Add(refer);
		}
		public Style Css {set; get;}
		
		public Video Video {set; get;}
		
		public void AddApp(App app) {
			Apps.Add(app);
		}
		
		public List<App> GetApps() {
			return Apps;
		}
		
		public void AddMileStone(Step step) {
			Steps.Add(step);
		}
		
		public void AddVideo(Video video) {
			Videos.Add(video);
		}
		
		public Video GetVideoByID(int id) {
			return Videos[id-1];
		}
		
		public Refer GetReferByID(int id) {
			return Refs[id-1];
		}
		
		public List<Step> GetMileStones() {
			return Steps;
		}
		
		public Step GetMileStoneByID(int id) {
			return Steps[id-1];
		}
		
		public void ClearMileStones() {
			Steps.Clear();
		}
		
		public Step GetLatestMileStone() {
			var ms = GetMileStoneByID(Steps.Count - 1);
			return ms;
		}
	}
	
	/// <summary>
	/// mile stone of the course
	/// </summary>
	public class Step {
		public Step(int id, string name, int refer, int link, string code, int status) {
			Id = id;
			Name = name;
			Reference = refer;
			Link = link;
			SourceCode = code;
			Status = status;
		}
		
		public int Id {set; get;}
		public string Name {set; get;}
		public int Reference {set; get;}
		public int Link {set; get;}
		public string SourceCode {set; get;}
		public int Status {set; get;}
	}
	
	/// <summary>
	/// course base style
	/// </summary>
	public class Style {
		public string Css {set; get;}
	}
	
	/// <summary>
	/// course Video
	/// </summary>
	public class Video {
		public int ID {set; get;}
		public string Title {set; get;}
		public string Link {set; get;}
		
		public Video (int id, string title, string link) {
			ID = id;
			title = Title;
			Link = link;
		}
	}
	
	public class App {
		public string ID {set; get;}
		
		public App(string id) {
			ID = id;
		}
	}
	
	public class Refer {
		public int ID {set; get;}
		public string Text {set; get;}
		
		public Refer(int id, string info) {
			ID = id;
			Text = info;
		}
	}
}
