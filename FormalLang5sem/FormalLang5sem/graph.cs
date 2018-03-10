﻿using System;
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
    }
}
