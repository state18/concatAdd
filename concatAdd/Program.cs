using System;
using System.Diagnostics;

namespace concatAdd {
    class Program {
        static void Main(string[] args) {
            // Prepare test array.
            int n = Int32.Parse(args[0]);
            Random rand = new Random();

            int[] a = new int[n];

            for (int i = 0; i < n; i++) {
                // a[i] = i + 1;
                a[i] = rand.Next(1, n);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            long sumN = computeSumNaive(a);
            stopwatch.Stop();
            Console.WriteLine(String.Format("Naive Elapsed time: {0} ms", stopwatch.ElapsedMilliseconds));

            stopwatch.Restart();
            long sumF = computeSumFast(a);
            stopwatch.Stop();
            Console.WriteLine(String.Format("Fast Elapsed time: {0} ms", stopwatch.ElapsedMilliseconds));

            if (sumN == sumF) {
                Console.WriteLine("Naive and Fast are equal :)");
            } else {
                Console.WriteLine("ERROR: Naive and Fast not equal!");
            }

            Console.ReadKey();
        }

        static long computeSumNaive(int[] a) {
            long sum = 0;
            for (int i = 0; i < a.Length; i++) {
                for (int j = 0; j < a.Length; j++) {
                    sum += Int64.Parse(String.Format("{0}{1}", a[i], a[j]));
                }
            }
            return sum;
        }

        static long computeSumFast(int[] a) {

            long sum = 0;

            // Build numDigit to occurrence histogram.
            int maxDigits = (int)Math.Floor(Math.Log10(a.Length)) + 1;

            int[] numHist = new int[maxDigits];

            for (int i = 0; i < a.Length; i++) {
                numHist[(int) Math.Floor(Math.Log10(a[i]))]++;
            }

            for (int i = 0; i < a.Length; i++) {
                for (int j = 0; j < maxDigits; j++) {
                    sum += (long) (Math.Pow(10, j + 1) * a[i] * numHist[j]);
                }

                sum += a[i] * a.Length;
            }

            return sum;
        }
    }
}
