using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    abstract class GraphGeneratorBase
    {
        public abstract GraphEnum MyGraphEnum { get; }
        public abstract bool SeedEnable { get; protected set; }
        public abstract RawGraph Generate(int graph_seed);
    }
}
