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

namespace App.Common.Tasks
{
	/// <summary>
	/// Description of UpgradeTask.
	/// </summary>
	public class UpgradeTask
	{
		private const string path = "json/cmeta";
		private const string site = "http://192.168.0.12/scup/";
		    
		public UpgradeTask()
		{	
		}
		
		public void LaunchTask() {
			var taskForAction = new Task(() =>
            {
			    //int radomTimeSlot = new Random().Next(100,200); //TODO: CHANGE IT TO LARGER ONE
				//Thread.Sleep(radomTimeSlot);
				DownloadCourseMetaFile();
            });
　　　　　　
			taskForAction.Start();
		}
		
		private  void  DownloadCourseMetaFile() {
			WebClient webClient = new WebClient();
			webClient.QueryString.Add("key", "1");
			webClient.QueryString.Add("token", "543OGN13d");
			string fileUrl = string.Empty;
			try {
				//normally http 200 returned
				fileUrl = webClient.DownloadString(site+path);
				string destFile = CodeBase.GetCourseDataPath() + "\\dat.ngjs";
				webClient.DownloadFile(new System.Uri(site+fileUrl), destFile);
	
				UnGzipFile(destFile);
				//MessageBox.Show("cmeta downloaded.");
		   
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
		
		
		private void UnGzipFile(string filePath) {
			FileInfo gzipFileName = new FileInfo(filePath);
			using (FileStream fileToDecompressAsStream = gzipFileName.OpenRead())
			{
				string decompressedFileName = filePath.Replace(".ngjs", ".js");
			    using (FileStream decompressedStream = File.Create(decompressedFileName))
			    {
			        using (GZipStream decompressionStream = new GZipStream(fileToDecompressAsStream, CompressionMode.Decompress))
			        {
			            try
			            {
			                decompressionStream.CopyTo(decompressedStream);
			            }
			                catch (Exception ex)
			            {
			                Console.WriteLine(ex.Message);
			            }
			        }
			    }
			}

		}
	}
}
