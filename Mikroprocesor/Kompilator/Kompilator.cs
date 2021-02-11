using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikroprocesor.Kompilator
{
    class Kompilator
    {
        private static Mikroprocesor procesor;
        private static string ex;
        private static int nr;
        public static bool Evaluate(string expression, Mikroprocesor mik, int numerLinii)
        {
            procesor = mik;
            ex = expression;
            nr = numerLinii;

            var astRoot = Parserr.Parse(expression, numerLinii);

            if (!Parserr.wystapilBlad)
            {
                procesor.ChangeLineForeColor(Color.Green, expression, numerLinii);
                Evaluate(astRoot as dynamic);
            }
            else procesor.ChangeLineForeColor(Color.Red, expression, numerLinii);

            return !Parserr.wystapilBlad;
        }

        static int Evaluate(LiczbaASTNode node) => node.Value;

        static int Evaluate(RejestrASTNode node)
        {
            switch (node.Token.TokenValue)
            {
                case "AX":
                    return procesor.AX;
                case "BX":
                    return procesor.BX;
                case "CX":
                    return procesor.CX;
                case "DX":
                    return procesor.DX;
                case "AL":
                    return procesor.AL;
                case "AH":
                    return procesor.AH;
                case "BL":
                    return procesor.BL;
                case "BH":
                    return procesor.BH;
                case "CL":
                    return procesor.CL;
                case "CH":
                    return procesor.CH;
                case "DL":
                    return procesor.DL;
                case "DH":
                    return procesor.DH;

                default: throw new Exception("Brak odpowiedniego rejestru");

            }
        }

        static void Evaluate(WyrazenieStosuASTNode node)
        {
            switch(node.Stos.Token.TokenValue)
            {
                case "PUSH":
                    switch(node.Rejestr.Token.TokenValue)
                    {
                        case "AX":
                            procesor.stos.Push(procesor.AX);
                            break;
                        case "BX":
                            procesor.stos.Push(procesor.BX);
                            break;
                        case "CX":
                            procesor.stos.Push(procesor.CX);
                            break;
                        case "DX":
                            procesor.stos.Push(procesor.DX);
                            break;
                        default: break;
                    }
                    break;
                case "POP":
                    if (procesor.stos.Count > 0)
                    {
                        switch (node.Rejestr.Token.TokenValue)
                        {
                            case "AX":
                                procesor.AX = procesor.stos.Pop();
                                break;
                            case "BX":
                                procesor.BX = procesor.stos.Pop();
                                break;
                            case "CX":
                                procesor.CX = procesor.stos.Pop();
                                break;
                            case "DX":
                                procesor.DX = procesor.stos.Pop();
                                break;
                            default: break;
                        }
                        break;
                    }
                    else
                    {
                        Parserr.wystapilBlad = true;
                        Parserr.tablicaBledow.Add($"W linii {nr + 1} \nNie można odczytać wartości ze stosu, ponieważ jest on pusty.");
                        procesor.ChangeLineForeColor(Color.Red, ex, nr);
                        break;
                    }
            }
            procesor.aktualizujRejestry();

        }

        static void Evaluate(WyrazeniePrzerwaniaASTNode node)
        {
            Interrupt.InterruptHandler(procesor, node.Przerwanie.Token.TokenValue, ex, nr);
        }

        static void Evaluate(WyrazenieASTNode node)
        {
            int result;


            switch (node.Rozkaz.Token.TokenValue)
            {
                case "ADD":
                    result = Evaluate(node.Argument1 as dynamic) + Evaluate(node.Argument2 as dynamic);
                    if (node.Argument1.Token.TokenValue.Contains("X") && result > 32767)
                    {
                        Parserr.wystapilBlad = true;
                        Parserr.tablicaBledow.Add($"W linii {nr + 1} \nNie można wykonać dodawania, ponieważ wynik przekracza pamięć rejestru.");
                        procesor.ChangeLineForeColor(Color.Red, ex, nr);
                        break;
                    } else if ((node.Argument1.Token.TokenValue.Contains("L") || node.Argument1.Token.TokenValue.Contains("H")) && result > 127)
                    {
                        Parserr.wystapilBlad = true;
                        Parserr.tablicaBledow.Add($"W linii {nr + 1} \nNie można wykonać dodawania, ponieważ wynik przekracza pamięć rejestru.");
                        procesor.ChangeLineForeColor(Color.Red, ex, nr);
                        break;
                    }
                    else {
                        switch (node.Argument1.Token.TokenValue)
                        {
                            case "AX":
                                procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF);
                                procesor.AX = (short)result;
                                procesor.AL = (sbyte)(procesor.AX >> 8);
                                procesor.AH = (sbyte)(procesor.AX & 0xFF);
                                break;
                            case "AL":
                                procesor.AL = (sbyte)result;
                                procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF);
                                break;
                            case "AH":
                                procesor.AH = (sbyte)result;
                                procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF);
                                break;
                            case "BX":
                                procesor.BX = (short)((procesor.BH << 8) | procesor.BL & 0xFF);
                                procesor.BX = (short)result;
                                procesor.BL = (sbyte)(procesor.BX >> 8);
                                procesor.BH = (sbyte)(procesor.BX & 0xFF);
                                break;
                            case "BL":
                                procesor.BL = (sbyte)(result);
                                procesor.BX = (short)((procesor.BH << 8) | procesor.BL & 0xFF);
                                break;
                            case "BH":
                                procesor.BH = (sbyte)result;
                                procesor.BX = (short)((procesor.BH << 8) | procesor.BL & 0xFF);
                                break;
                            case "CX":
                                procesor.CX = (short)((procesor.CH << 8) | procesor.CL & 0xFF);
                                procesor.CX = (short)result;
                                procesor.CL = (sbyte)(procesor.CX >> 8);
                                procesor.CH = (sbyte)(procesor.CX & 0xFF);
                                break;   
                            case "CL":   
                                procesor.CL = (sbyte)(result);
                                procesor.CX = (short)((procesor.CH << 8) | procesor.CL & 0xFF);
                                break;   
                            case "CH":   
                                procesor.CH = (sbyte)result;
                                procesor.CX = (short)((procesor.CH << 8) | procesor.CL & 0xFF);
                                break;
                            case "DX":
                                procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);
                                procesor.DX = (short)result;
                                procesor.DL = (sbyte)(procesor.DX >> 8);
                                procesor.DH = (sbyte)(procesor.DX & 0xFF);
                                break;
                            case "DL":
                                procesor.DL = (sbyte)(result);
                                procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);
                                break;
                            case "DH":
                                procesor.DH = (sbyte)result;
                                procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);
                                break;
                            default:
                                break;
                        }
                        procesor.aktualizujRejestry();
                    }
                    break;
                case "SUB":
                    result = Evaluate(node.Argument1 as dynamic) - Evaluate(node.Argument2 as dynamic);

                    if (node.Argument1.Token.TokenValue.Contains("X") && result < -32768)
                    {
                        Parserr.wystapilBlad = true;
                        Parserr.tablicaBledow.Add($"W linii {nr + 1} \nNie można wykonać odejmowania, ponieważ wynik przekracza pamięć rejestru.");
                        procesor.ChangeLineForeColor(Color.Red, ex, nr);
                        break;
                    }
                    else if ((node.Argument1.Token.TokenValue.Contains("L") || node.Argument1.Token.TokenValue.Contains("H")) && result < -128)
                    {
                        Parserr.wystapilBlad = true;
                        Parserr.tablicaBledow.Add($"W linii {nr + 1} \nNie można wykonać odejmowania, ponieważ wynik przekracza pamięć rejestru.");
                        procesor.ChangeLineForeColor(Color.Red, ex, nr);
                        break;
                    }


                    else
                    {
                        switch (node.Argument1.Token.TokenValue)
                        {
                            case "AX":
                                procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF);
                                procesor.AX = (short)result;
                                procesor.AL = (sbyte)(procesor.AX >> 8);
                                procesor.AH = (sbyte)(procesor.AX & 0xFF);
                                break;
                            case "AL":
                                procesor.AL = (sbyte)result;
                                procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF);
                                break;
                            case "AH":
                                procesor.AH = (sbyte)result;
                                procesor.AX = (short)(procesor.AH << 8 | procesor.AL & 0xFF);
                                break;
                            case "BX":
                                procesor.BX = (short)((procesor.BH << 8) | procesor.BL & 0xFF);
                                procesor.BX = (short)result;
                                procesor.BL = (sbyte)(procesor.BX >> 8);
                                procesor.BH = (sbyte)(procesor.BX & 0xFF);
                                break;
                            case "BL":
                                procesor.BL = (sbyte)(result);
                                procesor.BX = (short)((procesor.BH << 8) | procesor.BL & 0xFF);
                                break;
                            case "BH":
                                procesor.BH = (sbyte)result;
                                procesor.BX = (short)((procesor.BH << 8) | procesor.BL & 0xFF);
                                break;
                            case "CX":
                                procesor.CX = (short)((procesor.CH << 8) | procesor.CL & 0xFF);
                                procesor.CX = (short)result;
                                procesor.CL = (sbyte)(procesor.CX >> 8);
                                procesor.CH = (sbyte)(procesor.CX & 0xFF);
                                break;
                            case "CL":
                                procesor.CL = (sbyte)(result);
                                procesor.CX = (short)((procesor.CH << 8) | procesor.CL & 0xFF);
                                break;
                            case "CH":
                                procesor.CH = (sbyte)result;
                                procesor.CX = (short)((procesor.CH << 8) | procesor.CL & 0xFF);
                                break;
                            case "DX":
                                procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);
                                procesor.DX = (short)result;
                                procesor.DL = (sbyte)(procesor.DX >> 8);
                                procesor.DH = (sbyte)(procesor.DX & 0xFF);
                                break;
                            case "DL":
                                procesor.DL = (sbyte)(result);
                                procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);
                                break;
                            case "DH":
                                procesor.DH = (sbyte)result;
                                procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);
                                break;
                            default:
                                break;
                        }
                        procesor.aktualizujRejestry();
                    }
                    break;

                case "MOV":
                    result = Evaluate(node.Argument2 as dynamic);
                    switch (node.Argument1.Token.TokenValue)
                    {
                        case "AX":
                            procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF);
                            procesor.AX = (short)result;
                            procesor.AL = (sbyte)(procesor.AX >> 8);
                            procesor.AH = (sbyte)(procesor.AX & 0xFF);
                            break;
                        case "AL":
                            procesor.AL = (sbyte)result;
                            procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF);
                            break;
                        case "AH":
                            procesor.AH = (sbyte)result;
                            procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF);
                            break;
                        case "BX":
                            procesor.BX = (short)((procesor.BH << 8) | procesor.BL & 0xFF);
                            procesor.BX = (short)result;
                            procesor.BL = (sbyte)(procesor.BX >> 8);
                            procesor.BH = (sbyte)(procesor.BX & 0xFF);
                            break;
                        case "BL":
                            procesor.BL = (sbyte)(result);
                            procesor.BX = (short)((procesor.BH << 8) | procesor.BL & 0xFF);
                            break;
                        case "BH":
                            procesor.BH = (sbyte)result;
                            procesor.BX = (short)((procesor.BH << 8) | procesor.BL & 0xFF);
                            break;
                        case "CX":
                            procesor.CX = (short)((procesor.CH << 8) | procesor.CL & 0xFF);
                            procesor.CX = (short)result;
                            procesor.CL = (sbyte)(procesor.CX >> 8);
                            procesor.CH = (sbyte)(procesor.CX & 0xFF);
                            break;
                        case "CL":
                            procesor.CL = (sbyte)(result);
                            procesor.CX = (short)((procesor.CH << 8) | procesor.CL & 0xFF);
                            break;
                        case "CH":
                            procesor.CH = (sbyte)result;
                            procesor.CX = (short)((procesor.CH << 8) | procesor.CL & 0xFF);
                            break;
                        case "DX":
                            procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);
                            procesor.DX = (short)result;
                            procesor.DL = (sbyte)(procesor.DX >> 8);
                            procesor.DH = (sbyte)(procesor.DX & 0xFF);
                            break;
                        case "DL":
                            procesor.DL = (sbyte)(result);
                            procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);
                            break;
                        case "DH":
                            procesor.DH = (sbyte)result;
                            procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);
                            break;
                        default:
                            break;
/*


                        case "AX":
                            procesor.AX = (short)(Evaluate(node.Argument2 as dynamic));
                            break;
                        case "BX":
                            procesor.BX = (short)(Evaluate(node.Argument2 as dynamic));
                            break;
                        case "CX":
                            procesor.CX = (short)(Evaluate(node.Argument2 as dynamic));
                            break;
                        case "DX":
                            procesor.DX = (short)(Evaluate(node.Argument2 as dynamic));
                            break;
                        default:*/
                    }
                    procesor.aktualizujRejestry();
                    break;
                default: throw new Exception("Brak odpowiedniego rejestru");
            }
        }

        public static void ResetKompilator()
        {
            procesor.Blad = false;
            Parserr.wystapilBlad = false;
            Parserr.tablicaBledow.Clear();
        }

        public static void ShowMistakes()
        {
            MessageBox.Show(Parserr.tablicaBledow[0]);
        }

    }
}
