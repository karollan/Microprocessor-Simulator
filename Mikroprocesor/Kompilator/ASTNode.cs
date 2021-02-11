using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikroprocesor.Kompilator
{
    public abstract class ASTNode
    {
        public Token Token { get; private set; }
        public ASTNode(Token token) => Token = token;
    }

    public class LiczbaASTNode : ASTNode
    {
        public int Value => int.Parse(Token.TokenValue);

        public LiczbaASTNode(Token token) : base(token) { }
    }

    public class StosASTNode : ASTNode
    {
        public StosASTNode(Token token) : base(token) { }
    }

    public class PrzerwanieASTNode : ASTNode
    {
        public PrzerwanieASTNode(Token token) : base(token) { }
    }

    public class RozkazASTNode : ASTNode
    {
        public RozkazASTNode(Token token) : base(token) { }
    }

    public class RejestrASTNode : ASTNode
    {
        public RejestrASTNode(Token token) : base(token) { }
    }

    public class PrzecinekASTNode : ASTNode
    {
        public PrzecinekASTNode(Token token) : base(token) { }
    }

    public class WyrazenieASTNode : TrybAdresowaniaASTNode
    {
        public WyrazenieASTNode(ASTNode tryb, ASTNode rozkaz, ASTNode argument1, ASTNode argument2, ASTNode przecinek, Token token) : base(token)
        {
            Tryb = tryb;
            Rozkaz = rozkaz;
            Argument1 = argument1;
            Argument2 = argument2;
            Przecinek = przecinek;
        }

        public ASTNode Tryb { get; }
        public ASTNode Rozkaz { get; }
        public ASTNode Argument1 { get; }
        public ASTNode Argument2 { get; }
        public ASTNode Przecinek { get; }
    }

    public class WyrazenieStosuASTNode : StosASTNode
    {
        public WyrazenieStosuASTNode(ASTNode stos, ASTNode rejestr, Token token) : base(token)
        {
            Stos = stos;
            Rejestr = rejestr;
        }
        public ASTNode Stos { get; }
        public ASTNode Rejestr { get; }
    }

    public class WyrazeniePrzerwaniaASTNode : PrzerwanieASTNode
    {
        public WyrazeniePrzerwaniaASTNode(ASTNode przerwanie, Token token) : base(token)
        {
            Przerwanie = przerwanie;
        }

        public ASTNode Przerwanie { get; }
    }

    public class TrybAdresowaniaASTNode : ASTNode
    {
        public TrybAdresowaniaASTNode(Token token) : base(token)
        {
        }

    }

}
