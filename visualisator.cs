using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System.IO;
using Discrete;
using System.Diagnostics;


public class visualisator
{

    public void DotScriptGenerator()
    {
        var graph = new AdjacencyGraph<int, Edge<int>>();
        foreach (var item in RandomGraphGen.Connections)
        {
            graph.AddVertex(item.Key);
            foreach (var value in item.Value)
            {
                if (!graph.ContainsVertex(value))
                {
                    graph.AddVertex(value);
                }

                graph.AddEdge(new Edge<int>(item.Key, value));
            }
        }

        var graphviz = new GraphvizAlgorithm<int, Edge<int>>(graph);
        string dot = graphviz.Generate();

        string filePath = "new_graph.dot";
        File.WriteAllText(filePath, dot);

        // Generate an image from the dot file using the dot command-line tool
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "/opt/homebrew/bin/dot", // Correct absolute path without angle brackets
            Arguments = $"-Tpng {filePath} -o new_graph.png",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        var process = new Process { StartInfo = processStartInfo };
        process.Start();
        process.WaitForExit();
    }
}