/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/31
 * 时间: 22:41
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
	public class Test_Return
	{

		[Test]
		public void Test_Add_Return()
		{
			var mediator = Mock.MockTextController();
			
			
			char[] newChar = {'a'};
			mediator.CharIn(newChar);
			
			mediator.CharIn(Keyboard.CHR_RETURN);
			
			Block content = mediator.GetContent();
			var row1 = (Row)content.First.Value;
			Assert.AreEqual("[1:a][2:Return][3:LineFeed]", row1.ToString());
			
			char[] newChar2 = {'b'};
			mediator.CharIn(newChar2);
			
			Cursor cursor = mediator.GetCursor();
			Assert.AreEqual("b", cursor.Column.Value.ToString());
			
		}
		
		[Test]
		public void Test_Add_Return_Middle_2_Lines()
		{
			var mediator = Mock.MockTextController();
			Cursor cursor = mediator.GetCursor();
			Block content = mediator.GetContent();
			
			char[] newChar = {'a'};
			mediator.CharIn(newChar);
			char[] newChar1 = {'b'};
			mediator.CharIn(newChar1);
			char[] newChar2 = {'c'};
			mediator.CharIn(newChar2);
			char[] newChar3 = {'d'};
			mediator.CharIn(newChar3);
			
			var row = (Row)content.First.Value;
			//set cursor in middle of row
			cursor.Reposition(cursor.Line, row.IndexOf(2)); 
			
			mediator.CharIn(Keyboard.CHR_RETURN);
			
			Assert.AreEqual(2, content.Count);
			
			var row1 = (Row)content.First.Value;
			Assert.AreEqual("[1:a][2:b][3:Return][4:LineFeed]", row1.ToString());
			var row2 = (Row)content.Last.Value;
			Assert.AreEqual("[1:c][2:d]", row2.ToString());
			
		}
		
		[Test]
		public void Test_Add_Return_Middle_3_Lines()
		{
			var mediator = Mock.MockTextController();
			Cursor cursor = mediator.GetCursor();
			Block content = mediator.GetContent();
			
			//line1
			char[] newChar = {'a'};
			mediator.CharIn(newChar);
			mediator.CharIn(Keyboard.CHR_RETURN);
			
			//line2
			char[] newChar1 = {'b'};
			mediator.CharIn(newChar1);
			char[] newChar2 = {'c'};
			mediator.CharIn(newChar2);
			char[] newChar3 = {'d'};
			mediator.CharIn(newChar3);
			mediator.CharIn(Keyboard.CHR_RETURN);
			
			var line2 = content.IndexOf(2);
			//set cursor in middle of row
			cursor.Reposition(line2, ((Row)line2.Value).IndexOf(2));
			
			mediator.CharIn(Keyboard.CHR_RETURN);
			
			Assert.AreEqual(4, content.Count);
			
			var row1 = (Row)content.First.Value;
			Assert.AreEqual("[1:a][2:Return][3:LineFeed]", row1.ToString());
			var row2 = (Row)content.IndexOf(2).Value;
			Assert.AreEqual("[1:b][2:c][3:Return][4:LineFeed]", row2.ToString());
			var row3 = (Row)content.IndexOf(3).Value;
			Assert.AreEqual("[1:d][2:Return][3:LineFeed]", row3.ToString());
			var row4 = (Row)content.IndexOf(4).Value;
			Assert.AreEqual(null, row4.First);
			
		}
		
		
		[Test]
		public void Test_Add_Return_Create_Lines()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			
			Cursor cursor = mediator.GetCursor();
			Block content = mediator.GetContent();
			
			//create content
			document.Create("a\rbc\rdef\r");
			
			var line2 = content.IndexOf(2);
			//set cursor in middle of row
			cursor.Reposition(line2, ((Row)line2.Value).IndexOf(2));
			
			mediator.CharIn(Keyboard.CHR_RETURN);
			
			
			Assert.AreEqual(5, content.Count);
			
			var row1 = (Row)content.First.Value;
			Assert.AreEqual("[1:a][2:Return][3:LineFeed]", row1.ToString());
			var row2 = (Row)content.IndexOf(2).Value;
			Assert.AreEqual("[1:b][2:c][3:Return][4:LineFeed]", row2.ToString());
			var row3 = (Row)content.IndexOf(3).Value;
			Assert.AreEqual("[1:Return][2:LineFeed]", row3.ToString());
			var row4 = (Row)content.IndexOf(4).Value;
			Assert.AreEqual("[1:d][2:e][3:f][4:Return][5:LineFeed]", row4.ToString());
			
			var row5 = (Row)content.IndexOf(5).Value;
			Assert.AreEqual(null, row5.First);
			
		}
	}
}
