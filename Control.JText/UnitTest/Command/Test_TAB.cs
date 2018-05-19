/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/5
 * 时间: 0:05
 * 
 * 
 */
using System;
using NUnit.Framework;
using NGO.Pad.JText.Module;
using NGO.Pad.JText.UI;

namespace NGO.Pad.JText.UnitTest.Command
{
	[TestFixture]
	public class Test_TAB
	{
		[Test]
		public void Test_Tab_1()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			
			Cursor cursor = mediator.GetCursor();
			Block content = mediator.GetContent();
			
			//create content
			document.Create("abrcdefghi");
			cursor.Reposition(cursor.Line, null);
			
			mediator.CharIn(Keyboard.CHR_TAB);
			
			Assert.AreEqual(40, cursor.Column.Value.Width);
		}
		
		[Test]
		public void Test_Tab_2()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			
			Cursor cursor = mediator.GetCursor();
			Block content = mediator.GetContent();
			
			//create content
			document.Create("a");
			
			mediator.CharIn(Keyboard.CHR_TAB);
			
			Assert.AreEqual(30, cursor.Column.Value.Width);
					
		}
		
		[Test]
		public void Test_Tab_3()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			
			Cursor cursor = mediator.GetCursor();
			Block content = mediator.GetContent();
			
			//create content
			document.Create("ab");
			
			mediator.CharIn(Keyboard.CHR_TAB);
			
			Assert.AreEqual(20, cursor.Column.Value.Width);
					
		}
		
		[Test]
		public void Test_Tab_4()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			
			Cursor cursor = mediator.GetCursor();
			Block content = mediator.GetContent();
			
			//create content
			document.Create("abc");
			
			mediator.CharIn(Keyboard.CHR_TAB);
			
			Assert.AreEqual(10, cursor.Column.Value.Width);
					
		}
		
		[Test]
		public void Test_Tab_5()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			
			Cursor cursor = mediator.GetCursor();
			Block content = mediator.GetContent();
			
			//create content
			document.Create("abcd");
			
			mediator.CharIn(Keyboard.CHR_TAB);
			
			Assert.AreEqual(40, cursor.Column.Value.Width);
					
		}
	}
}
