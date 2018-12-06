/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/8/25
 * Time: 23:55
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using App.Common.Md5;
using log4net;

namespace NGO.Train
{
	/// <summary>
	/// Description of TrainSession.
	/// </summary>
	public class TrainingSession
	{
		[XmlIgnore]
		private static readonly ILog logger = LogManager.GetLogger(typeof(TrainingSession));  

		public  static  TrainingSession LoadSession(string path) {
			return CourseReader.Instance.ReadTrainingSessionFromFile(path);
		}
		
		public  static  void WriteSessionToFile(TrainingSession tr, string path) {
			CourseWriter.Instance.WriteTrainSessToFile(tr, path);
		}
		
		public static string mysalt = "tR@NG0";
		public static void InitSessionWithSecurity(string cid, string uid, int point, string path) {
			var tr = new TrainingSession();
			tr.Point = point;
			tr.Security = new Security();
			tr.Security.CID = cid;
			tr.Security.UID = uid;
			
			var token = MD5Util.StringMD5(uid+cid, mysalt);
			logger.Info("InitSessionWithSecurity, token="+token);
			
			tr.Security.Token = token;
			WriteSessionToFile(tr, path+"/tr.dat");
		}
		
		
		public int Point{set;get;}
		public Security Security {set; get;}
		
		public List<MsStatus> Progress {set; get;}
		
		public TrainingSession()
		{
		}
	}
	
	public class Security {
		[XmlIgnore]
		private static readonly ILog logger = LogManager.GetLogger(typeof(TrainingSession));  

		public bool Validated(string userId, string cid)
		{
			string md5 = MD5Util.StringMD5(userId+cid, TrainingSession.mysalt);
			logger.Info("Security.Validated, token="+md5);
			
			if (this.Token.Equals(md5))
			    return true;
			    
			return false;
		}

		public string CID {set; get;}
		public string UID {set; get;}
		public string Token {set; get;}
		
		public Security () {
			
		}
	}
	
	public class MsStatus {
		public int MSID {set; get;}
		public int Status {set; get;}

		public MsStatus(int iD, int status)
		{
			this.MSID = iD; this.Status = status;
		}
		public MsStatus() {
			
		}
	}
}
