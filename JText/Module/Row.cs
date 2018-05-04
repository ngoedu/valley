/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/8
 * 时间: 19:45
 * 
 * 
 */
using System;
using System.Runtime.CompilerServices;

namespace NGO.Pad.JText.Module
{
	/// <summary>
	/// Row is warpper of char linked list.
	/// https://stackoverflow.com/questions/1691846/does-garbage-collector-call-dispose
	/// </summary>
	public class Row : Glyph, IDisposable
	{
		LinkList chars = new LinkList();
		
		public Node First {
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return chars.First; }
			
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set { chars.First = value;}
		}
		public Node Last {
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return chars.Last; }
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set { chars.Last = value;}
		}
		
		public int Count {
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return chars.Count; }
		}
		
		public Row()
		{
			this.Width = 18; //row height
		}
		
		private Row(LinkList list) : this() {
			this.chars = list;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Node AddLast(Char c) {
			return chars.AddLast(c);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Node AddAfter(Node node, Char c) {
			return chars.AddAfter(node, c);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Node AddBefore(Node node, Char c) {
			return chars.AddBefore(node, c);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Node AddAfter(Node node, Row c) {
			return chars.AddAfter(node, c.chars);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Node AddLast(Row c) {
			return chars.AddLast(c.chars);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Row Break(Node node) {
			var breaked = chars.Break(node);
			return breaked == null ? null : new Row(breaked);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Node IndexOf(int index) {
			return chars.IndexOf(index);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Node Remove(Node node) {
			return chars.Remove(node);
		}

		public void Dispose()
		{
			First = null; Last = null;
			chars = null;
			System.Diagnostics.Debug.WriteLine("Row {0} disposed",GetHashCode());
		}
	
		public override string ToString()
		{
			return chars.ToString();
		}
	}
}
