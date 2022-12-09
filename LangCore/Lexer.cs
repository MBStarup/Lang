namespace Lang.LangCore;
public static class Lexer
{
    public static string Empty = " \n\r\t";
    public static string Letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string Digits = "1234567890";
    public static string Operators = "#+-*/=";
    public static string OpenBracket = "(";
    public static string CloseBracket = ")";
    public static string OpenCurlyBracket = "{";
    public static string CloseCurlyBracket = "}";
    public static string Comma = ",";
    public static string Colon = ":";
    public static string SemiColon = ";";

    public static Queue<Token> Parse(FileStream file) {

        Queue<Token> queue = new();
        using (var reader = new StreamReader(file)) {
            
            char c;

            while(!reader.EndOfStream) {

                c = reader.ReadC();
                while(true) {
                    if (Empty.Contains(c)) {
                        //System.Console.WriteLine(" Space ");
                        break;
                    } else if (Letters.Contains(c)) {
                        //System.Console.WriteLine(" Letter ");
                        string symbol = c.ToString();
                        while(Letters.Contains(reader.PeekC())) {
                            symbol += reader.ReadC();
                            //TODO: Catch file ends early
                        }
                        queue.Enqueue(("SYMBOL", symbol));
                        break;
                    } else if (Digits.Contains(c)) {
                        // System.Console.WriteLine(" Digit ");
                        string number = c.ToString();
                        while(Digits.Contains(reader.PeekC())) {
                            number += reader.ReadC();
                            //TODO: Catch file ends early
                            //TODO: Floats
                        }
                        queue.Enqueue(("NUMBER", number));
                        break;
                    } else if (Operators.Contains(c)) {
                        // System.Console.WriteLine(" Operator ");
                        queue.Enqueue(("OPERATOR", c.ToString()));
                        break;
                    } else if (OpenBracket.Contains(c)) {
                        // System.Console.WriteLine(" O_Bracket ");
                        queue.Enqueue(("(", ""));
                        break;
                    } else if (CloseBracket.Contains(c)) {
                        // System.Console.WriteLine(" C_Bracket ");
                        queue.Enqueue((")", ""));
                        break;
                    } else if (Comma.Contains(c)) {
                        // System.Console.WriteLine(" Comma ");
                        queue.Enqueue((",", ""));
                        break;
                    } else if (Colon.Contains(c)) {
                        // System.Console.WriteLine(" Colon ");
                        queue.Enqueue((":", ""));
                        break;
                    } else if (SemiColon.Contains(c)) {
                        // System.Console.WriteLine(" SemiColon ");
                        queue.Enqueue((";", ""));
                        break;
                    } else if (OpenCurlyBracket.Contains(c)) {
                        // System.Console.WriteLine(" O_Bracket ");
                        queue.Enqueue(("{", ""));
                        break;
                    } else if (CloseCurlyBracket.Contains(c)) {
                        // System.Console.WriteLine(" C_Bracket ");
                        queue.Enqueue(("}", ""));
                        break;
                    } else {
                        throw new Exception($"Could not parse letter \'{c}\' ({(int)c}), sorry");
                    }
                }
            }
        }
        return queue;
    }

    private static char ReadC(this StreamReader reader) {
        return (char)reader.Read();
    }
        private static char PeekC(this StreamReader reader) {
        return (char)reader.Peek();
    }
}

public class Token {
    public string Type;
    public string Value;
    public static implicit operator Token ((string, string) x) => new Token(){ Type = x.Item1, Value = x.Item2 };

    public void Print() {
        System.Console.WriteLine(($"(\"{Type}\", \"{Value}\")"));
    }
}


