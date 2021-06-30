using System;
using System.Collections.Generic;
using Graph;
namespace GraphDFS
{
    class GraphDFS
    {
        private Graph.Graph G;
        private bool[] visited;
        //深度优先先序遍历
        private List<int> Pre_order = new List<int>();
        //深度优先后序遍历
        private List<int> Post_order = new List<int>();

        public GraphDFS(Graph.Graph g)
        {
            this.G = g;
            visited = new bool[g.V];
            for (int i = 0; i < g.V; i++)
            {
                if (!visited[i])
                {
                    DFS(i);
                }
            }
        }

        private void DFS(int v)
        {
            visited[v] = true;
            Pre_order.Add(v);
            foreach (var w in G.GetAdj(v))
            {
                if (!visited[w])
                {
                    DFS(w);
                }
            }
            Post_order.Add(v);
        }

        public static void Main1(string[] arg)
        {
            Graph.Graph graph = new Graph.Graph("g.txt");
            GraphDFS graphDFS = new GraphDFS(graph);
            var o  = graphDFS.Order();
            foreach (var item in o)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// 获取图的遍历
        /// </summary>
        /// <param name="i">i = 0 先序遍历，i = 1 后续遍历</param>
        /// <returns></returns>
        public IEnumerable<int> Order(int  i = 0)
        {
            return   i==0? Pre_order:Post_order;
        }

    }
}
