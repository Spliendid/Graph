using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Graph
{
    class AdjSet
    {
        private int _v;//顶点
        public int V { get { return _v; } }
        private int _e;//边
        public int E { get { return _e; } }
        private HashSet<int>[] adj;//临接数组
        public AdjSet(string fileName)
        {
            string[] info = File.ReadAllLines(fileName);
            try
            {
                var s = info[0].Split(' ');
                _v = int.Parse(s[0]);
                if (_v < 0) throw new Exception("V must be non-negative");
                adj = new HashSet<int>[V];
                for (int i = 0; i < _v; i++)
                {
                    adj[i] = new HashSet<int>();
                }

                _e = int.Parse(s[1]);
                if (_e < 0) throw new Exception("V must be non-negative");

                for (int i = 1; i < info.Length; i++)
                {
                    s = info[i].Split(' ');
                    int a = int.Parse(s[0]);
                    ValidateVertex(a);
                    int b = int.Parse(s[1]);
                    ValidateVertex(b);

                    if (a == b) throw new Exception("Self Loop is Detected!");
                    if (adj[a].Contains(b)) throw new Exception("Parallel Edges is Detected!");

                    adj[a].Add(b);
                    adj[b].Add(a);
                }

            }
            catch (Exception e)
            {

                Console.WriteLine($"{e.Message}::{e.StackTrace}");
            }
        }

        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= _v)
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
            return adj[v].Contains(w);
        }

        /// <summary>
        /// 获取某一顶点的所有临接的点
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public HashSet<int> GetAdj(int v)
        {
            ValidateVertex(v);

            return adj[v];
        }

        public int Degree(int v)
        {
            return GetAdj(v).Count;
        }

        public static void Main(string[] args)
        {
            AdjSet adjset = new AdjSet("g.txt");
            Console.Write(adjset.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"V = {_v}, E = {_e}\n");
            for (int i = 0; i < _v; i++)
            {
                sb.Append($"{i} : ");
                foreach (var item in adj[i])
                {
                    sb.Append(item);
                    sb.Append(' ');
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}

