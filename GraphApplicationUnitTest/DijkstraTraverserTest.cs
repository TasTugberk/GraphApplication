using GraphApplication;
using GraphApplication.Common;
using GraphApplication.Interfaces;
using GraphApplication.Node;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApplicationUnitTest
{
    [TestFixture]
    public class DijkstraTraverserTest
    {
        [Test]
        public void Traverse_NullGraph_ThrowsCannotTraverseNullOrEmptyGraph()
        {
            // arrange
            var dijkstra = new Mock<DijkstraTraverser<string, NodeWithCost<string>>>();
            //act
            IGraph<string, NodeWithCost<string>> graph = null;
            var traverser = dijkstra.Object;
            //assert
            var ex = Assert.Throws<GraphError>(() => traverser.Traverse(graph, string.Empty));
            Assert.AreEqual(ErrorCode.CannotTraverseNullOrEmptyGraph, ex.ErrorCode);
        }

        [Test]
        public void Traverse_EmptyGraph_ThrowsCannotTraverseNullOrEmptyGraph()
        {
            // arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            var dijkstra = new Mock<DijkstraTraverser<string, NodeWithCost<string>>>();
            //act
            var graph = mockGraph.Object;
            var traverser = dijkstra.Object;
            //assert
            var ex = Assert.Throws<GraphError>(() => traverser.Traverse(graph, string.Empty));
            Assert.AreEqual(ErrorCode.CannotTraverseNullOrEmptyGraph, ex.ErrorCode);
        }

        [Test]
        public void Traverse_GraphWithNoVertex_ThrowsCannotTraverseNullOrEmptyGraph()
        {
            //arrange
            var mockDijkstra = new Mock<DijkstraTraverser<string, NodeWithCost<string>>>();
            var mockGraph = new Mock<WeightedGraph<string>>();
            //act
            var traverser = mockDijkstra.Object;
            var ex = Assert.Throws<GraphError>(() => traverser.Traverse(mockGraph.Object,
                string.Empty));
            //assert
            Assert.AreEqual(ErrorCode.CannotTraverseNullOrEmptyGraph, ex.ErrorCode);
        }

        [Test]
        public void Traverse_GraphWithNoAdjecent_ThrowsCannotTraverseNullOrEmptyGraph()
        {
            //arrange
            var mockDijkstra = new Mock<DijkstraTraverser<string, NodeWithCost<string>>>();
            var mockGraph = new Mock<WeightedGraph<string>>();
            //act
            var someVertex = string.Empty;
            var traverser = mockDijkstra.Object;
            var graph = mockGraph.Object;
            graph.AddVertex(someVertex);
            var ex = Assert.Throws<GraphError>(() => traverser.Traverse(mockGraph.Object,
                string.Empty));
            //assert
            Assert.AreEqual(ErrorCode.CannotTraverseGraphWithoutNode, ex.ErrorCode);
        }

        [Test]
        public void Traverse_NullStartReferenceTypeVertex_ThrowsNullStartVertex()
        {
            //arrange
            var someVertex = string.Empty;
            var someCost = 1;
            var mockDijkstra = new Mock<DijkstraTraverser<string, NodeWithCost<string>>>();
            var mockGraph = new Mock<WeightedGraph<string>>();
            var mockNode = new Mock<NodeWithCost<string>>(someVertex, someCost);
            //act
            var traverser = mockDijkstra.Object;
            var graph = mockGraph.Object;
            graph.AddVertex(someVertex);
            graph.AddNode(someVertex, mockNode.Object);
            var ex = Assert.Throws<GraphError>(() => traverser.Traverse(mockGraph.Object,
                null));
            //assert
            Assert.AreEqual(ErrorCode.NullStartVertex, ex.ErrorCode);
        }

        [Test]
        public void Traverse_NullStartValueTypeVertex_ThrowsNullStartVertex()
        {
            //arrange
            var someVertex = 0;
            var someCost = 1;
            var mockDijkstra = new Mock<DijkstraTraverser<int?, NodeWithCost<int?>>>();
            var mockGraph = new Mock<WeightedGraph<int?>>();
            var mockNode = new Mock<NodeWithCost<int?>>(someVertex, someCost);
            //act
            var traverser = mockDijkstra.Object;
            var graph = mockGraph.Object;
            graph.AddVertex(someVertex);
            graph.AddNode(someVertex, mockNode.Object);
            var ex = Assert.Throws<GraphError>(() => traverser.Traverse(mockGraph.Object,
                null));
            //assert
            Assert.AreEqual(ErrorCode.NullStartVertex, ex.ErrorCode);
        }

        [Test]
        public void Traverse_GraphNotContainsStartVertex_ThrowsGraphNotContainsStartVertex()
        {
            //arrange
            var someVertex = string.Empty;
            var someCost = 1;
            var mockDijkstra = new Mock<DijkstraTraverser<string, NodeWithCost<string>>>();
            var mockGraph = new Mock<WeightedGraph<string>>();
            var mockNode = new Mock<NodeWithCost<string>>(someVertex, someCost);
            //act
            var startVertex = "StartVertex";
            var traverser = mockDijkstra.Object;
            var graph = mockGraph.Object;
            graph.AddVertex(someVertex);
            graph.AddNode(someVertex, mockNode.Object);
            var ex = Assert.Throws<GraphError>(() => traverser.Traverse(mockGraph.Object,
                startVertex));
            //assert
            Assert.AreEqual(ErrorCode.GraphNotContainsStartVertex, ex.ErrorCode);
        }

        [Test]
        public void Traverse_ValidParameters_ReturnsDistanceDictionary()
        {
            //arrange
            var someVertex = 1;
            var someOtherVertex =2;
            var someCost = 1;
            var mockDijkstra = new Mock<DijkstraTraverser<int, NodeWithCost<int>>>();
            var mockGraph = new Mock<WeightedGraph<int>>();
            var mockNode = new Mock<NodeWithCost<int>>(someOtherVertex, someCost);
            //act
            var traverser = mockDijkstra.Object;
            var graph = mockGraph.Object;
            graph.AddVertex(someVertex);
            graph.AddVertex(someOtherVertex);
            graph.AddNode(someVertex, mockNode.Object);
            var result = traverser.Traverse(mockGraph.Object, someVertex);
            //assert
            Assert.IsInstanceOf(typeof(IDictionary<int, double>), result);
        }
    }
}
