﻿using Grpahs.Exceptions;
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
    }
}