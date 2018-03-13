using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FormalLang5sem
{
    class MatrixSolver : ISolver
    {
        public string Solve(Graph graph, Grammar grammar)
        {
            var matrix = graph.GenerateAdjacencyMatrix();
            var productionRules = grammar.Content.Value.productionRules;

            

            return null; 
        }
    }
}
