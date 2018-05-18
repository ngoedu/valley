using Snow.Syntax.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.Syntax
{
	/// <summary>
	/// 
	/// </summary>
    public sealed class Context
    {
        public static int LineNumber { set; get; }

        //Static initializer if required, same as java static block
        static Context()
        {
            
        }

        //initialize the context
        public static void Init()
        {
            GLOBAL = new Composite();

            REGISTRY.Clear();

            REGISTRY.Add("GLOBAL", GLOBAL);

            LineNumber = 1;
        }

        
        
        //Root block which included unresolved + resolved elements
        public static Composite GLOBAL { set; get; }

        //Hold registered block by its name space
        public static Dictionary<string, Composite> REGISTRY = new Dictionary<string, Composite>();
    }
}
