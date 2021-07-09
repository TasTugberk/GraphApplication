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

    public class WeightedGraph<TVertex> : IWeightedGraph<TVertex>
    {
        /// <summary>
        /// Graph
        /// </summary>
        private readonly IDictionary<TVertex, ICollection<NodeWithCost<TVertex>>> graph;

        /// <summary>
        /// Returns the dictionary that holds vertices and their adjacents.
        /// </summary>
        /// <returns>the dictionary that holds vertices and their adjacents.</returns>
        public IDictionary<TVertex, ICollection<NodeWithCost<TVertex>>> GetGraph()
        {
            return graph;
        }


        /// <summary>
        /// Ctor
        /// </summary>
        public WeightedGraph()
        {
            this.graph = new Dictionary<TVertex, ICollection<NodeWithCost<TVertex>>>();
        }

        private bool IsVertexTypeNullable()
        {
            Type type = typeof(TVertex);
            if (!type.IsValueType)
            {
                return true;
            }
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true;
            }

            return false;
        }

        private bool IsVertexExistInGraph(TVertex vertex)
        {
            var isExist = graph.ContainsKey(vertex);
            return isExist;
        }

        private bool IsNodeExist(TVertex sourceVertex, NodeWithCost<TVertex> node)
        {
            var isExistNode = graph[sourceVertex].Select(e => e.DestinationVertex).Contains(node.DestinationVertex);
            return isExistNode;
        }

        /// <summary>
        /// Adds an node between the given vertices at the given cost.
        /// </summary>
        /// <param name="sourceVertex">Source vertex</param>
        /// <param name="node">Node</param>
        public void AddNode(TVertex sourceVertex, NodeWithCost<TVertex> node)
        {
            if (node == null)
            {
                var exception = new GraphError(ErrorCode.CannotAddNullNode);
                throw exception;
            }

            bool isNullableType = IsVertexTypeNullable();
            if (isNullableType)
            {
                if (sourceVertex == null)
                {
                    var exception = new GraphError(ErrorCode.NullSourceVertex);
                    throw exception;
                }

                if (node.DestinationVertex == null)
                {
                    var exception = new GraphError(ErrorCode.NullDestinationVertex);
                    throw exception;
                }
            }

            if (node.Cost <= 0)
            {
                var exception = new GraphError(ErrorCode.InvalidCost);
                throw exception;
            }

            if (!IsVertexExistInGraph(sourceVertex))
            {
                var exception = new GraphError(ErrorCode.SourceVertexNotExist);
                throw exception;
            }

            if (!IsVertexExistInGraph(node.DestinationVertex))
            {
                var exception = new GraphError(ErrorCode.DestinationVertexNotExist);
                throw exception;
            }

            if (IsNodeExist(sourceVertex, node))
            {
                var exception = new GraphError(ErrorCode.DuplicateNode);
                throw exception;
            }

            graph[sourceVertex].Add(node);
        }

        /// <summary>
        /// Add vertex to graph.
        /// </summary>
        /// <param name="vertex">Vertex</param>
        public void AddVertex(TVertex vertex)
        {
            bool isNullableType = IsVertexTypeNullable();
            if (isNullableType && vertex == null)
            {
                var exception = new GraphError(ErrorCode.CannotAddNullVertex);
                throw exception;
            }

            if (IsVertexExistInGraph(vertex))
            {
                var exception = new GraphError(ErrorCode.DuplicateVertex);
                throw exception;
            }

            graph.Add(vertex, new List<NodeWithCost<TVertex>>());
        }

        public bool IsCyclic()
        {
            var visited = new Dictionary<TVertex, bool>(graph.Keys.Count);
            var recStack = new Dictionary<TVertex, bool>(graph.Keys.Count);

            foreach (var vertex in graph.Keys)
            {
                if (IsCyclicUtil(vertex, visited, recStack))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsCyclicUtil(TVertex vertex, Dictionary<TVertex, bool> visited,
                                 Dictionary<TVertex, bool> recStack)
        {
            if (recStack.ContainsKey(vertex) && recStack[vertex])
            {
                return true;
            }

            if (visited.ContainsKey(vertex) && visited[vertex])
            {
                return false;
            }

            visited[vertex] = true;

            recStack[vertex] = true;
            ICollection<NodeWithCost<TVertex>> children = graph[vertex];

            foreach (var node in children)
            {
                if (IsCyclicUtil(node.DestinationVertex, visited, recStack))
                {
                    return true;
                }
            }

            recStack[vertex] = false;

            return false;
        }

    }
}
