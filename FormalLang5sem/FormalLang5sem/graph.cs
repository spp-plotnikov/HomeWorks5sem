using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalLang5sem
{
    class Graph
    {
        /// <param name="dotCode"> Graph formated as code in Graphviz/DOT language </param>
        public Graph(string dotCode)
        {
            IsParsable = ParseDotCode(dotCode);
        }


        /// <summary>
        /// true if can parse dot
        /// </summary>
        public bool? IsParsable;


        /// <summary>
        /// contains exceptions messages if parsing was failed
        /// </summary>
        public string ParsingErrors;


        private bool ParseDotCode(string dotCode)
        {
            try
            {
                parsingResult = DotParser.parse(dotCode);
            }
            catch (Exception e)
            {
                ParsingErrors = e.Message;
                return false;
            }
            return true;
        }


        private GraphData.GraphData parsingResult;


        /// <summary>
        /// lists contain labels of edges (if edge (i, j) labeled by "A", list at pos (i, j) will contain "A")
        /// </summary>
        public List<string>[,] GenerateAdjacencyMatrix()
        {
            int size = parsingResult.Nodes.Count;
            var matrix = new List<string>[size, size];

            for (int i = 0, j = 0; i < size && j < size; i++, j++)
            {
                matrix[i, j] = new List<string>();
            }

            foreach (var edge in parsingResult.Edges)
            {
                var i = int.Parse(edge.Key.Item1);
                var j = int.Parse(edge.Key.Item2);
                
                foreach (var value in edge.Value)
                {
                    matrix[i, j].Add(value["label"]);
                }
            }
            return matrix;
        }
    }
}
