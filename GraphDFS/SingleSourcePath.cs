using System;
using System.Collections.Generic;
using System.Collections;
using Graph;
namespace GraphDFS
{
    class SingleSourcePath
    {
        private Graph.Graph G;
        private int S;
        private int[] pre;//遍历时的先导
        public SingleSourcePath(Graph.Graph g,int s)
        {

            this.S = s;
            this.G = g;
            G.ValidateVertex(s);
            pre = new int[G.V];
            for (int i = 0; i < G.V; i++)
            {
                pre[i] = -1;
            }
            DFS(this.S,s);
        }

        private void DFS(int v,int pre)
        {

            this.pre[v] = pre;
            foreach (var w in G.GetAdj(v))
            {
                if (this.pre[w] == -1)
                {
                    DFS(w,v);
                }
            }
        }

        //t节点是否和s节点连接
        public bool isConnected(int t)
        {
            G.ValidateVertex(t);
            return pre[t] != -1;
        }

        public List<int> path(int t)
        {
            List<int> p = new List<int>();
            if (isConnected(t))
            {
                int cur = t;
                while (cur!=S)
                {
                    p.Add(cur);
                    cur = pre[cur];
                }
                p.Add(S);
            }
            p.Reverse();
            return p;
        }

        public static void Main1(string[] arg)
        {
            Graph.Graph graph = new Graph.Graph("g.txt");
            SingleSourcePath singleSourcePath= new SingleSourcePath(graph,0);
            var ls = singleSourcePath.path(6);
            Console.WriteLine("0-->6");
            for (int i = 0; i < ls.Count; i++)
            {
                Console.WriteLine(ls[i]);
            }
        }

 
    }
}
