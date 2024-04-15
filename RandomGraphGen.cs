using System.Globalization;

namespace Discrete;
using System;
using System.Diagnostics;


public class RandomGraphGen
{
    public static Dictionary<int, List<int>> Connections { get; set; }

    public static int Size { get; set; } // graph size

    // значить так, поїхали, (*ня*)
    private int AddPoint(int x, int y) // class for points to have them structurised
    {
        if (Connections.ContainsKey(x)) // check if the dictionary contains the key
        {
            if (Connections[x].Contains(y)) // check if the value is already in the list
            {
                return 1; // return 1 if the value is already in the list
            }
            else
            {
                Connections[x].Add(y); // add the value to the list
                return 0; // return 0 if the value was added
            }
        }
        else
        {
            Connections.Add(x, new List<int> { y }); // add the key and the value to the dictionary
            return 0; // return 0 if the value was added
        }
    }

    public void GenerateGraph(int size, float density) // generate a random graph
    {
        Size = size;
        Connections = new Dictionary<int, List<int>>(); // create a new dictionary

        Random rand = new Random(); // create a new random object
        int edgeCount = (size * (size - 1)) / 2; // calculate the maximum number of edges
        int i = 0; // create an iterator
        for (int f = 1; f <= size; f++) // fill the dictionary with points
        {
            Connections.Add(f, new List<int>());
        }

        while (i < edgeCount) // add random edges
        {
            int x = rand.Next(0, size);
            int y = rand.Next(0, size);
            if (AddPoint(x, y) == 0)
            {
                i++;
            }
        }
    }

    public static long[,] MatrixVisualiser() //adjacency matrix mechanism
    {
        int size = Size; // get the size of the graph
        // create a new matrix
        long[,] matrix = new long[size, size];

        for (int i = 0; i < size; i++) // fill the matrix with default values
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = 0; // 0 means no connection
            }
        }

        foreach (var item in RandomGraphGen.Connections)
        {
            foreach (var value in item.Value)
            {
                matrix[item.Key, value] = 1; // 1 means connection exists
            }
        }

        return matrix;
    }

    public static void PrintMatrixWithAxesAndBorders(long[,] matrix) // print the matrix
    {
        int size = Size;

        // Print the column headers
        Console.Write("   |");
        string leftPadding;
        string rightPadding;
        for (int i = 0; i < size; i++)
        {
            leftPadding = new string(' ', ((5 - i.ToString().Length) / 2));
            rightPadding = new string(' ', 5 - i.ToString().Length - ((5 - i.ToString().Length) / 2));
            Console.Write(leftPadding + i + rightPadding + "|");
        }

        Console.WriteLine();

        // Printer (not a lawyer)
        for (long i = 0; i < size; i++)
        {
            Console.Write($" {i} ");
            for (long j = 0; j < size; j++)
            {
                Console.Write("|  " + matrix[i, j] + "  ");
            }

            Console.WriteLine("|");
        }
    }

    public long[,] ReachabilityMatrixUsingBFS()
    {
        int size = Size; // get the size of the graph
        long[,] reachabilityMatrix = new long[size, size]; // create a new matrix

        for (int i = 0; i < size; i++) // fill the matrix with default values
        {
            for (int j = 0; j < size; j++)
            {
                reachabilityMatrix[i, j] = 0; // 0 means no connection
            }
        }

        for (int startVertex = 0; startVertex < size; startVertex++)
        {
            bool[] visited = new bool[size];
            Queue<int> queue = new Queue<int>();
            visited[startVertex] = true;
            queue.Enqueue(startVertex);

            while (queue.Count != 0)
            {
                int vertex = queue.Dequeue();
                foreach (int neighbor in Connections[vertex])
                {
                    if (!visited[neighbor])
                    {
                        queue.Enqueue(neighbor);
                        visited[neighbor] = true;
                        reachabilityMatrix[startVertex, neighbor] = 1; // 1 means reachable
                    }
                }
            }
        }

        return reachabilityMatrix;
    }

    public long[,] ReachabilityMatrixUsingDFS()
    {
        int size = Size; // get the size of the graph
        long[,] reachabilityMatrix = new long[size, size]; // create a new matrix

        for (int i = 0; i < size; i++) // fill the matrix with default values
        {
            for (int j = 0; j < size; j++)
            {
                reachabilityMatrix[i, j] = 0; // 0 means no connection
            }
        }

        for (int startVertex = 0; startVertex < size; startVertex++)
        {
            bool[] visited = new bool[size];
            DFS(startVertex, visited, reachabilityMatrix);
        }

        return reachabilityMatrix;
    }

    private static void DFS(int vertex, bool[] visited, long[,] reachabilityMatrix)
    {
        visited[vertex] = true;
        foreach (int neighbor in Connections[vertex])
        {
            if (!visited[neighbor])
            {
                reachabilityMatrix[vertex, neighbor] = 1; // 1 means reachable
                DFS(neighbor, visited, reachabilityMatrix);
            }
        }
    }


}

