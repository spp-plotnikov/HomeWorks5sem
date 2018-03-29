﻿using System;
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
            foreach (var automationState in graph.Nodes)
            {
                foreach (var nonterminalStart in grammar.StartNodesOfNonterminals.Keys)
                {
                    var nonterminal = grammar.StartNodesOfNonterminals[nonterminalStart];

                    _gss.AddVertex(nonterminal, automationState);
                    var positionInGss = _gss.GetPositionOfVertex(nonterminal, automationState);
                    _workList.Push((automationState, nonterminalStart, positionInGss));
                }
            }
            
            GLL();
            return null;
        }


        private Stack<(int, int, int)> _workList = new Stack<(int, int, int)>();
        private GraphStructuredStack _gss = new GraphStructuredStack();


        private void GLL()
        {
            
        }
    }
}
