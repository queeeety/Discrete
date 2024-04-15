
namespace Discrete;

public class Program
{
    public static void Main(string[] args)
    {
        RandomGraphGen graph = new RandomGraphGen();

        Console.WriteLine("""
                          Hello!
                          I am your assistant in Discrete Math.
                          Actually, I am just a project on Discrete Math.

                          I can solve some tasks for you, including:
                          ––– Generating graphs (oriented) with your parameters (size and density) +
                          ––– Generating adjacency matrix and list -
                          ––– Generating reachability matrix -
                          ––– Generating .png with your graph +
                          ––– Analysing the time of the algorithms, building the same reachability matrix with different algorithms and counting the time of their work -
                          """);
        while (true)
        {
            Console.WriteLine("""
                              You can choose from the following tasks:

                              ––– Generating a random graph with your parameters (1)

                              ––– Calculating the difference between DFS and BFS algorithms (2)

                              You can choose your option by typing the number of the task you want to solve or type 'e' to exit.
                              """);
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":

                    int size;
                    float density;

                    IfAMistakeInSize:
                    Console.WriteLine("Enter the size of the graph:");
                    try
                    {
                        size = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a valid number.");
                        goto IfAMistakeInSize;
                    }

                    IfAMistakeInDensity:
                    Console.WriteLine("Enter the density of the graph (between 0,01 and 1):");
                    try
                    {
                        density = float.Parse(Console.ReadLine());
                        if (density <= 0.01 || density > 1)
                        {
                            Console.WriteLine("Your number is not valid.");
                            goto IfAMistakeInDensity;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a valid number.");
                        goto IfAMistakeInDensity;
                    }


                    graph.GenerateGraph(size, density);
                    GraphGeneratorCheckPoint:
                    Console.WriteLine($"""
                                       The graph with your parameters (size: {size}; density: {density}) has been generated!

                                       Want to see:
                                       ––– Adjacency matrix (1)
                                       ––– Adjacency list (2)
                                       ––– Both (3)
                                       ––– Create .png with the graph (4)
                                       ––– Reachability matrix (5)
                                       """);

                    string input2 = Console.ReadLine();
                    switch (input2)
                    {
                        case "1":
                            var matrix = RandomGraphGen.MatrixVisualiser();
                            RandomGraphGen.PrintMatrixWithAxesAndBorders(matrix);
                            break;

                        case "2":
                            Console.Write("[ ");
                            foreach (var item in RandomGraphGen.Connections)
                            {
                                if (item.Value.Count > 0)
                                {
                                    foreach (var value in item.Value)
                                    {
                                        Console.Write("({0} –> {1}), ", item.Key, value);
                                    }

                                    Console.WriteLine();
                                }
                            }

                            Console.WriteLine("]");
                            break;

                        case "3":
                            var matrix2 = RandomGraphGen.MatrixVisualiser();
                            RandomGraphGen.PrintMatrixWithAxesAndBorders(matrix2);
                            Console.Write("[ ");
                            foreach (var item in RandomGraphGen.Connections)
                            {
                                if (item.Value.Count > 0)
                                {
                                    foreach (var value in item.Value)
                                    {
                                        Console.Write("({0} –> {1}), ", item.Key, value);
                                    }

                                    Console.WriteLine();
                                }
                            }

                            Console.WriteLine("]");
                            break;

                        case "4":
                            visualisator vis = new visualisator();
                            try
                            {
                                vis.DotScriptGenerator();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"""
                                                   Oopsie( An error was thrown: {e}. There was some problem with the path and permission, so it is possible
                                                   that this function is only working on MacOS (or on my only).
                                                   But still you can see generated graph in the png folder. Just open the file 'new_graph.png' in the folder 'png'.
                                                   """);
                            }

                            break;

                        case "5":
                            long[,] reachMatrix = graph.ReachabilityMatrixUsingBFS();
                            RandomGraphGen.PrintMatrixWithAxesAndBorders(reachMatrix);
                            break;

                        default:
                            Console.WriteLine("Please, enter a valid number.");
                            goto GraphGeneratorCheckPoint;
                            break;
                    }

                    Console.WriteLine("Do you want to see more? (y/n)");
                    string input3 = Console.ReadLine();
                    if (input3 == "n")
                    {
                        break;
                    }
                    else
                    {
                        goto GraphGeneratorCheckPoint;
                    }

                    break;

                case "2":
                    Console.WriteLine("""
                                      Wellcome to BFS vs DFS comparison!
                                      This quick test will show you average time of the creating reachability matrix with BFS and DFS algorithms.
                                      And, the short conclusion will be given.
                                      Starting the test...
                                      """);
                    graph.GenerateGraph(1000, 1);
                    Experiments.CompareAlgorithms();

                    string input4 = Console.ReadLine();
                    if (input4 == "n")
                    {
                        break;
                    }
                    else
                    {
                        goto case "2";
                    }



                case "e":
                    Console.WriteLine("Ok, bye!");
                    return;

            }








        }
    }
}