public class Experiments
{
    public static void CompareAlgorithms()
    {
        Stopwatch stopwatch = new Stopwatch();
        var graph = new RandomGraphGen();
        double ts;
        int counter = 0;
        int[] sizes = [50, 80, 100, 150, 170, 200, 220, 250, 290, 300];
        float[] densities = [0.2f, 0.4f, 0.6f, 0.8f, 1.0f];
        var results = new double[(sizes.Length * densities.Length) * 40, 4];
        foreach (var cursize in sizes)
        {
            foreach (var curdensity in densities)
            {
                for (int i = 0; i < 20; i++)
                {
                    graph.GenerateGraph(cursize, curdensity);
                    stopwatch.Reset();
                    stopwatch.Start();
                    graph.ReachabilityMatrixUsingBFS();
                    stopwatch.Stop();
                    ts = stopwatch.ElapsedMilliseconds;
                    results[counter, 0] = 0;
                    results[counter, 1] = cursize;
                    results[counter, 2] = (long)(curdensity * 10);
                    results[counter, 3] = ts;
                    counter++;

                    stopwatch.Reset();
                    stopwatch.Start();
                    graph.ReachabilityMatrixUsingDFS();
                    stopwatch.Stop();
                    ts = stopwatch.ElapsedMilliseconds;
                    results[counter, 0] = 1;
                    results[counter, 1] = cursize;
                    results[counter, 2] = (long)(curdensity * 10);
                    results[counter, 3] = ts;
                    counter++;
                }
            }

            if (cursize == 1000)
            {
                Console.WriteLine("Half of the work has been done...");
            }
        }

        Console.WriteLine("BFS and DFS have been compared.Saving...");

        string csvFilePath =
            "/Users/tim_bzz/Documents/projects/Rider/Discrete-Project/Discrete-project/Discrete/Discrete/results.tsv";
        string algorithm;
        float density;
        string tsString;
        File.WriteAllText(csvFilePath, "Number,Algorithm,Size,Density,Time\n");

        for (int i = 0; i < results.GetLength(0); i++)
        {
            density = (float)(results[i, 2] / 10f);
            algorithm = results[i, 0] == 0 ? "BFS" : "DFS";
            tsString = results[i,3].ToString(System.Globalization.CultureInfo.InvariantCulture);

            File.AppendAllText(csvFilePath, $"{i}\t{algorithm}\t{results[i, 1]}\t{density}\t{tsString}\n");
        }

        Console.WriteLine($"Results have been saved to {csvFilePath}!");

        Console.WriteLine("Analyzing the results...");

        int counterBFS = 0;
        int counterDFS = 0;
        int[] winner = new int[results.GetLength(0)];
        double timeSumBFS = 0;
        double timeSumDFS = 0;
        double[] differences = new double[results.GetLength(0)];


        for (int i = 0; i < results.GetLength(0); i++)
        {
            if (results[i, 0] == 0)
            {
                timeSumBFS += results[i, 3];
            }
            else
            {
                timeSumDFS += results[i, 3];
            }

            if (i % 39 == 0)
            {
                if (timeSumBFS > timeSumDFS)
                {
                    counterDFS++;
                }
                else
                {
                    counterBFS++;
                }

                differences[i / 39] = ((timeSumBFS - timeSumDFS) / 20);
                winner[i / 39] = timeSumBFS > timeSumDFS ? 0 : (timeSumBFS < timeSumDFS ? 1 : 2);
                timeSumBFS = 0;
                timeSumDFS = 0;
                counterBFS = 0;
                counterDFS = 0;
            }

        } // reading.. 0 - BFS, 1 - DFS, 2 - equal


        int countEqual = winner.Count(x => x == 2);
        string absoluteWinner = winner.Count(x => x == 0) > winner.Count(x => x == 1)
            ? "BFS"
            : (winner.Count(x => x == 0) < winner.Count(x => x == 1) ? "BFS" : "noone");
        double averageDifference = Math.Abs(differences.Average());

        Console.WriteLine($"""
                           Results have been analyzed. See, the winner is {absoluteWinner}!
                           The average difference between the algorithms is {averageDifference} ms.
                           All info was saved to the results.csv file in the root of development folder.
                           Now, let's go to the detailed results:
                           """);

        for (int i = 0; i < 50; i++)
        {
            string temporaryWinner = winner[i] == 0 ? "BFS" : (winner[i] == 1 ? "DFS" : "equal");
            if (temporaryWinner == "equal")
            {
                Console.WriteLine(
                    $"For the {i + 1} case, with the {results[i * 40, 1]} of vertices and density of {(float)results[i * 40, 2]/10f} the BFS and DFS managed in the same time."
                );
            }
            else
            {
                Console.WriteLine(
                    $"For the {i + 1} case, with the {results[i * 40, 1]} of vertices and density of {(float)results[i * 40, 2]/10f} the winner is {temporaryWinner}. The difference is {Math.Abs(differences[i])} ms."
                );
            }
        }
    }

}

