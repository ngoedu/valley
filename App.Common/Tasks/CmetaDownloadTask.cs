/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/3
 * Time: 10:41
 * 
 * 
 */
using System;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

namespace App.Common.Tasks
{
	/// <summary>
	/// Description of DownloadTask.
	/// </summary>
	public class CmetaDownloadTask
	{
		private static readonly ILog logger = LogManager.GetLogger(typeof(CmetaDownloadTask));  

		private string filePath;
		public CmetaDownloadTask(string key, string path)
		{
			this.filePath = path;
			
		}
		
		public void LaunchTask() {
			var taskForAction = new Task(() =>
            {
			    logger.Info("CMETA download task started");
			    //int radomTimeSlot = new Random().Next(100,200); //TODO: CHANGE IT TO LARGER ONE
				//Thread.Sleep(radomTimeSlot);
				DownloadCourseMetaFile();
            });
		}
		
		private  void  DownloadCourseMetaFile() {
			WebClient webClient = new WebClient();
			webClient.QueryString.Add("token", "543OGN13d");
			try {
				
				string site = ""; //TODO:
				string destFile = CodeBase.GetWUIDataPath() + "\\dat.ngjs";
				webClient.DownloadFile(new System.Uri(site+filePath), destFile);
	
				UnGzipFile(destFile);
		   
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
