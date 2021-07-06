using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphWeighted;
namespace ShortestPath
{
    class BellmanFord
    {
        private WeightedGraph G;
        private int s; //Source
        private int[] dis;//Distance of pos to source

        private int[] Pre;
        private bool hasNegCycle;
        public bool HasNegCycle => hasNegCycle;
        public BellmanFord(WeightedGraph graph, int s)
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
            //进行V-1次松弛操作
            for (int j = 1; j < G.V; j++)
            {
                for (int i = 0; i < G.V; i++)
                {
                    foreach (var item in G.GetAdj(i))
                    {
                        if (dis[i] != int.MaxValue)
                        {
                            int temp = dis[i] + G.GetWeight(i, item);
                            if (temp < dis[item])
                            {
                                dis[item] = temp;
                                Pre[item] = i;
                            }

                        }
                    }
                }

            }

            //再进行一次松弛操作,判断是否有负权边
            for (int i = 0; i < G.V; i++)
            {
                foreach (var item in G.GetAdj(i))
                {
                    if (dis[i] != int.MaxValue)
                    {
                        int temp = dis[i] + G.GetWeight(i, item);
                        if (temp < dis[item])
                        {
                            hasNegCycle = true;
                            break;
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
            while (Pre[cur] != cur)
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
            return dis[v]!= int.MaxValue;
        }

        public int DistTo(int v)
        {
            G.ValidateVertex(v);
            return dis[v];
        }

        static void Main1(string[] args)
        {
            WeightedGraph wg = new WeightedGraph("g.txt");
            BellmanFord fb = new BellmanFord(wg, 0);
            if (fb.hasNegCycle)
            {
                Console.WriteLine("exits negative cycle");
            }
            else
            {
                //for (int i = 0; i < wg.V; i++)
                //{
                //    Console.WriteLine(fb.DistTo(i));
                //}
                foreach (var item in fb.Path(3))
                {
                    Console.WriteLine(item);
                }
            }

            //foreach (var item in fb.Path(3))
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
