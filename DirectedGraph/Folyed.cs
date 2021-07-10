using System;
using System.Collections.Generic;

namespace DirectedGraph
{
    class Folyed
    {
        private WeightedGraph G;
        private int[,] dis;//Distance of pos to source

        private int[] Pre;
        private bool hasNegCycle;
        public bool HasNegCycle => hasNegCycle;
        public Folyed(WeightedGraph graph)
        {
            this.G = graph;
            dis = new int[G.V,G.V];
            Pre = new int[G.V];
            for (int i = 0; i < dis.GetLength(0); i++)
            {
                for (int j = 0; j < dis.GetLength(1); j++)
                {
                    dis[i, j] = int.MaxValue;
                }
                dis[i, i] = 0;

                foreach (var j in G.GetAdj(i))
                {
                    dis[i, j] = G.GetWeight(i, j);
                }
            }

            //进行V-1次松弛操作
            for (int j = 1; j < G.V; j++)
            {
                //以k为原点
                for (int k = 0; k < G.V; k++)
                {
                    dis[k,k] = 0;
                    //k为原点,每条边的松弛操作
                    for (int i = 0; i < G.V; i++)
                    {
                        foreach (var item in G.GetAdj(i))
                        {
                            if (dis[k,i] != int.MaxValue && dis[i,item]!=int.MaxValue)
                            {
                                int temp = dis[k,i] + dis[i,item];
                                if (temp < dis[k,item])
                                {
                                    dis[k,item] = temp;

                                }

                            }
                        }
                    }

                }
               

            }

            //判断是否有负权环
            for (int i = 0; i < G.V; i++)
            {
                if (dis[i,i]<0)
                {
                    hasNegCycle = true;
                }
            }
          
        }

        //public IEnumerable<int> Path(int t)
        //{
        //    List<int> re = new List<int>();
        //    if (!isConnected(t))
        //    {
        //        return re;
        //    }

        //    int cur = t;
        //    while (Pre[cur] != cur)
        //    {
        //        re.Add(cur);
        //        cur = Pre[cur];
        //    }
        //    re.Add(s);
        //    re.Reverse();

        //    return re;
        //}

        public bool isConnected(int v,int w)
        {
            G.ValidateVertex(v);
            G.ValidateVertex(w);
            return dis[v,w] != int.MaxValue;
        }

        public int DistTo(int v,int w)
        {
            G.ValidateVertex(v);
            G.ValidateVertex(w);
            return dis[v,w];
        }

        static void Main1(string[] args)
        {
            WeightedGraph wg = new WeightedGraph("g.txt");
            Folyed fb = new Folyed(wg);
            if (fb.hasNegCycle)
            {
                Console.WriteLine("exits negative cycle");
            }
            else
            {
                for (int i = 0; i < wg.V; i++)
                {
                    for (int k = 0; k < wg.V; k++)
                    {
                        Console.WriteLine($"{i}->{k}  : {fb.DistTo(i,k)}");
                    }
                }
                //foreach (var item in fb.Path(3))
                //{
                //    Console.WriteLine(item);
                //}
            }

         
        }
    }
}
