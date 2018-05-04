/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/8
 * 时间: 19:51
 * 
 * 
 */
using System;
using System.Runtime.CompilerServices;

namespace NGO.Pad.JText.Module
{
	/// <summary>
	/// Description of Block.
	/// </summary>
	public class Block 
	{
		readonly LinkList rows = new LinkList();
		
		public Block()
		{
		}
		
		public Node First {
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return rows.First; }
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set { rows.First = value;}
		}
		public Node Last {
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return rows.Last; }
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set { rows.Last = value;}
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Node AddLast(Row r) {
			return rows.AddLast(r);
		}
		
		public int Count {
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return rows.Count; }
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Node AddAfter(Node node, Row r) {
			return rows.AddAfter(node, r);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Node IndexOf(int index) {
			return rows.IndexOf(index);
		}
	}
}
