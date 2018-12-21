/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/12/21
 * Time: 10:43
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace XWindowsService
{
	public class XWindowsService : ServiceBase
	{
		public const string MyServiceName = "NGO-XWindowsService";
		
		private CatalinaServer catalinaServer = new CatalinaServer("127.0.0.1",80);
		private MySQLServer mysqlServer = new MySQLServer(3306);
		
		public XWindowsService()
		{
			InitializeComponent();
		}
		
		private void InitializeComponent()
		{
			this.ServiceName = MyServiceName;
		}
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			// TODO: Add cleanup code here (if required)
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// Start this service.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			try {
				StartMySQL();
				StartTomcat();
			} catch (Exception e) {
				WriteLog(e.InnerException.ToString());
			}
		}
		
		private void StartMySQL() {
			WriteLog("startup mysql v1.0");
			
			var mysqlConf = AppDomain.CurrentDomain.BaseDirectory+ @"MySQLConfig.xml";
			
			if(!System.IO.File.Exists(mysqlConf)) {
				WriteLog(mysqlConf+" does not exist");
				return;
			}
			
			MySQLConfig conf = null;
			var serializer = new XmlSerializer(typeof(MySQLConfig));
			using (var reader = XmlReader.Create(mysqlConf))
			{
			    conf = (MySQLConfig)serializer.Deserialize(reader);
			}
			
			string fileName = conf.Executable.Replace("%MySQLHome%", conf.MySQLHome);
			string parameter = conf.Parameters.Replace("%MySQLHome%", conf.MySQLHome);
			WriteLog("fileName="+fileName);
			WriteLog("parameter="+parameter);
			
			mysqlServer.StartupSync(fileName,parameter);
			WriteLog("mysql started up");
		}
		
		private void ShutdownMySQL() {
			if (!mysqlServer.IsStartedUp())
				return;
			
			var mysqlConf = AppDomain.CurrentDomain.BaseDirectory+ @"MySQLConfig.xml";
			
			if(!System.IO.File.Exists(mysqlConf)) {
				WriteLog(mysqlConf+" does not exist");
				return;
			}
			
			MySQLConfig conf = null;
			var serializer = new XmlSerializer(typeof(MySQLConfig));
			using (var reader = XmlReader.Create(mysqlConf))
			{
			    conf = (MySQLConfig)serializer.Deserialize(reader);
			}
			
			string fileName = conf.Executable.Replace("%MySQLHome%", conf.MySQLHome);
			string parameter = conf.Parameters.Replace("%MySQLHome%", conf.MySQLHome);
			
			mysqlServer.ShutdownSync(fileName,parameter, conf.RootPass);
			WriteLog("mysql shuted down");
		}
		
		private void StartTomcat() {
			WriteLog("startup tomcat");
			
			var tomcatConf = AppDomain.CurrentDomain.BaseDirectory+ @"CatalinaConfig.xml";
			
			if(!System.IO.File.Exists(tomcatConf)) {
				WriteLog(tomcatConf+" does not exist");
				return;
			}
			
			CatalinaConfig conf = null;
			var serializer = new XmlSerializer(typeof(CatalinaConfig));
			using (var reader = XmlReader.Create(tomcatConf))
			{
			    conf = (CatalinaConfig)serializer.Deserialize(reader);
			}
			
			string fileName = conf.Executable.Replace("%JAVA_HOME%", conf.JavaHome);
			string parameter = conf.Parameters.Replace("%TOMCAT_HOME%", conf.TomcatHome);
			WriteLog("fileName="+fileName);
			WriteLog("parameter="+parameter);
			
			catalinaServer.StartupSync(fileName,parameter);
			WriteLog("tomcat started up");
		}
		
		
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			if (catalinaServer.IsStartedUp()) {
				catalinaServer.ShutdownSync();
				WriteLog("tomcat shuted down");
			}
			
			if (mysqlServer.IsStartedUp()) {
				ShutdownMySQL();
				WriteLog("mysql shuted down");
			}
		}
		
		public static void WriteLog(string message) {
			StreamWriter sw = null;
			try{
				sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+"\\logFile.txt", true);
				sw.WriteLine(DateTime.Now.ToString()+":"+message);
				sw.Flush();
				sw.Close();
			} catch {
			
			}
		}
	}
}
