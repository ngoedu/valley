/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/22
 * Time: 23:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Serialization;

namespace NGO.Train
{
	/// <summary>
	/// Description of CourseWriter.
	/// </summary>
	public class CourseWriter
	{
		private static readonly Lazy<CourseWriter> lazy = new Lazy<CourseWriter>(() => new CourseWriter());
	    public static CourseWriter Instance { get { return lazy.Value; } }
	    
		private CourseWriter()
		{
		}
		
		public void WriteToFile(Course course, string file) {
			var serializer = new XmlSerializer(course.GetType());
			using (var writer = XmlWriter.Create(file))
			{
			    serializer.Serialize(writer, course);
			}
		}
	
	}
}
