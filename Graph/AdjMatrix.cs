using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace Graph
{
    class AdjMatrix
    {

        private int V;//顶点
        private int E;//边
        private int[,] adj;//临接矩阵
        public AdjMatrix(string fileName)
        {
            string[] info = File.ReadAllLines(fileName);
            try
            {
                var s = info[0].Split(' ');
                V = int.Parse( s[0]);
                adj = new int[V,V];
                E = int.Parse(s[1]);
                for (int i = 1; i < info.Length; i++)
                {
                    s = info[i].Split(' ');
                    int a = int.Parse(s[0]);
                    int b = int.Parse(s[1]);
                    adj[a, b] = 1;
                    adj[b, a] = 1;
                }

            }
            catch (Exception e)
            {

                Console.WriteLine($"{e.Message}::{e.StackTrace}");
            }
        }

        public static void Main(string[] args)
        {
            AdjMatrix adjMatrix = new AdjMatrix("g.txt");
            Console.Write(adjMatrix.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"V = {V}, E = {E}\n");
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    sb.Append(adj[i,j]);
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}
