namespace Discrete;
using System.Diagnostics;

public class visualisator
{
   public void VisualizeGraph(string dotScript)
{
    // Replace '->' with '--'
    dotScript = dotScript.Replace("->", "--"); 
    
    // Define the file paths
    string dotFilePath = "/Users/tim_bzz/Documents/projects/Rider/Discrete-Project/Discrete/Discrete/graph.dot";
    string pngFilePath = "/Users/tim_bzz/Documents/projects/Rider/Discrete-Project/Discrete/Discrete/graph.png";
    
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
}