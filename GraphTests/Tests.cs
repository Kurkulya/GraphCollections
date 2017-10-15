using System;
using NUnit.Framework;
using Grpahs;
using Grpahs.Exceptions;
using Grpahs.Oriented;
using System.Collections.Generic;
using System.Linq;

namespace GraphTests
{

    [TestFixture(typeof(GraphD))]
    [TestFixture(typeof(GraphM))]
    [TestFixture(typeof(GraphH))]
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
        public void TestInputEdgeCount(int count)
        {
            graph.AddVertex("A");
            for (int i = 0; i < count; i++)
            {
                graph.AddVertex("A" + i);
                graph.AddEdge("A" + i, "A", 4);
            }
            Assert.AreEqual(count, graph.GetInputEdgeCount("A"));
        }


        [Test]
        public void TestInputEdgeCountEx()
        {
            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.GetInputEdgeCount("A"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestOutputEdgeCountEx()
        {
            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.GetOutputEdgeCount("A"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestInputVertexNamesEx()
        {
            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.GetInputVertexNames("A"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestOutputVertexNamesEx()
        {
            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.GetOutputVertexNames("A"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [TestCase("A")]
        [TestCase("A B")]
        [TestCase("A B C D E")]
        public void TestInputVertexNames(string names)
        {                      
            List<string> result = names.Split(' ').ToList();

            graph.AddVertex("T");

            foreach(string name in result)
            {
                graph.AddVertex(name);             
                graph.AddEdge(name, "T", 4);
            }
            Assert.AreEqual(result, graph.GetInputVertexNames("T"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestOutputEdgeCount(int count)
        {
            graph.AddVertex("A");
            for (int i = 0; i < count; i++)
            {
                graph.AddVertex("A" + i);
                graph.AddEdge("A", "A" + i, 4);
            }
            Assert.AreEqual(count, graph.GetOutputEdgeCount("A"));
        }

        [TestCase("A")]
        [TestCase("A B")]
        [TestCase("A B C D E")]
        public void TestOutputVertexNames(string names)
        {
            List<string> result = names.Split(' ').ToList();

            graph.AddVertex("T");

            foreach (string name in result)
            {
                graph.AddVertex(name);
                graph.AddEdge("T", name, 4);
            }
            Assert.AreEqual(result, graph.GetOutputVertexNames("T"));
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



        [TestCase("1","6",new string[] {"1","3","6"})]
        [TestCase("1", "5", new string[] { "1", "3", "4","5" })]
        [TestCase("1", "4", new string[] { "1", "3", "4" })]
        [TestCase("1", "3", new string[] { "1", "3" })]
        [TestCase("1", "2", new string[] { "1", "2"})]
        [TestCase("2", "3", new string[] { "2", "3" })]
        [TestCase("2", "4", new string[] { "2", "4"})]
        [TestCase("2", "5", new string[] { "2", "4", "5" })]
        [TestCase("2", "6", new string[] { "2", "3", "6" })]
        [TestCase("3", "4", new string[] { "3", "4" })]
        [TestCase("3", "5", new string[] { "3", "4","5" })]
        [TestCase("3", "6", new string[] { "3", "6" })]
        [TestCase("4", "5", new string[] { "4", "5"})]
        [TestCase("4", "6", new string[] { "4", "5", "6" })]
        [TestCase("5", "6", new string[] { "5", "6" })]
        public void TestShortestPath(string from, string to, string[] result)
        {
            graph.AddVertex("1");
            graph.AddVertex("2");
            graph.AddVertex("3");
            graph.AddVertex("4");
            graph.AddVertex("5");
            graph.AddVertex("6");
            graph.AddEdge("1", "2", 7);
            graph.AddEdge("2", "4", 15);
            graph.AddEdge("2", "3", 10);
            graph.AddEdge("1", "3", 9);
            graph.AddEdge("1", "6", 14);
            graph.AddEdge("3", "6", 2);
            graph.AddEdge("3", "4", 11);
            graph.AddEdge("4", "5", 6);
            graph.AddEdge("5", "6", 9);

            Assert.AreEqual(result.ToList(), graph.GetShortestPath(from, to));
        }

        [TestCase("1", "1")]
        [TestCase("2", "2")]
        [TestCase("3", "3")]
        [TestCase("4", "4")]
        [TestCase("5", "5")]
        [TestCase("6", "6")]
        public void TestShortestPath_Similar(string from, string to)
        {
            graph.AddVertex("1");
            graph.AddVertex("2");
            graph.AddVertex("3");
            graph.AddVertex("4");
            graph.AddVertex("5");
            graph.AddVertex("6");
            graph.AddEdge("1", "2", 7);
            graph.AddEdge("2", "4", 15);
            graph.AddEdge("2", "3", 10);
            graph.AddEdge("1", "3", 9);
            graph.AddEdge("1", "6", 14);
            graph.AddEdge("3", "6", 2);
            graph.AddEdge("3", "4", 11);
            graph.AddEdge("4", "5", 6);
            graph.AddEdge("5", "6", 9);

            Assert.AreEqual(0, graph.GetShortestPath(from, to).Count);
        }

        [TestCase("6", "1")]
        [TestCase("6", "2")]
        [TestCase("6", "3")]
        [TestCase("6", "4")]
        [TestCase("6", "5")]
        [TestCase("5", "1")]
        [TestCase("5", "2")]
        [TestCase("5", "3")]
        [TestCase("5", "4")]
        [TestCase("4", "1")]
        [TestCase("4", "2")]
        [TestCase("4", "3")]
        [TestCase("3", "1")]
        [TestCase("3", "2")]
        [TestCase("2", "1")]
        public void TestShortestPathEx(string from, string to)
        {
            graph.AddVertex("1");
            graph.AddVertex("2");
            graph.AddVertex("3");
            graph.AddVertex("4");
            graph.AddVertex("5");
            graph.AddVertex("6");
            graph.AddEdge("1", "2", 7);
            graph.AddEdge("2", "4", 15);
            graph.AddEdge("2", "3", 10);
            graph.AddEdge("1", "3", 9);
            graph.AddEdge("1", "6", 14);
            graph.AddEdge("3", "6", 2);
            graph.AddEdge("3", "4", 11);
            graph.AddEdge("4", "5", 6);
            graph.AddEdge("5", "6", 9);

            var ex = Assert.Throws<PathDoesNotExistException>(() => graph.GetShortestPath(from, to));
            Assert.AreEqual(typeof(PathDoesNotExistException), ex.GetType());
        }
    }
}
