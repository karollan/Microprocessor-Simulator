using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikroprocesor.Kompilator
{
    // This defines the Token object
    /// <summary>
    /// a Token object class
    /// </summary>
    /// <remarks>
    /// A Token object holds the token and token value.
    /// </remarks>
    public class Token
    {
        public TokenParser.Tokens TokenName { get; set; }

        public string TokenValue { get; set; }

        public Token(TokenParser.Tokens name, string value)
        {
            TokenName = name;
            TokenValue = value;
        }


    }
}
