using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpahs
{
    public class GraphD : IGraph
    {
        class Vertex
        {
            public string Name { get; set; }
            public Vertex(string name)
            {
                Name = name;
            }
        }

        class Edge
        {
            public int Length { get; set; }
            public Edge(int length)
            {
                Length = length;
            }
        }

        Dictionary<Vertex, Dictionary<Vertex, Edge>> graph = new Dictionary<Vertex, Dictionary<Vertex, Edge>>();

        private Vertex GetVertexByName(string name)
        {
            return graph.FirstOrDefault(x => x.Key.Name == name).Key;
        }
        private Vertex GetLinkedVertexByName(string main, string link)
        {
            return graph[GetVertexByName(main)].FirstOrDefault(x => x.Key.Name == link).Key;
        }

        private bool IsLinked(string first, string second)
        {
            return graph[GetVertexByName(first)].FirstOrDefault(x => x.Key.Name == second).Key != null;
        }

        private void AddLinkedVertex(string first, string added, int length)
        {
            graph[GetVertexByName(first)].Add(new Vertex(added), new Edge(length));
        }
        private void DelLinkedVertex(string first, string del)
        {
            graph[GetVertexByName(first)].Remove(GetLinkedVertexByName(first,del));
        }



        public void AddEdge(string name1, string name2, int length)
        {
            if (GetVertexByName(name1) != null && GetVertexByName(name2) != null)
            {
                if (IsLinked(name1, name2) == false)
                {
                    AddLinkedVertex(name1, name2, length);
                    AddLinkedVertex(name2, name1, length);
                }
            }
        }

        public void AddVertex(string name)
        {
            if (graph.FirstOrDefault(x => x.Key.Name == name).Key == null)
                graph.Add(new Vertex(name), new Dictionary<Vertex, Edge>());
        }

        public int DelEdge(string name1, string name2)
        {
            int delLength = 0;
            if (GetVertexByName(name1) != null && GetVertexByName(name2) != null)
            {
                if (IsLinked(name1, name2) == true)
                {
                    delLength = GetEdge(name1, name2);
                    DelLinkedVertex(name1, name2);
                    DelLinkedVertex(name2, name1);
                }
            }
            return delLength;
        }


        public void DelVertex(string name)
        {
            if (GetVertexByName(name) != null)
            {
                Vertex del = GetVertexByName(name);
                List<Vertex> needDel = graph[del].Keys.ToList();
                foreach (Vertex link in needDel)
                {
                    DelLinkedVertex(link.Name, del.Name);
                    DelLinkedVertex(del.Name, link.Name);
                }
                graph.Remove(GetVertexByName(del.Name));
            }
        }

        public int GetEdge(string name1, string name2)
        {
            int length = 0;

            if (GetVertexByName(name1) != null && GetVertexByName(name2) != null)
                if (IsLinked(name1, name2) == true)
                    length = graph[GetVertexByName(name1)].FirstOrDefault(x => x.Key.Name == name2).Value.Length;

            return length;
        }

        public void Print()
        {
            foreach(Vertex vertex in graph.Keys)
            {
                Console.Write(vertex.Name + ": ");
                foreach(Vertex lVertex in graph[vertex].Keys)
                {
                    Console.Write($"{graph[vertex][lVertex].Length} to {lVertex.Name}, ");
                }
                Console.WriteLine();
            }
        }

        public void SetEdge(string name1, string name2, int length)
        {
            if (GetVertexByName(name1) != null && GetVertexByName(name2) != null)
                if (IsLinked(name1, name2) == true)
                    graph[GetVertexByName(name1)].FirstOrDefault(x => x.Key.Name == name2).Value.Length = length;
        }
    }
}
