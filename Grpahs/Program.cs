using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpahs
{
    class Program
    {
        static void Main(string[] args)
        {
            GraphD graph = new GraphD();
            graph.AddVertex("K");
            graph.AddVertex("F");
            graph.AddVertex("D");
            graph.AddVertex("E");
            graph.AddVertex("S");
            graph.AddEdge("K", "F", 1);
            graph.AddEdge("F", "D", 2);
            graph.AddEdge("D", "E", 3);
            graph.AddEdge("E", "S", 4);
            graph.AddEdge("S", "K", 5);
            graph.DelVertex("D");
            graph.Print();
        }
    }
}
