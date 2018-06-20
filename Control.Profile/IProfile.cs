/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/6/20
 * Time: 19:51
 * 
 * 
 */
using System;

namespace Control.Profile
{
	/// <summary>
	/// Description of IProfile.
	/// </summary>
	public interface IProfile
	{
		void SetEnergy(int percent);
		void SetName(string name);
	}
}
