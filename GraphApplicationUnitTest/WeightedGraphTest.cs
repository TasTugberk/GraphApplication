using GraphApplication;
using GraphApplication.Common;
using GraphApplication.Node;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace GraphApplicationUnitTest
{
    [TestFixture]
    public class WeightedGraphTest
    {
        [Test]
        public void GetGraph_Always_ReturnGraphDictionary()
        {
            // arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            //act
            var graph = mockGraph.Object;
            var graphDictionary = graph.GetGraph();
            //assert
            Assert.IsInstanceOf(typeof(IDictionary<string, ICollection<NodeWithCost<string>>>), graphDictionary);
        }

        [Test]
        public void IsCyclic_EmptyGraph_ReturnsFalse()
        {
            // arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            //act
            var graph = mockGraph.Object;
            var isCyclic = graph.IsCyclic();
            //assert
            Assert.AreEqual(false, isCyclic);
        }

        [Test]
        public void IsCyclic_GraphWithNoNode_ReturnsFalse()
        {
            // arrange
            var mockGraph = new Mock<WeightedGraph<int>>();
            //act
            var graph = mockGraph.Object;
            graph.AddVertex(0);
            graph.AddVertex(1);
            var isCyclic = graph.IsCyclic();
            //assert
            Assert.AreEqual(false, isCyclic);
        }

        [Test]
        public void IsCyclic_NotCyclicGraph_ReturnsFalse()
        {
            // arrange
            var mockGraph = new Mock<WeightedGraph<int>>();
            //act
            var graph = mockGraph.Object;
            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddNode(0, new NodeWithCost<int>(1, 2));
            graph.AddNode(0, new NodeWithCost<int>(2, 2));
            var isCyclic = graph.IsCyclic();
            //assert
            Assert.AreEqual(false, isCyclic);
        }

        [Test]
        public void IsCyclic_CyclicGraph_ReturnsTrue()
        {
            // arrange
            var mockGraph = new Mock<WeightedGraph<int>>();
            //act
            var graph = mockGraph.Object;
            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddNode(0, new NodeWithCost<int>(1, 2));
            graph.AddNode(1, new NodeWithCost<int>(2, 5));
            graph.AddNode(2, new NodeWithCost<int>(0, 5));
            var isCyclic = graph.IsCyclic();
            //assert
            Assert.AreEqual(true, isCyclic);
        }

        [Test]
        public void AddVertex_NullReferenceTypeVertex_ThrowsCannotAddNullVertexException()
        {
            // arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            //act
            var graph = mockGraph.Object;
            string nullVertex = null;
            //assert
            var ex = Assert.Throws<GraphError>(() => graph.AddVertex(nullVertex));
            Assert.AreEqual(ErrorCode.CannotAddNullVertex, ex.ErrorCode);
        }

        [Test]
        public void AddVertex_NullNullableValueTypeVertex_ThrowsCannotAddNullVertexException()
        {
            // arrange
            var mockGraph = new Mock<WeightedGraph<int?>>();
            //act
            var graph = mockGraph.Object;
            int? nullVertex = null;
            //assert
            var ex = Assert.Throws<GraphError>(() => graph.AddVertex(nullVertex));
            Assert.AreEqual(ErrorCode.CannotAddNullVertex, ex.ErrorCode);
        }

        [Test]
        public void AddVertex_ExistingVertex_ThrowsDuplicateVertex()
        {
            // arrange
            var mockGraph = new Mock<WeightedGraph<int>>();
            //act
            var graph = mockGraph.Object;
            int vertex = 1;
            int duplicateVertex = 1;
            graph.AddVertex(vertex);
            //assert
            var ex = Assert.Throws<GraphError>(() => graph.AddVertex(duplicateVertex));
            Assert.AreEqual(ErrorCode.DuplicateVertex, ex.ErrorCode);
        }

        [Test]
        public void AddVertex_ValidParameter_AddVertexToGraph()
        {
            // arrange
            var mockGraph = new Mock<WeightedGraph<int>>();
            //act
            int vertex = 1;
            var graph = mockGraph.Object;
            graph.AddVertex(vertex);
            //assert
            var graphDictionary = graph.GetGraph();
            Assert.Contains(vertex, (graphDictionary.Keys as ICollection));
        }

        [Test]
        public void AddNode_NullNode_ThrowsCannnotAddNullNode()
        {
            //arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            NodeWithCost<string> nullNode = null;
            string sourceVertex = string.Empty;
            //act
            var graph = mockGraph.Object;
            //assert
            var ex = Assert.Throws<GraphError>(() => graph.AddNode(sourceVertex, nullNode));
            Assert.AreEqual(ErrorCode.CannotAddNullNode, ex.ErrorCode);
        }

        [Test]
        public void AddNode_NullSourceVertex_ThrowsNullSourceVertex()
        {
            //arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            string nullSourceVertex = null;
            var notNullNode = new NodeWithCost<string>(string.Empty, 1);
            //act
            var graph = mockGraph.Object;
            //assert
            var ex = Assert.Throws<GraphError>(() => graph.AddNode(nullSourceVertex, notNullNode));
            Assert.AreEqual(ErrorCode.NullSourceVertex, ex.ErrorCode);
        }

        [Test]
        public void AddNode_NullDestinationVertex_ThrowsNullDestinationVertex()
        {
            //arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            string sourceVertex = string.Empty;
            var nodeWithNullDestination = new NodeWithCost<string>(null, 1);
            //act
            var graph = mockGraph.Object;
            //assert
            var ex = Assert.Throws<GraphError>(() => graph.AddNode(sourceVertex,
                nodeWithNullDestination));
            Assert.AreEqual(ErrorCode.NullDestinationVertex, ex.ErrorCode);
        }

        [Test]
        public void AddNode_NegativeCost_ThrowsInvalidCost()
        {
            //arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            string sourceVertex = string.Empty;
            var nodeWithNegativeCost = new NodeWithCost<string>(string.Empty, -1);
            //act
            var graph = mockGraph.Object;
            //assert
            var ex = Assert.Throws<GraphError>(() => graph.AddNode(sourceVertex,
                nodeWithNegativeCost));
            Assert.AreEqual(ErrorCode.InvalidCost, ex.ErrorCode);
        }

        [Test]
        public void AddNode_SourceVertexNotExistInGraph_ThrowsSourceVertexNotExist()
        {
            //arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            string sourceVertex = string.Empty;
            var nodeWithNegativeCost = new NodeWithCost<string>(string.Empty, 1);
            //act
            var graph = mockGraph.Object;
            //assert
            var ex = Assert.Throws<GraphError>(() => graph.AddNode(sourceVertex,
                nodeWithNegativeCost));
            Assert.AreEqual(ErrorCode.SourceVertexNotExist, ex.ErrorCode);
        }

        [Test]
        public void AddNode_DestinationVertexNotExistInGraph_ThrowsDestinationVertexNotExist()
        {
            //arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            string sourceVertex = string.Empty;
            var nodeWithNegativeCost = new NodeWithCost<string>("Some String", 1);
            //act
            var graph = mockGraph.Object;
            graph.AddVertex(sourceVertex);
            //assert
            var ex = Assert.Throws<GraphError>(() => graph.AddNode(sourceVertex,
                nodeWithNegativeCost));
            Assert.AreEqual(ErrorCode.DestinationVertexNotExist, ex.ErrorCode);
        }

        [Test]
        public void AddNode_DuplicateNode_ThrowsDuplicateNode()
        {
            //arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            string sourceVertex = "Vertex";
            var node = new NodeWithCost<string>(sourceVertex, 1);
            //act
            var graph = mockGraph.Object;
            graph.AddVertex(sourceVertex);
            graph.AddNode(sourceVertex, node);
            //assert
            var ex = Assert.Throws<GraphError>(() => graph.AddNode(sourceVertex, node));
            Assert.AreEqual(ErrorCode.DuplicateNode, ex.ErrorCode);
        }

        [Test]
        public void AddNode_ValidParameters_AddNodeToSourceVertex()
        {
            //arrange
            var mockGraph = new Mock<WeightedGraph<string>>();
            string sourceVertex = "Vertex";
            var node = new NodeWithCost<string>(sourceVertex, 1);
            //act
            var graph = mockGraph.Object;
            graph.AddVertex(sourceVertex);
            graph.AddNode(sourceVertex, node);
            //assert
            var graphDictionary = graph.GetGraph();
            Assert.Contains(node, (graphDictionary[sourceVertex] as ICollection));
        }
    }
}