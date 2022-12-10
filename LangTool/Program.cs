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

            globalScope.Insert("if", new Item() { 
                Type = typeof(LangFunc), 
                Value = new LangFunc () {
                    Scope = globalScope,
                    ArgNames = new() {"bool", "function"}, 
                    Func = (DiveableDictStack<string, Item> s) => {
                        if ((int)s.Seek("bool").Value == 1) {
                            var func = (LangFunc)s.Seek("function").Value;
                            return func.Func.Invoke(func.Scope);
                        }
                        return new Item() { Type = typeof(int), Value = 0 };
                    }
                }
            });

            //* End of magic functions provided to the language by the interpreter

            System.Console.WriteLine("\n-----WELCOME-----\n");
            var exitCode = (int)Interpreter.Run(parseRes, globalScope).Value;
            System.Console.WriteLine("\n-------END-------\n");
            System.Console.WriteLine($"\n\nProgram finsished with exit code: {exitCode}");
        }

        return 0;
    }

    public static void Foreach<T>(this IEnumerable<T> list, Action<T> action) {
        foreach (var item in list) {action(item);}
    }
}