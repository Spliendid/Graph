using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DirectedGraph
{
    class Dijkstra
    {
        private WeightedGraph G;
        private int s; //Source
        private int[] dis;//Distance of pos to source
        private bool[] visited;
        private int[] Pre;
        public Dijkstra(WeightedGraph graph,int s)
        {
            this.G = graph;
            G.ValidateVertex(s);
            this.s = s;
            dis = new int[G.V];
            Pre = new int[G.V];
            for (int i = 0; i < dis.Length; i++)
            {
                dis[i] = int.MaxValue;
                Pre[i] = -1;
            }

            dis[0] = 0;
            Pre[s] = 0;
            visited = new bool[G.V];
            while (true)
            {
                int curDis = int.MaxValue,cur = -1;
                //1.遍历dis数组,找到未被定下的最小的节点
                for (int i = 0; i < G.V; i++)
                {
                    if (!visited[i] && dis[i]<curDis)
                    {
                        cur = i;
                        curDis = dis[i];
                    }
                }

                if (cur == -1) break;

                //2.确定过这个最小节点的最短路径
                visited[cur] = true;

                //3.根据这个节点的最短路的大小,更新其他节点路径的长度
                foreach (var item in G.GetAdj(cur))
                {
                    if (!visited[item])
                    {
                        int temp = dis[cur] + G.GetWeight(cur, item);
                        if (temp<dis[item])
                        {
                            dis[item] = temp;
                            Pre[item] = cur;
                        }
                    }
                }
            }
        }

        public IEnumerable<int> Path(int t)
        {
            List<int> re = new List<int>();
            if (!isConnected(t))
            {
                return re;
            }

            int cur = t;
            while (Pre[cur]!=cur)
            {
                re.Add(cur);
                cur = Pre[cur];
            }
            re.Add(s);
            re.Reverse();

            return re;
        }

        public bool isConnected(int v)
        {
            G.ValidateVertex(v);
            return visited[v];
        }

        public int DistTo(int v)
        {
            G.ValidateVertex(v);
            return dis[v];
        }

        static void Main1(string[] args)
        {
            WeightedGraph wg = new WeightedGraph("g.txt",true);
            Dijkstra dijkstra = new Dijkstra(wg,0);
            //for (int i = 0; i < wg.V; i++)
            //{
            //    Console.WriteLine(dijkstra.DistTo(i));
            //}
            foreach (var item in dijkstra.Path(3))
            {
                Console.WriteLine(item);
            }
        }
    }
}
