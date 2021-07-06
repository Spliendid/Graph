using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphWeighted
{
    class WeightedEdge :IComparable<WeightedEdge>
    {
        private int v, w, weight;
        public int V => v;
        public int W => w;
        public WeightedEdge(int v,int w,int weight)
        {
            this.v = v;
            this.w = w;
            this.weight = weight;
        }

        public int CompareTo(WeightedEdge other)
        {
            return this.weight - other.weight;
        }

        public override string ToString()
        {
            return $"({v} - {w} : {weight})";
        }
    }
}
