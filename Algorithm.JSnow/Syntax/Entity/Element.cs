#define DIA_DEBUG

using Snow.Syntax;
using System;
using System.Collections.Generic;


namespace Snow.Syntax.Entity
{
    /// <summary>
    /// Element represents a basic entry of source code which is either 
    /// - decomposed from a block or 
    /// - divided into a basic logic unit by splitor
    /// </summary>
    public class Element : Matchable
    {
        public string NameSpace { set; get; }

        //TODO: THIS machine seems duplicated???
        protected static Matcher EMatcher ;

        //elelemt parser dictionary
        protected static Dictionary<string, Parser> PARSERS = new Dictionary<string, Parser>();

        //initiaizer
        static Element()
        {
            string[] ELEMENT_KEYS = { Source.KEY_FUNCTION, Source.KEY_IF, Source.KEY_ELSE, System.Environment.NewLine };
            EMatcher = new Matcher(ELEMENT_KEYS);

            PARSERS.Add(Source.KEY_FUNCTION, new Function());
            PARSERS.Add(Source.KEY_IF, new If());
            PARSERS.Add(Source.KEY_ELSE, new Else());
            PARSERS.Add(Source.KEY_FOR, new For());
            PARSERS.Add(Source.CONST_DEFAULT, new Default());
        }

        /// <summary>
        /// this Snippet hold a pice of script, it may be 
        ///     1. ';' splited element
        ///     2. open and close braced block/composite along with its predecessor  
        /// </summary>
        internal Source Snippet;

        public Element()
        {
        }

        public Element(Source src, string nameSpace)
        {
            Snippet = src;

            NameSpace = nameSpace;

            EMatcher.Scan(Snippet, this);
        }

        /// <summary>
        /// some key word matching event callback will occure here.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="position"></param>
        public virtual int OnMatch(Source src, string key, int position)
        {
            if (key == System.Environment.NewLine)
            {
                //new line found in one element
                Context.LineNumber++;
            }
            else
            {
                //Key word processor take over the src to process instead
                return PARSERS[key].Process(src, position, this);
            }
            return 0;
        }


        public Object Interpret()
        {
            return null;
        }

        public override string ToString()
        {
            return "["+Snippet.Content+"]";
        }
    }
}
