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
            _result = string.Empty;

            BottomUp();

            return _result;
        }


        private Grammar _grammar;
        private Graph _graph;
        private CartesianProduct<(int, int)> _matrix;
        private string _result;


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


        private void BottomUp()
        {
            bool hasChanged = false;
            do
            {
                GenerateAdditions();
                AddGeneratedAdditions();
            } while (hasChanged);
        }


        /// <todo>
        /// make refactoring: rename this method
        /// </todo>
        private void GenerateAdditions()
        {
            foreach (var currNonterminal in _grammar.Nonterminals)
            {
                var startNodes = _grammar.StartNodesOfNonterminals[currNonterminal];
                foreach (var currGrammarState in startNodes)
                {
                    foreach (var currAutomationState in _graph.Nodes)
                    {
                        FindGraphAndGrammarIntersection(currAutomationState, 
                                                        currGrammarState, currNonterminal);
                    }
                }
            }
        }


        private void FindGraphAndGrammarIntersection(int automationState, int grammarState, string nonterminal)
        {
            var finalNodes = _grammar.FinalNodesOfNonterminals[nonterminal];
            var currAutomationState = automationState;
            var currGrammarState = grammarState;

            // item in worklist contains (PAIR1, TOKEN, PAIR2), where
            // PAIR1 and PAIR2 are pairs (automationState, grammarState)
            // this means that there is an ability to move from PAIR1 to PAIR2 via TOKEN
            var workList = new Stack<((int, int), string, (int, int))>();
            var history = new HashSet<((int, int), string, (int, int))>();
            do
            {
                if (workList.Count > 0)
                {
                    var currState = workList.Pop();
                    var pair1 = currState.Item1;
                    var pair2 = currState.Item3;
                    var token = currState.Item2;
                    if (!_matrix[pair1, pair2].Contains(token))
                    {
                        _matrix[pair1, pair2].Add(token);
                    }

                    currAutomationState = pair2.Item1;
                    currGrammarState = pair2.Item2;
                }

                if (_graph.AdjacencyList.ContainsKey(currAutomationState)) // not a dead end
                {
                    var paths = _graph.AdjacencyList[currAutomationState];
                    foreach (var (token, nextAutomationState) in paths)
                    {
                        if (_grammar.AdjacencyList.ContainsKey(currGrammarState)
                            // next line means that tokens in grammar and in graph (automation) are the same
                            && _grammar.AdjacencyList[currGrammarState].Count(t => t.Item1 == token) != 0)
                        {
                            var nextGrammarState = _grammar.AdjacencyList[currGrammarState]
                                                           .First(t => t.Item1 == token)  // why only first??!!
                                                           .Item2;  

                            var tuple = ((currAutomationState, currGrammarState), 
                                         token, 
                                         (nextAutomationState, nextGrammarState));

                            if (!history.Contains(tuple))
                            {
                                workList.Push(tuple);
                                history.Add(tuple);
                            }
                        }
                    }
                }

                if (finalNodes.Contains(currGrammarState))
                {
                    var pair1 = (automationState, grammarState);
                    var pair2 = (currAutomationState, currGrammarState);
                    _matrix[pair1, pair2].Add(nonterminal);
                }
            } while (workList.Count > 0);
        }


        /// <todo>
        /// make refactoring: rename this method
        /// </todo>
        private void AddGeneratedAdditions()
        {

        }
    }
}
