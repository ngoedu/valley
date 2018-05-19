/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/7
 * 时间: 11:32
 * 
 * 
 */

namespace NGO.Pad.JText.Module
{
	/// <summary>
	/// represent a Glyph Node in drawing system.
	/// </summary>
	public class Node
	{
		public Node(Glyph item)
        {
            this.Value = item;
        }
		
		public int Index {set; get;}
		
		public int Start {set; get;}
		
		public int End {set; get;}

        public Glyph Value { set; get; }
        
        public Node Next { set; get; }
        
        public Node Previous { set; get; }
        
		public override string ToString()
		{
			return this.Value.ToString();
		}        
	}
}
