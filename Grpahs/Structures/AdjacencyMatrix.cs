using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpahs.Structures
{
    public class AdjacencyMatrix
    {
        public class Vertex
        {
            public string Name { get; set; }
            public Vertex(string name)
            {
                Name = name;
            }
        }

        public class Edge
        {
            public int Length { get; set; }
            public Edge(int length)
            {
                Length = length;
            }
        }

        int count;
        List<Vertex> vertexIndex;
        Edge[,] matrix;


        public int NumOfEdges()
        {
            int edges = 0;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (matrix[i, j] != null)
                        edges++;
                }
            }
            return edges;
        }


        public AdjacencyMatrix()
        {
            count = 0;
            vertexIndex = new List<Vertex>();
            matrix = new Edge[count, count];
        }

        public bool Contains(string name)
        {
            return vertexIndex.FirstOrDefault(x => x.Name == name) != null;
        }

        public Vertex GetVertex(string name)
        {
            return vertexIndex.FirstOrDefault(x => x.Name == name);
        }

        public List<Vertex> GetInputLinks(Vertex v)
        {
            List<Vertex> links = new List<Vertex>();
            for (int i = 0; i < count; i++)
            {
                if (matrix[i, vertexIndex.IndexOf(v)] != null)
                    links.Add(vertexIndex[i]);
            }
            return links;
        }

        public List<Vertex> GetVertexList()
        {
            return vertexIndex;
        }

        public List<Vertex> GetLinks(Vertex fromVertex)
        {
            List<Vertex> links = new List<Vertex>();
            for (int i = 0; i < count; i++)
            {
                if (matrix[vertexIndex.IndexOf(fromVertex), i] != null)
                    links.Add(vertexIndex[i]);
            }
            return links;
        }

        public void Add(Vertex vertex)
        {
            vertexIndex.Add(vertex);
            ++count;
            Edge[,] temp = new Edge[count, count];
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - 1; j++)
                {
                    temp[i, j] = matrix[i, j];
                }
            }
            matrix = temp;
        }

        public void Del(Vertex vertex)
        {           
            --count;
            Edge[,] temp = new Edge[count, count];
            int diff = vertexIndex.IndexOf(vertex);
            for (int i = 0; i < count + 1; i++)
            {
                for (int j = 0; j < count + 1; j++)
                {
                    if (i != diff && j != diff)
                    {
                        temp[i - ((i > diff) ? 1 : 0),
                             j - ((j > diff) ? 1 : 0)] = matrix[i,j];
                    }
                }
            }
            matrix = temp;
            vertexIndex.Remove(vertex);
        }

        public Edge this[Vertex from, Vertex to]
        {
            get
            {
                return matrix[vertexIndex.IndexOf(from), vertexIndex.IndexOf(to)];
            }
            set
            {
                matrix[vertexIndex.IndexOf(from), vertexIndex.IndexOf(to)] = value;
            }
        }
       
    }
}
