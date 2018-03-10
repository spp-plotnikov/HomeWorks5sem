using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalLang5sem
{
    interface ISolver
    {
        /// <returns> returns all triplets (i, N[k], j) </returns>
        string Solve(Graph graph, Grammar grammar);
    }
}
