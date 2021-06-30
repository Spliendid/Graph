using System;
using System.Collections.Generic;
using Graph;
namespace GraphDFS
{
    /// <summary>
    /// 环的监测
    /// </summary>
    class CycleDetection
    {
        private Graph.Graph G;
        private bool[] visited;
        private bool isCycle = false;
        public bool IsCycle => isCycle;
        
        public CycleDetection(Graph.Graph g)
        {
            this.G = g;
            visited = new bool[g.V];
            for (int i = 0; i < g.V; i++)
            {
                if (!visited[i])
                {
                    if (DFS(i, i))
                    {
                        isCycle = true;
                        break;
                    } 
                }
            }
        }

        private bool DFS(int v,int pre)
        {
            Console.WriteLine($"{pre} ->{v}");
            visited[v] = true;
            foreach (var w in G.GetAdj(v))
            {
                if (!visited[w])
                {
                    if (DFS(w, v))
                    {
                        return true;
                    }
                }
                else if(w!=pre)
                {
                    return true;
                }
            }

            return false;
        }


        public static void Main1(string[] arg)
        {
            Graph.Graph graph = new Graph.Graph("g.txt");
            CycleDetection cd  = new CycleDetection(graph);
            Console.WriteLine(cd.isCycle);
        }

    }
}
