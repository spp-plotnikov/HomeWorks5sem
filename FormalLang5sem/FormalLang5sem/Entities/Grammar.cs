﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphviz4Net.Dot.AntlrParser;
using Graphviz4Net.Dot;


namespace FormalLang5sem
{
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
        /// true if can parse dot
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
                _parsingResult = AntlrParserAdapter<string>.GetParser().Parse(dotCode);
            }
            catch (Exception e)
            {
                ParsingErrors = e.Message;
                return false;
            }
            return true;
        }


        private DotGraph<string> _parsingResult;


        public struct GrammarContent
        {
            char[] nonterminals;
            char[] terminals;
        }


        public GrammarContent? Content;


        private static GrammarContent ParseSimpleFormat(string simpleCode)
        {
            return new GrammarContent();
        }
    }
}
