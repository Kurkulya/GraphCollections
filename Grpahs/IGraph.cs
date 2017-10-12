using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpahs
{
    public interface IGraph
    {
        int Vertexes { get; }
        int Edges { get; }
        void AddVertex(string name);
        void AddEdge(string name1, string name2, int length);
        int DelEdge(string name1, string name2);
        void DelVertex(string name);
        void Print();
        int GetEdge(string name1, string name2);
        void SetEdge(string name1, string name2, int length);
    }
}
