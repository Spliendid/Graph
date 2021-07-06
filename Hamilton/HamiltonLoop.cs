using System;
using System.Collections.Generic;
//哈密尔顿回路
namespace Hamilton
{
    class HamiltonLoop
    {
        private Graph.Graph G;
        //上一节点,默认位-1
        private int[] Pre;
        private List<int> path;
        public List<int> Path => path;
        public bool HasPath;
        public HamiltonLoop(Graph.Graph g)
        {
            this.G = g;
            Pre = new int[g.V];
            for (int i = 0; i < g.V; i++)
            {
                Pre[i] = -1;
            }
            if (DFS(0, 0))
            {
                HasPath = true;
                GetPath();
            } 
        }

        private void GetPath()
        {
            path = new List<int>();
            int cur = Pre[0];
            while (cur!=0)
            {
                path.Add(cur);
                cur = Pre[cur];
            }
            path.Add(0);
            path.Reverse();
        }

        private bool DFS(int v,int step)
        {
            if (step == G.V  && v == 0)
            {
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

        public static void Main1(string[] arg)
        {
            Graph.Graph graph = new Graph.Graph("g.txt");
            HamiltonLoop hl = new HamiltonLoop(graph);
            if (!hl.HasPath)
            {
                Console.WriteLine("不存在哈密顿回路");
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
