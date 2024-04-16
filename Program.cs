
using QuickGraph.Algorithms.Services;

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
                            // Adjacency matrix
                            var matrix = RandomGraphGen.MatrixVisualiser();
                            RandomGraphGen.PrintMatrixWithAxesAndBorders(matrix);
                            break;

                        case "2":
                            // Adjacency list
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
                            // Both adjacency matrix and list
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
                            // Create .png with the graph
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
                            Console.WriteLine("Please, enter the number of the points you want to remove:");
                            try
                            {
                                int number = int.Parse(Console.ReadLine() ?? string.Empty);

                                    Console.WriteLine("Please, enter the point you want to remove:");
                                    int point = int.Parse(Console.ReadLine());
                                    int checker = graph.RemovePoint(point);
                                    if (checker == 0)
                                    {
                                        Console.WriteLine("The point has been removed successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("The point doesn't exist. Please, try again.");
                                        goto case "7";
                                    }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("There is a mistake with your input. Please, try again.");
                                goto case "7";
                            }
                            break;

                        case "8":
                            // Add edge
                            try
                            {
                                Console.WriteLine("Please, enter the edge you want to add:  (example: x1 y1)");
                                string[] splitInput = Console.ReadLine().Split(' ');
                                int p1 = int.Parse(splitInput[0]);
                                int p2 = int.Parse(splitInput[1]);
                                int checker = graph.AddPoint(p1, p2);
                                if (checker == 0)
                                {
                                    Console.WriteLine("The edge has been added successfully.");
                                }
                                else if (checker == 1)
                                {
                                    Console.WriteLine("The edge already exists. Please, try again.");
                                    goto case "8";
                                }
                                else if (checker == 2)
                                {
                                    Console.WriteLine("At least one of the vertex doesn't exist. Please, try again.");
                                    goto case "8";
                                }

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("There is a mistake with your input. Please, try again.");
                                goto case "8";
                            }

                            break;

                        case "9":
                            // Remove the edge with all incident points
                            try
                            {
                                Console.WriteLine("Please, enter the edge you want to remove:  (example: x1 y1)");
                                string[] splitInput = Console.ReadLine().Split(' ');
                                int p1 = int.Parse(splitInput[0]);
                                int p2 = int.Parse(splitInput[1]);
                                int checker = graph.RemoveEdge(p1, p2);
                                if (checker == 0)
                                {
                                    Console.WriteLine("The edge has been removed successfully.");
                                }
                                else if (checker == 1)
                                {
                                    Console.WriteLine("The edge does not exist. Please, try again.");
                                    goto case "9";
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("There is a mistake with your input. Please, try again.");
                                goto case "9";
                            }
                            break;

                        case "10":
                            // Insert the vertex to the defined edge
                            try
                            {
                                Console.WriteLine("Please, enter the edge you want to interrupt:  (example: x1 y1)");
                                string[] splitInput = Console.ReadLine().Split(' ');
                                int p1 = int.Parse(splitInput[0]);
                                int p2 = int.Parse(splitInput[1]);
                                int checker = graph.CheckTheEdge(p1, p2);
                                if (checker == 1)
                                {
                                    Console.WriteLine("Seems like at least one of the vertex does not exist. Try again.");
                                    goto case "10";
                                }
                                Console.WriteLine("Please, enter the point you want to insert:");
                                int point = int.Parse(Console.ReadLine());
                                graph.AddPoint(p1, point);
                                graph.AddPoint(point, p2);
                                graph.RemoveEdge(p1, p2);
                                    Console.WriteLine($"Done! The point {point} has been inserted between {p1} and {p2}.");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("There is a mistake with your input. Please, try again.");
                                goto case "10";
                            }
                            break;

                        case "11":
                            // Edges pulling
                            try
                            {
                                Console.WriteLine("Please, enter the edge you want to pull:  (example: x1 y1)");
                                string[] splitInput = Console.ReadLine().Split(' ');
                                int p1 = int.Parse(splitInput[0]);
                                int p2 = int.Parse(splitInput[1]);
                                int checker = graph.PointPuller(p1, p2);
                                if (checker == 0)
                                {
                                    Console.WriteLine("The edge has been pulled successfully.");
                                }
                                else if (checker == 1)
                                {
                                    Console.WriteLine("The edge does not exist. Please, try again.");
                                    goto case "11";
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("There is a mistake with your input. Please, try again.");
                                goto case "11";
                            }
                            break;

                        case "12":
                            // Vertex identification
                            try
                            {
                                Console.WriteLine("Please, enter the vertices you want to concate:  (example: x1 y1)");
                                string[] splitInput = Console.ReadLine().Split(' ');
                                int p1 = int.Parse(splitInput[0]);
                                int p2 = int.Parse(splitInput[1]);
                                int checker = graph.PointPuller(p1, p2);
                                if (checker == 0)
                                {
                                    Console.WriteLine("The vertices has been unified successfully.");
                                }
                                else if (checker == 1)
                                {
                                    Console.WriteLine("One of the vertice does not exist. Please, try again.");
                                    goto case "12";
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("There is a mistake with your input. Please, try again.");
                                goto case "12";
                            }
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