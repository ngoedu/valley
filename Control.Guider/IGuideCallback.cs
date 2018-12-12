/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/12/6
 * Time: 21:04
 * 
 * 
 */
using System;

namespace Control.Guider
{
	/// <summary>
	/// Description of IGuideCallback.
	/// </summary>
	public interface IGuideCallback
	{
		int EnergyDecrease(int level, int index);
		
	}
}
