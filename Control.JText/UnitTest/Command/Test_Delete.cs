/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/8
 * 时间: 20:42
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
	public class Test_Delete
	{
		[Test]
		public void Test_Delete_1()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			
			Cursor cursor = mediator.GetCursor();
			var content = mediator.GetContent();
			
			//create content
			document.Create("abc");
			cursor.Reposition(cursor.Line, null);
			
			mediator.CharIn(Keyboard.CHR_DELETE);
			
			Assert.AreEqual(null, cursor.Column);
			Assert.AreEqual("[1:b][2:c]", cursor.Line.Value.ToString());
			
			mediator.CharIn(Keyboard.CHR_DELETE);
			
			Assert.AreEqual(null, cursor.Column);
			Assert.AreEqual("[1:c]", cursor.Line.Value.ToString());
			
			
			mediator.CharIn(Keyboard.CHR_DELETE);
			
			Assert.AreEqual(null, cursor.Column);
			Assert.AreEqual("[Empty]", cursor.Line.Value.ToString());
			
		}
		
		
		[Test]
		public void Test_Delete_2()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			
			Cursor cursor = mediator.GetCursor();
			var content = mediator.GetContent();
			
			//create content
			document.Create("abcd");
			var row = (Row)cursor.Line.Value;
			cursor.Reposition(cursor.Line, row.IndexOf(1));
			
			mediator.CharIn(Keyboard.CHR_DELETE);
			Assert.AreEqual("b", cursor.Column.Value.ToString());
			Assert.AreEqual("[1:b][2:c][3:d]", cursor.Line.Value.ToString());
			
			mediator.CharIn(Keyboard.CHR_DELETE);
			Assert.AreEqual("c", cursor.Column.Value.ToString());
			Assert.AreEqual("[1:c][2:d]", cursor.Line.Value.ToString());
			
			mediator.CharIn(Keyboard.CHR_DELETE);
			Assert.AreEqual("d", cursor.Column.Value.ToString());
			Assert.AreEqual("[1:d]", cursor.Line.Value.ToString());
			
			mediator.CharIn(Keyboard.CHR_DELETE);
			Assert.AreEqual(null, cursor.Column);
			Assert.AreEqual("[Empty]", cursor.Line.Value.ToString());
		}
	}
}
