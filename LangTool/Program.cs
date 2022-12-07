using System;

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
            System.Console.WriteLine("Opened File");
        }

        return 0;
    }

}