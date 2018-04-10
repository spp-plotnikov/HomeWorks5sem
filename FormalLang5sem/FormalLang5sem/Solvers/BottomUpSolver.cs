﻿using System;
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
                var finalNodes = _grammar.FinalNodesOfNonterminals[currNonterminal];
                foreach (var currGrammarState in startNodes)
                {
                    foreach (var currAutomationState in _graph.Nodes)
                    {
                        FindGraphAndGrammarIntersection(currAutomationState, currGrammarState);
                    }
                }
            }
        }


        private void FindGraphAndGrammarIntersection(int currAutomationState, int currGrammarState)
        {
            do
            {
                if (_graph.AdjacencyList.ContainsKey(currAutomationState)) // not a dead end
                {
                    var paths = _graph.AdjacencyList[currAutomationState];
                    foreach (var (token, nextAutomationState) in paths)
                    {
                        if (_grammar.AdjacencyList.ContainsKey(currGrammarState)
                            // next line means that tokens in grammar and in graph (automation) are the same
                            && _grammar.AdjacencyList[currGrammarState].Count(t => t.Item1 == token) != 0)
                        {

                        }
                    }
                }
            } while ();
        }


        /// <todo>
        /// make refactoring: rename this method
        /// </todo>
        private void AddGeneratedAdditions()
        {

        }
    }
}
