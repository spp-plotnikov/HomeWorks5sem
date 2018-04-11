﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormalLang5sem.Interfaces;
using FormalLang5sem.Entities;
using FormalLang5sem.Solvers;
using System.IO;


namespace FormalLang5sem
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            new Tests().MatrixSolverTest();
            PrintExplanation();
            InteractWithFiles();
        }


        private static void PrintExplanation()
        {
            Console.WriteLine("(c) Sasha Plotnikov Production, 2018");
            Console.WriteLine();
            Console.WriteLine("__________________________________");
            Console.WriteLine("This application solves the following problem:");
            Console.WriteLine("Input: 1) context-free grammar G = (Z, N, P),");
            Console.WriteLine("where Z - alphabet, N - nonterminals, P - production rules;");
            Console.WriteLine("2) finite automaton (as graph) R = (E, V, L), whrere ");
            Console.WriteLine("E - edges, V - vertices, L is contained in Z, E = V x L x V;");
            Console.WriteLine("Output: all triplets (i, N[k], j), where i, j are elements of V");
            Console.WriteLine("and exists path p in R and N[k] in N: w(p) is an element of L(G, N[k]),");
            Console.WriteLine("where w(p) is word formed by p, L(G, N[k]) is the language");
            Console.WriteLine("generated by G with start symbol N[k]");
            Console.WriteLine("__________________________________");
            Console.WriteLine();
        }


        private static void InteractWithFiles()
        {
            Console.WriteLine("Please enter graph's file path");
            var graphFilePath = Console.ReadLine();
            if (!File.Exists(graphFilePath))
            {
                Console.WriteLine("Invalid path or file does not exist");
                return;
            }

            var graph = File.ReadAllText(graphFilePath, Encoding.Default);

            Console.WriteLine("Please enter grammar's file path");
            var grammarFilePath = Console.ReadLine();
            if (!File.Exists(grammarFilePath))
            {
                Console.WriteLine("Invalid path or file does not exist");
                return;
            }

            var grammar = File.ReadAllText(grammarFilePath, Encoding.Default);

            Console.WriteLine("Please enter output file path or press Enter to output to console");
            var outputFilePath = Console.ReadLine();

            GenerateResult(graph, grammar, outputFilePath);
        }


        private static void GenerateResult(string graphText, string grammarText, string outputPath)
        {
            var graph = new Graph(graphText);
            var grammar = Grammar.FromSimpleFormat(grammarText);

            if (!graph.IsParsable.Value || !grammar.IsParsable.Value)
            {
                InformingAboutParsingErrors(graph, grammar);
                return;
            }

            ISolver solver = new MatrixSolver();
            //ISolver solver = new GLLSolver();
            //ISolver solver = new BottomUpSolver();

            var result = solver.Solve(graph, grammar);
            
            if (outputPath != "")
            {
                File.WriteAllText(outputPath, result);
            }
            else
            {
                Console.Write(result);
            }

            Console.WriteLine();
        }


        private static void InformingAboutParsingErrors(Graph graph, Grammar grammar)
        {
            Console.WriteLine("Cannot parse file");
            if (!graph.IsParsable.Value)
            {
                Console.WriteLine("Graph parsing errors: " + graph.ParsingErrors);
            }

            if (!grammar.IsParsable.Value)
            {
                Console.WriteLine("Grammar parsing errors: " + grammar.ParsingErrors);
            }
            Console.WriteLine();
        }
    }
}
