/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/5
 * Time: 14:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml;
using App.Common.Md5;

namespace NGO.Train
{
	/// <summary>
	/// Singlton of CourseLoader.
	/// </summary>
	public class CourseLoader
	{
		private static readonly Lazy<CourseLoader> lazy = new Lazy<CourseLoader>(() => new CourseLoader());
	    public static CourseLoader Instance { get { return lazy.Value; } }
	    
		private CourseLoader()
		{
		}
		
		public Course Load(string cFolder, string cid, bool md5) {
			
			//1. check if specific md5 hashed folder existed
			string md5Folder = md5 ? MD5Util.StringMD5(cid) : cid;
			if (!Directory.Exists(cFolder+@"\"+md5Folder)) {
				return null;
			}
			
			//2. read course into memory
			string courseData = System.IO.File.ReadAllText(cFolder+@"\"+md5Folder+@"\package.dat");
	    	
			var xdoc = new XmlDocument();
            xdoc.LoadXml(courseData);
            var root=xdoc.DocumentElement;  
           
            var courseId	=root.SelectNodes("/course/schema/id")[0].InnerText;
            var courseName	=root.SelectNodes("/course/schema/name")[0].InnerText;  
            var duration	=root.SelectNodes("/course/schema/duration")[0].InnerText;  
            var sessionNum	=root.SelectNodes("/course/schema/session")[0].InnerText;  
            var milestoneNum=root.SelectNodes("/course/schema/milestone")[0].InnerText;  
            var level		=root.SelectNodes("/course/schema/condition")[0].InnerText;  
            var ews		=root.SelectNodes("/course/schema/workspace")[0].InnerText;  
            
            Course course = new Course(courseId, courseName, ews);
            
             var apps=root.SelectNodes("/course/app/tile");  
            for(int i=0;i<apps.Count;i++)  
            {    
            	course.AddApp(new App(apps[i].Attributes["id"].Value));
            }
            
            var videos=root.SelectNodes("/course/video/link");  
            for(int i=0;i<videos.Count;i++)  
            {    
            	var vid = Int16.Parse(videos[i].Attributes["id"].Value);
                var vtitle = videos[i].Attributes["title"].Value;
				var vlink = videos[i].InnerText;
				course.AddVideo(new Video(vid, vtitle, vlink));
            }
            
			var mileStones=root.SelectNodes("/course/milestone/entry");  
            for(int i=0;i<mileStones.Count;i++)  
            {    
            	var eid = Int16.Parse(mileStones[i].Attributes["id"].Value);
                var etitle = mileStones[i].Attributes["title"].Value;
                var elink = Int16.Parse(mileStones[i].Attributes["link"].Value);
				var eref = mileStones[i].Attributes["ref"].Value;
				var esrc = mileStones[i].InnerText;
				course.AddMileStone(new Step(eid, etitle, eref, elink, esrc, 0));
            }
            
            
			
			//3. read trainig records into memory
			string trainingRecords = System.IO.File.ReadAllText(cFolder+@"\"+md5Folder+@"\tr.dat");
			var xtr = new XmlDocument();
            xtr.LoadXml(trainingRecords);
            var troot=xtr.DocumentElement; 
           	//TODO: check security
           	
           	//
	    	var trainings=troot.SelectNodes("/course/milestone/entry");  
            for(int i=0;i<trainings.Count;i++)  
            {    
            	var eid = Int16.Parse(trainings[i].Attributes["id"].Value);
            	var estat = Int16.Parse(trainings[i].Attributes["status"].Value);
				course.GetMileStoneByID(eid).Status = estat;
            }
			return course;
		}
	}
}
