namespace Discrete;
using System.Diagnostics;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public class visualisator
{
    public void VisualizeGraph(string dotScript)
    {
        // Replace '->' with '--'
        dotScript = dotScript.Replace("->", "--");

        // Define the file paths
        string dotFilePath = "graph.dot";
        string pngFilePath = "graph.png";

        // Save the dotScript to the dotFilePath
        File.WriteAllText(dotFilePath, dotScript);

        // Create a new process start info
        var startInfo = new ProcessStartInfo
        {
            FileName = "dot",
            Arguments = $"-Tpng {dotFilePath} -o {pngFilePath}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        // Start the process
        var process = new Process { StartInfo = startInfo };
        process.Start();

        // Wait for the process to finish
        process.WaitForExit();
    }



    //
    //
    // public void CreateImageFromMatrix(int[,] matrix)
    // {
    //     int size = matrix.GetLength(0);
    //     int cellSize = 50; // size of each cell in pixels
    //
    //     // Create a new image
    //     using (Image<Rgba32> image = new Image<Rgba32>(size * cellSize, size * cellSize))
    //     {
    //         // Set the background color
    //         image.Mutate(x => x.BackgroundColor(Rgba32.White));
    //
    //         // Draw the matrix
    //         for (int i = 0; i < size; i++)
    //         {
    //             for (int j = 0; j < size; j++)
    //             {
    //                 // Choose a color based on the matrix value
    //                 Rgba32 color = matrix[i, j] == 0 ? Rgba32.Black : Rgba32.White;
    //
    //                 // Draw the cell
    //                 image.Mutate(x => x.Fill(color, new Rectangle(i * cellSize, j * cellSize, cellSize, cellSize)));
    //             }
    //         }
    //
    //         // Save the image as a PNG file
    //         image.Save("matrix.png");
    //     }
    // }
}
