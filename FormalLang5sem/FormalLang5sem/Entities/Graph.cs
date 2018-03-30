using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalLang5sem.Entities
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
                _parsingResult = DotParser.parse(dotCode);
            }
            catch (Exception e)
            {
                ParsingErrors = e.Message;
                return false;
            }
            return true;
        }


        private GraphData.GraphData _parsingResult;


        private List<string>[,] GenerateAdjacencyMatrix()
        {
            int size = _parsingResult.Nodes.Count;
            var matrix = new List<string>[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = new List<string>();
                }
            }

            foreach (var edge in _parsingResult.Edges)
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


        /// <summary>
        /// lists contain labels of edges (if edge (i, j) labeled by "A", list at pos (i, j) will contain "A")
        /// </summary>
        public List<string>[,] AdjacencyMatrix
        {
            get
            {
                if (_adjacencyMatrix != null)
                {
                    return _adjacencyMatrix;
                }
                else
                {
                    _adjacencyMatrix = GenerateAdjacencyMatrix();
                    return _adjacencyMatrix;
                }
            }
        }


        private List<int> _nodes;
        private Dictionary<int, List<(string, int)>> _adjacencyList;
        private List<string>[,] _adjacencyMatrix;


        /// <summary>
        /// dictionary where Key is start node and Value is list of pairs (label, final node)
        /// </summary>
        public Dictionary<int, List<(string, int)>> AdjacencyList
        {
            get
            {
                if (_adjacencyList != null)
                {
                    return _adjacencyList;
                }

                _adjacencyList = new Dictionary<int, List<(string, int)>>();

                foreach (var edge in _parsingResult.Edges)
                {
                    var i = int.Parse(edge.Key.Item1);
                    var j = int.Parse(edge.Key.Item2);

                    if (!_adjacencyList.ContainsKey(i))
                    {
                        _adjacencyList.Add(i, new List<(string, int)>());
                    }

                    foreach (var value in edge.Value)
                    {
                        _adjacencyList[i].Add((value["label"], j));
                    }
                }
                return _adjacencyList;
            }
        }


        /// <summary>
        /// number of vertices in graph
        /// </summary>
        public int CountOfNodes => _parsingResult.Nodes.Count;


        /// <summary>
        /// all vertices in graph
        /// </summary>
        public List<int> Nodes
        {
            get
            {
                if (_nodes != null)
                {
                    return _nodes;
                }

                _nodes = new List<int>();
                foreach (var node in _parsingResult.Nodes)
                {
                    var i = int.Parse(node.Key);
                    _nodes.Add(i);
                }
                return _nodes;
            }
        }
    }
}
