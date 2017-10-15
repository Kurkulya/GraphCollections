using Grpahs.Exceptions;
using Grpahs.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpahs.Structures.AdjacencyArray;

namespace Grpahs.Oriented
{
    public class GraphH : IGraph
    {
        AdjacencyArray array = new AdjacencyArray();

        public int Vertexes => array.GetVertexList().Count;

        public int Edges => array.NumOfEdges();

        public void AddEdge(string name1, string name2, int length)
        {
            if (array.Contains(name1) && array.Contains(name2))
            {
                Vertex first = array.GetVertex(name1);
                Vertex second = array.GetVertex(name2);

                if (array[first, second] == null)
                {
                    Edge edge = new Edge(second, length);
                    array.AddLink(first, edge);
                }
            }
            else
            {
                throw new VertexDoesNotExistException();
            }
        }

        public void AddVertex(string name)
        {
            if (array.Contains(name) == false)
                array.Add(new Vertex(name));
        }

        public int DelEdge(string name1, string name2)
        {
            int delLength = 0;
            if (array.Contains(name1) && array.Contains(name2))
            {
                Vertex first = array.GetVertex(name1);
                Vertex second = array.GetVertex(name2);

                if (array[first, second] != null)
                {
                    delLength = array[first, second].Length;

                    array.DelLink(first, second);

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
            if (array.Contains(name) == true)
            {
                Vertex del = array.GetVertex(name);
                foreach (Vertex vertex in array.GetInputLinks(del))
                {
                    array.DelLink(vertex, del);
                }
                array.Del(del);
            }
            else
            {
                throw new VertexDoesNotExistException();
            }
        }

        public int GetEdge(string name1, string name2)
        {
            int length = 0;

            if (array.Contains(name1) && array.Contains(name2))
            {
                Vertex first = array.GetVertex(name1);
                Vertex second = array.GetVertex(name2);

                if (array[first, second] != null)
                    return length = array[first, second].Length;
                else
                    throw new EdgeDoesNotExistException();
            }
            else
            {
                throw new VertexDoesNotExistException();
            }
        }

        public int GetInputEdgeCount(string name)
        {
            if (array.Contains(name) != false)
                return array.GetInputLinks(array.GetVertex(name)).Count;
            else
                throw new VertexDoesNotExistException();
        }

        public List<string> GetInputVertexNames(string name)
        {
            if (array.Contains(name) != false)
                return array.GetInputLinks(array.GetVertex(name)).Select(x => x.Name).ToList();
            else
                throw new VertexDoesNotExistException();
        }

        public int GetOutputEdgeCount(string name)
        {
            if (array.Contains(name) != false)
                return array.GetLinks(array.GetVertex(name)).Count;
            else
                throw new VertexDoesNotExistException();
        }

        public List<string> GetOutputVertexNames(string name)
        {
            if (array.Contains(name) != false)
                return array.GetLinks(array.GetVertex(name)).Select(x => x.To.Name).ToList();
            else
                throw new VertexDoesNotExistException();
        }

        public void Print()
        {
            List<Vertex> visited = new List<Vertex>();
            Queue<Vertex> queue = new Queue<Vertex>();
            Vertex start = array.GetVertexList().First();

            visited.Add(start);
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                Vertex temp = queue.Dequeue();

                Console.Write(temp.Name + ": ");

                foreach (Edge edge in array.GetLinks(temp))
                {
                    Console.Write($"{edge.Length} to {edge.To.Name}, ");

                    if (visited.Contains(edge.To) == false)
                    {
                        visited.Add(edge.To);
                        queue.Enqueue(edge.To);
                    }
                }
                visited.Add(temp);
                Console.WriteLine();
            }
        }

        public void SetEdge(string name1, string name2, int length)
        {
            if (array.Contains(name1) && array.Contains(name2))
            {
                Vertex first = array.GetVertex(name1);
                Vertex second = array.GetVertex(name2);

                if (array[first, second] != null)
                {
                    array[first, second].Length = length;
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
