namespace Lang.LangCore;
public static class Parser
{
     public static Expression Parse(Queue<Token> tokens) {

        Expression result = new();
        if (tokens.Count > 0) {
            var token = tokens.Dequeue();

            if (token.Type == "SYMBOL"){
                
                var symbol = token.Value;

                if (tokens.Count > 0 && tokens.Peek().Type == ":"){ //* Case: assignment
                
                    token = tokens.Dequeue();
                    result.Type = "Assign";
                    result.Identifiers = new() { symbol };
                    result.Values = new() { Parse(tokens) }; //* One statement assignment only
                    return result;
                
                } else if (tokens.Count > 0 && tokens.Peek().Type == "OPERATOR") {

                    token = tokens.Dequeue();
                    result.Type = "BiExpr";
                    result.Identifiers = new() { token.Value };
                    result.Values = new() { new Expression() {Type = "Symbol", Identifiers = new() { symbol } }, Parse(tokens) }; //* No operator preceedence
                    return result;
                
                } else if (tokens.Count > 0 && tokens.Peek().Type == "(") {
                
                    token = tokens.Dequeue();
                    result.Type = "Call";
                    result.Identifiers = new() { symbol };
                    result.Values = new();
                    while (tokens.Count > 0 && tokens.Peek().Type != ")") {
                        result.Values.Add(Parse(tokens)); 
                    }
                    token = tokens.Dequeue(); //* Remove the ")"

                    if (tokens.Count > 0 && tokens.Peek().Type == "OPERATOR") {
                    
                        Expression opResult = new();
                        token = tokens.Dequeue();
                        opResult.Type = "BiExpr";
                        opResult.Identifiers = new() { token.Value };
                        opResult.Values = new() { result, Parse(tokens) }; //* No operator preceedence
                        return opResult;
                    }



                    return result;
                
                } else {
                
                    return new Expression() {Type = "Symbol", Identifiers = new() { symbol } }; 

                }
            
            } else if (token.Type == "NUMBER") { //* Case: assign to int literal
      
                var number = token.Value;

                if (tokens.Count > 0 && tokens.Peek().Type == "OPERATOR") {

                    token = tokens.Dequeue();
                    result.Type = "BiExpr";
                    result.Identifiers = new() { token.Value };
                    result.Values = new() { new Expression() {Type = "Int", Identifiers = new() { number } }, Parse(tokens) }; //* No operator preceedence
                    return result;
                
                } else {
                
                    return new Expression() {Type = "Int", Identifiers = new() { number } }; 
                                    
                }
            
            // } else if (token.Type == "(") { //* Case: expression

            //     while (tokens.Peek() != ")") {

            //     }

                // if (tokens.Count > 0 && tokens.Peek().Type == "OPERATOR") {
                    
                //     Expression opResult = new();
                //     token = tokens.Dequeue();
                //     opResult.Type = "BiExpr";
                //     opResult.Identifiers = new() { token.Value };
                //     opResult.Values = new() { result, Parse(tokens) }; //* No operator preceedence
                //     return opResult;
                // }

            
            } else if (token.Type == "{") { //* Case: multi expression
            
                result.Type = "MultiExpr";
                result.Values = new();
                while (tokens.Count > 0 && tokens.Peek().Type != "}") {
                    result.Values.Add(Parse(tokens));
                }
                tokens.Dequeue(); //* Remove the "}"

                if (tokens.Count > 0 && tokens.Peek().Type == "OPERATOR") {
                    
                    Expression opResult = new();
                    token = tokens.Dequeue();
                    opResult.Type = "BiExpr";
                    opResult.Identifiers = new() { token.Value };
                    opResult.Values = new() { result, Parse(tokens) }; //* No operator preceedence
                    return opResult;
                }

                return result;

            } else if (token.Type == ";") { //* Case: Function

                result.Type = "Func";
                result.Identifiers = new();
                while (tokens.Count > 0 && tokens.Peek().Type != ";") {
                    var next = Parse(tokens);
                    
                    if (next.Type != "Symbol") throw new Exception("Function params must be symbols");
                    if (next.Identifiers.Count != 1) throw new Exception("Symbols must have exactly one identidier");
                    
                    result.Identifiers.Add(next.Identifiers[0]);
                }
                tokens.Dequeue(); //* Remove the ";"

                result.Values = new() { Parse(tokens) };

                return result;

            } else {

                throw new Exception($"Unexpected token : (\"{token.Type}\", \"{token.Value}\") encountered!");
            
            }
        }
        else {
            throw new Exception($"Ran out of tokens");
        }
        throw new Exception($"Missing return");
     } 
} 


public class Expression {
    public string Type;
    public List<string> Identifiers;
    public List<Expression> Values;

    public void Print(int indentAmount = 0)
    {
        var indent = new string(' ', indentAmount);
        
        System.Console.WriteLine($"{indent}(");
        System.Console.WriteLine($"{indent}Type: \"{Type}\",");
        
        for (int i = 0; i < Identifiers?.Count; i++) {
            System.Console.WriteLine($"{indent}Identifier: \"{Identifiers[i]}\",");
        }

        for (int i = 0; i < Values?.Count; i++) {
            Values[i].Print(indentAmount + 2);
        }
        
        System.Console.WriteLine($"{indent})");
    }

    public override string ToString()
    {
        return Type;
    }
}
