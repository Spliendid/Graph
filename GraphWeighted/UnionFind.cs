using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphWeighted
{
    /// <summary>
    /// 并查集
    /// </summary>
    class UnionFind
    {
        private int[] uf;
        public UnionFind(int length)
        {
            uf = new int[length];
            for (int i = 0; i < length; i++)
            {
                uf[i] = i;
            }
        }

        private int FindRoot(int i)
        {
            while (uf[i]!=i)
            {
                uf[i] = uf[uf[i]];
                i = uf[i];
            }
            return i;
        }

        public bool isConnected(int a,int b)
        {
            int roota = FindRoot(a);
            int rootb = FindRoot(b);
            return roota == rootb;
        }

        public void Union(int a, int b)
        {
            int roota = FindRoot(a);
            int rootb = FindRoot(b);
            uf[roota] = rootb;
        }
    }
}
