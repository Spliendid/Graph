using System;
using System.Collections.Generic;
using System.Collections;
using Graph;
namespace GraphDFS
{
    class CC
    {
        private Graph.Graph G;
        private int[] visited;
        private List<int> Pre_order = new List<int>();
        private List<int> Post_order = new List<int>();
        //联通分量
        private int cccount;
        /// <summary>
        /// 联通分量
        /// </summary>
        public int  CCCount { get { return cccount; } }
        public CC(Graph.Graph g)
        {
            visited = new int[g.V];
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = -1;
            }
            this.G = g;
            for (int i = 0; i < g.V; i++)
            {
                if (visited[i] == -1)
                {
                    DFS(i,cccount);
                    cccount++;
                }
            }
        }
        /// <summary>
        /// 深度优先遍历
        /// </summary>
        /// <param name="v">要遍历的顶点</param>
        /// <param name="index">联通分量id</param>
        private void DFS(int v,int index)
        {
            visited[v] = index;
            foreach (var w in G.GetAdj(v))
            {
                if (visited[w]==-1)
                {
                    DFS(w,index);
                }
            }
        }

        public bool isConnected(int v, int w) 
        {
            //判断点是否合法
            G.ValidateVertex(v);
            G.ValidateVertex(w);
            return visited[v] == visited[w];
        }

        public List<int>[] components() 
        {
            List<int>[] res = new List<int>[cccount];

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new List<int>();
            }

            for (int i = 0; i < visited.Length; i++)
            {
                res[visited[i]].Add(i);
            }
            return res;
        }

        public static void Main1(string[] arg)
        {
            Graph.Graph graph = new Graph.Graph("g.txt");
            CC cc = new CC(graph);

            string s = "";
            foreach (var item in cc.visited)
            {
                s += item.ToString();
            }
            Console.WriteLine(s);

            Console.WriteLine(cc.CCCount);

            Console.WriteLine(cc.isConnected(0,6));
            Console.WriteLine(cc.isConnected(0,5));

            var com = cc.components();

            for (int i = 0; i < com.Length; i++)
            {
                Console.Write(i+"   :   ");
                for (int j = 0; j < com[i].Count; j++)
                {
                    Console.Write($" {com[i][j]},");
                }
                Console.Write("\n");
            }

        }


    }
}
