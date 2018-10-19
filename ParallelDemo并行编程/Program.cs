using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelDemo
{
    class Program
    {
        static void Main()
        {
            var sim = new SimpleRun();
            //目录示例
            // sim.FormalDirRun();
            //  sim.ParallelForDirRun();

            // MultiplyMatrices.Run();
            // sim.ParallelForEachDirRun();

            //sim.ParallelForThreadLocalVariables();

            //sim.ParallelForEachThreadLocalVariables();


            // sim.CancelParallel();

            // sim.HandleExceptionParallelLoop();

            sim.SpeedUpMicroParallelBody();
            Console.ReadLine();
        }
    }
}
