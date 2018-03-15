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
            _matrix = graph.GenerateAdjacencyMatrix();
            _productionRules = grammar.Content.Value.productionRules;
            _matrixSize = _matrix.GetLength(0);
            _result = string.Empty;

            AddNonterminalsToMatrix();


            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        foreach (var label1 in matrix[i, k])
                        {
                            foreach (var label2 in matrix[k, j])
                            {
                                foreach (var productionRule in productionRules)
                                {
                                    foreach (var rightHandSide in productionRule.Value)
                                    if (AreArraysOfStringsEqual(new string[] { label1, label2}, rightHandSide))
                                    {
                                        matrix[i, j].Add(productionRule.Key);
                                        result += i.ToString() + ',' + productionRule.Key + ',' + j.ToString() + Environment.NewLine;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //
            //var a = new List<string[]>();
            //a.Add(new string[] { "aa", "ab" });
            //a.Contains()
            //bool test = a[0] == (new string[] { "aa", "ab" });
            //
            return result; 
        }


        //private static bool AreArraysOfStringsEqual(string[] array1, string[] array2)
        //{
        //    return array1.Except(array2).Count() == 0 && array2.Except(array1).Count() == 0;
        //}


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
                                foreach (var terminalSymbol in _matrix[i, j])
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
    }
}
