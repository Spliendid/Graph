using System;
using System.Collections.Generic;
namespace DirectedGraph
{
    class GraphBFS
    {
        Graph G;

        private List<int> order;

        public List<int> GetOrder()
        {
            return new List<int>(order);
        }

        private bool[] visited;
        public GraphBFS(Graph G)
        {
            this.G = G;
            visited = new bool[G.V];
            order = new List<int>();
            for (int i = 0; i < G.V; i++)
            {
                if (!visited[i])
                {
                    BFS(i);
                }
            }
        }

        private void BFS(int  node)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(node);
            while (queue.Count>0)
            {
                int temp = queue.Dequeue();
                if (visited[temp]) continue;
                visited[temp] = true;
                order.Add(temp);
                foreach (var item in G.GetAdj(temp))
                {
                    queue.Enqueue(item);
                }
            }
        }

        public static void  Main1 (string[] arg)
        {

            Graph graph = new Graph("g.txt");
            GraphBFS graphBFS = new GraphBFS(graph);

            foreach (var item in graphBFS.GetOrder())
            {
                Console.WriteLine(item);
            }

        }
    }
}
