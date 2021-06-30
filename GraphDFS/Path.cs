using System;
using System.Collections.Generic;
using System.Collections;
using Graph;
namespace GraphDFS
{
    class Path
    {
        private Graph.Graph G;
        private int S;
        private int T;
        private int[] pre;//遍历时的先导
        public Path(Graph.Graph g, int s,int t)
        {
            this.T = t;
            this.S = s;
            this.G = g;
            G.ValidateVertex(s);
            G.ValidateVertex(t);
            pre = new int[G.V];
            for (int i = 0; i < G.V; i++)
            {
                pre[i] = -1;
            }
            DFS(this.S, s);
            //foreach (var item in pre)
            //{
            //    Console.WriteLine(item);
            //}
        }

        private bool DFS(int v, int pre)
        {
            this.pre[v] = pre;
            if (v == T)
            {
                return true;
            }
            foreach (var w in G.GetAdj(v))
            {
                if (this.pre[w] == -1)
                {
                    if (DFS(w, v))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //t节点是否和s节点连接
        public bool isConnected()
        {
            return pre[T] != -1;
        }

        public List<int> path()
        {
            List<int> p = new List<int>();
            if (isConnected())
            {
                int cur = T;
                while (cur != S)
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
            Path p= new Path(graph, 0,6);
            var ls = p.path();
            Console.WriteLine("0-->6");
            for (int i = 0; i < ls.Count; i++)
            {
                Console.WriteLine(ls[i]);
            }
        }


    }
}
