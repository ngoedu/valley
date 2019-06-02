/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/10/29
 * Time: 14:14
 * 
 * 
 */
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using App.Common.Tasks.Upgrade;

namespace App.Common.Tasks
{
	/// <summary>
	/// Description of UpgradeTask.
	/// </summary>
	public class UpgradeTask
	{
		private const string path = "json/supg";
		private const string site = "http://192.168.0.11/scup/";
		
		private static readonly ILog logger = LogManager.GetLogger(typeof(UpgradeTask));  

		public UpgradeTask()
		{	
		}
		
		public void LaunchTask() {
			var taskForAction = new Task(() =>
            {
			    logger.Info("upgrade task started");
			    //int radomTimeSlot = new Random().Next(100,200); //TODO: CHANGE IT TO LARGER ONE
				//Thread.Sleep(radomTimeSlot);
				DownloadUpgradePackFile();
            });
　　　　　　
			taskForAction.Start();
		}
		
		private  void  DownloadUpgradePackFile() {
			WebClient webClient = new WebClient();
			webClient.QueryString.Add("key", "1");
			webClient.QueryString.Add("token", "543OGN13d");
			string packJson = string.Empty;
			try {
				//normally http 200 returned
				packJson = webClient.DownloadString(site+path);
				
		   
			} catch (WebException webex) {
				
				HttpWebResponse webResp = (HttpWebResponse) webex.Response;
				
				switch (webResp.StatusCode)
				{
					case HttpStatusCode.BadRequest: // 400
					{
						MessageBox.Show("ex=400 bad request");	
						break;
					}
					case HttpStatusCode.NotFound: // 404
						break;
					case HttpStatusCode.InternalServerError: // 500 
					{
						MessageBox.Show("ex=500, internal error");	
						break;
					}
					default:
						break;
				}
			}
		}
		
		
	}
}
