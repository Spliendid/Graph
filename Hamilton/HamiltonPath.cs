using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamilton
{
    //求解哈密尔顿路径
    class HamiltonPath
    {
        private Graph.Graph G;
        //上一节点,默认位-1
        private int[] Pre;
        private List<int> path;
        public List<int> Path => path;
        public bool HasPath;
        private int orign;
        private int end = -1;
        public HamiltonPath(Graph.Graph g,int ori)
        {
            this.G = g;
            this.orign = ori;
            Pre = new int[g.V];
            for (int i = 0; i < g.V; i++)
            {
                Pre[i] = -1;
            }
            Pre[orign] = orign;
            if (DFS(orign, 0))
            {
                HasPath = true;
                GetPath();
            }
        }

        private void GetPath()
        {
            path = new List<int>();
            int cur = end;
            while (cur != orign)
            {
                path.Add(cur);
                cur = Pre[cur];
            }
            path.Add(orign);
            path.Reverse();
        }

        private bool DFS(int v, int step)
        {
            if (step == G.V-1)
            {
                end = v;
                return true;
            }

            foreach (var w in G.GetAdj(v))
            {
                if (Pre[w] == -1)
                {
                    Pre[w] = v;
                    if (DFS(w, step + 1))
                    {
                        return true;
                    }
                    Pre[w] = -1;
                }
            }

            return false;
        }

        public static void Main(string[] arg)
        {
            Graph.Graph graph = new Graph.Graph("g.txt");
            HamiltonPath hl = new HamiltonPath(graph,3);
            if (!hl.HasPath)
            {
                Console.WriteLine("不存在哈密顿路径");
            }
            else
            {
                foreach (var item in hl.Path)
                {
                    Console.WriteLine(item);
                }
            }

        }
    }
}
