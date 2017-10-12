using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpahs.Structures
{
    public class EdgeList_O
    {
        public class Node
        {
            public Vertex FirstVertex { get; set; }
            public Vertex SecondVertex { get; set; }
            public Edge Edge { get; set; }

            public List<Vertex> Vertexes;

            public Node(Vertex first, Vertex second, Edge edge)
            {
                FirstVertex = first;
                SecondVertex = second;
                Edge = edge;
                Vertexes = new List<Vertex>() { FirstVertex, SecondVertex };
            }
        }

        public int Edges => list.Count;
        public int Vertexes => vertexes.Count;

        List<Node> list;
        List<Vertex> vertexes;

        public EdgeList_O()
        {
            list = new List<Node>();
            vertexes = new List<Vertex>();
        }

        public void Add(Vertex vertex)
        {
            vertexes.Add(vertex);
        }

        public bool Contains(string name)
        {
            return vertexes.FirstOrDefault(x => x.Name == name) != null;
        }

        public Vertex GetVertex(string name)
        {
            return vertexes.FirstOrDefault(x => x.Name == name);
        }

        public void AddLink(Vertex from, Vertex to, Edge edge)
        {
            list.Add(new Node(from, to, edge));
        }

        public void DelLink(Vertex from, Vertex to)
        {
            list.Remove(list.Find(x => x.FirstVertex == from && x.SecondVertex == to));
        }

        public Edge GetLink(Vertex from, Vertex to)
        {
            return list.Find(x => x.FirstVertex == from && x.SecondVertex == to).Edge;
        }

        public bool IsLinked(Vertex from, Vertex to)
        {
            return list.FirstOrDefault(x => x.FirstVertex==from && x.SecondVertex == to) != null;
        }

        public void Del(Vertex del)
        {
            list.RemoveAll(x => x.Vertexes.Contains(del));
            vertexes.Remove(del);
        }

        public List<Vertex> GetVertexList()
        {
            return vertexes;
        }

        public List<Vertex> GetLinks(Vertex vertex)
        {
            List<Vertex> links = new List<Vertex>();
            foreach (Node node in list)
            {
                if(node.FirstVertex == vertex)
                    links.Add(node.SecondVertex);
            }
            return links;
        }
    }
}
