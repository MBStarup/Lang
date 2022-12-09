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

    public static Stack<(string type, string value)> Parse(FileStream file) {

        Stack<(string type, string value)> stack = new();
        
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
                        stack.Push(("SYMBOL", symbol));
                        System.Console.WriteLine($"(\"SYMBOL\", \"{symbol}\")");
                        break;
                    } else if (Digits.Contains(c)) {
                        // System.Console.WriteLine(" Digit ");
                        string number = c.ToString();
                        while(Digits.Contains(reader.PeekC())) {
                            number += reader.ReadC();
                            //TODO: Catch file ends early
                            //TODO: Floats
                        }
                        stack.Push(("NUMBER", number));
                        System.Console.WriteLine($"(\"NUMBER\", \"{number}\")");
                        break;
                    } else if (Operators.Contains(c)) {
                        // System.Console.WriteLine(" Operator ");
                        stack.Push(("OPERATOR", c.ToString()));
                        System.Console.WriteLine($"(\"OPERATOR\", \"{c}\")");
                        break;
                    } else if (OpenBracket.Contains(c)) {
                        // System.Console.WriteLine(" O_Bracket ");
                        stack.Push(("(", ""));
                        System.Console.WriteLine($"(\"(\", \"\")");
                        break;
                    } else if (CloseBracket.Contains(c)) {
                        // System.Console.WriteLine(" C_Bracket ");
                        stack.Push((")", ""));
                        System.Console.WriteLine($"(\")\", \"\")");
                        break;
                    } else if (Comma.Contains(c)) {
                        // System.Console.WriteLine(" Comma ");
                        stack.Push((",", ""));
                        System.Console.WriteLine($"(\",\", \"\")");
                        break;
                    } else if (Colon.Contains(c)) {
                        // System.Console.WriteLine(" Colon ");
                        stack.Push((":", ""));
                        System.Console.WriteLine($"(\":\", \"\")");
                        break;
                    } else if (SemiColon.Contains(c)) {
                        // System.Console.WriteLine(" SemiColon ");
                        stack.Push((";", ""));
                        System.Console.WriteLine($"(\";\", \"\")");
                        break;
                    } else if (OpenCurlyBracket.Contains(c)) {
                        // System.Console.WriteLine(" O_Bracket ");
                        stack.Push(("{", ""));
                        System.Console.WriteLine($"(\"{{\", \"\")");
                        break;
                    } else if (CloseCurlyBracket.Contains(c)) {
                        // System.Console.WriteLine(" C_Bracket ");
                        stack.Push(("}", ""));
                        System.Console.WriteLine($"(\"}}\", \"\")");
                        break;
                    } else {
                        throw new Exception($"Could not parse letter \'{c}\' ({(int)c}), sorry");
                    }
                }
            }
        }
        return stack;
    }

    private static char ReadC(this StreamReader reader) {
        return (char)reader.Read();
    }
        private static char PeekC(this StreamReader reader) {
        return (char)reader.Peek();
    }
}


