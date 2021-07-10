using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectedGraph
{
    class TopoSortPostorderDFS
    {
        private Graph G;
        private List<int> res;
        private bool hasCycle;
        public bool HasCycle => hasCycle;
        private bool[] visited;

        public TopoSortPostorderDFS(Graph graph)
        {
            this.G = graph;

            res = new List<int>();
            if (!G.IsDirected)
            {
                throw new Exception("TopoSort only works in directed graph.");
            }

            DirctedCycleDetection dc = new DirctedCycleDetection(graph);
            hasCycle = dc.IsCycle;

            if (hasCycle)
            {
                return;
            }

            visited = new bool[graph.V];

            for (int i = 0; i < G.V; i++)
            {
                if (!visited[i])
                {
                    DFS(i);
                }
            }
         
            res.Reverse();
        }

        public void DFS(int v)
        {
            visited[v] = true;
            foreach (var m in G.GetAdj(v))
            {
                if (!visited[m])
                {
                    DFS(m);
                }
            }

            res.Add(v);
            
        }

        public List<int> Result()
        {
            return res;
        }

        public static void Main(string[] arg)
        {
            int a = 10,b = 3,c = 4;
            Console.WriteLine($"{a/b},{a/c},{b/c}");
            Graph graph = new Graph("_g.txt", true);
            TopoSortPostorderDFS ts = new TopoSortPostorderDFS(graph);
            if (!ts.hasCycle)
            {
                foreach (var item in ts.Result())
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("该图存在环,不能进行拓扑排序");
            }
        }
    }
}
