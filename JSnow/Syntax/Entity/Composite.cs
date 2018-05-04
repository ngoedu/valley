/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2017/2/26
 * Time: 5:15
 * 
 * 
 */
#define DIA_DEBUG

using System;
using System.Collections.Generic;
using System.Text;


namespace Snow.Syntax.Entity
{
    /// <summary>
    /// Composite is a container which hold a list of decomposed element
    /// </summary>
    public class Composite : Element
    {
        private List<Element> Childs = new List<Element>();

        private static Matcher CMatcher;

        //static initializer
        static Composite()
        {
            string[] BLOCK_KEYS = { Source.KEY_FUNCTION, 
                                      Source.KEY_IF, 
                                      Source.KEY_ELSE, 
                                      Source.KEY_FOR, 
                                      Source.KEY_WHILE 
                                  };

            CMatcher = new Matcher(BLOCK_KEYS);
        }


        public Composite()
        {
        }

        /// <summary>
        /// append element to the child list
        /// </summary>
        /// <param name="e"></param>
        public void Add(Element e)
        {
            Childs.Add(e);
        }

        /// <summary>
        /// New block was identified and instantiated with in current name space
        /// </summary>
        /// <param name="src"></param>
        /// <param name="nameSpace"></param>
        public Composite(Source src, string nameSpace)
        {
            Snippet = src;

            //before any ... set passed name space as default
            NameSpace = nameSpace;

            //HXY: should there be some pre-check here?
            CMatcher.Scan(Snippet, this);
        }


        /// <summary>
        /// key word matching event called back here.
        /// </summary>
        /// <param name="src">source char array</param>
        /// <param name="key">key word</param>
        /// <param name="position">first position of the key found in the src</param>
        /// <returns>-1 : will stop the AcMachine; >=0 : leave the AcMachine running</returns>
        public override int OnMatch(Source src, string key, int position)
        {
            var processor = PARSERS[key];

            processor = processor ?? PARSERS[Source.CONST_DEFAULT];

            //Key word processor take over the src to process instead
            return processor.Process(src, position, this);
        }


        public override string ToString()
        {
            var temp = new StringBuilder();
            foreach (var e in Childs)
                temp.Append(e.ToString());
            return temp.ToString();
        }


        public object Interpret()
        {
            throw new NotImplementedException();
        }
    }
}
