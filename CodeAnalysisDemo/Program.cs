using System;
using Newtonsoft.Json;

namespace CodeAnalysisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var code = @"using System;

                        namespace UsingCollectorCS
                        {
                            class Program
                            {
                                static void Main(string[] args)
                                {
                                    Console.WriteLine(""Hello World"");
                                }
                            }

                            class Student
                            {
                                public string Name { get; set; }
                            }
                        }";

            var tree = new AnalysisDemo().GetRoot(code);



            var code2 =
            @"using System;
                        using System.Collections.Generic;
                        using System.Linq;
                        using System.Text;
                        using Microsoft.CodeAnalysis;
                        using Microsoft.CodeAnalysis.CSharp;


            namespace TopLevel
                {
                    using Microsoft;
                    using System.ComponentModel;

                    namespace Child1
                    {
                        using Microsoft.Win32;
                        using System.Runtime.InteropServices;

                        class Foo {  
                            public string FChildA{get;set;}
                            public string FChildB{get;set;}
                        }
                    }

                    namespace Child2
                    {
                        using System.CodeDom;
                        using Microsoft.CSharp;

                        class Bar {
                             public string BChildA{get;set;}
                             public string BChildB{get;set;}
                        }
                    }
                }";

            var collector = new AnalysisDemo().GetCollector(code2);

            foreach (var directive in collector.Usings)
            {
                Console.WriteLine($"Name:{directive.Name}");
            }
            Console.WriteLine($"models:{JsonConvert.SerializeObject(collector.models)}");

            Console.ReadLine();

            // Console.WriteLine(JsonConvert.SerializeObject(collector.models));
        }
    }
}
