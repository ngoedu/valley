/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2019/1/21
 * Time: 22:31
 * 
 * 
 */
using System;

namespace App.Common.Tasks.Upgrade
{
	/// <summary>
	/// Description of UpgradeConf.
	/// </summary>
	public class UpgradeConf
	{
		public string upgradeSite {set; get;}
		
		public string dllJarUpgradeSite {set; get;}
		
		public int upgradeInterval {set; get;}
	}
}
