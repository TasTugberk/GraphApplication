using GraphApplication.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.Interfaces
{
    public interface IWeightedGraph<TVertex> : IGraph<TVertex, NodeWithCost<TVertex>>
    {
    }
}
