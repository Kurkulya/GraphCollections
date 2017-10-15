using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpahs.Structures
{
    public class AdjacencyList
    {
        public class Edge
        {
            public Vertex To { get; set; }
            public int Length { get; set; }
            public Edge(Vertex to, int length)
            {
                To = to;
                Length = length;
            }
        }


        public class Vertex
        {
            public string Name { get; set; }
            public List<Edge> Edges { get; set; }
            public Vertex(string name)
            {
                Name = name;
                Edges = new List<Edge>();
            }
        }

        List<Vertex> list;

        public AdjacencyList()
        {
            list = new List<Vertex>();
        }

        public Vertex GetVertex(string name)
        {
            return list.FirstOrDefault(x => x.Name == name);
        }

        public List<Edge> GetLinks(Vertex from)
        {
            return list.Find(x => x == from).Edges;
        }

        public List<Vertex> GetVertexList()
        {
            return new List<Vertex>(list);
        }

        public void Add(Vertex vertex)
        {
            list.Add(vertex);
        }

        public void AddLink(Vertex from, Edge edge)
        {
            list.Find(x => x == from).Edges.Add(edge);
        }

        public void DelLink(Vertex from, Vertex del)
        {
            list.Find(x => x == from).Edges.RemoveAll(x => x.To == del);
        }

        public List<Vertex> GetInputLinks(Vertex v)
        {
            List<Vertex> input = new List<Vertex>();
            foreach (Vertex vertex in list)
            {
                foreach(Edge edge in vertex.Edges)
                {
                    if (edge.To == v)
                        input.Add(vertex);
                }
            }
            return input;
        }

        public void Del(Vertex del)
        {
            list.Remove(del);
        }

        public bool Contains(string name)
        {
            return list.FirstOrDefault(x => x.Name == name) != null;
        }

        public int NumOfEdges()
        {
            int count = 0;
            foreach (Vertex vertex in list)
            {
                count += vertex.Edges.Count;
            }
            return count;
        }


        public Edge this[Vertex from, Vertex to]
        {
            get
            {
                if (list.FirstOrDefault(x => x == from) != null)
                    return list.FirstOrDefault(x => x == from).Edges.FirstOrDefault(x => x.To == to);
                else
                    return null;                       
            }
        }
    }
}
