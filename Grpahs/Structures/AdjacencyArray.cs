using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpahs.Structures
{
    public class AdjacencyArray
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

        int count;
        Vertex[] array;

        public AdjacencyArray()
        {
            count = 0;
            array = new Vertex[count];
        }

        public Vertex GetVertex(string name)
        {
            return array.FirstOrDefault(x => x.Name == name);
        }

        public List<Edge> GetLinks(Vertex from)
        {
            return array.FirstOrDefault(x => x == from).Edges;
        }

        public List<Vertex> GetVertexList()
        {
            return array.ToList();
        }

        public void Add(Vertex vertex)
        {
            Vertex[] temp = new Vertex[++count];
            for (int i = 0; i < count - 1; i++)
            {
                temp[i] = array[i];
            }
            temp[count - 1] = vertex;
            array = temp;
        }

        public void AddLink(Vertex from, Edge edge)
        {
            array.FirstOrDefault(x => x == from).Edges.Add(edge);
        }

        public void DelLink(Vertex from, Vertex del)
        {
            array.FirstOrDefault(x => x == from).Edges.RemoveAll(x => x.To == del);
        }

        public List<Vertex> GetInputLinks(Vertex v)
        {
            List<Vertex> input = new List<Vertex>();
            foreach (Vertex vertex in array)
            {
                foreach (Edge edge in vertex.Edges)
                {
                    if (edge.To == v)
                        input.Add(vertex);
                }
            }
            return input;
        }

        public void Del(Vertex del)
        {
            Vertex[] temp = new Vertex[--count];
            int diff = 0;
            for (int i = 0; i < count + 1; i++)
            {
                if (array[i] == del)
                    diff = 1;
                else
                    temp[i - diff] = array[i];
            }
            array = temp;
        }

        public bool Contains(string name)
        {
            return array.FirstOrDefault(x => x.Name == name) != null;
        }

        public int NumOfEdges()
        {
            int count = 0;
            foreach (Vertex vertex in array)
            {
                count += vertex.Edges.Count;
            }
            return count;
        }


        public Edge this[Vertex from, Vertex to]
        {
            get
            {
                if (array.FirstOrDefault(x => x == from) != null)
                    return array.FirstOrDefault(x => x == from).Edges.FirstOrDefault(x => x.To == to);
                else
                    return null;
            }
        }
    }
}
