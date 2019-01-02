using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    class Layout
    {
        public LayoutEnum MyLayoutEnum { get; set; }
        public List<Vector<double>> PosList { get; }

        public Layout(List<Vector<double>> pos_list, LayoutEnum layout_enum)
        {
            this.MyLayoutEnum = layout_enum;
            this.PosList = pos_list;
        }

        public Vector<double> GetAgentPosition(OsmAgent agent)
        {
            return this.PosList[agent.AgentID];
        }

        public (Vector<double> source_pos, Vector<double> target_pos) GetLinkPosition(AgentLink agent_link)
        {
            var s_pos = this.GetAgentPosition(agent_link.SourceAgent);
            var t_pos = this.GetAgentPosition(agent_link.TargetAgent);

            return (s_pos, t_pos);
        }
    }
}
