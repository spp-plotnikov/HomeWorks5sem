using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormalLang5sem.Entities;
using FormalLang5sem.Interfaces;

namespace FormalLang5sem.Solvers
{
    /// <summary>
    /// Based on GLL algorithm
    /// For more info read "GLL Parsing", Elizabeth Scott and Adrian Johnstone
    /// </summary>
    class GLLSolver : ISolver
    {
        public string Solve(Graph graph, Grammar grammar)
        {
            var gr = graph.GenerateAdjacencyList();
            return null;
        }
    }
}
