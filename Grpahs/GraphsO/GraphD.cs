using Grpahs.Exceptions;
using Grpahs.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpahs.Structures.AdjacencyList;

namespace Grpahs.Oriented
{
    public class GraphD : IGraph
    {
        AdjacencyList list = new AdjacencyList();

        public int Vertexes => list.GetVertexList().Count;

        public int Edges => list.NumOfEdges();

        public void AddEdge(string name1, string name2, int length)
        {
            if (list.Contains(name1) && list.Contains(name2))
            {
                Vertex first = list.GetVertex(name1);
                Vertex second = list.GetVertex(name2);

                if (list[first, second] == null)
                {
                    Edge edge = new Edge(second, length);
                    list.AddLink(first, edge);
                }
            }
            else
            {
                throw new VertexDoesNotExistException();
            }
        }

        public void AddVertex(string name)
        {
            if (list.Contains(name) == false)
                list.Add(new Vertex(name));
        }

        public int DelEdge(string name1, string name2)
        {
            int delLength = 0;
            if (list.Contains(name1) && list.Contains(name2))
            {
                Vertex first = list.GetVertex(name1);
                Vertex second = list.GetVertex(name2);

                if (list[first, second] != null)
                {                   
                    delLength = list[first, second].Length;

                    list.DelLink(first, second);

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
            if (list.Contains(name) == true)
            {
                Vertex del = list.GetVertex(name);
                foreach(Vertex vertex in list.GetInputLinks(del))
                {
                    list.DelLink(vertex, del);
                }
                list.Del(del);
            }
            else
            {
                throw new VertexDoesNotExistException();
            }
        }

        public int GetEdge(string name1, string name2)
        {
            int length = 0;

            if (list.Contains(name1) && list.Contains(name2))
            {
                Vertex first = list.GetVertex(name1);
                Vertex second = list.GetVertex(name2);

                if (list[first, second] != null)
                    return length = list[first, second].Length;
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
            if (list.Contains(name) != false)
                return list.GetInputLinks(list.GetVertex(name)).Count;
            else
                throw new VertexDoesNotExistException();
        }

        public List<string> GetInputVertexNames(string name)
        {
            if(list.Contains(name) != false)
                return list.GetInputLinks(list.GetVertex(name)).Select(x => x.Name).ToList();
            else
                throw new VertexDoesNotExistException();
        }

        public int GetOutputEdgeCount(string name)
        {
            if (list.Contains(name) != false)
                return list.GetLinks(list.GetVertex(name)).Count;
            else
                throw new VertexDoesNotExistException();
        }

        public List<string> GetOutputVertexNames(string name)
        {
            if (list.Contains(name) != false)
                return list.GetLinks(list.GetVertex(name)).Select(x => x.To.Name).ToList();
            else
                throw new VertexDoesNotExistException();
        }

        public void Print()
        {
            List<Vertex> visited = new List<Vertex>();
            Queue<Vertex> queue = new Queue<Vertex>();
            Vertex start = list.GetVertexList().First();

            visited.Add(start);
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                Vertex temp = queue.Dequeue();

                Console.Write(temp.Name + ": ");

                foreach (Edge edge in list.GetLinks(temp))
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
            if (list.Contains(name1) && list.Contains(name2))
            {
                Vertex first = list.GetVertex(name1);
                Vertex second = list.GetVertex(name2);

                if (list[first, second] != null)
                {
                    list[first, second].Length = length;
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
