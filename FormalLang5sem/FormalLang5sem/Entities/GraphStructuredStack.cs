using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalLang5sem.Entities
{
    class GraphStructuredStack
    {
        private List<(string, int)> _vertices;
        private Dictionary<int, List<(int, int)>> _edges;


        public GraphStructuredStack()
        {
            _vertices = new List<(string, int)>();
            _edges = new Dictionary<int, List<(int, int)>>();
        }


        public void AddVertex(string nonterminal, int positionInGraph)
        {
            var tuple = (nonterminal, positionInGraph);

            if (!_vertices.Contains(tuple))
            {
                _vertices.Add(tuple);
            }
        }


        public int GetPositionOfVertex(string nonterminal, int positionInGraph) 
            => _vertices.IndexOf((nonterminal, positionInGraph));


        public bool ContainsVertex(string nonterminal, int positionInGraph) 
            => _vertices.Contains((nonterminal, positionInGraph));


        public void AddEdge(int vertexPosition1, int positionInGrammar, int vertexPosition2)
        {
            if (!_edges.ContainsKey(vertexPosition1))
            {
                _edges.Add(vertexPosition1, new List<(int, int)>());
            }

            var tuple = (positionInGrammar, vertexPosition2);
            if (!_edges[vertexPosition1].Contains(tuple))
            {
                _edges[vertexPosition1].Add(tuple);
            }
        }
    }
}
