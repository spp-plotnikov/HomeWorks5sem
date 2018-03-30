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
            _workList = new Stack<(int, int, int)>();
            _history = new HashSet<(int, int, int)>();
            _gss = new GraphStructuredStack();
            _graph = graph;
            _grammar = grammar;

            foreach (var automationState in graph.Nodes)
            {
                foreach (var nonterminal in grammar.Nonterminals)
                {
                    _gss.AddVertex(nonterminal, automationState);
                    var positionInGss = _gss.GetPositionOfVertex(nonterminal, automationState);

                    foreach (var nonterminalStart in grammar.StartNodesOfNonterminals[nonterminal])
                    {
                        _workList.Push((automationState, nonterminalStart, positionInGss));
                    }                   
                }
            }
            
            GLL();
            return null;
        }


        private Stack<(int, int, int)> _workList;
        private HashSet<(int, int, int)> _history;
        private GraphStructuredStack _gss;
        private Graph _graph;
        private Grammar _grammar;


        private void GLL()
        {
            while (_workList.Count != 0)
            {
                var configuration = _workList.Pop();

                if (_history.Contains(configuration))
                {
                    continue;   //  to avoid looping
                }
                _history.Add(configuration);

                HandleConfiguration(configuration);
            }
        }


        private void HandleConfiguration((int automationPos, int grammarPos, int gssPos) config)
        {
            var automationCurrentPos = config.automationPos;
            var grammarCurrentPos = config.grammarPos;
            var gssCurrentPos = config.gssPos;

            foreach (var pos in _grammar.AdjacencyList[grammarCurrentPos])
            {
                var grammarLabel = pos.Item1;
                var grammarNextPos = pos.Item2;
                
                // case 1: current token in grammar is nonterminal
                if (_grammar.Nonterminals.Contains(grammarLabel))
                {
                    _gss.AddVertex(grammarLabel, automationCurrentPos);
                    var gssLastPos = _gss.GetPositionOfVertex(grammarLabel, automationCurrentPos);
                    _gss.AddEdge(gssLastPos, grammarNextPos, gssCurrentPos);

                    foreach (var start in _grammar.StartNodesOfNonterminals[grammarLabel])
                    {
                        _workList.Push((automationCurrentPos, start, gssLastPos));
                    }
                }

                // case 2: current tokens in grammar and in automation are equal terminals
                if (_graph.AdjacencyList.ContainsKey(automationCurrentPos))
                {
                    foreach (var pair in _graph.AdjacencyList[automationCurrentPos])
                    {
                        var (automationLabel, automationNextPos) = pair;

                        if (automationLabel == grammarLabel)
                        {
                            _workList.Push((automationNextPos, grammarNextPos, gssCurrentPos));
                        }
                    }
                }
                // case 3:
            }
        }
    }
}
