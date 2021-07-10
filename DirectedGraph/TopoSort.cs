using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectedGraph
{
    class TopoSort
    {
        private Graph G;
        private List<int> res;
        private bool hasCycle;
        public bool HasCycle => hasCycle;
        public TopoSort(Graph graph)
        {
            this.G = graph;
            
            if (!G.IsDirected)
            {
                throw new Exception("TopoSort only works in directed graph.");
            }

            res = new List<int>();
            int[] indegress = new int[G.V];
            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < G.V; i++)
            {
                indegress[i] = G.InDegree(i);
                //将入度为0的节点放入队列中(队列只放入度为0的节点)
                if (indegress[i] == 0)
                {
                    queue.Enqueue(i);
                }
            }
            //开启循环
            while (queue.Count>0)
            {
                int v = queue.Dequeue();
                res.Add(v);
                //将当前节点的边都删掉,并且将更新后入度为0的节点加入队列
                foreach (var w in G.GetAdj(v))
                {
                    indegress[w]--;
                    if (indegress[w] == 0)
                    {
                        queue.Enqueue(w);
                    }
                }
            }

            if (res.Count!=G.V)
            {
                res.Clear();
                hasCycle = true;
            }

        }
        public List<int> Result()
        {
            return res;
        }

        public static void Main1(string[] arg)
        {
            Graph graph = new Graph("_g.txt", true);
            TopoSort ts = new TopoSort(graph);
            if (!ts.hasCycle)
            {
                foreach (var item in ts.Result())
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("该图存在环,不能进行拓扑排序");
            }
        }

    }
}
