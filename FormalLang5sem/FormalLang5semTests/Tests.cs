using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormalLang5sem.Solvers;
using FormalLang5sem.Entities;
using System.Text;
using System.IO;


namespace FormalLang5semTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GLLSolverTest()
        {
            string allResults = "";
            var solver = new GLLSolver();
            foreach (var grammarFile in _grammars)
            {
                foreach (var graphFile in _automata)
                {
                    var graphDot = File.ReadAllText("Data\\Automata\\" + graphFile, Encoding.Default);
                    var grammarDot = File.ReadAllText("Data\\Grammars\\" + grammarFile, Encoding.Default);

                    var graph = new Graph(graphDot);
                    var grammar = Grammar.FromDot(grammarDot);

                    var result = solver.Solve(graph, grammar);
                    var count = result.Split('S').Length - 1;  // count of triplets (i,S,j)

                    allResults += graphFile + ' ' + grammarFile + ' '
                               + count.ToString() + Environment.NewLine;
                }
            }

            File.WriteAllText("GLLSolver_results.txt", allResults);
        }


        private static string[] _automata =
        {
            "skos.dot",
            "generations.dot",
            "travel.dot",
            "univ-bench.dot",
            "atom-primitive.dot",
            "biomedical-mesure-primitive.dot",
            "foaf.dot",
            "people-pets.dot",
            "funding.dot",
            "wine.dot",
            "pizza.dot"
        };


        private static string[] _grammars =
        {
            "Q1.dot",
            "Q2.dot",
            "Q3.dot"
        };
    }
}
