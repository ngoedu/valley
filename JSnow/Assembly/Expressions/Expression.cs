/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017/1/6
 * Time: 23:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Snow.Assembly.Expressions
{
	/// <summary>
	/// Description of Expression.
	/// </summary>
	public interface Expression
	{
		Object Interpret();
		
		//TODO: line number
		//int GetLineNumber();
	}
}
