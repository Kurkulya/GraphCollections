using Grpahs.Exceptions;
using Grpahs.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpahs.Structures.AdjacencyList;

namespace Grpahs
{
    public class GraphM : IGraph
    {
        AdjacencyMatrix matrix = new AdjacencyMatrix();

        public int Vertexes => matrix.GetVertexList().Count;

        public int Edges => matrix.NumOfEdges();

        public void AddEdge(string name1, string name2, int length)
        {
            if (matrix.Contains(name1) && matrix.Contains(name2))
            {
                Vertex first = matrix.GetVertex(name1);
                Vertex second = matrix.GetVertex(name2);

                if (matrix[first,second] == null)
                {
                    Edge edge = new Edge(length);
                    matrix[first, second] = edge;
                    matrix[second, first] = edge; //oriented
                }
            }
            else
            {
                throw new VertexDoesNotExistException();
            }
        }

        public void AddVertex(string name)
        {
            if (matrix.Contains(name) == false)
                matrix.Add(new Vertex(name));
        }

        public int DelEdge(string name1, string name2)
        {
            int delLength = 0;
            if (matrix.Contains(name1) && matrix.Contains(name2))
            {
                Vertex first = matrix.GetVertex(name1);
                Vertex second = matrix.GetVertex(name2);

                if (matrix[first, second] != null)
                {
                    delLength = matrix[first, second].Length;

                    matrix[first, second] = null;
                    matrix[second, first] = null; //oriented

                    return delLength;
                }
                else
                {
                    throw new EdgeDoesNotExistException();
                }
            }
            else
            {
                throw new VertexDoesNotExistException();
            }         
        }


        public void DelVertex(string name)
        {
            if (matrix.Contains(name) == true)
                matrix.Del(matrix.GetVertex(name));
            else
                throw new VertexDoesNotExistException();
        }

        public int GetEdge(string name1, string name2)
        {
            if (matrix.Contains(name1) && matrix.Contains(name2))
            {
                Vertex first = matrix.GetVertex(name1);
                Vertex second = matrix.GetVertex(name2);

                if (matrix[first, second] != null)
                    return matrix[first, second].Length;
                else
                    throw new EdgeDoesNotExistException();
            }
            else
            {
                throw new VertexDoesNotExistException();
            }            
        }

        public void Print()
        {
            List<Vertex> visited = new List<Vertex>();
            Queue<Vertex> queue = new Queue<Vertex>();
            Vertex start = matrix.GetVertexList().First();

            visited.Add(start);
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                Vertex temp = queue.Dequeue();

                Console.Write(temp.Name + ": ");

                foreach (Vertex link in matrix.GetLinks(temp))
                {
                    Console.Write($"{matrix[temp, link].Length} to {link.Name}, ");

                    if (visited.Contains(link) == false)
                        queue.Enqueue(link);
                }
                visited.Add(temp);
                Console.WriteLine();
            }
        }


        public void SetEdge(string name1, string name2, int length)
        {
            if (matrix.Contains(name1) && matrix.Contains(name2))
            {
                Vertex first = matrix.GetVertex(name1);
                Vertex second = matrix.GetVertex(name2);

                if (matrix[first, second] != null)
                {
                    matrix[first, second].Length = length;
                    matrix[second, first].Length = length; //oriented
                }
                else
                {
                    throw new EdgeDoesNotExistException();
                }
            }
            else
            {
                throw new VertexDoesNotExistException();
            }
        }
    }
}
