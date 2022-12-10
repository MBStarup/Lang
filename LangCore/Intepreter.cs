namespace Lang.LangCore;

public static class Interpreter
{
    public static void Run(Expression expression) {
        switch (expression.Type)
        {
            case "Assign": 

                break;
            
            default:
                throw new Exception("Uhmmmmm");
                break;
        }
    }
}

class Item
{
    public Type Type;
    public object Value;
}