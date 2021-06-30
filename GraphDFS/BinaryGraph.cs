using System;
using System.Collections.Generic;
using Graph;
namespace GraphDFS
{
    /// <summary>
    /// 判断图是否是二分图
    /// </summary>
    class BinaryGraph
    {
        private Graph.Graph G;
        //访问标记 0--未访问 -1,1 分别代表一种颜色
        private int[] visited;
        private bool isBinary = true;
        public bool IsBinary => isBinary;
        public BinaryGraph(Graph.Graph g)
        {
            this.G = g;
            visited = new int[g.V];
            for (int i = 0; i < g.V; i++)
            {
                visited[i] = 0;
            }
            for (int i = 0; i < g.V; i++)
            {
                if (visited[i] == 0)
                {
                    if (!DFS(i,1))
                    {
                        isBinary = false;
                        break;
                    }
                }
            }

            foreach (var item in visited)
            {
                Console.WriteLine(item);
            }
        }

        private bool DFS(int v,int col)
        {
            visited[v] = col;
            foreach (var w in G.GetAdj(v))
            {
                if (visited[w] == 0)
                {
                    if (!DFS(w, -col))
                    {
                        return false;
                    }
                }
                else if (col == visited[w])
                {
                    return false;
                }
            }

            return true;
        }

        public static void Main(string[] arg)
        {
            Graph.Graph graph = new Graph.Graph("g.txt");
            BinaryGraph bg = new BinaryGraph(graph);
            Console.WriteLine(bg.isBinary);
        }

    }
}
