/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/8
 * 时间: 19:37
 * 
 * 
 */
using NUnit.Framework;
using NGO.Pad.JText.Module;


namespace NGO.Pad.JText.UnitTest.Module
{
	[TestFixture]
	public class Test_List
	{
		[Test]
		public void Test_List_Glyph_Width()
		{
			LinkList row = new LinkList();
			Glyph glyph = Char.A;
			
			Assert.AreEqual(10, glyph.Width);
			Assert.AreEqual(8, Char.CR.Width);
			Assert.AreEqual(4, Char.LF.Width);
		}
		
		[Test]
		public void Test_List_Start()
		{
			LinkList row = new LinkList();
			row.AddLast(Char.LF);
			Assert.AreEqual(1, row.Last.Start);
			row.AddLast(Char.A);
			Assert.AreEqual(5, row.Last.Start);
			row.AddLast(Char.B);
			Assert.AreEqual(15, row.Last.Start);
			row.AddLast(Char.CR);
			Assert.AreEqual(25, row.Last.Start);
		}
		
		[Test]
		public void Test_List_PostionOf()
		{
			LinkList row = new LinkList();
			row.AddLast(Char.LF);
			row.AddLast(Char.A);
			row.AddLast(Char.B);
			row.AddLast(Char.CR);
			
			var node = row.PositionOf(33);
			Assert.AreEqual(row.Last, node);
			
			node = row.PositionOf(1);
			Assert.AreEqual(row.First, node);
			
			node = row.PositionOf(5);
			Assert.AreEqual(row.First.Next, node);
			
			node = row.PositionOf(14);
			Assert.AreEqual(row.First.Next, node);
			
			node = row.PositionOf(32);
			Assert.AreEqual(row.Last, node);
			
			node = row.PositionOf(26);
			Assert.AreEqual(row.Last, node);
			
			node = row.PositionOf(24);
			Assert.AreEqual(row.Last.Previous, node);
		}
		
		[Test]
		public void Test_List_End()
		{
			LinkList row = new LinkList();
			row.AddLast(Char.LF);
			Assert.AreEqual(4, row.Last.End);
			row.AddLast(Char.A);
			Assert.AreEqual(14, row.Last.End);
			row.AddLast(Char.B);
			Assert.AreEqual(24, row.Last.End);
			row.AddLast(Char.CR);
			Assert.AreEqual(32, row.Last.End);
		}
		
		[Test]
		public void Test_List_AddLast()
		{
			LinkList row = new LinkList();
			row.AddLast(new Char('a'));
			row.AddLast(new Char('b'));
			row.AddLast(new Char('c'));
			
			Assert.AreEqual(1, row.First.Index);
			Assert.AreEqual(2, row.First.Next.Index);		
			Assert.AreEqual(3, row.Last.Index);
		}
		
		[Test]
		public void Test_List_AddAfter_With_List()
		{
			LinkList row1 = new LinkList();
			row1.AddLast(new Char('a'));
			row1.AddLast(new Char('b'));
			row1.AddLast(new Char('c'));
			
			
			LinkList row2 = new LinkList();
			row2.AddLast(new Char('1'));
			row2.AddLast(new Char('2'));
			row2.AddLast(new Char('3'));
			
			row1.AddAfter(row1.Last, row2);
			Assert.AreEqual("[1:a][2:b][3:c][4:1][5:2][6:3]",row1.ToString());
			
			
			LinkList row3 = new LinkList();
			row3.AddLast(new Char('a'));
			row3.AddLast(new Char('b'));
			row3.AddLast(new Char('c'));
			
			
			LinkList row4 = new LinkList();
			row4.AddLast(new Char('1'));
			row4.AddLast(new Char('2'));
			row4.AddLast(new Char('3'));
			
			row3.AddAfter(row3.First, row4);
			Assert.AreEqual("[1:a][2:1][3:2][4:3][5:b][6:c]",row3.ToString());
			
			
			LinkList row5 = new LinkList();
			row5.AddLast(Char.LF);
			row5.AddLast(new Char('b'));
			row5.AddLast(Char.LF);
			row5.AddLast(new Char('c'));
			row5.AddLast(Char.CR);
			
			
			LinkList row6 = new LinkList();
			row6.AddLast(new Char('1'));
			row6.AddLast(Char.LF);
			row6.AddLast(new Char('3'));
			
			row3.AddAfter(row5.First, row6);
			Assert.AreEqual(row5.IndexOf(2),row5.PositionOf(5));
			Assert.AreEqual(row5.IndexOf(3),row5.PositionOf(16));
		}
		
