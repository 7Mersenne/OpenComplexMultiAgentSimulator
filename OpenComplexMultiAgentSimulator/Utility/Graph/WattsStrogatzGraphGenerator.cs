using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    class WattsStrogatzGraphGenerator : GraphGeneratorBase
    {
        public int NearestNeighbors;
        public double RewiringProbability;

        public WattsStrogatzGraphGenerator(int n, int k, double p)
        {
            this.MyGraphEnum = GraphEnum.WattsStrogatz;
            this.GraphSize = n;
            this.NearestNeighbors = k;
            this.RewiringProbability = p;
            this.SeedEnable = true;
        }

        public override RawGraph Generate(int graph_seed)
        {
            if (this.NearestNeighbors >= this.GraphSize) throw new Exception("choose smaller k or large n");

            var g = new RawGraph();
            return g;
        }
    }
}
