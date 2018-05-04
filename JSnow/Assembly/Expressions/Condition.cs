/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017/1/8
 * Time: 19:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Snow.VM;

namespace Snow.Assembly.Expressions
{
	/// <summary>
	/// Description of IF.
	/// </summary>
	public class Condition : Expression
	{
		private Expression expression;
		
		public Condition(Expression e)
		{
			this.expression = e;
			
		}

		#region Expression implementation
		public object Interpret()
		{
			bool cond = (bool)expression.Interpret();
			return new Pointer(cond ? 1 : 2);
			
		}
		#endregion
	}
}
