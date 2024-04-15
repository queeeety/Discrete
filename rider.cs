using System.IO;
using Discrete;
public class CsvWriter
{
    public static void WriteToCsv(float bfsTime, float dfsTime, int winner)
    {
        string csvFilePath = "results.csv"; // Replace with your actual file path

        // Prepare the line to be written in the CSV file
        string line = $"{bfsTime},{dfsTime},{winner}";

        // Append the line to the CSV file
        File.AppendAllText(csvFilePath, line + "\n");
    }

    public static string ReadAndAnalyzeCsv()
    {
        string csvFilePath = "results.csv"; // Replace with your actual file path
        string[] lines = File.ReadAllLines(csvFilePath);

        int bfsWins = 0;
        int dfsWins = 0;

        foreach (string line in lines)
        {
            string[] values = line.Split(',');

            int winner = int.Parse(values[2]);

            if (winner == 1)
            {
                bfsWins++;
            }
            else if (winner == 2)
            {
                dfsWins++;
            }
        }

        string winnerAlgorithm = bfsWins > dfsWins ? "BFS" : "DFS";

        return $"BFS was the winner {bfsWins} times, when the DFS – {dfsWins} times. The {winnerAlgorithm} wins!";
    }
}



