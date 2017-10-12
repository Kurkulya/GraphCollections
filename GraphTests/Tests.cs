using System;
using NUnit.Framework;
using Grpahs;
using Grpahs.Exceptions;

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

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestNumVertexes(int length)
        {
            for (int i = 0; i < length; i++)
            {
                graph.AddVertex("A" + i);
            }
            Assert.AreEqual(length, graph.Vertexes);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestNumEdges(int length)
        {
            string name = "A";
            for (int i = 0; i < length; i++)
            {
                string temp = name;
                graph.AddVertex(temp);

                name = "A" + i;
                graph.AddVertex(name);

                graph.AddEdge(temp, name, 4);
            }
            Assert.AreEqual(length, graph.Edges);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestAddEdge(int length)
        {
            string name = "A";
            for (int i = 0; i < length; i++)
            {
                string temp = name;
                graph.AddVertex(temp);

                name = "A" + i;
                graph.AddVertex(name);

                graph.AddEdge(temp, name, 4);
            }

            Assert.AreEqual(length, graph.Edges);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestAddExistingEdge(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            for (int i = 0; i < length; i++)
            {
                graph.AddEdge("A", "B", 4);
            }
            Assert.AreEqual(1, graph.Edges);
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestAddEdgeEx(string name)
        {
            graph.AddVertex(name);

            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.AddEdge("A", "B", 1));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }


        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestGetEdge(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddEdge("A", "B", length);
            Assert.AreEqual(length, graph.GetEdge("A", "B"));
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestGetEdgeEx_NoVertex(string name)
        {
            graph.AddVertex(name);

            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.GetEdge("A", "B"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestGetEdgeEx_NoEdges()
        {
            graph.AddVertex("A");
            graph.AddVertex("B");

            var ex = Assert.Throws<EdgeDoesNotExistException>(() => graph.GetEdge("A", "B"));
            Assert.AreEqual(typeof(EdgeDoesNotExistException), ex.GetType());
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestDelEdge(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddEdge("A", "B", length);
            Assert.AreEqual(length, graph.DelEdge("A", "B"));
            Assert.AreEqual(0, graph.Edges);
            
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestDelEdgeEx_NoVertex(string name)
        {
            graph.AddVertex(name);

            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.DelEdge("A", "B"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestDelEdgeEx_NoEdge()
        {
            graph.AddVertex("A");
            graph.AddVertex("B");

            var ex = Assert.Throws<EdgeDoesNotExistException>(() => graph.DelEdge("A", "B"));
            Assert.AreEqual(typeof(EdgeDoesNotExistException), ex.GetType());
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestSetEdge(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddEdge("A", "B", 458);
            graph.SetEdge("A", "B", length);
            Assert.AreEqual(length, graph.GetEdge("A","B"));
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestSetEdgeEx_NoVertex(string name)
        {
            graph.AddVertex(name);

            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.SetEdge("A", "B", 2));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestSetEdgeEx_NoEdges()
        {
            graph.AddVertex("A");
            graph.AddVertex("B");

            var ex = Assert.Throws<EdgeDoesNotExistException>(() => graph.SetEdge("A", "B", 2));
            Assert.AreEqual(typeof(EdgeDoesNotExistException), ex.GetType());
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestAddVertex(int length)
        {
            for (int i = 0; i < length; i++)
            {
                graph.AddVertex("A" + i);
            }
            Assert.AreEqual(length, graph.Vertexes);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestAddSimilarVertex(int length)
        {
            for (int i = 0; i < length; i++)
            {
                graph.AddVertex("A");
            }
            Assert.AreEqual(1, graph.Vertexes);
        }

        [TestCase("A")]
        [TestCase("Bdfd")]
        [TestCase("")]
        public void TestDelVertex(string name)
        {
            graph.AddVertex(name);
            graph.DelVertex(name);
            Assert.AreEqual(0,graph.Vertexes);
        }

        [Test]
        public void TestDelVertexEx_NoVertex()
        {
            graph.AddVertex("Test");

            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.DelVertex("DelTest"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestDelVertex_Edges()
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");

            graph.AddEdge("A", "B", 1);
            graph.AddEdge("B", "C", 2);
            graph.AddEdge("C", "A", 3);

            graph.DelVertex("B");

            Assert.AreEqual(1, graph.Edges);
        }
    }
}
