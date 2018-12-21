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
using System.ServiceProcess;
using System.Text;

namespace XWindowsService
{
	static class Program
	{
		/// <summary>
		/// This method starts the service.
		/// </summary>
		static void Main()
		{
			// To run more than one service you have to add them here
			ServiceBase.Run(new ServiceBase[] { new XWindowsService() });
		}
	}
}
