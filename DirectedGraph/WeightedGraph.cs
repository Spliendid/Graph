using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirectedGraph
{
    public class WeightedGraph
    {
        private int _v;//顶点
        public int V { get { return _v; } }
        private int _e;//边
        public int E { get { return _e; } }
        private Dictionary<int,int>[] adj;//临接数组
        private bool isDirected;
        public bool IsDirected => isDirected;
        public WeightedGraph(string fileName,bool isDir = false)
        {
            this.isDirected = isDir;
            string[] info = File.ReadAllLines(fileName);
            try
            {
                var s = info[0].Split(' ');
                _v = int.Parse(s[0]);
                if (_v < 0) throw new Exception("V must be non-negative");
                adj = new Dictionary<int, int>[V];
                for (int i = 0; i < _v; i++)
                {
                    adj[i] = new Dictionary<int, int>();
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
                    int weighted = int.Parse(s[2]);
                    if (a == b) throw new Exception("Self Loop is Detected!");
                    if (adj[a].ContainsKey(b)) throw new Exception("Parallel Edges is Detected!");

                    adj[a].Add(b,weighted);
                    if(!IsDirected)
                        adj[b].Add(a,weighted);
                }

            }
            catch (Exception e)
            {

                Console.WriteLine($"{e.Message}::{e.StackTrace}");
            }
        }

        public void ValidateVertex(int v)
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
            return adj[v].ContainsKey(w);
        }

        /// <summary>
        /// 获取某一顶点的所有临接的点
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public HashSet<int> GetAdj(int v)
        {
            ValidateVertex(v);

            return new HashSet<int>( adj[v].Keys);
        }

        //public int Degree(int v)
        //{
        //    return GetAdj(v).Count;
        //}

        public int GetWeight(int v, int w)
        {
            if (HasEdge(v,w))
            {
                return adj[v][w];
            }
            throw new Exception($"No edge {v} - {w}");
        }

        public void RemoveEdge(int v, int w)
        {
            ValidateVertex(v);
            ValidateVertex(w);
            adj[v].Remove(w);
            //adj[w].Remove(v);
        }

        public static void Main1(string[] args)
        {
            WeightedGraph adjset = new WeightedGraph("g.txt",true);
            Console.Write(adjset.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"V = {_v}, E = {_e}\n");
            for (int i = 0; i < _v; i++)
            {
                sb.Append($"{i} : ");
                foreach (KeyValuePair<int,int> item in adj[i])
                {
                    sb.Append($"({item.Key} : {item.Value})   ");
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}
