using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphviz4Net.Dot.AntlrParser;
using Graphviz4Net.Dot;


namespace FormalLang5sem.Entities
{
    /// <todo>
    /// make refactoring: split into two classes GrammarDot and GrammarSimpleFormat
    /// </todo>
    class Grammar
    {
        /// <param name="dotCode"> Grammar formated as code in Graphviz/DOT language </param>
        private Grammar(string dotCode)
        {
            IsParsable = ParseDotCode(dotCode);
        }


        /// <param name="dotCode"> Grammar formated as code in Graphviz/DOT language </param>
        public static Grammar FromDot(string dotCode)
        {
            return new Grammar(dotCode);
        }


        /// <summary>
        /// Chomsky normal form, every line contains production rule, 
        ///  left and right sides are separated by colons (:), eps is epsilon
        /// </summary>
        public static Grammar FromSimpleFormat(string simpleCode)
        {
            try
            {
                var result = ParseSimpleFormat(simpleCode);
                return new Grammar(result, true, null);
            }
            catch (Exception e)
            {
                return new Grammar(null, false, e.Message);
            }
        }


        private Grammar(GrammarContent? parsingResult, bool wasParsed, string exceptionMessage)
        {
            Content = parsingResult;
            IsParsable = wasParsed;
            ParsingErrors = exceptionMessage;
        }


        /// <summary>
        /// true if can parse 
        /// </summary>
        public bool? IsParsable;


        /// <summary>
        /// contains exceptions messages if parsing was failed
        /// </summary>
        public string ParsingErrors;


        /// <returns> returns true if parsed successfully </returns>
        private bool ParseDotCode(string dotCode)
        {
            try
            {
                _parsingResult = AntlrParserAdapter<int>.GetParser().Parse(dotCode);
                RepresentAsGraph();
            }
            catch (Exception e)
            {
                ParsingErrors = e.Message;
                return false;
            }
            return true;
        }


        private DotGraph<int> _parsingResult;


        /// <summary>
        /// if grammar represented as graph
        /// </summary>
        public Dictionary<int, List<(string, int)>> AdjacencyList;


        private void RepresentAsGraph()
        {
            AdjacencyList = new Dictionary<int, List<(string, int)>>();

            foreach (var edge in _parsingResult.VerticesEdges)
            {
                var i = edge.Source.Id;
                var j = edge.Destination.Id;
                var label = edge.Label;

                if (!AdjacencyList.ContainsKey(i))
                {
                    AdjacencyList.Add(i, new List<(string, int)>());
                }

                AdjacencyList[i].Add((label, j));
            }
        }


        public struct GrammarContent
        {
            public Dictionary<string, List<string[]>> productionRules;
        }


        public GrammarContent? Content;


        private static GrammarContent ParseSimpleFormat(string simpleCode)
        {
            var parsingResult = new GrammarContent();
            parsingResult.productionRules = new Dictionary<string, List<string[]>>();

            var lines = simpleCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(':');
                var leftHandSide = parts[0].Trim();
                var rightHandSide = parts[1].Split(new string[] { " ", "\t", "\r" }, StringSplitOptions.RemoveEmptyEntries);



                if (!parsingResult.productionRules.Keys.Contains(leftHandSide))
                {
                    parsingResult.productionRules.Add(leftHandSide, new List<string[]> { rightHandSide });
                }
                else
                {
                    parsingResult.productionRules[leftHandSide].Add(rightHandSide);
                }
            }

            return parsingResult;
        }


        /// <summary>
        /// If grammar represented as graph. Contains Key (nonterminal) and Value (list of start vertices)
        /// </summary>
        public Dictionary<string, List<int>> StartNodesOfNonterminals
        {
            get
            {
                if (_startNodesOfNonterminals != null)
                {
                    return _startNodesOfNonterminals;
                }

                _startNodesOfNonterminals = new Dictionary<string, List<int>>();
                foreach (var node in _parsingResult.Vertices)
                {
                    if (node.Attributes.ContainsKey("color") && node.Attributes["color"] == "green")
                    {
                        var nonterminal = node.Attributes["label"];
                        if (!_startNodesOfNonterminals.ContainsKey(nonterminal))
                        {
                            _startNodesOfNonterminals.Add(nonterminal, new List<int>());
                        }
                        _startNodesOfNonterminals[nonterminal].Add(node.Id);
                    }
                }
                return _startNodesOfNonterminals;
            }
        }


        private Dictionary<string, List<int>> _startNodesOfNonterminals;
    }
}
