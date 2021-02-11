using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mikroprocesor.Kompilator.TokenParser;

namespace Mikroprocesor.Kompilator
{
    //SKŁADNIA

    //TRYB REJESTROWY
    // [NUMER LINII] [TRYB ADRESOWANIA] [ROZKAZ] [REJESTR] [PRZECINEK] [REJESTR] [ŚREDNIK]

    //TRYB NATYCHMIASTOWY
    // [NUMER LINII] [TRYB ADRESOWANIA] [ROZKAZ] [PRZECINEK] [LICZBA MAX 16BIT] [ŚREDNIK]

    //PRZERWANIE

    // [PUSH] [REJESTR] [ŚREDNIK]
    // []

    public static class Parserr
    {
        public static int numerLinii { get; set; }
        public static bool wystapilBlad = false;
        public static List<string> tablicaBledow = new List<string>();

        public static ASTNode Parse(string expression, int numerLinii)
        {
            Parserr.numerLinii = numerLinii;
            var tokenParser = new TokenParser()
            {
                InputString = expression
            };

            var wyrazenie = ParseWyrazenie(tokenParser);

            return wyrazenie;
        }

        public static ASTNode ParseWyrazenie(TokenParser tokenParser)
        {
            var peek = tokenParser.Peek().TokenPeek;
            if (peek.TokenName == Tokens.STOS)
            {
                var stos = ParseStos(tokenParser);
                var rejestr = ParseRejestr(tokenParser);
                if (!rejestr.Token.TokenValue.Contains("X"))
                {
                    tablicaBledow.Add($"W linii {numerLinii + 1} \nMożna umieścić na stosie tylko rejestry 16-bitowe.");
                    wystapilBlad = true;
                }
                if (tokenParser.Peek() != null)
                {
                    var lookahead = tokenParser.Peek();
                    Token koniecLinii;
                    if (lookahead.TokenPeek.TokenName == Tokens.KONIECLINII)
                    {
                        koniecLinii = tokenParser.GetToken();
                        stos = stworzWyrazenieStosuASTNode(stos, rejestr, koniecLinii);
                    }
                    else
                    {
                        tablicaBledow.Add($"Oczekiwano ŚREDNIKA w linii nr {numerLinii + 1}, a otrzymano {tokenParser.Peek().TokenPeek.TokenName}");
                        wystapilBlad = true;
                    }

                }
                else
                {
                    tablicaBledow.Add($"Oczekiwano ŚREDNIKA w linii nr {numerLinii + 1}");
                    wystapilBlad = true;
                }
                return stos;
            }

            else if (peek.TokenName == Tokens.PRZERWANIE)
            {
                var przerwanie = ParsePrzerwanie(tokenParser);
                if (tokenParser.Peek() != null)
                {
                    var lookahead = tokenParser.Peek();
                    Token koniecLinii;
                    if (lookahead.TokenPeek.TokenName == Tokens.KONIECLINII)
                    {
                        koniecLinii = tokenParser.GetToken();
                        przerwanie = stworzWyrazeniePrzerwaniaASTNode(przerwanie, koniecLinii);
                    }
                    else
                    {
                        tablicaBledow.Add($"Oczekiwano ŚREDNIKA w linii nr {numerLinii + 1}, a otrzymano {tokenParser.Peek().TokenPeek.TokenName}");
                        wystapilBlad = true;
                    }

                }
                else
                {
                    tablicaBledow.Add($"Oczekiwano ŚREDNIKA w linii nr {numerLinii + 1}");
                    wystapilBlad = true;
                }
                return przerwanie;
            }
            else
            {
                var tryb = ParseTrybAdresowania(tokenParser);
                var rozkaz = ParseRozkaz(tokenParser);
                var argument1 = ParseRejestr(tokenParser);
                var przecinek = ParsePrzecinek(tokenParser);
                ASTNode argument2;
                if (tryb.Token.TokenValue == "IMM")
                {
                    argument2 = ParseLiczba(tokenParser);

                    if (tryb.Token.TokenValue == "IMM")
                    {
                        if (rozkaz.Token.TokenValue == "MOV")
                        {
                            if (Int32.Parse(argument2.Token.TokenValue) > 32767 && argument1.Token.TokenValue.Contains("X"))
                            {
                                tablicaBledow.Add($"W linii {numerLinii + 1} \nNie można przenieść tak dużej wartości. Maksymalna liczba to 32767");
                                wystapilBlad = true;
                            }
                            if (Int32.Parse(argument2.Token.TokenValue) > 127 && (argument1.Token.TokenValue.Contains("H") || argument1.Token.TokenValue.Contains("L")))
                            {
                                tablicaBledow.Add($"W linii {numerLinii + 1} \nNie można przenieść tak dużej wartości. Maksymalna liczba to 127");
                                wystapilBlad = true;
                            }
                        }
                    }
                }
                else argument2 = ParseRejestr(tokenParser);

                if (tokenParser.Peek() != null)
                {
                    var lookahead = tokenParser.Peek();
                    Token koniecLinii;
                    if (lookahead.TokenPeek.TokenName == Tokens.KONIECLINII)
                    {
                        koniecLinii = tokenParser.GetToken();
                        tryb = stworzWyrazenieASTNode(tryb, rozkaz, argument1, argument2, przecinek, koniecLinii);
                    }
                    else
                    {
                        tablicaBledow.Add($"Oczekiwano ŚREDNIKA w linii nr {numerLinii + 1}, a otrzymano {tokenParser.Peek().TokenPeek.TokenName}");
                        wystapilBlad = true;
                    }

                }
                else
                {
                    tablicaBledow.Add($"Oczekiwano ŚREDNIKA w linii nr {numerLinii + 1}");
                    wystapilBlad = true;
                }

                return tryb;
            }

        }