		[Test]
		public void Test_List_AddAfter()
		{
			LinkList row = new LinkList();
			row.AddLast(new Char('a'));
			row.AddLast(new Char('b'));
			
			Node node = row.Last;
			
			row.AddLast(new Char('d'));
			row.AddLast(new Char('e'));
			
			row.AddAfter(node, new Char('c'));
			row.AddAfter(node, new Char('z'));
			
			System.Console.WriteLine("list content: {0}", row.ToString());
			
			Assert.AreEqual(6, row.Last.Index);
			Assert.AreEqual("e", row.Last.Value.ToString());
		}
		
		[Test]
		public void Test_List_AddBefore()
		{
			LinkList row = new LinkList();
			row.AddLast(new Char('a'));
			
			Node node = row.Last;
			row.AddBefore(node, new Char('1'));
			
			node = row.Last;
			
			row.AddLast(new Char('b'));
			row.AddBefore(node, new Char('2'));
			
			System.Console.WriteLine("list content: {0}", row.ToString());
			
			Assert.AreEqual("1", row.IndexOf(1).Value.ToString());
			Assert.AreEqual("2", row.IndexOf(2).Value.ToString());
		}
		
		[Test]
		[ExpectedException(typeof(System.InvalidOperationException))]
		public void Test_List_AddAfter_WithException()
		{
			LinkList row = new LinkList();
			row.AddLast(new Char('a'));
			row.AddLast(new Char('b'));
			
			Node node = row.Last;
			
			row.AddLast(new Char('d'));
			row.AddLast(new Char('e'));
			
			row.AddAfter(node, new Char('c'));
			row.AddAfter(null, new Char('z'));
		}
		
		[Test]
		public void Test_List_IndexOf()
		{
			LinkList row = new LinkList();
			row.AddLast(new Char('a'));
			row.AddLast(new Char('b'));
			row.AddLast(new Char('c'));
			row.AddLast(new Char('d'));
			row.AddLast(new Char('e'));
			row.AddLast(new Char('f'));
			
			System.Console.WriteLine("list content: {0}", row.ToString());
			
			Assert.AreEqual(null, row.IndexOf(0));
			Assert.AreEqual(1, row.IndexOf(1).Index);
			
			Assert.AreEqual(3, row.IndexOf(3).Index);
			Assert.AreEqual(6, row.IndexOf(6).Index);
			Assert.AreEqual(6, row.IndexOf(7).Index);
			
			
			Assert.AreEqual("a", row.IndexOf(1).Value.ToString());
			Assert.AreEqual("b", row.IndexOf(2).Value.ToString());
			Assert.AreEqual("d", row.IndexOf(4).Value.ToString());
			Assert.AreEqual("f", row.IndexOf(6).Value.ToString());
			
		}
		
		[Test]
		public void Test_List_Count()
		{
			LinkList row = new LinkList();
			Assert.AreEqual(0, row.Count);
			
			row.AddLast(new Char('a'));
			Assert.AreEqual(1, row.Count);
			
			row.AddLast(new Char('b'));
			row.AddLast(new Char('c'));
			Assert.AreEqual(3, row.Count);			
		}
		
		[Test]
		public void Test_List_Break()
		{
			LinkList row = new LinkList();
			row.AddLast(new Char('a'));
			row.AddLast(new Char('b'));
			var node = row.AddLast(new Char('c'));
			row.AddLast(new Char('d'));
			row.AddLast(new Char('e'));
			row.AddLast(new Char('f'));
			
			var rowBreak = row.Break(node);
			Assert.AreEqual("c", row.Last.Value.ToString());
			
			Assert.AreEqual(3, rowBreak.Count);
			Assert.AreEqual("d", rowBreak.First.Value.ToString());
			Assert.AreEqual("f", rowBreak.Last.Value.ToString());
			Assert.AreEqual(3, rowBreak.Last.Index);
			
		}
		
		[Test]
		public void Test_List_AddLast_With_List()
		{
			LinkList row1 = new LinkList();
			row1.AddLast(new Char('a'));
			row1.AddLast(new Char('b'));
			row1.AddLast(new Char('c'));
			
			
			LinkList row2 = new LinkList();
			row2.AddLast(new Char('1'));
			row2.AddLast(new Char('2'));
			row2.AddLast(new Char('3'));
			
			row1.AddLast(row2);
			Assert.AreEqual("[1:a][2:b][3:c][4:1][5:2][6:3]",row1.ToString());
			
			
			LinkList row3 = new LinkList();
			
			LinkList row4 = new LinkList();
			row4.AddLast(new Char('1'));
			row4.AddLast(new Char('2'));
			row4.AddLast(new Char('3'));
			
			row3.AddLast( row4);
			Assert.AreEqual("[1:1][2:2][3:3]",row3.ToString());
			
		}
		
