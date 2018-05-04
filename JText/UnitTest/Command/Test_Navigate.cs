/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/3
 * 时间: 23:00
 * 
 * 
 */
using System;
using NUnit.Framework;
using NGO.Pad.JText.Module;
using NGO.Pad.JText.UI;

namespace NGO.Pad.JText.UnitTest.Command
{
	/// <summary>
	/// Description of TestNavigate.
	/// </summary>
	[TestFixture]
	public class Test_Navigate
	{
		[Test]
		public void Test_Nav_1()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			
			Cursor cursor = mediator.GetCursor();
			Block content = mediator.GetContent();
			
			//create content
			document.Create("ab\rcde\rfghi");
			mediator.CharIn(Keyboard.SHIFTOUT_UP);
			Assert.AreEqual("e", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_RIGHT);
			Assert.AreEqual("e", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			Assert.AreEqual("d", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			Assert.AreEqual("c", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			Assert.AreEqual(null, cursor.Column);
			
			mediator.CharIn(Keyboard.SHIFTOUT_UP);
			Assert.AreEqual(null, cursor.Column);
			
			mediator.CharIn(Keyboard.SHIFTOUT_RIGHT);
			Assert.AreEqual("a", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_RIGHT);
			Assert.AreEqual("b", cursor.Column.Value.ToString());
			
		}
		
		[Test]
		public void Test_Nav_2()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			
			Cursor cursor = mediator.GetCursor();
			Block content = mediator.GetContent();
			
			//create content
			document.Create("ab\rcde\r\rfghi");
			mediator.CharIn(Keyboard.SHIFTOUT_RIGHT);
			Assert.AreEqual("i", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			Assert.AreEqual("f", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			Assert.AreEqual(null, cursor.Column);
			
			mediator.CharIn(Keyboard.SHIFTOUT_UP);
			Assert.AreEqual(null, cursor.Column);
			
			mediator.CharIn(Keyboard.SHIFTOUT_RIGHT);
			Assert.AreEqual(null, cursor.Column);
			
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			Assert.AreEqual(null, cursor.Column);
			
			mediator.CharIn(Keyboard.SHIFTOUT_UP);
			Assert.AreEqual(null, cursor.Column);
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			Assert.AreEqual(null, cursor.Column);
			
			mediator.CharIn(Keyboard.SHIFTOUT_RIGHT);
			Assert.AreEqual("c", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_RIGHT);
			Assert.AreEqual("d", cursor.Column.Value.ToString());
			mediator.CharIn(Keyboard.SHIFTOUT_RIGHT);
			Assert.AreEqual("e", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_RIGHT);
			Assert.AreEqual("e", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			Assert.AreEqual("d", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_UP);
			Assert.AreEqual("b", cursor.Column.Value.ToString());
			
			mediator.CharIn(Keyboard.SHIFTOUT_RIGHT);
			Assert.AreEqual("b", cursor.Column.Value.ToString());
			
			
			mediator.CharIn(Keyboard.SHIFTOUT_LEFT);
			Assert.AreEqual("a", cursor.Column.Value.ToString());
			
			
		}
	}
}
