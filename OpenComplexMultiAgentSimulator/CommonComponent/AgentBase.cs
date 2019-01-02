using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    abstract class AgentBase
    {
        public int AgentID { get; set; }
        public Vector<double> Position { get; set; }
    }
}
