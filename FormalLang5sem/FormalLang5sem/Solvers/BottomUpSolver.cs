using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormalLang5sem.Interfaces;
using FormalLang5sem.Entities;


namespace FormalLang5sem.Solvers
{
    class BottomUpSolver : ISolver
    {
        public string Solve(Graph graph, Grammar grammar)
        {
            _grammar = grammar;
            _graph = graph;

            return null;
        }

        private Grammar _grammar;
        private Graph _graph;
    }
}
