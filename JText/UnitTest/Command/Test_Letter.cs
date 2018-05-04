/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/8
 * 时间: 20:29
 * 
 * 
 */
using NUnit.Framework;
using NGO.Pad.JText.Controller;
using NGO.Pad.JText.Module;
using NGO.Pad.JText.UI;


namespace NGO.Pad.JText.UnitTest.Command
{
	[TestFixture]
	public class Test_Letter
	{
		
		[Test]
		public void Test_AddLast()
		{
			var mediator = Mock.MockTextController();
			
			char chr = 'c';
			mediator.Dispatch(chr).Execute(Char.ValueOf(chr));
			
			Block content = mediator.GetContent();
			
			var row1 = (Row)content.First.Value;
			Assert.AreEqual("c", row1.First.Value.ToString());
		}
		
		[Test]
		public void Test_AddAfter_null()
		{
			var mediator = Mock.MockTextController();
			var document = new MockDocument(mediator);
			Block content = mediator.GetContent();
			
			Cursor cursor = mediator.GetCursor();
			
			//create content
			document.Create("abrcdefghi");
			cursor.Reposition(cursor.Line, null);
			
			char chr = '1';
			mediator.Dispatch(chr).Execute(Char.ValueOf(chr));
			
			var row = (Row)content.First.Value;
			Assert.AreEqual("1", row.First.Value.ToString());
		}
	}
}
