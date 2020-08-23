using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace Graph
{
    /// <summary>
    /// 图的邻接矩阵
    /// </summary>
    class AdjMatrix
    {

        private int _v;//顶点
        public int V { get { return _v; } }
        private int _e;//边
        public int E { get { return _e; } }
        private int[,] adj;//临接矩阵
        public AdjMatrix(string fileName)
        {
            string[] info = File.ReadAllLines(fileName);
            try
            {
                var s = info[0].Split(' ');
                _v = int.Parse( s[0]);
                if (_v<0)
                {
                    throw new Exception("V must be non-negative");
                }
                adj = new int[_v,_v];
                _e = int.Parse(s[1]);
                if (_e< 0)
                {
                    throw new Exception("V must be non-negative");
                }
                for (int i = 1; i < info.Length; i++)
                {
                    s = info[i].Split(' ');
                    int a = int.Parse(s[0]);
                    ValidateVertex(a);
                    int b = int.Parse(s[1]);
                    ValidateVertex(b);

                    if (a == b) throw new Exception("Self Loop is Detected!");
                    if (adj[a,b] == 1) throw new Exception("Parallel Edges is Detected!");

                    adj[a, b] = 1;
                    adj[b, a] = 1;
                }

            }
            catch (Exception e)
            {

                Console.WriteLine($"{e.Message}::{e.StackTrace}");
            }
        }

        private void ValidateVertex(int v)
        {
            if (v<0 || v >= _v)
            {
                throw new Exception($"vertex {v} is invalid");
            }
        }

        /// <summary>
        /// 判断边
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool HasEdge(int v, int w)
        {
            ValidateVertex(v);
            ValidateVertex(w);
            return adj[v, w] == 1;
        }

        /// <summary>
        /// 获取某一顶点的所有临接的点
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public List<int> GetAdj(int v)
        {
            ValidateVertex(v);
            List<int> res = new List<int>();
            for (int i = 0; i < V; i++)
            {
                if (adj[v,i] == 1)
                {
                    res.Add(i);
                }
            }
            return res;
        }

        public int Degree(int v)
        {
            return GetAdj(v).Count;
        }

        public static void Main(string[] args)
        {
            AdjMatrix adjMatrix = new AdjMatrix("g.txt");
            Console.Write(adjMatrix.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"V = {_v}, E = {_e}\n");
            for (int i = 0; i < _v; i++)
            {
                for (int j = 0; j < _v; j++)
                {
                    sb.Append(adj[i,j]);
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}
