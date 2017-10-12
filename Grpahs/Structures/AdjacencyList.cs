using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpahs.Structures
{
    public class AdjacencyList
    {
        public class Link
        {
            public Vertex Vertex { get; set; }
            public Edge Edge { get; set; }
            public Link(Vertex vertex, Edge edge)
            {
                Vertex = vertex;
                Edge = edge;
            }
        }
        Dictionary<Vertex, List<Link>> graph;

        public AdjacencyList()
        {
            graph = new Dictionary<Vertex, List<Link>>();
        }

        public Vertex GetVertex(string name)
        {
            return graph.FirstOrDefault(x => x.Key.Name == name).Key;
        }
        public Link GetLink(Vertex fromVertex, Vertex link)
        {
            return graph[fromVertex].FirstOrDefault(x => x.Vertex == link);
        }

        public bool IsLinked(Vertex first, Vertex second)
        {
            return graph[first].FirstOrDefault(x => x.Vertex == second) != null;
        }

        public List<Link> GetLinks(Vertex fromVertex)
        {
            return graph[fromVertex];
        }

        public List<Vertex> GetVertexList()
        {
            return graph.Keys.ToList();
        }

        public void Add(Vertex vertex)
        {
            graph.Add(vertex, new List<Link>());
        }

        public void AddLink(Vertex toVertex, Vertex added, Edge edge)
        {
            graph[toVertex].Add(new Link(added, edge));
        }

        public void DelLink(Vertex fromVertex, Vertex del)
        {
            graph[fromVertex].Remove(graph[fromVertex].First(x => x.Vertex == del));
        }

        public void Del(Vertex del)
        {
            graph.Remove(del);
        }

        public bool Contains(Vertex vertex)
        {
            return graph.FirstOrDefault(x => x.Key.Name == vertex.Name).Key != null;
        }

        public bool Contains(string name)
        {
            return graph.FirstOrDefault(x => x.Key.Name == name).Key != null;
        }

        public int NumOfEdges()
        {
            List<Edge> list = new List<Edge>();
            foreach (Vertex vertex in GetVertexList())
            {
                foreach (Link link in GetLinks(vertex))
                {
                    if (list.Contains(link.Edge) == false)
                        list.Add(link.Edge);
                }
            }
            return list.Count;
        }
    }
}
