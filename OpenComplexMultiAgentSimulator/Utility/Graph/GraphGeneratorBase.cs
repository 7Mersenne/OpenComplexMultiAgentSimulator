using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    abstract class GraphGeneratorBase
    {
        public GraphEnum MyGraphEnum { get; protected set; }
        public bool SeedEnable { get; protected set; }
        public int GraphSeed { get; protected set; }
        public int GraphSize { get; protected set; }

        public abstract RawGraph Generate(int graph_seed);
    }
}
