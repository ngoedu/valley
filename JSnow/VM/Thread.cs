using System;
using System.Collections.Generic;
using System.Linq;


namespace Snow.VM
{
    public class Thread
    {
    	private static readonly Thread runable = new Thread();
    	
    	private Stack<Frame> stack = new Stack<Frame>();
    	
    	public static Thread GetCurrentThread()
    	{
    		//UNDONE: implementes multi-threading situation ?
    		return runable;
    	}
    	
    	public Stack<Frame> GetStack()
    	{
    		return stack;
    	}
    	
		public object Run()
		{
			//TODO: 
			return null;
		}
    }
}
