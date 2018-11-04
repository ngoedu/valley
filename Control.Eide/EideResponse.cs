/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/4
 * Time: 19:20
 * 
 * 
 */
using System;
using System.IO;
using System.Xml.Serialization;

namespace Control.Eide
{
	/// <summary>
	/// Description of EideResponse.
	/// </summary>
	[XmlRoot]
	public class EideResponse
	{
		public const string STATUS_OK = "OK";
		public const string STATUS_NOK = "NOK";
		public const string STATUS_EXCEPTION = "EXCEPTION";
		
		
		[XmlElement]
		public string action {set; get;}
		[XmlElement]
		public string status {set; get;}
		[XmlElement]
		public string message {set; get;}
		
		public EideResponse()
		{
		}
		
		public static EideResponse Parse(string xml) {
			var serializer = new XmlSerializer(typeof(EideResponse));
			EideResponse result;
			
			using (TextReader reader = new StringReader(xml))
			{
			    result = (EideResponse)serializer.Deserialize(reader);
			}
			
			return result;
		}
	}
}
