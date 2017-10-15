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


        private Vertex GetMin(List<Vertex> unvis, Dictionary<Vertex, int> lengths, int max)
        {
            int min = max;
            Vertex ret = null;
            foreach(Vertex v in unvis)
            {
                if(lengths[v] <= min)
                {
                    ret = v;
                    min = lengths[v];
                }
            }
            return ret;
        }

        public List<string> GetShortestPath(string from, string to)
        {
            if (from == to)
                return new List<string>();

            const int MAX = 10000;
            List<Vertex> unvisited = list.GetVertexList();
            Dictionary<Vertex, int> lengths = new Dictionary<Vertex,int>();
            Dictionary<Vertex,Vertex> path = new Dictionary<Vertex, Vertex>();



            foreach(Vertex v in list.GetVertexList())
            {
                if(v == list.GetVertex(from))
                    lengths.Add(v, 0);
                else
                    lengths.Add(v, MAX);
            }

            while(unvisited.Count != 0)
            {
                Vertex temp = GetMin(unvisited, lengths, MAX);

                if (lengths[temp] == MAX)
                    break;

                unvisited.Remove(temp);

                foreach(Edge edge in list.GetLinks(temp))
                {
                    if (lengths[temp] + edge.Length < lengths[edge.To])
                    {
                        lengths[edge.To] = lengths[temp] + edge.Length;
                        if(path.ContainsKey(edge.To))
                            path[edge.To] = temp;
                        else
                            path.Add(edge.To, temp);
                    }
                }
            }

            List<string> finalPath = new List<string>();
            Vertex source = list.GetVertex(to);
            while(source != list.GetVertex(from))
            {
                finalPath.Add(source.Name);
                if (path.ContainsKey(source) != false)
                    source = path[source];
                else
                    throw new PathDoesNotExistException();
            }
            finalPath.Add(from);
            finalPath.Reverse();
            return finalPath;
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
