/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/3
 * 时间: 22:48
 * 
 * 
 */
using System;
using NGO.Pad.JText.Controller;
using NGO.Pad.JText.UI;

namespace NGO.Pad.JText.UnitTest.Command
{
	/// <summary>
	/// A mock Document creator with string.
	/// </summary>
	public class MockDocument
	{
		private IKeyCallback callback;
		public MockDocument(IKeyCallback kc)
		{
			this.callback = kc; 
		}
		
		public void Create(String chars) {
			char[] array = chars.ToCharArray();
			foreach (char i in array)
				callback.CharIn(new char[]{i});
		}
	}
}
