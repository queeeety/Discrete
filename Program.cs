
namespace Discrete;

public class Program
{
    public static void Main(string[] args)
    {
        RandomGraphGen ourGraph = new RandomGraphGen();
        ourGraph.GenerateGraph(100, 0.5f);
        
        foreach (var item in RandomGraphGen.Connections)
        {
            Console.WriteLine("Key: {0}", item.Key);
            foreach (var value in item.Value)
            {
                Console.WriteLine("Value: {0}", value);
            }
        }
        var matrix = RandomGraphGen.MatrixVisualiser();
        RandomGraphGen.PrintMatrixWithAxesAndBorders(matrix);
        visualisator Vis = new visualisator();
        
//visualising the graph

// var graph = new UndirectedGraph<int, IEdge<int>>();
//         graph.AddVertex(1);
//         graph.AddVertex(2);
//         graph.AddVertex(3);
//         graph.AddVertex(4);
//         graph.AddVertex(5);
//         graph.AddEdge(new Edge<int>(1, 2));
//         graph.AddEdge(new Edge<int>(2, 3));
//         graph.AddEdge(new Edge<int>(3, 4));
//         graph.AddEdge(new Edge<int>(4, 1));
//         graph.AddEdge(new Edge<int>(5, 2));
//
//
// // Create a Graphviz algorithm
//         var graphviz = new GraphvizAlgorithm<int, IEdge<int>>(graph);
//
// // Customize the edge rendering
//         graphviz.FormatEdge += (sender, args) => { args.EdgeFormatter.Label.Value = args.Edge.ToString(); };
//
//         // Generate DOT script and create png file
//         var visualisator = new visualisator();
//         string dotScript = graphviz.Generate();
//         visualisator.VisualizeGraph(dotScript);
//         
//         
// // Run the following command in the terminal to generate the PNG image:
// //dot -Tpng graph.dot -o graph.png



    }
}
