using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mikroprocesor.Kompilator
{
    public class TokenParser
    {
        // This dictionary will store our RegEx rules
        private readonly Dictionary<Tokens, string> _tokens;
        // This dictionary will store our matches
        private readonly Dictionary<Tokens, MatchCollection> _regExMatchCollection;
        // This input string will store the string to parse
        private string _inputString;
        // This index is used internally so the parser knows where it left off
        private int _index;

        // This is our token enumeration. It holds every token defined in the grammar
        /// <summary>
        /// Tokens is an enumeration of all possible token values.
        /// </summary>
        public enum Tokens
        {
            UNDEFINED = 0,
            SPACJA = 1,
            LICZBA = 2,
            REJESTR = 3,
            ROZKAZ = 4,
            TRYB = 5,
            PRZECINEK = 6,
            KONIECLINII = 7,
            NOWALINIA = 8,
            STOS = 9,
            PRZERWANIE = 10,
        }

        public int getPosition()
        {
            return _index;
        }

        // A public setter for our input string
        /// <summary>
        /// InputString Property
        /// </summary>
        /// <value>
        /// The string value that holds the input string.
        /// </value>
        public string InputString
        {
            set
            {
                _inputString = value;
                _inputString = Regex.Replace(_inputString, @"\s+", string.Empty);
                PrepareRegex();
            }
        }

        // Our Constructor, which simply initializes values
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <remarks>
        /// The constructor initalizes memory and adds all of the tokens to the token dictionary.
        /// </remarks>
        public TokenParser()
        {
            _tokens = new Dictionary<Tokens, string>();
            _regExMatchCollection = new Dictionary<Tokens, MatchCollection>();
            _index = 0;
            _inputString = string.Empty;

            // These lines add each grammar rule to the dictionary
            _tokens.Add(Tokens.SPACJA, "[ \\s]+");
            _tokens.Add(Tokens.LICZBA, "[0-9]+");
            _tokens.Add(Tokens.REJESTR, "[A][X]|[B][X]|[C][X]|[D][X]|[A][L]|[A][H]|[B][L]|[B][H]|[C][L]|[C][H]|[D][L]|[D][H]");
            _tokens.Add(Tokens.ROZKAZ, "[A][D][D]|[S][U][B]|[M][O][V]");
            _tokens.Add(Tokens.TRYB, "[I][M][M]|[R][E][G]");
            _tokens.Add(Tokens.PRZECINEK, "\\,");
            _tokens.Add(Tokens.KONIECLINII, "\\;");
            _tokens.Add(Tokens.NOWALINIA, "\\n+");
            _tokens.Add(Tokens.STOS, "[P][U][S][H]|[P][O][P]");
            _tokens.Add(Tokens.PRZERWANIE, "[I][N][T][0-9][0-9]|[I][N][T][0-9][A-Z]");
        }

        // This function preloads the matches based on our rules and the input string
        /// <summary>
        /// PrepareRegex prepares the regex for parsing by pre-matching the Regex tokens.
        /// </summary>
        private void PrepareRegex()
        {
            _regExMatchCollection.Clear();
            foreach (KeyValuePair<Tokens, string> pair in _tokens)
            {
                _regExMatchCollection.Add(pair.Key, Regex.Matches(_inputString, pair.Value));
            }
        }

        // ResetParser() will reset the parser.
        // Keep in mind that you must set the input string again
        /// <summary>
        /// ResetParser resets the parser to its inital state. Reloading InputString is required.
        /// </summary>
        /// <seealso cref="InputString">
        public void ResetParser()
        {
            _index = 0;
            _inputString = string.Empty;
            _regExMatchCollection.Clear();
        }

        // GetToken() retrieves the next token and returns a token object
        /// <summary>
        /// GetToken gets the next token in queue
        /// </summary>
        /// <remarks>
        /// GetToken attempts to the match the next character(s) using the
        /// Regex rules defined in the dictionary. If a match can not be
        /// located, then an Undefined token will be created with an empty
        /// string value. In addition, the token pointer will be incremented
        /// by one so that this token doesn't attempt to get identified again by
        /// GetToken()
        /// </remarks>
        public Token GetToken()
        {
            // If we are at the end of our input string then
            // we return null to signify the end of our input string.
            // While parsing tokens, you will undoubtedly be in a loop.
            // Having your loop check for a null token is a good way to end that
            // loop
            if (_index >= _inputString.Length)
                return null;

            // Iterate through our prepared matches/Tokens dictionary
            foreach (KeyValuePair<Tokens, MatchCollection> pair in _regExMatchCollection)
            {
                // Iterate through each prepared match
                foreach (Match match in pair.Value)
                {
                    // If we find a match, update our index pointer and return a new Token object
                    if (match.Index == _index)
                    {
                        _index += match.Length;
                        return new Token(pair.Key, match.Value);
                    }

                    if (match.Index > _index)
                    {
                        break;
                    }
                }
            }
            // If execution got here, then we increment our index pointer
            // and return an Undefined token. 
            _index++;
            return new Token(Tokens.UNDEFINED, string.Empty);
        }

        // Peek() will retrieve a PeekToken object and will allow you to see the next token
        // that GetToken() will retrieve.
        /// <summary>
        /// Returns the next token that GetToken() will return.
        /// </summary>
        /// <seealso cref="Peek(PeekToken)">
        public PeekToken Peek()
        {
            return Peek(new PeekToken(_index, new Token(Tokens.UNDEFINED, string.Empty)));
        }

        // This is an overload for Peek(). By passing in the last PeekToken object
        // received from Peek(), you can peek ahead to the next token, and the token after that, etc...
        /// <summary>
        /// Returns the next token after the Token passed here
        /// </summary>
        /// <param name="peekToken">The PeekToken token returned from a previous Peek() call</param>
        /// <seealso cref="Peek()">
        public PeekToken Peek(PeekToken peekToken)
        {
            int oldIndex = _index;

            _index = peekToken.TokenIndex;

            if (_index >= _inputString.Length)
            {
                _index = oldIndex;
                return null;
            }

            foreach (KeyValuePair<Tokens, string> pair in _tokens)
            {
                Regex r = new Regex(pair.Value);
                Match m = r.Match(_inputString, _index);

                if (m.Success && m.Index == _index)
                {
                    _index += m.Length;
                    PeekToken pt = new PeekToken(_index, new Token(pair.Key, m.Value));
                    _index = oldIndex;
                    return pt;
                }
            }
            PeekToken pt2 = new PeekToken(_index + 1, new Token(Tokens.UNDEFINED, string.Empty));
            _index = oldIndex;
            return pt2;
        }
    }

    // This defines the PeekToken object
    /// <summary>
    /// A PeekToken object class
    /// </summary>
    /// <remarks>
    /// A PeekToken is a special pointer object that can be used to Peek() several
    /// tokens ahead in the GetToken() queue.
    /// </remarks>
    public class PeekToken
    {
        public int TokenIndex { get; set; }

        public Token TokenPeek { get; set; }

        public PeekToken(int index, Token value)
        {
            TokenIndex = index;
            TokenPeek = value;
        }
    }

}
