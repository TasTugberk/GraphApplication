using GraphApplication.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.Interfaces
{
    public interface IGraph<TVertex, TNode> where TNode : NodeBase<TVertex>
    {
        /// <summary>
        /// Add vertex to graph.
        /// </summary>
        /// <param name="vertex">Vertex</param>
        void AddVertex(TVertex vertex);

        /// <summary>
        /// Adds an node between the given vertices at the given cost.
        /// </summary>
        /// <param name="sourceVertex">Source vertex</param>
        /// <param name="node">Node</param>
        void AddNode(TVertex sourceVertex, TNode node);

        /// <summary>
        /// Returns the dictionary that holds vertices and their adjacents
        /// </summary>
        /// <returns>Dictionary that holds vertices and their adjacents</returns>
        IDictionary<TVertex, ICollection<TNode>> GetGraph();

        /// <summary>
        /// Check graph has cycle.
        /// </summary>
        /// <returns>True if graph has cycle, otherwise false</returns>
        bool IsCyclic();
    }
}
