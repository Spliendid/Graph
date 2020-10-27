using System;
using System.Collections.Generic;
using Graph;
namespace GraphDFS
{
    class CC
    {
        private Graph.Graph G;
        private int[] visited;
        private List<int> Pre_order = new List<int>();
        private List<int> Post_order = new List<int>();
        private int cccount;
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


        public static void Main(string[] arg)
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
        }


    }
}
