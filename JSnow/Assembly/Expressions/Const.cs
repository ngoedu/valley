/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017/1/7
 * Time: 7:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Snow.Assembly.Expressions
{
	/// <summary>
	/// Description of Const.
	/// </summary>
	public class Const : Expression
	{
		private Constant<object> value;
		private string constName;
			
		public Const(string name, Constant<object> v)
		{
			this.constName = name; this.value = v;
		}

		#region Expression implementation

		public object Interpret()
		{
			Perm.GLOBAL.SetConstant(constName, this.value);
			return this.value;
		}

		#endregion
	}
}
