/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/8
 * 时间: 20:34
 * 
 * 
 */


using NUnit.Framework;
using NGO.Pad.JText.Module;

namespace NGO.Pad.JText.UnitTest.Module
{
	/// <summary>
	/// Description of Test_Row.
	/// </summary>
	public class Test_Row
	{
		[Test]
		public void Test_Row_AddLast()
		{
			var row1 = new Row();
			row1.AddLast(new Char('a'));
			row1.AddLast(new Char('b'));
			row1.AddLast(new Char('c'));
						
			Assert.AreEqual(1, row1.First.Index);
			Assert.AreEqual(2, row1.First.Next.Index);
			Assert.AreEqual(3, row1.Last.Index);
		}
		
		[Test]
		public void Test_Row_AddAfter()
		{
			var row = new Row();
			
			row.AddLast(new Char('a'));
			row.AddLast(new Char('b'));
			
			Node node = row.Last;
			
			row.AddLast(new Char('d'));
			row.AddLast(new Char('e'));
			
			row.AddAfter(node, new Char('c'));
			row.AddAfter(node, new Char('x'));
			
			System.Console.WriteLine("list content: {0}", row.ToString());
			
			Assert.AreEqual(6, row.Last.Index);
			Assert.AreEqual("e", row.Last.Value.ToString());
		}
		
		[Test]
		public void Test_Row_AddAfter_Last()
		{
			var row = new Row();
			
			row.AddLast(new Char('a'));
			Node node = row.AddLast(new Char('b'));
			
			Assert.AreEqual(node,row.Last);
			
			node = row.AddAfter(node, new Char('c'));
			Assert.AreEqual(node, row.Last);
			
			System.Console.WriteLine("list content: {0}", row.ToString());
			
			Assert.AreEqual(3, row.Last.Index);
			Assert.AreEqual("c", row.Last.Value.ToString());
		}
	}
}
