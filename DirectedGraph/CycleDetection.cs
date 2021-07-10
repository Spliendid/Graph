using System;
using System.Collections.Generic;
namespace DirectedGraph
{
    /// <summary>
    /// 环的监测
    /// </summary>
    class DirctedCycleDetection
    {
        private Graph G;
        private bool[] visited;
        private bool[] onPath;
        private bool isCycle = false;
        public bool IsCycle => isCycle;
        
        public DirctedCycleDetection(Graph g)
        {
            if (!g.IsDirected)
            {
                throw new Exception("只支持有向图的换检测");
            }
            this.G = g;
            visited = new bool[g.V];
            onPath = new bool[g.V];
            for (int i = 0; i < g.V; i++)
            {
                if (!visited[i])
                {
                    if (DFS(i))
                    {
                        isCycle = true;
                        break;
                    } 
                }
            }
        }

        private bool DFS(int v)
        {
            //Console.WriteLine($"{v}");
            visited[v] = true;
            onPath[v] = true;
            foreach (var w in G.GetAdj(v))
            {
                if (!visited[w])
                {
                    if (DFS(w))
                    {
                        return true;
                    }
                }
                else if(onPath[w])
                {
                    return true;
                }
            }
            onPath[v] = false;
            return false;
        }


        public static void Main1(string[] arg)
        {
            Graph graph = new Graph("_g.txt",true);
            DirctedCycleDetection cd  = new DirctedCycleDetection(graph);
            Console.WriteLine(cd.isCycle);
        }

    }
}
