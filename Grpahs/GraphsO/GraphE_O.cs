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
    public class GraphE_O : IGraph
    {
        EdgeList_O list = new EdgeList_O();

        public int Vertexes => list.Vertexes;

        public int Edges => list.Edges;

        public void AddEdge(string name1, string name2, int length)
        {
            if (list.Contains(name1) && list.Contains(name2))
            {
                Vertex first = list.GetVertex(name1);
                Vertex second = list.GetVertex(name2);

                if (list.IsLinked(first,second) == false)
                {
                    Edge edge = new Edge(length);
                    list.AddLink(first, second, edge);
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

                if (list.IsLinked(first, second) == true)
                {
                    delLength = list.GetLink(first, second).Length;

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
                list.Del(list.GetVertex(name));
            else
                throw new VertexDoesNotExistException();
        }

        public int GetEdge(string name1, string name2)
        {
            if (list.Contains(name1) && list.Contains(name2))
            {
                Vertex first = list.GetVertex(name1);
                Vertex second = list.GetVertex(name2);

                if (list.IsLinked(first, second) == true)
                    return list.GetLink(first, second).Length;
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
            Vertex start = list.GetVertexList().First();

            visited.Add(start);
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                Vertex temp = queue.Dequeue();

                Console.Write(temp.Name + ": ");

                foreach (Vertex link in list.GetLinks(temp))
                {
                    Console.Write($"{list.GetLink(temp, link).Length} to {link.Name}, ");

                    if (visited.Contains(link) == false)
                    {
                        visited.Add(link);
                        queue.Enqueue(link);
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

                if (list.IsLinked(first, second) == true)
                {
                    list.GetLink(first, second).Length = length;
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