		[Test]
		public void Test_List_RemoveLast()
		{
			LinkList row0 = new LinkList();
			var node0 = row0.RemoveLast();
			Assert.AreEqual(null,node0);
			Assert.AreEqual(null,row0.Last);
			Assert.AreEqual(null,row0.First);
			
			LinkList row1 = new LinkList();
			row1.AddLast(new Char('a'));
			
			var node = row1.RemoveLast();
			Assert.AreEqual(null,node);
			Assert.AreEqual(null,row1.Last);
			Assert.AreEqual(null,row1.First);
			
			LinkList row2 = new LinkList();
			row2.AddLast(new Char('a'));
			row2.AddLast(new Char('b'));
			row2.RemoveLast();
			Assert.AreEqual("[1:a]",row2.ToString());
			Assert.AreEqual("a",row2.First.Value.ToString());
			Assert.AreEqual("a",row2.Last.Value.ToString());
			
		}
		
		[Test]
		public void Test_List_RemoveAfter()
		{
			LinkList row0 = new LinkList();
			var node0 = row0.RemoveAfter(null);
			Assert.AreEqual(null,node0);
			Assert.AreEqual(null,row0.Last);
			Assert.AreEqual(null,row0.First);
			
			LinkList row1 = new LinkList();
			row1.AddLast(new Char('a'));
			var noder = row1.RemoveAfter(null);
			Assert.AreEqual(null,noder);
			Assert.AreEqual(null,row1.First);
			Assert.AreEqual(null,row1.Last);
			
			LinkList row2 = new LinkList();
			row2.AddLast(new Char('a'));
			row2.AddLast(new Char('b'));
			noder = row2.RemoveAfter(null);
			Assert.AreEqual("b",noder.Value.ToString());
			Assert.AreEqual("b",row2.First.Value.ToString());
			Assert.AreEqual("b",row2.Last.Value.ToString());
			
			
			LinkList row3 = new LinkList();
			row3.AddLast(new Char('a'));
			var nb = row3.AddLast(new Char('b'));
			row3.AddLast(new Char('c'));
			noder = row3.RemoveAfter(nb);
			Assert.AreEqual("b",noder.Value.ToString());
			Assert.AreEqual("a",row3.First.Value.ToString());
			Assert.AreEqual("b",row3.Last.Value.ToString());
			Assert.AreEqual(1,row3.First.Index);
			Assert.AreEqual(2,row3.Last.Index);
			
			LinkList row4 = new LinkList();
			row4.AddLast(new Char('a'));
			row4.AddLast(new Char('b'));
			var nc = row4.AddLast(new Char('c'));
			noder = row4.RemoveAfter(nc);
			Assert.AreEqual("c",noder.Value.ToString());
			Assert.AreEqual("a",row4.First.Value.ToString());
			Assert.AreEqual("c",row4.Last.Value.ToString());
			Assert.AreEqual(1,row4.First.Index);
			Assert.AreEqual(3,row4.Last.Index);
		}
		
		
		[Test]
		public void Test_List_Remove()
		{
			LinkList row0 = new LinkList();
			var node0 = row0.Remove(null);
			Assert.AreEqual(null,node0);
			
			LinkList row1 = new LinkList();
			var na = row1.AddLast(new Char('a'));
			var noder = row1.Remove(na);
			Assert.AreEqual(null,noder);
			Assert.AreEqual(null,row1.First);
			Assert.AreEqual(null,row1.Last);
			
			LinkList row3 = new LinkList();
			row3.AddLast(new Char('a'));
			var nb = row3.AddLast(new Char('b'));
			row3.AddLast(new Char('c'));
			noder = row3.Remove(nb);
			Assert.AreEqual("c",noder.Value.ToString());
			Assert.AreEqual("a",row3.First.Value.ToString());
			Assert.AreEqual("c",row3.Last.Value.ToString());
			Assert.AreEqual(1,row3.First.Index);
			Assert.AreEqual(2,row3.Last.Index);
			
			LinkList row4 = new LinkList();
			row4.AddLast(new Char('a'));
			row4.AddLast(new Char('b'));
			var nc = row4.AddLast(new Char('c'));
			noder = row4.Remove(nc);
			Assert.AreEqual(null,noder);
			Assert.AreEqual("a",row4.First.Value.ToString());
			Assert.AreEqual("b",row4.Last.Value.ToString());
			Assert.AreEqual(1,row4.First.Index);
			Assert.AreEqual(2,row4.Last.Index);
		}
	}
}
