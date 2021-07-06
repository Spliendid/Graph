using System;
using System.Collections.Generic;

namespace GraphWeighted
{
    //最小生成树
    class Kruskal
    {
        private WeightedGraph G;
        private List<WeightedEdge> mst;
        public Kruskal(WeightedGraph weightedGraph)
        {
            this.G = weightedGraph;
            CC cc = new CC(G);
            mst = new List<WeightedEdge>();
            //只有联通分量为1时才能有最小生成树
            if (cc.CCCount>1)
            {
                return;
            }
            List<WeightedEdge> edges = new List<WeightedEdge>();
            for (int i = 0; i < G.V; i++)
            {
                foreach (var j in G.GetAdj(i))
                {
                    if (i<j)
                    {
                        edges.Add(new WeightedEdge(i,j,G.GetWeight(i,j)));
                    }
                }
            }

            edges.Sort();

            UnionFind uf = new UnionFind(G.V);

            foreach (var edge in edges)
            {
                if (!uf.isConnected(edge.V,edge.W))
                {
                    uf.Union(edge.V,edge.W);
                    mst.Add(edge);
                }
            }

        }

        public List<WeightedEdge> Result()
        {
            return mst;
        }

        public static void Main(string[] args)
        {
            WeightedGraph adjset = new WeightedGraph("g.txt");
            Kruskal kruskal = new Kruskal(adjset);
            foreach (var item in kruskal.Result())
            {
                Console.WriteLine(item);
            }
        }
    }
}
