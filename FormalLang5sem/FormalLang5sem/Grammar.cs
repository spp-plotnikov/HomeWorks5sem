using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalLang5sem
{
    class Grammar
    {
        /// <param name="dotCode"> Grammar formated as code in Graphviz/DOT language </param>
        public Grammar(string dotCode)
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
                // parsing ...
            }
            catch (Exception e)
            {
                ParsingErrors = e.Message;
                return false;
            }
            return true;
        }
    }
}
