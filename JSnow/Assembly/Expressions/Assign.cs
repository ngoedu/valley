/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017/1/7
 * Time: 0:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Snow.VM;

namespace Snow.Assembly.Expressions
{
	/// <summary>
	/// Description of Assign.
	/// </summary>
	public class Assign : Expression
	{
		private Expression expression;
		
		private string varName;

		public Assign(string v, Expression e)
		{
			this.varName = v ; this.expression = e;
		}

		#region Base implementation

		public object Interpret()
		{
			object value = expression == null ? Constant<object>.UNDEFINED : expression.Interpret();
			Frame frame = Thread.GetCurrentThread().GetStack().Peek();
			frame.Variables.Add(varName, value);
			return value;
		}
		#endregion
	}
}
