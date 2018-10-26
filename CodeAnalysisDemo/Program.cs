using System;
using Newtonsoft.Json;

namespace CodeAnalysisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //new SyntaxAnalysisDemo().Start();
            new SemanticAnalysisDemo().Start();
            Console.ReadLine();

            // Console.WriteLine(JsonConvert.SerializeObject(collector.models));
        }
    }
}
