/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/5/16
 * Time: 0:44
 * 
 * 
 */
using System;
using NGO.Train;

namespace NGO.Pad.Guider
{
	/// <summary>
	/// Description of IGuider.
	/// </summary>
	public interface IGuider
	{
		void BindCourse(Course course);
	}
}
