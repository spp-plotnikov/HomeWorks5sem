using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormalLang5sem.Entities;


namespace FormalLang5sem.Interfaces
{
    interface ISolver
    {
        /// <returns> returns all triplets (i, N[k], j) </returns>
        string Solve(Graph graph, Grammar grammar);
    }
}
