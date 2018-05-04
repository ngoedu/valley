/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2017/1/8
 * Time: 22:26
 * 
 * 
 */
using System;
using System.Collections.Generic;
using Snow.VM;

namespace Snow.Assembly.Expressions
{
	/// <summary>
	/// Block/compound statement - used to group zero or more statements/Expressions
	/// it is commonly used with control flow statements (e.g. if...else, for, while)
    /// further more, in JSnow, a block can also be a body of a function.
	/// 
	/// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Statements/block
	/// </summary>
	public partial class Block : List<Expression>, Expression
	{
		
		#region block Expression implementation
		public object Interpret()
		{
			//HACK: there should be a block scope pointer which used to trace the statements in current block
			var ptr = new Pointer(0);
			var frame = Thread.GetCurrentThread().GetStack().Peek();
			frame.Pointers.Push(ptr);
			
			object returnValue = null;
			while (ptr.value <= this.Count) {
				returnValue = this[ptr.value].Interpret();
				
				//HACK: if pointer was cleaned - that meants a return was just invoked
        		if (frame.Pointers.Count == 0)
        			return returnValue;
        		
				//HACK: a Condition will return a pointer,
				if (returnValue is Pointer)
					ptr.value += ((Pointer)returnValue).value;
				else
					ptr.value++;
			}
			
			//all the statements in block has been done. pop the block pointer
			frame.Pointers.Pop();
			return returnValue;
		}
		#endregion
	}
}
