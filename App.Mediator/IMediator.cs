/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/2
 * Time: 21:26
 * 
 * 
 */
using System;
using Component.Bridge;
using NGO.Protocol.AEther;

namespace App.Mediator
{
	/// <summary>
	/// Description of IMediator.
	/// </summary>
	public interface IMediator :  IOutputCallback, IEndpointCallback
	{
		void FormResized(int newHeight, int newWidth);
		void FormLoaded();
		void FormClosed();
	}
}
