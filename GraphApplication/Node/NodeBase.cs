using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.Node
{
    /// <summary>
    /// Node
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    public class NodeBase<TVertex> : IComparable
    {
        /// <summary>
        /// Destination Vertex
        /// </summary>
        public TVertex DestinationVertex { get; set; }


        public NodeBase(TVertex destinationVertex)
        {
            DestinationVertex = destinationVertex;
        }

        public virtual int CompareTo(object obj)
        {
            return 0;
        }
    }
}
