namespace Lang.LangCore;

public static class Interpreter
{
    public static Item Run(Expression expression, DiveableDictStack<string, Item> scope) {
        switch (expression.Type)
        {
            case "Func": 
                {
                    return new() 
                    { 
                        Type = typeof(LangFunc), 
                        Value = new LangFunc() 
                        {
                            ArgNames = expression.Identifiers, 
                            Func = scp => Run(expression.Values[0], scp), 
                            Scope = scope.Copy<string, Item>()
                        }
                    };
                }

            case "Assign":
                {
                    if (expression.Values.Count != 1) throw new Exception($"Assignment expected one expression, found {expression.Values.Count}");
                    Item item = Run(expression.Values[0], scope);
                    if (item.Type == typeof(LangFunc)) (item.Value as LangFunc).Scope.Insert(expression.Identifiers[0], item); // Recursion
                    scope.Insert(expression.Identifiers[0], item);
                    return item; //* Assigments evaluate to their assigned value
                }
            
            case "Int":
                {
                    if (expression.Identifiers.Count < 1) throw new Exception("Int with no identifier???");
                    try {
                        var val = new Item() { Type = typeof(int) , Value = int.Parse(expression.Identifiers[0]) };
                        return val;
                    }
                    catch {
                        throw new Exception($"Value {expression.Identifiers[0]} could not be parses as an int.");
                    }
                }
            
            case "Symbol":
                {
                    if (expression.Identifiers.Count < 1) throw new Exception("Symbol with no identifier???");
                    var val = scope.Seek(expression.Identifiers[0]);
                    if (val == null) throw new Exception($"Symbol with identifier {expression.Identifiers[0]} could not be found.");
                    return val;
                }
            
            case "Call":
                {
                    var function = scope.Seek(expression.Identifiers[0]);
                    if (function == null) throw new Exception($"Symbol with identifier {expression.Identifiers[0]} could not be found.");
                    if (function.Type != typeof(LangFunc)) throw new Exception($"Can only call functions, tried to call {expression.Identifiers[0]}, which is a {function.Type}.");
                    var langFunc = (LangFunc)function.Value;
                    
                    
                    if (expression.Values.Count != langFunc.ArgNames.Count) throw new Exception($"Function {expression.Identifiers[0]} expected {langFunc.ArgNames.Count} arguments, but got {expression.Values.Count}");
                    

                    langFunc.Scope.Stack(); //* Add a new scope for the function arguments, and insert the arguments
                    for (int i = 0; i < langFunc.ArgNames.Count; i++)
                    {
                        langFunc.Scope.Insert(langFunc.ArgNames[i], Run(expression.Values[i], scope) );
                    }
                    var val = langFunc.Func.Invoke(langFunc.Scope); //* Evaluate the function call
                    langFunc.Scope.Pop(); //* Remove function arguments scope before return
                    return val;
                }
            
            case "MultiExpr":
                {
                    if (expression.Values.Count < 1) throw new Exception($"Empty MultiExpr");
                    scope.Stack();
                    var final = Run(expression.Values[0], scope);
                    for (int i = 1; i < expression.Values.Count; i++)
                    {
                        final = Run(expression.Values[i], scope);
                    }
                    scope.Pop();
                    return final;
                }

            case "BiExpr":
                switch (expression.Identifiers[0])
                {
                    case "+":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            var a = Run(expression.Values[0], scope);
                            var b = Run(expression.Values[1], scope);

                            if (a.Type != typeof(int)) throw new Exception($"Argument 1 of operator {expression.Identifiers[0]} must evaluate to a number");
                            if (b.Type != typeof(int)) throw new Exception($"Argument 2 of operator {expression.Identifiers[0]} must evaluate to a number");
                            return new Item(){Type = typeof(int), Value = (int)a.Value + (int)b.Value};
                        }

                    case "-":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            var a = Run(expression.Values[0], scope);
                            var b = Run(expression.Values[1], scope);

                            if (a.Type != typeof(int)) throw new Exception($"Argument 1 of operator {expression.Identifiers[0]} must evaluate to a number");
                            if (b.Type != typeof(int)) throw new Exception($"Argument 2 of operator {expression.Identifiers[0]} must evaluate to a number");
                            return new Item(){Type = typeof(int), Value = (int)a.Value - (int)b.Value};
                        }

                    case "*":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            var a = Run(expression.Values[0], scope);
                            var b = Run(expression.Values[1], scope);

                            if (a.Type != typeof(int)) throw new Exception($"Argument 1 of operator {expression.Identifiers[0]} must evaluate to a number");
                            if (b.Type != typeof(int)) throw new Exception($"Argument 2 of operator {expression.Identifiers[0]} must evaluate to a number");
                            return new Item(){Type = typeof(int), Value = (int)a.Value * (int)b.Value};
                        }
                    