        public static ASTNode ParseTrybAdresowania(TokenParser tokenParser)
        {
            Expect(tokenParser, Tokens.TRYB);
            return new RozkazASTNode(tokenParser.GetToken());
        }

        private static ASTNode ParseRozkaz(TokenParser tokenParser)
        {
            Expect(tokenParser, Tokens.ROZKAZ);
            return new RozkazASTNode(tokenParser.GetToken());
        }

        public static ASTNode ParseRejestr(TokenParser tokenParser)
        {
            Expect(tokenParser, Tokens.REJESTR);
            return new RejestrASTNode(tokenParser.GetToken());
        }

        public static ASTNode ParsePrzecinek(TokenParser tokenParser)
        {
            Expect(tokenParser, Tokens.PRZECINEK);
            return new PrzecinekASTNode(tokenParser.GetToken());
        }

        public static ASTNode ParseKoniec(TokenParser tokenParser)
        {
            Expect(tokenParser, Tokens.KONIECLINII);
            return new PrzecinekASTNode(tokenParser.GetToken());
        }

        public static ASTNode ParseLiczba(TokenParser tokenParser)
        {
            Expect(tokenParser, Tokens.LICZBA);
            return new LiczbaASTNode(tokenParser.GetToken());
        }

        public static ASTNode ParseStos(TokenParser tokenParser)
        {
            Expect(tokenParser, Tokens.STOS);
            return new StosASTNode(tokenParser.GetToken());
        }

        public static ASTNode ParsePrzerwanie(TokenParser tokenParser)
        {
            Expect(tokenParser, Tokens.PRZERWANIE);
            return new PrzerwanieASTNode(tokenParser.GetToken());
        }

        private static ASTNode stworzWyrazenieASTNode(ASTNode tryb, ASTNode rozkaz, ASTNode argument1, ASTNode argument2, ASTNode przecinek, Token koniecLinii)
        {
            return new WyrazenieASTNode(tryb, rozkaz, argument1, argument2, przecinek, koniecLinii);
        }

        private static ASTNode stworzWyrazenieStosuASTNode(ASTNode stos, ASTNode rejestr, Token koniecLinii)
        {
            return new WyrazenieStosuASTNode(stos, rejestr, koniecLinii);
        }

        private static ASTNode stworzWyrazeniePrzerwaniaASTNode(ASTNode przerwanie, Token koniecLinii)
        {
            return new WyrazeniePrzerwaniaASTNode(przerwanie, koniecLinii);
        }

        public static void Expect(TokenParser tokenParser, Tokens expected)
        {
            if (tokenParser.Peek() != null)
            {
                if (tokenParser.Peek().TokenPeek.TokenName != expected)
                {
                    tablicaBledow.Add($"Oczekiwano {expected} w linii nr {numerLinii + 1}, a otrzymano {tokenParser.Peek().TokenPeek.TokenName}");
                    wystapilBlad = true;
                }
            }

        }

    }
}
