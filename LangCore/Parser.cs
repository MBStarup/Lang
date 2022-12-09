namespace Lang.LangCore;
public static class Parser
{
     public static Expression Parse(Queue<Token> tokens, Token prev) {
        Expression result = new();
        while (tokens.Count > 0) {

            var curr = tokens.Dequeue();

            if (prev.Type == "SYMBOL"){

                if (curr.Type == ":"){ //* Case: assignment
                    System.Console.WriteLine("START ASSIGN");
                    result.Type = "Assign";
                    result.Identifier = prev.Value;
                    result.Values = new[] { Parse(tokens, curr) };
                    System.Console.WriteLine("END ASSIGN");
                }
            } else if (prev.Type == ":") {
                // TODO: check if the next one is a math operator
                if (curr.Type == "NUMBER") { //* Case: assign to int literal
                    System.Console.WriteLine("START ASSIGN NUMBER");
                    result.Type = "Int";
                    result.Identifier = curr.Value;
                    System.Console.WriteLine("END ASSIGN NUMBER");

                } else if (curr.Type == "{") { //* Case: assign to expression value
                    System.Console.WriteLine("START ASSIGN {}");
                    var values = new List<Expression>();
                    while (curr.Type != "}") {
                        values.Add(Parse(tokens, curr));
                        curr = tokens.Dequeue();
                    }
                    result.Values = values.ToArray();
                    System.Console.WriteLine("START ASSIGN {}");
                }
            } else if (prev.Type == "") { //* First round
                System.Console.WriteLine("FIRST");
                result = Parse(tokens, curr);
                System.Console.WriteLine("END");
            } else {
                throw new Exception("BIG ERROR #1");
            }
            return result;
        }
        throw new Exception("BIG ERROR #2");
     } 
} 


public class Expression {
    public string Type;
    public string Identifier;
    public Expression[] Values;

    public void Print(int indentAmount = 0)
    {
        var indent = new string(' ', indentAmount);
        System.Console.WriteLine($"{indent}(");
        System.Console.WriteLine($"{indent}\"{Type}\",");
        System.Console.WriteLine($"{indent}\"{Identifier}\",");
        for (int i = 0; i < Values?.Length; i++)
        {
            Values[i].Print(indentAmount + 2);
        }
        System.Console.WriteLine($"{indent})");
    }
}
