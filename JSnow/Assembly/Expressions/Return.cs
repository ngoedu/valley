/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017/1/7
 * Time: 8:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Snow.VM;

namespace Snow.Assembly.Expressions
{
	/// <summary>
	/// Description of Return.
	/// </summary>
	public class Return : Expression
	{
		private Expression returnValue;
		
		public Return(Expression e)
		{
			this.returnValue = e;
		}

		#region Expression implementation
		public object Interpret()
		{
			object value = returnValue.Interpret();
			
			//UNDONE: how about a return clause in the middle of the script (e.g. IF block).
			//when traversal in a block invok tree. there seems only one way to do fast quit.
			//clear all the pointers in the Frame, so that the statements/expressions iteration in the block
			//will have to break.
			Thread.GetCurrentThread().GetStack().Peek().Pointers.Clear();
			
			return value;
		}
		#endregion
	}
}
