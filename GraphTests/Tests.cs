using System;
using NUnit.Framework;
using Grpahs;

namespace GraphTests
{
    [TestFixture(typeof(GraphD))]
    public class Tests<TGraph> where TGraph : IGraph, new()
    {
        IGraph graph;

        [SetUp]
        public void SetUp()
        {
            graph = new TGraph();
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestAddEdges(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddEdge("A", "B", length);
            Assert.AreEqual(length, graph.GetEdge("A", "B"));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestGetEdges(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddEdge("A", "B", length);
            Assert.AreEqual(length, graph.GetEdge("A", "B"));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestDelEdges(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddEdge("A", "B", length);
            Assert.AreEqual(length, graph.DelEdge("A", "B"));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestSetEdges(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddEdge("A", "B", 458);
            graph.SetEdge("A", "B", length);
            Assert.AreEqual(length, graph.GetEdge("A","B"));
        }
    }
}
