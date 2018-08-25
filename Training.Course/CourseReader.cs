/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/23
 * Time: 22:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using App.Common.Md5;
using NGO.Train.Entity;

namespace NGO.Train
{
	/// <summary>
	/// Description of CourseReader.
	/// </summary>
	public class CourseReader
	{
		private static readonly Lazy<CourseReader> lazy = new Lazy<CourseReader>(() => new CourseReader());
	    public static CourseReader Instance { get { return lazy.Value; } }
	    
		private CourseReader()
		{
		}
		
		public Course ReadCourseFrom(string folder, string fileName, bool isHashed) {
			string md5Folder = isHashed ? MD5Util.StringMD5(fileName) : fileName;
			if (!Directory.Exists(folder+@"\"+md5Folder)) {
				return null;
			}
			
			return ReadCourseFromFile(folder+@"\"+md5Folder+@"\pack.dat");
		}
		
		public TrainingSession ReadTrainingSessionFrom(string folder, string fileName, bool isHashed) {
			string md5Folder = isHashed ? MD5Util.StringMD5(fileName) : fileName;
			if (!Directory.Exists(folder+@"\"+md5Folder)) {
				return null;
			}
			
			return ReadTrainingSessionFromFile(folder+@"\"+md5Folder+@"\tr.dat");
		}
		
		
		public Course ReadCourseFromFile(string fileName) {
			var serializer = new XmlSerializer(typeof(Course));
			using (var reader = XmlReader.Create(fileName))
			{
			    var course = (Course)serializer.Deserialize(reader);
			    return course;
			}
		}
		
		public TrainingSession ReadTrainingSessionFromFile(string fileName) {
			var serializer = new XmlSerializer(typeof(TrainingSession));
			using (var reader = XmlReader.Create(fileName))
			{
			    var trSession = (TrainingSession)serializer.Deserialize(reader);
			    return trSession;
			}
		}
	}
}
