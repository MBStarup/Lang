namespace Lang.LangCore;

public static class Interpreter
{
    public static Item Run(Expression expression, DiveableDictStack<string, Item> scope) {
        switch (expression.Type)
        {
            case "Func": 
                {
                    return new() { Type = typeof(LangFunc), Value = new LangFunc() {ArgNames = expression.Identifiers, Func = scp => Run(expression.Values[0], scp)}};
                }
                break;

            case "Assign":
                {
                    if (expression.Values.Count != 1) throw new Exception($"Assignment expected one expression, found {expression.Values.Count}");
                    Item item = Run(expression.Values[0], scope);
                    scope.Insert(expression.Identifiers[0], item);
                    return item; //* Assigments evaluate to their assigned value
                }
                break;
            
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
                break;
            
            case "Symbol":
                {
                    if (expression.Identifiers.Count < 1) throw new Exception("Symbol with no identifier???");
                    var val = scope.Seek(expression.Identifiers[0]);
                    if (val == null) throw new Exception($"Symbol with identifier {expression.Identifiers[0]} could not be found.");
                    return val;
                }
                break;
            
            case "Call":
                {
                    var function = scope.Seek(expression.Identifiers[0]);
                    if (function == null) throw new Exception($"Symbol with identifier {expression.Identifiers[0]} could not be found.");
                    if (function.Type != typeof(LangFunc)) throw new Exception("Can only call functions");
                    var langFunc = (LangFunc)function.Value;
                    
                    
                    if (expression.Values.Count != langFunc.ArgNames.Count) throw new Exception($"Function {expression.Identifiers[0]} expected {langFunc.ArgNames.Count} arguments, but got {expression.Values.Count}");
                    

                    scope.Stack(); //* Add a new scope for the function, and insert the arguments
                    for (int i = 0; i < langFunc.ArgNames.Count; i++)
                    {
                        scope.Insert(langFunc.ArgNames[i], Run(expression.Values[i], scope) );
                    }
                    var val = langFunc.Func.Invoke(scope); //* Evaluate the function call
                    scope.Pop(); //* Remove function from scope before return
                    return val;
                }
                break;
            
            case "MultiExpr":
                if (expression.Values.Count < 1) throw new Exception($"Empty MultiExpr");
                scope.Stack();
                var final = Run(expression.Values[0], scope);
                for (int i = 1; i < expression.Values.Count; i++)
                {
                    final = Run(expression.Values[i], scope);
                }
                scope.Pop();
                return final;
                break;

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
                        break;

                    case "-":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            var a = Run(expression.Values[0], scope);
                            var b = Run(expression.Values[1], scope);

                            if (a.Type != typeof(int)) throw new Exception($"Argument 1 of operator {expression.Identifiers[0]} must evaluate to a number");
                            if (b.Type != typeof(int)) throw new Exception($"Argument 2 of operator {expression.Identifiers[0]} must evaluate to a number");
                            return new Item(){Type = typeof(int), Value = (int)a.Value - (int)b.Value};
                        }
                        break;

                    case "*":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            var a = Run(expression.Values[0], scope);
                            var b = Run(expression.Values[1], scope);

                            if (a.Type != typeof(int)) throw new Exception($"Argument 1 of operator {expression.Identifiers[0]} must evaluate to a number");
                            if (b.Type != typeof(int)) throw new Exception($"Argument 2 of operator {expression.Identifiers[0]} must evaluate to a number");
                            return new Item(){Type = typeof(int), Value = (int)a.Value * (int)b.Value};
                        }
                        break;

                    
                    case "/":
                        {
                            if (expression.Values.Count != 2) throw new Exception($"Operator {expression.Identifiers[0]} expected 2 arguments, but got {expression.Values.Count}");
                            var a = Run(expression.Values[0], scope);
                            var b = Run(expression.Values[1], scope);

                            if (a.Type != typeof(int)) throw new Exception($"Argument 1 of operator {expression.Identifiers[0]} must evaluate to a number");
                            if (b.Type != typeof(int)) throw new Exception($"Argument 2 of operator {expression.Identifiers[0]} must evaluate to a number");
                            return new Item(){Type = typeof(int), Value = (int)a.Value / (int)b.Value};
                        }
                        break;

                    default:
                        throw new Exception($"Unknown operator {expression.Identifiers[0]}");
                        break;
                }
                break;

            default:
                throw new Exception("Uhmmmmm");
                break;
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
}