using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormalLang5sem.Entities;
using FormalLang5sem.Solvers;
using FormalLang5sem.Interfaces;
using System.IO;


namespace FormalLang5sem
{
    class Tests
    {
        public void RunAllTests()
        {
            SingleStateAutomationTest();
            SimpleTest();
            ABCTest();
            LoopsTest();
            AmbiguousTest();
            ABTest();
            GLLSolverTest();
            BottomUpSolverTest();
            MatrixSolverTest();
        }


        public void ABCTest()
        {
            StandardTest("abc", 1);
        }


        public void ABTest()
        {
            StandardTest("a...ab", 6);
        }


        public void LoopsTest()
        {
            StandardTest("loops", 3);
        }


        private void StandardTest(string testName, int correctCount)
        {
            Console.WriteLine("Test name: " + testName);

            var grammarFileDot = "Resources\\Grammars\\" + testName + ".dot";
            var grammarFileSimple = "Resources\\Grammars\\" + testName + ".txt";
            var graphFile = "Resources\\Automata\\" + testName + ".dot";

            var graphDot = File.ReadAllText(graphFile, Encoding.Default);
            var grammarDot = File.ReadAllText(grammarFileDot, Encoding.Default);
            var grammarSimple = File.ReadAllText(grammarFileSimple, Encoding.Default);

            var graph = new Graph(graphDot);
            var grammar = Grammar.FromDot(grammarDot);
            var grammarForMatrix = Grammar.FromSimpleFormat(grammarSimple);

            var gllSolver = new GLLSolver();
            var matrixSolver = new MatrixSolver();
            var bottomUpSolver = new BottomUpSolver();

            var resultGll = gllSolver.Solve(graph, grammar);
            var resultMatrix = matrixSolver.Solve(graph, grammarForMatrix);
            var resultBottomUp = bottomUpSolver.Solve(graph, grammar);

            var all = new List<string>() { resultBottomUp, resultMatrix, resultGll };
            foreach (var result in all)
            {
                var count = result.Split('S').Length - 1;  // count of triplets (i,S,j)
                if (count != correctCount)
                {
                    Console.WriteLine("Test failed: " + testName + Environment.NewLine);
                    return;
                }
            }
            Console.WriteLine("Test passed: " + testName);
            Console.WriteLine();
        }


        public void SimpleTest()
        {
            StandardTest("simple", 2);
        }


        public void SingleStateAutomationTest()
        {
            StandardTest("one_state", 1);
        }


        public void AmbiguousTest()
        {
            StandardTest("ambiguous", 15);
        }


        public void MatrixSolverTest()
        {
            Console.WriteLine("Tests for Matrix:");
            StandardTest(new MatrixSolver());
            Console.WriteLine();
        }


        public void BottomUpSolverTest()
        {
            Console.WriteLine("Tests for Bottom Up:");
            StandardTest(new BottomUpSolver());
            Console.WriteLine();
        }


        public void GLLSolverTest()
        {
            Console.WriteLine("Tests for GLL:");
            StandardTest(new GLLSolver());
            Console.WriteLine();
        }


        private void StandardTest(ISolver solver)
        {
            bool isMatrixSolver = solver.GetType() == typeof(MatrixSolver);

            foreach (var grammarName in _grammars)
            {
                foreach (var graphName in _automata)
                {
                    var filenameExtension = isMatrixSolver ? ".txt" : ".dot";
                    var grammarFile = "Resources\\Grammars\\" + grammarName + filenameExtension;
                    var graphFile = "Resources\\Automata\\" + graphName + ".dot";

                    var graphDot = File.ReadAllText(graphFile, Encoding.Default);
                    var grammarDotOrSimple = File.ReadAllText(grammarFile, Encoding.Default);

                    var graph = new Graph(graphDot);
                    var grammar = isMatrixSolver ? Grammar.FromSimpleFormat(grammarDotOrSimple) 
                                                 : Grammar.FromDot(grammarDotOrSimple);

                    var result = solver.Solve(graph, grammar);
                    var count = result.Split('S').Length - 1;  // count of triplets (i,S,j)

                    Console.Write("Automation: " + graphName + "; Grammar: " + grammarName);
                    Console.Write("; Result: " + count.ToString());
                    result = _correctResults[graphName + grammarName] == count ? " test passed" : " test failed";
                    Console.Write(result + Environment.NewLine);
                }
            }
        }


        private static string[] _automata =
        {
            "skos",
            "generations",
            "travel",
            "univ-bench",
            "atom-primitive",
            "biomedical-mesure-primitive",
            "foaf",
            "people_pets",
            "funding",
            "wine",
            "pizza"
        };


        private static string[] _grammars =
        {
            "Q1",
            "Q2",
            "Q3"
        };


        private static Dictionary<string, int> _correctResults = new Dictionary<string, int>()
        {
            { "skosQ1", 810 },
            { "generationsQ1", 2164 },
            { "travelQ1", 2499 },
            { "univ-benchQ1", 2540 },
            { "atom-primitiveQ1", 15454 },
            { "biomedical-mesure-primitiveQ1", 15156 },
            { "foafQ1", 4118 },
            { "people_petsQ1", 9472 },
            { "fundingQ1", 17634 },
            { "wineQ1", 66572 },
            { "pizzaQ1", 56195 },
            { "skosQ2", 1 },
            { "generationsQ2", 0 },
            { "travelQ2", 63 },
            { "univ-benchQ2", 81 },
            { "atom-primitiveQ2", 122 },
            { "biomedical-mesure-primitiveQ2", 2871 },
            { "foafQ2", 10 },
            { "people_petsQ2", 37 },
            { "fundingQ2", 1158 },
            { "wineQ2", 133 },
            { "pizzaQ2", 1262 },
            { "skosQ3", 32 },
            { "generationsQ3", 19 },
            { "travelQ3", 31 },
            { "univ-benchQ3", 12 },
            { "atom-primitiveQ3", 3 },
            { "biomedical-mesure-primitiveQ3", 0 },
            { "foafQ3", 46 },
            { "people_petsQ3", 36 },
            { "fundingQ3", 18 },
            { "wineQ3", 1215 },
            { "pizzaQ3", 9520 }
        };
    }
}
