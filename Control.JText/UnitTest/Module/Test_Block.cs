/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/8
 * 时间: 20:29
 * 
 * 
 */
using NUnit.Framework;
using NGO.Pad.JText.Module;


namespace NGO.Pad.JText.UnitTest.Module
{
	[TestFixture]
	public class Test_Block
	{
		[Test]
		public void Test_BLock_AddLast()
		{
			var block = new Block();
				
			var row1 = new Row();
			row1.AddLast(new Char('a'));
			var row2 = new Row();
			row2.AddLast(new Char('b'));
			var row3 = new Row();
			row3.AddLast(new Char('c'));
			
			block.AddLast(row1);
			block.AddLast(row2);
			block.AddLast(row3);
			
			Assert.AreEqual(1, block.First.Index);
			Assert.AreEqual(2, block.First.Next.Index);
			Assert.AreEqual(3, block.Last.Index);
		}
		
		[Test]
		public void Test_BLock_AddAfter()
		{
			var block = new Block();
				
			var row1 = new Row();
			row1.AddLast(new Char('a'));
			var row2 = new Row();
			row2.AddLast(new Char('b'));
			var row3 = new Row();
			row3.AddLast(new Char('c'));
			
			block.AddLast(row1);
			block.AddLast(row2);
			
			var node = block.Last;
			
			block.AddLast(row3);
			
			var rowInsert = new Row();
			rowInsert.AddLast(new Char('i'));
			block.AddAfter(node, rowInsert);
			
			Assert.AreEqual(1, block.First.Index);
			Assert.AreEqual(2, block.First.Next.Index);
			Assert.AreEqual(4, block.Last.Index);
			Assert.AreEqual("[1:c]", block.Last.ToString());
		}
	}
}
