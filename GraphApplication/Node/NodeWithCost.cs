using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.Node
{
    /// <summary>
    /// Directed Node
    /// </summary>
    public class NodeWithCost<TVertex> : NodeBase<TVertex>
    {
        public NodeWithCost(TVertex destinationVertex, double cost)
            : base(destinationVertex)
        {
            Cost = cost;
        }

        /// <summary>
        /// Cost of the node
        /// </summary>
        public double Cost { get; set; }

        public override int CompareTo(object obj)
        {
            if (this.Cost > (obj as NodeWithCost<TVertex>).Cost)
            {
                return 1;
            }

            if (this.Cost < (obj as NodeWithCost<TVertex>).Cost)
            {
                return -1;
            }

            return 0;
        }
    }
}
