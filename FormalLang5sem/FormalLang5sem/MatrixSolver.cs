using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphviz4Net.Dot.AntlrParser;
//using QuickGraph.Graphviz.Dot;



namespace FormalLang5sem
{
    class MatrixSolver : ISolver
    {
        public string Solve(string graph, string grammar)
        {
            var a = DotParser.parse(graph);

            //int count = a.Nodes.Count();
            //var gg = a.Nodes.ElementAt(0).Value;

            //string resultTest = "";
            //for (int i = 0; i < count; i++)
            //{
            //    //resultTest += a.EdgeAttributes.ElementAt(i);
            //    //resultTest += a.Nodes.ElementAt(i).Key;
            //}

            //foreach (var el in a.Nodes)
            //{
            //    var x = el.Key;
            //    var y = el.Value;
            //    //int x1 = Convert.ToInt32(el.Value);
            //   // int x2 = Convert.ToInt32(el.Key.Item2);
            //}

            //var u =  QuickGraph.AdjacencyGraph<int, QuickGraph.IEdge<int>>.LoadDot();
            //QuickGraph.dotp
            //u.

            var j = AntlrParserAdapter<int>.GetParser().Parse(graph);
            //QuickGraph.IGraph<int, int>()

            return null; // resultTest;
        }
    }
}
