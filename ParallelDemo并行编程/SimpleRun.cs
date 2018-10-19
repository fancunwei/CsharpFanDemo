using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelDemo
{
    public class SimpleRun
    {
        private Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// 正常循环
        /// </summary>
        public void FormalDirRun()
        {
            long totalSize = 0;
            var dir = @"E:\LearnWall\orleans";//args[1];
            String[] files = Directory.GetFiles(dir);

            stopwatch.Restart();
            for (var i = 0; i < files.Length; i++)
            {
                FileInfo fi = new FileInfo(files[i]);
                long size = fi.Length;
                Interlocked.Add(ref totalSize, size);
                //  Thread.Sleep(1000);
            }
            stopwatch.Stop();
            Console.WriteLine($"FormalDirRun------{files.Length} files, {totalSize} bytes,time:{stopwatch.ElapsedMilliseconds},Dir:{dir}");
        }
        /// <summary>
        /// 并行循环
        /// </summary>
        public void ParallelForDirRun()
        {
            long totalSize = 0;
            var dir = @"E:\LearnWall\orleans";//args[1];
            String[] files = Directory.GetFiles(dir);

            stopwatch.Restart();
            Parallel.For(0, files.Length,
                         index =>
                         {
                             FileInfo fi = new FileInfo(files[index]);
                             long size = fi.Length;
                             Interlocked.Add(ref totalSize, size);
                             // Thread.Sleep(1000);
                         });
            stopwatch.Stop();
            Console.WriteLine($"ParallelForDirRun-{files.Length} files, {totalSize} bytes,time:{stopwatch.ElapsedMilliseconds},Dir:{dir}");
        }
        /// <summary>
        /// 
        /// </summary>

        public void ParallelForEachDirRun()
        {
            long totalSize = 0;
            var dir = @"E:\LearnWall\orleans";//args[1];
            String[] files = Directory.GetFiles(dir);
            stopwatch.Restart();
            Parallel.ForEach(files, (current) =>
            {
                FileInfo fi = new FileInfo(current);
                long size = fi.Length;
                Interlocked.Add(ref totalSize, size);
                Console.WriteLine($"name:{fi.Name}");
            });
            stopwatch.Stop();
            Console.WriteLine($"ParallelForEachDirRun-{files.Length} files, {totalSize} bytes,Time:{stopwatch.ElapsedMilliseconds}");
        }

        /// <summary>
        /// 
        /// </summary>
        public void ParallelForThreadLocalVariables()
        {
            int[] nums = Enumerable.Range(0, 1000000).ToArray();
            long total = 0;

            // Use type parameter to make subtotal a long, not an int
            Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
            {
                subtotal += nums[j];
                return subtotal;
            },
                (x) => Interlocked.Add(ref total, x)
            );

            Console.WriteLine("The total is {0:N0}", total);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ParallelForEachThreadLocalVariables()
        {
            int[] nums = Enumerable.Range(0, 1000000).ToArray();
            long total = 0;

            // First type parameter is the type of the source elements
            // Second type parameter is the type of the thread-local variable (partition subtotal)
            Parallel.ForEach<int, long>(nums, // source collection
                                        () => 0, // method to initialize the local variable
                                        (j, loop, subtotal) => // method invoked by the loop on each iteration
                                     {
                                         subtotal += j; //modify local variable
                                         return subtotal; // value to be passed to next iteration
                                     },
                                        // Method to be executed when each partition has completed.
                                        // finalResult is the final value of subtotal for a particular partition.
                                        (finalResult) => Interlocked.Add(ref total, finalResult)
                                        );

            Console.WriteLine("The total from Parallel.ForEach is {0:N0}", total);
        }

        /// <summary>
        /// 取消并行
        /// </summary>
        public void CancelParallel()
        {
            int[] nums = Enumerable.Range(0, 10000000).ToArray();
            CancellationTokenSource cts = new CancellationTokenSource();

            // Use ParallelOptions instance to store the CancellationToken
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;
            po.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
            Console.WriteLine("Press any key to start. Press 'c' to cancel.");
            Console.ReadKey();

            // Run a task so that we can cancel from another thread.
            Task.Factory.StartNew(() =>
            {
                var s = Console.ReadKey().KeyChar;
                if (s == 'c')
                    cts.Cancel();
                Console.WriteLine("press any key to exit111");
            });

            try
            {
                Parallel.ForEach(nums, po, (num) =>
                {
                    double d = Math.Sqrt(num);
                    Console.WriteLine("{0} on {1}", d, Thread.CurrentThread.ManagedThreadId);
                    po.CancellationToken.ThrowIfCancellationRequested();
                });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                cts.Dispose();
            }

            Console.ReadKey();
        }

        #region 捕获并行异常
        /// <summary>
        /// 捕获并行体内的异常
        /// </summary>
        public void HandleExceptionParallelLoop()
        {
            // Create some random data to process in parallel.
            // There is a good probability this data will cause some exceptions to be thrown.
            byte[] data = new byte[5000];
            Random r = new Random();
            r.NextBytes(data);

            try
            {
                ProcessDataInParallel(data);
            }
            catch (AggregateException ae)
            {
                var ignoredExceptions = new List<Exception>();
                // This is where you can choose which exceptions to handle.
                foreach (var ex in ae.Flatten().InnerExceptions)
                {
                    if (ex is ArgumentException)
                        Console.WriteLine(ex.Message);
                    else
                        ignoredExceptions.Add(ex);
                }
                if (ignoredExceptions.Count > 0) throw new AggregateException(ignoredExceptions);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        private  void ProcessDataInParallel(byte[] data)
        {
            // Use ConcurrentQueue to enable safe enqueueing from multiple threads.
            var exceptions = new ConcurrentQueue<Exception>();

            // Execute the complete loop and capture all exceptions.
            Parallel.ForEach(data, d =>
            {
                try
                {
                    // Cause a few exceptions, but not too many.
                    if (d < 3)
                        throw new ArgumentException($"Value is {d}. Value must be greater than or equal to 3.");
                    else
                        Console.Write(d + " ");
                }
                // Store the exception and continue with the loop.                    
                catch (Exception e)
                {
                    exceptions.Enqueue(e);
                }
            });
            Console.WriteLine();

            // Throw the exceptions here after the loop completes.
            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }
        #endregion

        /// <summary>
        /// 提速
        /// </summary>
        public void SpeedUpMicroParallelBody() {
            // Source must be array or IList.
            var source = Enumerable.Range(0, 100000).ToArray();

            // Partition the entire source array.
            var rangePartitioner = Partitioner.Create(0, source.Length);

            double[] results = new double[source.Length];

            // Loop over the partitions in parallel.
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                // Loop over each range element without a delegate invocation.
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = source[i] * Math.PI;
                }
            });

            Console.WriteLine("Operation complete. Print results? y/n");
            char input = Console.ReadKey().KeyChar;
            if (input == 'y' || input == 'Y')
            {
                foreach (double d in results)
                {
                    Console.Write("{0} ", d);
                }
            }
        }
    }
}
