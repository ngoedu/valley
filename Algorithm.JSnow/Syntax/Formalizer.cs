#define DIA_DEBUG_DISABLED

using Snow.Syntax.Entity;
/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2017/2/25
 * Time: 16:43
 * 
 * 
 */
using System;
using System.Collections.Generic;

namespace Snow.Syntax
{
    /// <summary>
    /// This class define the procedure to formalize the JS source code before the syntax process.
    /// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide/Grammar_and_types#Basics
    /// </summary>
    public sealed class Formalizer
    {
        
        private Formalizer()
        {         
        }

        /// <summary>
        /// In general, this function will do decompose of source code, it will
        /// 	1. by pass all sorts of the comments, includes line, section and nested section comments
        /// 	2. keep original line number
        /// 	3. do clause validation
        ///         a. validate the openning and closing brace are well paired.
        ///         b. validate the openning and closing section are well paired
        ///         c. validate the openning and closing bracket are well paired
        ///     4. pack the statment in element.
        ///     5. pack the script block in composite.
        /// <summary>
        /// revised version of process
        /// </summary>
        /// <param name="src"></param>
        /// <param name="nameSpace"></param>
        /// <param name="block"></param>
        public static void Process(Source src, string nameSpace, Composite block)
        {
            if (src.Length == 0)
                return;

            int state = 0, nestedSect = 0, pairedBrace = 0, pairedBracket = 0;
            bool splitorFound = false;

            var temp = src.ClearCopy();

            for (int i = src.Start; i < src.End; i++)
            {
                switch (state)
                {
                    case 0: // source code
                        {
                            if (src.cArray[i] == Source.COMMENT[0] && src.cArray[i + 1] == Source.COMMENT[1])
                            {
                                state = 1;
                                continue;
                            }
                            else if (src.cArray[i] == Source.OSC[0] && src.cArray[i + 1] == Source.OSC[1])
                            {
                                state = 2;
                                nestedSect++;
                                continue;
                            }
                            else
                            {

                                #region here comes the EFFCTIVE SROUCE code - without any comments
                                //PUSH to temp
                                temp.cArray[++temp.End] = src.cArray[i];

                                switch (src.cArray[i])
                                {
                                    case Source.CHAR_SPLITOR:
                                        {
                                            splitorFound = true;
                                            //element MUST NOT includes openning Brace and/or Bracket
                                            if (pairedBrace == 0 && pairedBracket == 0)
                                            {
                                                block.Add(new Element(temp.Copy(temp.Start, temp.End), nameSpace));
                                                temp.Clear();
                                                splitorFound = false;
                                            }
                                            break;
                                        }
                                    case Source.CHAR_LBK:
                                        {
                                            pairedBracket++;
                                            break;
                                        }
                                    case Source.CHAR_RBK:
                                        {
                                            pairedBracket--;
                                            break;
                                        }
                                    case Source.CHAR_LBR:
                                        {
                                            pairedBrace++;
                                            break;
                                        }
                                    case Source.CHAR_RBR:
                                        {
                                            pairedBrace--;
                                            //here is packing one block, might nested child blocks
                                            if (pairedBrace == 0 && splitorFound)
                                            {
                                                block.Add(new Composite(temp.Copy(), nameSpace));
                                                temp.Clear();
                                            }
                                            break;
                                        }
                                }
                                #endregion
                            }
                            break;
                        }
                    #region in line style comment
                    case 1:
                        {
                            if (src.cArray[i] == Source.LB[0] && src.cArray[i + 1] == Source.LB[1])
                            {
                                state = 0;
                                temp.cArray[++temp.End] = Source.LB[0];
                                temp.cArray[++temp.End] = Source.LB[1];
                                i++;
                                continue;
                            }
                            else
                            {
                                //skip chars in a comment
                                continue;
                            }
                        }
                    #endregion

                    #region in block style comment, nested comment is possible
                    case 2:
                        {
                            if (src.cArray[i] == Source.OSC[0] && src.cArray[i + 1] == Source.OSC[1])
                            {
                                nestedSect++;
                                state = 2;
                                i++;
                            }
                            else if (src.cArray[i] == Source.CSC[0] && src.cArray[i + 1] == Source.CSC[1])
                            {
                                nestedSect--;
                                if (nestedSect == 0)
                                    state = 0;
                                i++;
                            }
                            else
                            {
                                if (src.cArray[i] == Source.LB[0] && src.cArray[i + 1] == Source.LB[1])
                                {
                                    //append a new line to keep the line numbering 
                                    temp.cArray[++temp.End] = Source.LB[0];
                                    temp.cArray[++temp.End] = Source.LB[1];
                                    i++;
                                }
                                continue;
                            }
                            break;
                        }
                    #endregion
                }
            }

            //##############################################################
            // some chars may still be remaining in temp...they should be 
            // appended to element and therefore block again.
            //
            // NOTE: EVERY PIECE OF SRC SHOULD BE SCANNED AND VALIDATED 
            //##############################################################
            if (temp.Length > 0)
            {
#if (DIA_DEBUG)					
System.Diagnostics.Debug.WriteLine(string.Format("DEBUG: There are {0} [trimable={1}] chars with content=[{2}] ramining,", temp.Length, (temp.Content.Trim().Length == 0 ? "Y":"N"), temp.Content));
#endif
                block.Add(new Element(temp.Copy(temp.Start, temp.End), nameSpace));
            }

            #region validation check
            if (pairedBracket != 0)
                throw new InvalidOperationException("JSE-A001, Openning and Closing bracket are not well paired.");
            if (pairedBrace != 0)
                throw new InvalidOperationException("JSE-A002, Openning and Closing brace are not well paired.");
            if (nestedSect != 0)
                throw new InvalidOperationException("JSE-A003, section comments are not well closed.");
            #endregion

        }
    }
}
