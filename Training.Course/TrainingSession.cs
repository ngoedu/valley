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

namespace NGO.Train
{
	/// <summary>
	/// Description of TrainSession.
	/// </summary>
	public class TrainingSession
	{
		public int Point{set;get;}
		public Security Security {set; get;}
		
		public List<MsStatus> Progress {set; get;}
		
		public TrainingSession()
		{
		}
	}
	
	public class Security {
		public string CID {set; get;}
		public string UID {set; get;}
		public string Token {set; get;}
		
		public Security () {
			
		}
	}
	
	public class MsStatus {
		public int MSID {set; get;}
		public int Status {set; get;}
		
		public MsStatus() {
			
		}
	}
}
