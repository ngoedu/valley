/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/12/4
 * Time: 19:40
 * 
 * 
 */
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Control.Profile
{
	/// <summary>
	/// Description of Account.
	/// </summary>
	public class Account
	{
		
		public static void WriteToFile(Account account, string fileName) {
			var serializer = new XmlSerializer(account.GetType());
			using (var writer = XmlWriter.Create(fileName))
			{
			    serializer.Serialize(writer, account);
			}
		}
		
		public static Account ReadAccountFromFile(string fileName) {
			if(!File.Exists(fileName))
				return null;
			
			var serializer = new XmlSerializer(typeof(Account));
			using (var reader = XmlReader.Create(fileName))
			{
			    var acc = (Account)serializer.Deserialize(reader);
			    return acc;
			}
		}
		
		public Account() {
			
			Random rand = new Random(DateTime.Now.Millisecond);
			int id = rand.Next( int.MinValue, int.MaxValue );
			id = Math.Abs(id);
			this.ID = id.ToString();
		}
		
		public Account(string id) {
			this.ID = id;
		}
		
		public string ID {set; get;}
		
		public string Name {set; get;}
		
		public string Mobile {set; get;}
		
		public string Email {set; get;}
		
		
	}
}
