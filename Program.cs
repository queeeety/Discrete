using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System.IO;
namespace Discrete;

public class Program
{
    public static void Main(string[] args)
    {
// Your existing QuickGraph graph
        var graph = new UndirectedGraph<int, IEdge<int>>();
        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);
        graph.AddVertex(4);
        graph.AddEdge(new Edge<int>(1, 2));
        graph.AddEdge(new Edge<int>(2, 3));
        graph.AddEdge(new Edge<int>(3, 4));
        graph.AddEdge(new Edge<int>(4, 1));

// Create a Graphviz algorithm
        var graphviz = new GraphvizAlgorithm<int, IEdge<int>>(graph);

// Customize the edge rendering
        graphviz.FormatEdge += (sender, args) => { args.EdgeFormatter.Label.Value = args.Edge.ToString(); };

        // Generate DOT script and create png file
        var visualisator = new visualisator();
        string dotScript = graphviz.Generate();
        visualisator.VisualizeGraph(dotScript);
        
        
// Run the following command in the terminal to generate the PNG image:
//dot -Tpng graph.dot -o graph.png



    }
}
