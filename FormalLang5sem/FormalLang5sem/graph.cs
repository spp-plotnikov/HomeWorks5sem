using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalLang5sem
{
    class Graph
    {
        /// <param name="dotCode"> Graph formated as code in dot language </param>
        public Graph(string dotCode)
        {
            IsParsable = ParseDotCode(dotCode);
        }


        /// <summary>
        /// true if can parse dot
        /// </summary>
        public bool? IsParsable;


        private bool ParseDotCode(string dotCode)
        {
            return false;
        }

    }
}
