using System;
using System.Collections.Generic;
using Snow.Assembly.Expressions;
using Snow.VM;


namespace Snow.Assembly
{
	/// <summary>
	/// A Method holds the constants, child methods and its statements/instructions
	/// </summary>
    public class Method
    {
    	
        private Dictionary<String, Constant<object>> constants;
        
        private Dictionary<String, Method> methods;
        
        private List<Expression> instructions;
        
        public Method()
        {
        	constants = new Dictionary<string, Constant<object>>();
        	methods = new Dictionary<string, Method>();
        	instructions = new List<Expression>();
        }
        
        public Constant<object> GetConstant(string name)
        {
        	string[] objNav = name.Split('.');
        	if (objNav.Length == 0)
        	{
        		Constant<object> value;
        		constants.TryGetValue(name, out value);
        		return value;	
        	} 
        	else
        	{
        		Method method;
        		methods.TryGetValue(objNav[0], out method);
        		string subName = name.Substring(name.IndexOf('.'));
        		return method.GetConstant(subName);
        	}       		       	
        }
        
        public void SetConstant(string name, Constant<object> value)
        {
        	string[] objNav = name.Split('.');
        	if (objNav.Length == 0)
        	{
        		constants.Add(name, value);
        	} 
        	else
        	{
        		Method method;
        		methods.TryGetValue(objNav[0], out method);
        		string subName = name.Substring(name.IndexOf('.'));
        		method.SetConstant(subName, value);
        	}       		       	
        }
        
        public Method GetMethod(string name)
        {
       		Method method;
        	string[] objNav = name.Split('.');
        	if (objNav.Length == 0)
        	{
        		methods.TryGetValue(name, out method);
        		return method;
        	}
        	else
        	{
        		methods.TryGetValue(objNav[0], out method);
        		string subName = name.Substring(name.IndexOf('.'));
        		return method.GetMethod(subName);
        	}
        }
        
        public void SetMethod(string name, Method value)
        {
        	string[] objNav = name.Split('.');
        	if (objNav.Length == 0)
        	{
        		methods.Add(name, value);
        	} 
        	else
        	{
        		Method method;
        		methods.TryGetValue(objNav[0], out method);
        		string subName = name.Substring(name.IndexOf('.'));
        		method.SetMethod(subName, value);
        	}       		       	
        }
        
        public List<Expression> GetInstructions()
        {
        	return this.instructions;
        }
        
        
        public partial class NativeMethod : Method
        {
        	public object Invoke()
        	{
        		//TODO: implementes native API invokation
        		return null;
        	}
        }
        
    }
}