                    case "/":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            var a = Run(expression.Values[0], scope);
                            var b = Run(expression.Values[1], scope);

                            if (a.Type != typeof(int)) throw new Exception($"Argument 1 of operator {expression.Identifiers[0]} must evaluate to a number");
                            if (b.Type != typeof(int)) throw new Exception($"Argument 2 of operator {expression.Identifiers[0]} must evaluate to a number");
                            return new Item(){Type = typeof(int), Value = (int)a.Value / (int)b.Value};
                        }

                    case ">":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            var a = Run(expression.Values[0], scope);
                            var b = Run(expression.Values[1], scope);

                            if (a.Type != typeof(int)) throw new Exception($"Argument 1 of operator {expression.Identifiers[0]} must evaluate to a number");
                            if (b.Type != typeof(int)) throw new Exception($"Argument 2 of operator {expression.Identifiers[0]} must evaluate to a number");
                            return new Item(){Type = typeof(int), Value = (int)a.Value > (int)b.Value ? 1 : 0};
                        }

                    case "<":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            var a = Run(expression.Values[0], scope);
                            var b = Run(expression.Values[1], scope);

                            if (a.Type != typeof(int)) throw new Exception($"Argument 1 of operator {expression.Identifiers[0]} must evaluate to a number");
                            if (b.Type != typeof(int)) throw new Exception($"Argument 2 of operator {expression.Identifiers[0]} must evaluate to a number");
                            return new Item(){Type = typeof(int), Value = (int)a.Value < (int)b.Value ? 1 : 0};
                        }

                    case "=":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            var a = Run(expression.Values[0], scope);
                            var b = Run(expression.Values[1], scope);

                            if (a.Type != typeof(int)) throw new Exception($"Argument 1 of operator {expression.Identifiers[0]} must evaluate to a number");
                            if (b.Type != typeof(int)) throw new Exception($"Argument 2 of operator {expression.Identifiers[0]} must evaluate to a number");
                            return new Item(){Type = typeof(int), Value = (int)a.Value == (int)b.Value ? 1 : 0};
                        }
                    
                    case "&":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            return new Item(){Type = typeof(int), Value = ((int)(Run(expression.Values[0], scope)).Value == 1) && ((int)(Run(expression.Values[1], scope)).Value == 1) ? 1 : 0}; //* Automatically has shortcircuit eval since c# does
                        }
                    
                    case "|":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            return new Item(){Type = typeof(int), Value = ((int)(Run(expression.Values[0], scope)).Value == 1) || ((int)(Run(expression.Values[1], scope)).Value == 1) ? 1 : 0}; //* Automatically has shortcircuit eval since c# does
                        }

                    default:
                        throw new Exception($"Unknown operator {expression.Identifiers[0]}");
                }

            default:
                throw new Exception("Uhmmmmm");
        }
    }
}

public class Item
{
    public Type Type;
    public object Value;

    public override string ToString()
    {
            return $"I | {Type} : {Value}";
    }         
}

public class LangFunc 
{
    public DiveableDictStack<string, Item> Scope;
    public Func<DiveableDictStack<string, Item>, Item> Func;
    public List<string> ArgNames;
}
public class DiveableDictStack<TKey, TValue> where TKey : notnull
{
    Stack<Dictionary<TKey, TValue>> stack = new();

    public void Stack() {
        stack.Push(new Dictionary<TKey, TValue>());
    }

    public void Pop() {
        stack.Pop();
    }

    public TValue Seek(TKey key) {
        foreach (var scope in stack)
        {
            if (scope.ContainsKey(key)) return scope[key];
        }

        return default(TValue);
    }

    public void Insert(TKey key, TValue value) {
        stack.Peek()[key] = value;
    }

    public DiveableDictStack<TKey, TValue> Copy<TKey, TValue>() where TKey : notnull 
    {
         DiveableDictStack<TKey, TValue> result = new DiveableDictStack<TKey, TValue>();
         Stack<Dictionary<TKey, TValue>> reverseStack = new Stack<Dictionary<TKey, TValue>>();
         foreach (var dictionary in stack)
         {
            reverseStack.Push((Dictionary<TKey, TValue>)(object)dictionary); // TODO: somethign less stupid that this LULE
         }

         foreach (var dictionary in reverseStack)
         {
            result.Stack();
            foreach (var item in dictionary)
            {
                result.Insert(item.Key, item.Value);
            }
         }

         return result;
    }
}