/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/23
 * Time: 22:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Serialization;

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
		
		public Course ReadFromFile(string fileName) {
			var serializer = new XmlSerializer(typeof(Course));
			using (var reader = XmlReader.Create(fileName))
			{
			    var course = (Course)serializer.Deserialize(reader);
			    return course;
			}
		}
	}
}
