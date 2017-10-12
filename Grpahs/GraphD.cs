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

                if (list.IsLinked(first, second) == false)
                {
                    Edge edge = new Edge(length);
                    list.AddLink(first, second, edge);
                    list.AddLink(second, first, edge);//oriented
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
                    delLength = list.GetLink(first, second).Edge.Length;

                    list.DelLink(first, second);
                    list.DelLink(second, first); //oriented

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
                List<Link> needDel = list.GetLinks(del);
                int len = needDel.Count;
                for (int i =0;i < len; i++)
                {
                    list.DelLink(needDel.First().Vertex, del); //oriented
                    list.DelLink(del, needDel.First().Vertex);
                }
                list.Del(del);
                #region Oriented
                /*
                foreach (Vertex vertex in list.GetVertexList())
                {
                    List<Link> delList = list.GetLinks(vertex);
                    int length = needDel.Count;
                    for (int i = 0; i < len; i++)
                    {
                        if (delList[i].Vertex == del)
                        {
                            list.DelLink(vertex, del);
                        }
                    }
                    Console.WriteLine();
                }
                */
                #endregion
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

                if (list.IsLinked(first, second) == true)
                    return length = list.GetLink(first, second).Edge.Length;
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
            foreach(Vertex vertex in list.GetVertexList())
            {
                Console.Write(vertex.Name + ": ");
                foreach(Link link in list.GetLinks(vertex))
                {
                    Console.Write($"{link.Edge.Length} to {link.Vertex.Name}, ");
                }
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
                    list.GetLink(first, second).Edge.Length = length;
                    list.GetLink(second, first).Edge.Length = length; //oriented
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
