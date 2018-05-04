using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.VM
{
    public class Frame
    {
        public Dictionary<String, object> Variables;
                
        public Stack<Pointer> Pointers {set; get;}
        
    }
    
    public partial class Pointer
    {
    	public Pointer(int step)
    	{
    		this.value = step;
    	}   	
    	public int value {set; get;}
    }
}
