using System;

namespace AnalyticHierarchyProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            var decisionMatrix = new double[3, 3] 
            {
                { 1, 5, 9 },
                { 0.2, 1, 3 },
                { 0.1111, 0.3333, 1 }
            };

            int n = decisionMatrix.GetLength(0);

            var result = new Calculation(decisionMatrix, "wharton");

            Console.WriteLine("Normalized Matrix\n");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(result.NormalizedMatrix[i, j] + " ");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("\nPriority Values\n");

            for (int i = 0; i < n; i++)
            {
                Console.Write(result.PriorityVector[i] + " ");
            }

            Console.WriteLine("\n\nColumn Vector\n");

            for (int i = 0; i < n; i++)
            {
                Console.Write(result.ColumnVector[i] + " ");
            }

            Console.WriteLine("\n\nLambda Max: " + result.LambdaMax);
            Console.WriteLine("\nConsistency Index: " + result.ConsistencyIndex);
            Console.WriteLine("\nConsistency Ratio: " + result.ConsistencyRatio);
            Console.WriteLine("\nConsistency: " + result.IsConsistent);
            Console.WriteLine("");
        }
    }
}
