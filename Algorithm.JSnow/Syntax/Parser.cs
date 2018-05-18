#define DIA_DEBUG

using Snow.Syntax;
using Snow.Syntax.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.Syntax
{
    /// <summary>
    /// Key word processor
    /// </summary>
    public abstract class Parser
    {
        /// <summary>
        /// object which can process the char array in the block which have a matched key word 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="position"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        public abstract int Process(Source src, int position, Composite block);

        /// <summary>
        /// object which can process the char array in the element which have a matched key word 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="position"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public abstract int Process(Source src, int position, Element element);

        /// <summary>
        /// back to formalizer again
        /// </summary>
        /// <param name="src"></param>
        /// <param name="nameSpace"></param>
        /// <param name="block"></param>
        protected void Formalize(Source src, string nameSpace, Composite block)
        {
            int ptr1 = src.IndexOf(Source.CHAR_LBR) + 1;

            //extract the content which inside openning and closing brace
            var content = src.Clone(ptr1, src.End - 1);

            //parse the statement in this block
            Formalizer.Process(content, block.NameSpace, block);
        }
    }

    /// <summary>
    /// When no key word matched use Default
    /// </summary>
    public class Default : Parser
    {
        public override int Process(Source src, int position, Composite block)
        {
            
            //add function element on top of the block
            var cElement = src.Copy(0, src.IndexOf(Source.CHAR_LBR) -1);
            block.Add(new Element(cElement, block.NameSpace));

            //formalize child elements
            Formalize(src, block.NameSpace, block);

            //register this function block
            Context.REGISTRY.Add(block.NameSpace, block);

            return -1;
        }

        public override int Process(Source src, int position, Element element)
        {
            /*
            if (!Source.LeftTrimable(src.cArray, position - Source.KEY_FUNCTION.Length))
                    throw new InvalidProgramException(string.Format("function key word not valid near line {0}",Context.LineNumber));
            */
#if (DIA_DEBUG)
                System.Diagnostics.Debug.WriteLine(string.Format("[{0}] found at {1}", element.NameSpace, Context.LineNumber));
#endif
                return 1;
        }
    }

    public class Function : Parser
    {
        public override int Process(Source src, int position, Composite block)
        {
            //TODO
            string funcName = "PENDING?";

            //function name/method resolved
            block.NameSpace += "." + funcName;

            //add function element on top of the block
            var cFunction = src.Copy(0, src.IndexOf(Source.CHAR_LBR) - 1);
            block.Add(new Element(cFunction, block.NameSpace));

            //formalize child elements
            Formalize(src, block.NameSpace, block);

            //register this function block
            //Context.REGISTRY.Add(block.NameSpace, block);

            //HXY: once a function detected, it needs to break the execution 
            //of the rest of current scriptlet which is being processing in the AcMachine
            return -1;
        }

        public override int Process(Source src, int position, Element element)
        {
            /*
            if (!Source.LeftTrimable(src.cArray, position - Source.KEY_FUNCTION.Length))
                    throw new InvalidProgramException(string.Format("function key word not valid near line {0}",Context.LineNumber));
            */
#if (DIA_DEBUG)
                System.Diagnostics.Debug.WriteLine(string.Format("[{0}] found at {1}", element.NameSpace, Context.LineNumber));
#endif
                return 1;
        }
    }

    public class If : Parser
    {
        public override int Process(Source src, int position, Composite block)
        {
            //'if' statement found here
            if (Source.SeekCharS(src.cArray, position + 1, src.End, Source.CHAR_LBK))
            {
                block.NameSpace += ".IF" + position;

                var cIf = src.Copy(0, src.IndexOf(Source.CHAR_LBR) - 1);
                block.Add(new Element(cIf, block.NameSpace));

                Formalize(src, block.NameSpace, block);

                Context.REGISTRY.Add(block.NameSpace, block);

                return -1;

            }
            return 1;
        }

        public override int Process(Source src, int position, Element element)
        {

#if (DIA_DEBUG)
                System.Diagnostics.Debug.WriteLine(string.Format("[{0}] found at {1}", element.NameSpace, Context.LineNumber));
#endif

            return 1;
        }
    }

    public class For : Parser
    {
        public override int Process(Source src, int position, Composite block)
        {
            //'for' statement found here
            if (Source.SeekCharS(src.cArray, position + 1, src.End, Source.CHAR_LBK))
            {
                block.NameSpace += ".FOR" + position;

                var cFor = src.Copy(0, src.IndexOf(Source.CHAR_LBR) - 1);
                block.Add(new Element(cFor, block.NameSpace));

                Formalize(src, block.NameSpace, block);

                Context.REGISTRY.Add(block.NameSpace, block);

                return -1;

            }
            return 1;
        }

        public override int Process(Source src, int position, Element element)
        {

#if (DIA_DEBUG)
            System.Diagnostics.Debug.WriteLine(string.Format("[{0}] found at {1}", element.NameSpace, Context.LineNumber));
#endif

            return 1;
        }
    }
    
    public class Else : Parser
    {
        public override int Process(Source src, int position, Composite block)
        {
            //'else' statemnt here
            if (Source.SeekCharM(src.cArray, position + 1, src.End, Source.CHAR_LBR))
            {
                block.NameSpace += ".ELSE" + position;

                var cElse = src.Copy(0, src.IndexOf(Source.CHAR_LBR) - 1);
                block.Add(new Element(cElse, block.NameSpace));

                Formalize(src, block.NameSpace, block);

                Context.REGISTRY.Add(block.NameSpace, block);

                return -1;
            }
            else
            {

            }
            return 0;
        }

        public override int Process(Source src, int position, Element element)
        {

#if (DIA_DEBUG)
                System.Diagnostics.Debug.WriteLine(string.Format("[{0}] found at {1}", element.NameSpace, Context.LineNumber));
#endif
            return 0;
        }
    }
}

