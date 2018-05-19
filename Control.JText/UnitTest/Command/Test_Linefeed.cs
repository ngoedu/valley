/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/29
 * 时间: 23:30
 * 
 * 
 */
using System;
using NUnit.Framework;
using NGO.Pad.JText.Controller;
using NGO.Pad.JText.Module;
using NGO.Pad.JText.UI;

namespace NGO.Pad.JText.UnitTest.Command
{
	[TestFixture]
	public class Test_Linefeed
	{	
		
		[Test]
		public void Test_NewLF()
		{
			var mediator = Mock.MockTextController();
			
			mediator.CharIn(Keyboard.CHR_LINEFEED);
			
			Block content = mediator.GetContent();
			var row1 = (Row)content.First.Value;
			Assert.AreEqual("LineFeed", row1.First.Value.ToString());
			
			Cursor cursor = mediator.GetCursor();
			Assert.AreEqual(null, cursor.Column);
			
		}
	}
}
