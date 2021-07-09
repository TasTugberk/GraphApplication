using GraphApplication.Common;
using GraphApplication.Interfaces;
using GraphApplication.Node;
using System;

namespace GraphApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IGraph<int, NodeWithCost<int>> graph = new WeightedGraph<int>();
                IDataWithCostReader<int> dataReader = new IntegerWeightedGraphDataReader();
                var vertices = dataReader.GetVertices("Vertices.txt");
                for (int i = 0; i < vertices.Count; i++)
                {
                    graph.AddVertex(vertices[i]);
                }

                var nodes = dataReader.GetNodes("Nodes.txt");
                for (int i = 0; i < nodes.Count; i++)
                {
                    int sourceVertexIndex = nodes[i].Item1;
                    int destinationVertexIndex = nodes[i].Item2;
                    double cost = nodes[i].Item3;
                    graph.AddNode(vertices[sourceVertexIndex], new NodeWithCost<int>(vertices[destinationVertexIndex], cost));
                }

                var isCyclicGraph = graph.IsCyclic();
                Console.WriteLine("Graph is cyclic:" + isCyclicGraph);
                Console.WriteLine();
                int source = vertices[0];
                ITraverser<int, NodeWithCost<int>> traverser = new DijkstraTraverser<int, NodeWithCost<int>>();
                var result = traverser.Traverse(graph, 0);

                Console.WriteLine("The shorted path from source node to other nodes:");
                Console.WriteLine("Source\t\t" + "Node#\t\t" + "Distance");
                foreach (var node in result.Keys)
                {
                    Console.WriteLine(source + " \t\t " + node + " \t\t " + result[node]);
                }
            }
            catch (GraphError er)
            {
                Console.WriteLine(er.ErrorCode + "-" + er.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
