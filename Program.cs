
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
                                       
                                       Or if you want to make changes to your tree, you can:
                                       
                                       ––– Add point (6)
                                       ––– Remove point (7)
                                       ––– Add edge (8)
                                       ––– Remove the edge with all incident points (9)
                                       ––– Insert the vertex to the defined edge (10)
                                       ––– Edges pulling (11)
                                       ––– Vertex identification (12)

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

                        case "6":
                            Console.WriteLine("Please, enter the number of the point you want to add:");
                            try
                            {
                                int point = int.Parse(Console.ReadLine());
                                int checker = graph.AddPoint(point);
                                if (checker == 1)
                                {
                                    Console.WriteLine("The point already exists. Wanna try another one? (y/n)");
                                    if (Console.ReadLine() == "y")
                                    {
                                        goto case "6";
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("The point has been added succesfully. Wanna add edge to this point? (y/n)");
                                    if (Console.ReadLine() == "y")
                                    {
                                        ConnectionCheckPoint:
                                        Console.WriteLine("Enter the number of the point you want to connect with the new point:");
                                        try
                                        {
                                            int point2 = int.Parse(Console.ReadLine());
                                            int a = graph.AddPoint(point, point2);
                                            if (a == 1)
                                            {
                                                Console.WriteLine("The edge already exists.");
                                                goto ConnectionCheckPoint;
                                            }

                                            if (a == 2)
                                            {
                                                Console.WriteLine("Looks like the point doesn't exist. try again");
                                                goto ConnectionCheckPoint;
                                            }
                                            Console.WriteLine("The edge has been added succesfully.");
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Please enter a valid number.");
                                            goto case "6";
                                        }

                                    }
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Please enter a valid number.");
                                goto case "6";
                            }
                            break;

                        case "7":
                            // Remove point
                            break;

                        case "8":
                            // Add edge
                            break;

                        case "9":
                            // Remove the edge with all incident points
                            break;

                        case "10":
                            // Insert the vertex to the defined edge
                            break;

                        case "11":
                            // Edges pulling
                        break;

                        case "12":
                            // Vertex identification
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