using Snow.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.X.Algorithm
{
    /// <summary>
    /// This state machine can ONLY hanlde Series-wound matrices
    /// </summary>
    public class StateMachine
    {
        private List<StateMatrix> Matrices = new List<StateMatrix>();

        /// <summary>
        /// append a new matrix to the end of marices array
        /// </summary>
        /// <param name="matrix"></param>
        public void Append(StateMatrix matrix)
        {
            Matrices.Add(matrix);
        }

        /// <summary>
        /// parse the snippet by the given pattern of this machine
        /// state = -1 means mismatch by matrix
        /// state = -2 means intented stop (not able to handle it) by matrix, one char needs drop back to src and try next matrix again
        /// state = -3 reach the end of char array
        /// </summary>
        /// <param name="Snippet"></param>
        /// <returns>back the final state to invoker</returns>
        public int Parse(Source snippet)
        {
            int state = 0;
            for (int i = 0; i < Matrices.Count && state != -1; i++)
            {
                state = Matrices[i].Transit(snippet);
                if (state == -2)
                    snippet.Drop();
            }
            return state;
        }
    }
}
