﻿using System;
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
            _matrix = graph.GenerateAdjacencyMatrix();
            _productionRules = grammar.Content.Value.productionRules;
            _matrixSize = _matrix.GetLength(0);
            _result = string.Empty;

            AddNonterminalsToMatrix();
            TransitiveClosure();

            return _result; 
        }


        private static bool AreArraysOfStringsEqual(string[] array1, string[] array2)
        {
            if (array1.Count() != array2.Count())
            {
                return false;
            }

            for (int i = 0; i < array1.Count(); i++)
            {
                if (!array1[i].Equals(array2[i]))
                {
                    return false;
                }
            }
            return true;
        }


        private string _result;
        private int _matrixSize;
        private List<string>[,] _matrix;
        private Dictionary<string, List<string[]>> _productionRules;


        private void AddNonterminalsToMatrix()
        {
            for (int i = 0; i < _matrixSize; i++)
            {
                for (int j = 0; j < _matrixSize; j++)
                {
                    foreach (var productionRule in _productionRules)
                    {
                        foreach (var rightHandSide in productionRule.Value)
                        {
                            if (rightHandSide.Count() == 1)  // this means that right hand side is only one terminal symbol
                            {
                                foreach (var terminalSymbol in _matrix[i, j].ToList())
                                {
                                    if (terminalSymbol == rightHandSide.First())
                                    {
                                        _matrix[i, j].Add(productionRule.Key);
                                        _result += i.ToString() + ',' + productionRule.Key + ',' 
                                                + j.ToString() + Environment.NewLine;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        /// <todo>
        /// TODO: make refactoring: make code more readable
        /// </todo>
        private void TransitiveClosure()
        {
            for (int k = 0; k < _matrixSize; k++)
            {
                for (int i = 0; i < _matrixSize; i++)
                {
                    for (int j = 0; j < _matrixSize; j++)
                    {
                        foreach (var label1 in _matrix[i, k].ToList())
                        {
                            foreach (var label2 in _matrix[k, j].ToList())
                            {
                                foreach (var productionRule in _productionRules)
                                {
                                    foreach (var rightHandSide in productionRule.Value)
                                    {
                                        if (AreArraysOfStringsEqual(new string[] { label1, label2 }, rightHandSide))
                                        {
                                            _matrix[i, j].Add(productionRule.Key);
                                            _result += i.ToString() + ',' + productionRule.Key + ',' + j.ToString() + Environment.NewLine;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
