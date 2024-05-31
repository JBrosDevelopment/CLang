﻿// command line to release
// dotnet publish -o "publish" Norma.csproj -r win-x64 -c Release -p:PublishReadyToRun=true -p:PublishSingleFile=true --self-contained

namespace NormaLang 
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No file specified. Please use 'Open with' in the file explorer or pass the file as an argument in the command prompt to run this file.");
                return;
            }

            string filePath = args[0];
            string contents = "";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"The file '{filePath}' does not exist.");
                return;
            }

            try
            {
                contents = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }

            //System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            var code = contents;
            var lexerLines = Lexer.Tokenizer(code);
            var parserLines = Parser.Parse(lexerLines);
            Execution.Execute(parserLines);

            // To time how fast Norm is
            //Console.WriteLine("------------------\nMiliseconds: " + sw.ElapsedMilliseconds.ToString());
        }
    }
}