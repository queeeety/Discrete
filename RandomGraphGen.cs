namespace Discrete;



public class RandomGraphGen
{
    public static Dictionary<int, List<int>> Connections { get; set; }

    public static int Size { get; set; } // graph size
    // значить так, поїхали, (*ня*)
    private int AddPoint(int x, int y) // add a point to the graph
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

    public static long[,] MatrixVisualiser()
    {
        int size = Size; // get the size of the graph
        // create a new matrix
        long[,] matrix = new long[size, size];
        
        for (int i = 0; i < size; i++)  // fill the matrix with default values
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

    public static void PrintMatrixWithAxesAndBorders(long[,] matrix)
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
}

