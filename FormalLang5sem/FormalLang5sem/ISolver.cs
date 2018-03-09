using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalLang5sem
{
    interface ISolver
    {
        /// <param name="graph">Graph represented in Graphviz/DOT </param>
        /// <param name="grammar">Grammar represented in Graphviz/DOT </param>
        /// <returns> returns all triplets (i, N[k], j) </returns>
        string Solve(string graph, string grammar);
    }
}
