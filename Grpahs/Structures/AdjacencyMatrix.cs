using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpahs.Structures
{
    public class AdjacencyMatrix
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
            return edges / 2;
        }

        public int NumOfEdgesOriented()
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

        int count;
        Dictionary<Vertex, int> vertexIndex;
        Edge[,] matrix;



        public AdjacencyMatrix()
        {
            count = 0;
            vertexIndex = new Dictionary<Vertex, int>();
            matrix = new Edge[count, count];
        }

        public bool Contains(string name)
        {
            return vertexIndex.FirstOrDefault(x => x.Key.Name == name).Key != null;
        }

        public Vertex GetVertex(string name)
        {
            return vertexIndex.FirstOrDefault(x => x.Key.Name == name).Key;
        }

        public List<Vertex> GetVertexList()
        {
            return vertexIndex.Keys.ToList();
        }

        public List<Vertex> GetLinks(Vertex fromVertex)
        {
            List<Vertex> links = new List<Vertex>();
            for (int i = 0; i < count; i++)
            {
                if (matrix[vertexIndex[fromVertex], i] != null)
                    links.Add(vertexIndex.Keys.ElementAt(i));
            }
            return links;
        }

        public void Add(Vertex vertex)
        {
            vertexIndex.Add(vertex, count);
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
            int diff = vertexIndex[vertex];
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

            int value = vertexIndex[vertex];
            for(int i = value;i < vertexIndex.Count; i++)         
            {
                if (vertexIndex.ElementAt(i).Value > value)
                    vertexIndex[vertexIndex.ElementAt(i).Key]--;
            }
            vertexIndex.Remove(vertex);
        }

        public Edge this[Vertex vertex1, Vertex vertex2]
        {
            get
            {
                return matrix[vertexIndex[vertex1], vertexIndex[vertex2]];
            }
            set
            {
                matrix[vertexIndex[vertex1], vertexIndex[vertex2]] = value;
            }
        }
       
    }
}
