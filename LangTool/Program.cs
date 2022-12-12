using System.Text;
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
#if !DEBUG
try {
#endif

        using (FileStream fs = new FileStream(args[0], FileMode.Open))
        {
#if DEBUG
            System.Console.WriteLine("-------------------------------");
            System.Console.WriteLine("\nOpened File\n");
#endif
            var lexRes = Lexer.Parse(fs);
#if DEBUG
            System.Console.WriteLine("\nFinieshed Lexer parsing\nResult:\n");
            lexRes.Foreach(x => x.Print());
#endif
            var parseRes = Parser.Parse(lexRes);
#if DEBUG
            System.Console.WriteLine("\nFinieshed Parser parsing\nResult:\n");
            parseRes.Print();
#endif
            var globalScope = new DiveableDictStack<string, Item>();
            globalScope.Stack();

            //* Magic functions provided to the language by the interpreter

            globalScope.Insert("print", new Item() { 
                Type = typeof(LangFunc), 
                Value = new LangFunc () {
                    Scope = globalScope,
                    ArgNames = new() {"x"}, 
                    Func = (DiveableDictStack<string, Item> s) => {
                        System.Console.Write(Encoding.ASCII.GetString(new byte[] { (byte)(int)s.Seek("x").Value }));
                        return new Item() { Type = typeof(int), Value = 1 };
                    }
                }
            });

            //* End of magic functions provided to the language by the interpreter

#if DEBUG
            System.Console.WriteLine("\n-----WELCOME-----\n");
#endif
            var result = Interpreter.Run(parseRes, globalScope, new()).Value;
            try {
                var exitCode = (int)result;
                #if DEBUG
                System.Console.WriteLine("\n-------END-------\n");
                System.Console.WriteLine($"\n\nProgram finsished with exit code: {exitCode}");
                #endif
            } catch {
                throw new Exception("Bad exit code");
            }
        }
#if !DEBUG
} catch (Exception e) {
    Console.WriteLine($"\n ERROR: {e.Message}\n");
    return 1;
    //throw new Exception(e.Message); //Stack trace remover (it's not usefull, and quite annoying sometimes)
}
#endif

        return 0;
    }

    public static void Foreach<T>(this IEnumerable<T> list, Action<T> action) {
        foreach (var item in list) {action(item);}
    }
}