using Grpahs.Exceptions;
using Grpahs.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpahs.Structures.AdjacencyMatrix;

namespace Grpahs.Oriented
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

        public int GetInputEdgeCount(string name)
        {
            if (matrix.Contains(name) != false)
                return matrix.GetInputLinks(matrix.GetVertex(name)).Count;
            else
                throw new VertexDoesNotExistException();
        }

        public List<string> GetInputVertexNames(string name)
        {
            if (matrix.Contains(name) != false)
                return matrix.GetInputLinks(matrix.GetVertex(name)).Select(x => x.Name).ToList();
            else
                throw new VertexDoesNotExistException();
        }

        public int GetOutputEdgeCount(string name)
        {
            if (matrix.Contains(name) != false)
                return matrix.GetLinks(matrix.GetVertex(name)).Count;
            else
                throw new VertexDoesNotExistException();
        }

        public List<string> GetOutputVertexNames(string name)
        {
            if (matrix.Contains(name) != false)
                return matrix.GetLinks(matrix.GetVertex(name)).Select(x => x.Name).ToList();
            else
                throw new VertexDoesNotExistException();
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
            if (matrix.Contains(name1) && matrix.Contains(name2))
            {
                Vertex first = matrix.GetVertex(name1);
                Vertex second = matrix.GetVertex(name2);

                if (matrix[first, second] != null)
                {
                    matrix[first, second].Length = length;
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


        private Vertex GetMin(List<Vertex> unvis, Dictionary<Vertex, int> lengths, int max)
        {
            int min = max;
            Vertex ret = null;
            foreach (Vertex v in unvis)
            {
                if (lengths[v] <= min)
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
            List<Vertex> unvisited = matrix.GetVertexList();
            Dictionary<Vertex, int> lengths = new Dictionary<Vertex, int>();
            Dictionary<Vertex, Vertex> path = new Dictionary<Vertex, Vertex>();



            foreach (Vertex v in matrix.GetVertexList())
            {
                if (v == matrix.GetVertex(from))
                    lengths.Add(v, 0);
                else
                    lengths.Add(v, MAX);
            }

            while (unvisited.Count != 0)
            {
                Vertex temp = GetMin(unvisited, lengths, MAX);

                if (lengths[temp] == MAX)
                    break;

                unvisited.Remove(temp);

                foreach (Vertex tempTo in matrix.GetLinks(temp))
                {
                    if (lengths[temp] + matrix[temp,tempTo].Length < lengths[tempTo])
                    {
                        lengths[tempTo] = lengths[temp] + matrix[temp, tempTo].Length;
                        if (path.ContainsKey(tempTo))
                            path[tempTo] = temp;
                        else
                            path.Add(tempTo, temp);
                    }
                }
            }

            List<string> finalPath = new List<string>();
            Vertex source = matrix.GetVertex(to);
            while (source != matrix.GetVertex(from))
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
    }
}
