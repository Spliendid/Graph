using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirectedGraph
{
    public class Graph
    {
        private int _v;//顶点
        public int V { get { return _v; } }
        private int _e;//边
        public int E { get { return _e; } }
        private HashSet<int>[] adj;//临接数组
        private bool isDirected;
        private int[] indegrees, outdegrees;//入度和出度
        public bool IsDirected => isDirected;
        public Graph(string fileName, bool isDirected = false)
        {
            this.isDirected = isDirected;
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
                indegrees = new int[V];
                outdegrees = new int[V];
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
                    if (!isDirected)
                        adj[b].Add(a);
                    if (isDirected)
                    {
                        indegrees[b] ++;
                        outdegrees[a] ++;
                    }
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

        public int InDegree(int v)
        {
            if (!isDirected)
            {
                throw new Exception("indegree only works in directed graph");
            }
            ValidateVertex(v);
            return indegrees[v];
        }
        public int OutDegree(int v)
        {
            if (!isDirected)
            {
                throw new Exception("outdegree only works in directed graph");
            }
            ValidateVertex(v);
            return outdegrees[v];
        }
        public void RemoveEdge(int v,int w)
        {
            ValidateVertex(v);
            ValidateVertex(w);
            if (adj[v].Contains(w))
            {
                _e--;
                if (isDirected)
                {
                    indegrees[w]--;
                    outdegrees[v]--;
                }
            }
            adj[v].Remove(w);
            if (!isDirected)
            {
                adj[w].Remove(v);
            }
        }

        public static void Main1(string[] args)
        {
            Graph adjset = new Graph("g.txt", true);
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

