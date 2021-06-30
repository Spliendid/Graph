using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphBFS
{
    class SingleSourcePath
    {
        Graph.Graph G;

        //节点的上一个节点
        private int[] Pre;

        public int[] Distance;

        private int S;

        public SingleSourcePath(Graph.Graph G,int S)
        {
            this.G = G;
            this.S = S;
            Pre = new int[G.V];
            Distance = new int[G.V];

            //将Pre元素都置为-1
            for (int i = 0; i < G.V; i++)
            {
                Pre[i] = -1;
                Distance[i] = -1;
            }

             BFS(S);

            
        }

        private void BFS(int node)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(node);
            Pre[node] = 0;
            Distance[node] = 0;
            while (queue.Count > 0)
            {
                int temp = queue.Dequeue();

                foreach (var item in G.GetAdj(temp))
                {
                    if (Pre[item] == -1)
                    {
                        queue.Enqueue(item);
                        Pre[item] = temp;
                        Distance[item] = Distance[temp] + 1;
                    }
                }
            }
        }

        //t节点是否和s节点连接
        public bool isConnected(int t)
        {
            G.ValidateVertex(t);
            return Pre[t] != -1;
        }

        public List<int> path(int t)
        {
            List<int> p = new List<int>();
            if (isConnected(t))
            {
                int cur = t;
                while (cur != S)
                {
                    p.Add(cur);
                    cur = Pre[cur];
                }
                p.Add(S);
            }
            p.Reverse();
            return p;
        }


        public static void Main(string[] arg)
        {

            Graph.Graph graph = new Graph.Graph("g.txt");
            SingleSourcePath singleSourcePath = new SingleSourcePath(graph, 0);
            var ls = singleSourcePath.path(6);
            Console.WriteLine("0-->6");
            //for (int i = 0; i < ls.Count; i++)
            //{
            //    Console.WriteLine(ls[i]);
            //}
            for (int i = 0; i < graph.V; i++)
            {
                Console.WriteLine(singleSourcePath.Distance[i]);
            }

        }
    }
}
