using Lang.LangCore;

namespace Lang.LangTool;

public static class Program{
    public static string UsageMessage = "Provide a file";
    public static async Task<int> Main(String[] args)
    {
        //TODO: make, steal or use something to handle args
        if(args.Count() < 1){
            System.Console.Error.WriteLine(UsageMessage);
            return 1;
        }

        using (FileStream fs = new FileStream(args[0], FileMode.Open))
        {
            System.Console.WriteLine("-------------------------------");
            System.Console.WriteLine("\nOpened File\n");
            var lexRes = Lexer.Parse(fs);
            System.Console.WriteLine("\nFinieshed Lexer parsing\nResult:\n");
            lexRes.Foreach(x => x.Print());
            var parseRes = Parser.Parse(lexRes);
            System.Console.WriteLine("\nFinieshed Parser parsing\nResult:\n");
            parseRes.Print();
        }

        return 0;
    }

    public static void Foreach<T>(this IEnumerable<T> list, Action<T> action) {
        foreach (var item in list) {action(item);}
    }
}