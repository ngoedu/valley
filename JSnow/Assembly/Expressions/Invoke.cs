/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017/1/7
 * Time: 0:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Snow.VM;

namespace Snow.Assembly.Expressions
{
	/// <summary>
	/// Description of Invoke.
	/// </summary>
	public class Invoke : Expression
	{
		private string methodName;
		private Parameter[] arguments;
		
		public Invoke(string m, Parameter[] args)
		{
			this.methodName = m; this.arguments = args;
		}

		#region Expression implementation
		public object Interpret()
		{
			
			//push a new call stack and put parameters into it
			var frame = new Frame();
			Thread.GetCurrentThread().GetStack().Push(frame);
			foreach(var arg in arguments)
			{
				frame.Variables.Add(arg.Name, arg.Value);
			}
				
			object returnValue = null;
        	
			//initialize pointer
			var ptr = new Pointer(0);
        	frame.Pointers.Push(ptr);
        	
			//get method instructions and interprets them one by one
			Method method = Perm.GLOBAL.GetMethod(methodName);
			
			
			//TODO: below actually can be replaced by a block
			
			/*
        	List<Expression> instructions = method.GetInstructions();
        	while (ptr.value <= instructions.Count)
        	{
        		//HACK: in case a return clause in source, a Return expression should be in the bottom of this instrustions table
        		returnValue = instructions[ptr.value].Interpret();
        		
        		//HACK: if pointer was cleaned - that meants a return was just invoked
        		if (frame.Pointers.Count == 0)
        			return returnValue;
        		
        		//HACK: a Condition will returns a pointer,
        		if (returnValue is Pointer)
        			ptr.value += ((Pointer)returnValue).value;
        		else
        			ptr.value++;      			
        	}
        	*/
        	
        	Thread.GetCurrentThread().GetStack().Pop();
        	return returnValue;
        }
		#endregion
		
		public  class Parameter
		{
			public string Name {set; get;}
			public object Value {set; get;}
		}
		
	}
}
