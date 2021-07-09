using GraphApplication.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplication.Interfaces
{
    public interface ITraverser<TVertex, TNode> where TNode : NodeBase<TVertex>
    {
        IDictionary<TVertex, double> Traverse(IGraph<TVertex, TNode> graph, TVertex startVertex);
    }
}
