/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/7
 * 时间: 11:37
 * 
 * 
 */

using System.Text;

namespace NGO.Pad.JText.Module
{
	/// <summary>
	/// bi-directional linked List.
	/// </summary>
	public class LinkList 
	{
		public Node First {set; get;}
		public Node Last {set; get;}
		
		public LinkList()
		{
		}
		
		/// <summary>
		/// Count of Nodes in the list
		/// </summary>
		public int Count {
			get { return Last == null ? 0 : Last.Index; }
		}
		
		/// <summary>
		/// Return a Node at given index/position in the list
		/// </summary>
		/// <param name="idx">Index of the Node</param>
		/// <returns></returns>
		public Node IndexOf(int idx) {
			if (idx >= Last.Index)
				return Last;
			
			if ( (Last.Index - idx) > idx) {
			    var node = this.First;
			    while (node != null && node.Index != idx)
			    	node = node.Next;
			    return node;
			} else {
				 var node = this.Last;
			    while (node != null && node.Index != idx)
			    	node = node.Previous;
			    return node;
			}
		}
		
		/// <summary>
		/// return a Node at given position
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public Node PositionOf(int pos) {
			if (pos >= Last.End)
				return Last;
			if ( (Last.End - pos) > pos) {
			    var node = this.First;
			    while (node != null  && (pos - node.Start) >= node.Value.Width)
			    	node = node.Next;
			    return node;
			} else {
				 var node = this.Last;
			    while (node != null && (node.End - pos) >= node.Value.Width)
			    	node = node.Previous;
			    return node;
			}
		}
		
		/// <summary>
		/// Append new node to the Last node
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public Node AddLast(Glyph item) {
			var node = new Node(item);
			if (this.First == null) {
				node.Index = 1;
				node.Start = 1; node.End = item.Width;
				this.First = node;
				this.Last = node;
			} else {
				node.Index = this.Last.Index + 1;
				node.Start = this.Last.End + 1; node.End = node.Start + item.Width - 1;
				node.Previous = this.Last;
				this.Last.Next = node;
				this.Last = node;	
			}
			return node;	  
		}
		
		public Node AddAfter(Node node, LinkList list) {
			if (node == null)
				throw new System.InvalidOperationException("Node can't be null");
			var next = node.Next;
			if (next != null)
				next.Previous = list.Last;
			//re-link 4 pointers
			node.Next = list.First;
			list.First.Previous = node;
			list.Last.Next = next;
			
			//release list ??
			//list = null;			
			//re-index & positioning subsequent
			ReIndexPosition(node);	
			return list.Last;
		}
		
		
		public Node AddLast(LinkList list) {
			if (list == null)
				throw new System.InvalidOperationException("list can't be null");
			if(this.First == null) {
				this.First = list.First;
				this.Last = list.Last;
				ReIndexPosition(this.First);
				return this.First;
			}
			
			this.Last.Next = list.First;
			list.First.Previous = this.Last;
			
			//re-index & positioning subsequent
			ReIndexPosition(this.Last);	
			return this.First;
		}
		
		private void ReIndexPosition(Node node) {
			var n = node;
			while(n != null) {
				n.Index = n.Previous == null ? 1 : n.Previous.Index+1;
				n.Start = n.Previous == null ? 1 : n.Previous.End + 1;
				n.End = n.Start + n.Value.Width - 1;
				n = n.Next;
			}			
		}
		
		/// <summary>
		/// Insert new node after the given node
		/// </summary>
		/// <param name="node"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public Node AddAfter(Node node, Glyph item) {
			if (node == null)
				throw new System.InvalidOperationException("Node can't be null");
			var newNode = new Node(item);
			//re-link 4 pointers
			ReLinkNext(node, newNode);
			//re-index & positioning subsequent
			//ReIndexPosition(newNode, item.Width);
			ReIndexPosition(newNode);
			return newNode;
		}
		
		private void ReLinkNext(Node node, Node newNode) {
			if (node.Next != null)
				node.Next.Previous = newNode;
			else
				this.Last = newNode; //fixed a bug of determine Last node
			newNode.Next = node.Next;
			newNode.Previous = node;
			node.Next = newNode;
		}
		
		private void ReLinkBefore(Node node, Node newNode) {
			if (node.Previous != null)
				node.Previous.Next = newNode;
			else
				First = newNode;
			newNode.Previous = node.Previous;
			newNode.Next = node;
			node.Previous = newNode;
		}
		
		/// <summary>
		/// Insert new node before the given node
		/// </summary>
		/// <param name="node"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public Node AddBefore(Node node, Glyph item) {
			if (node == null)
				throw new System.InvalidOperationException("Node can't be null");
			var newNode = new Node(item);
			//re-link 4 pointers
			ReLinkBefore(node, newNode);
			//re-index subsequent
			ReIndexPosition(newNode);		
			return newNode;		
		}
		
		/// <summary>
		/// Break list into two parts by node
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public LinkList Break(Node node) {
			if (node == null || node.Next == null)
				return null;
			var newList = new LinkList();
			newList.First = node.Next;
			newList.Last = this.Last;
			
			node.Next.Previous = null;
			node.Next = null;
			this.Last = node;
			
			ReIndexPosition(newList.First);
			return newList;
		}
		
		/// <summary>
		/// remote last node
		/// </summary>
		/// <returns></returns>
		public Node RemoveLast() {
			if (Last == null)
				return null;
			if (Last == First) {
				this.Last = null;
				this.First = null;
				return null;
			}
			var previous = Last.Previous;
			previous.Next = null;
			this.Last = previous;
			return previous;
		}
		
		/// <summary>
		/// remove entry after given node
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public Node RemoveAfter(Node node) {
			if (node == null) {
				if (Last == First) {
					this.Last = null;this.First = null;
					return null;
				}
				this.First = this.First.Next;
				ReIndexPosition(this.First);
				return this.First;
			}
			if (node.Next == null)
				return node;
			var nnext = node.Next.Next;
			node.Next = nnext;
			if (nnext != null) {
				nnext.Previous = node;
				this.Last = nnext;
			} else {
				this.Last = node;
			}
			ReIndexPosition(node);			
			return node;
		}
		
		/// <summary>
		/// remove given node.
		/// </summary>
		/// <param name="node">return current node</param>
		/// <returns></returns>
		public Node Remove(Node node) {
			if (node == null) {
				return null;
			}

			if (this.First == this.Last && this.Last == node) {
				this.First = null; this.Last = null; return null;
			}
			var p = node.Previous;	var n = node.Next;
			if (p != null)
				p.Next = n;
			else
				this.First = n;
			if (n != null)
				n.Previous = p;
			else
				this.Last = p;
			ReIndexPosition(n);
			return n;
		}
		
		
		/// <summary>
		/// Give a meaningful string for all linked node
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var str = new StringBuilder();
			var node = this.First;
			if (this.First == null)
				return "[Empty]";
			while(node != null) {
				str.Append("["+node.Index+":"+node.Value.ToString()+"]");
				node = node.Next;
			}
			return str.ToString();
		}
	}
}
