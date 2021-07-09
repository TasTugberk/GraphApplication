using GraphApplication.Common;
using GraphApplication.Interfaces;
using GraphApplication.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication
{
    public class DijkstraTraverser<TVertex, TNode> : ITraverser<TVertex, TNode> where TNode : NodeBase<TVertex>
    {
        private HashSet<TVertex> visitedVertices;
        private IDictionary<TVertex, double> distances;
        private ICollection<TNode> priorityQueue;

        public DijkstraTraverser()
        {
            visitedVertices = new HashSet<TVertex>();
            distances = new Dictionary<TVertex, double>();
            priorityQueue = new List<TNode>();
        }

        public IDictionary<TVertex, double> Traverse(IGraph<TVertex, TNode> graph, TVertex startVertex)
        {
            if (graph == null)
            {
                var exception = new GraphError(ErrorCode.CannotTraverseNullOrEmptyGraph);
                throw exception;
            }

            var graphDictionary = graph.GetGraph();
            if (graphDictionary == null || graphDictionary.Keys == null || graphDictionary.Keys.Count == 0)
            {
                var exception = new GraphError(ErrorCode.CannotTraverseNullOrEmptyGraph);
                throw exception;
            }

            var verticesThatHaveAdjacent = graphDictionary.Where(e => e.Value != null
            && e.Value.Count > 0);
            if (verticesThatHaveAdjacent != null && verticesThatHaveAdjacent.Count() == 0)
            {
                var exception = new GraphError(ErrorCode.CannotTraverseGraphWithoutNode);
                throw exception;
            }


            if (IsVertexTypeNullable() && startVertex == null)
            {
                var exception = new GraphError(ErrorCode.NullStartVertex);
                throw exception;
            }

            if (!graphDictionary.ContainsKey(startVertex))
            {
                var exception = new GraphError(ErrorCode.GraphNotContainsStartVertex);
                throw exception;
            }

            foreach (var vertex in graphDictionary.Keys)
            {
                distances.Add(vertex, double.MaxValue);
            }

            TNode node;
            if (graph is IWeightedGraph<TVertex>)
            {
                node = new NodeWithCost<TVertex>(startVertex, 0) as TNode;
            }
            else
            {
                node = new NodeBase<TVertex>(startVertex) as TNode;
            }

            priorityQueue.Add(node);
            distances[startVertex] = 0;
            while (visitedVertices.Count != graph.GetGraph().Keys.Count)
            {
                TVertex vertex = GetLeastCostDestination();

                visitedVertices.Add(vertex);
                AdjacentNodes(graph, vertex);
            }

            return distances;
        }

        private bool IsVertexTypeNullable()
        {
            Type type = typeof(TVertex);
            if (!type.IsValueType)
            {
                return true; // ref-type
            }
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true; // Nullable<T>
            }

            return false; // value-type
        }

        private void AdjacentNodes(IGraph<TVertex, TNode> graph, TVertex vertex)
        {
            double nodeDistance = -1;
            double newDistance = -1;

            // process all neighbouring nodes of u 
            for (int i = 0; i < graph.GetGraph()[vertex].Count; i++)
            {
                TNode currentNode = graph.GetGraph()[vertex].ElementAt(i);

                //  proceed only if current node is not in 'visited'
                if (!visitedVertices.Contains(currentNode.DestinationVertex))
                {
                    TNode node;
                    if (graph is IWeightedGraph<TVertex>)
                    {
                        nodeDistance = (currentNode as NodeWithCost<TVertex>).Cost;
                        newDistance = distances[vertex] + nodeDistance;

                        // compare distances 
                        if (newDistance < distances[currentNode.DestinationVertex])
                        {
                            distances[currentNode.DestinationVertex] = newDistance;
                        }

                        node = new NodeWithCost<TVertex>(currentNode.DestinationVertex, distances[currentNode.DestinationVertex]) as TNode;
                    }
                    else
                    {
                        node = new NodeBase<TVertex>(currentNode.DestinationVertex) as TNode;
                    }

                    priorityQueue.Add(node);
                }
            }
        }

        private TVertex GetLeastCostDestination()
        {
            var leastCodeNode = priorityQueue.Min();
            priorityQueue.Remove(leastCodeNode);
            return leastCodeNode.DestinationVertex;
        }
    }
}
