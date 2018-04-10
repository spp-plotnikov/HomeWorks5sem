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
            _matrix = CreateMatrix();
            
            return null;
        }


        private Grammar _grammar;
        private Graph _graph;
        private CartesianProduct<(int, int)> _matrix;


        private CartesianProduct<(int, int)> CreateMatrix()
        {
            var allPairs = new HashSet<(int, int)>();
            foreach (var automationState in _graph.Nodes)
            {
                foreach (var grammarState in _grammar.Nodes)
                {
                    allPairs.Add((automationState, grammarState));
                }
            }

            return new CartesianProduct<(int, int)>(allPairs, allPairs);
        }
    }
}
