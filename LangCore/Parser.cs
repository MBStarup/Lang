namespace Lang.LangCore;
public static class Parser
{
     public static void Parse(Stack<(string type, string value)> tokens) {
        Stack<object> stack = new();
        while (tokens.Count > 0) {
            var curr = tokens.Pop();

            while (true)
            {
                if (curr.type == "")
            }
        }
     } 
}