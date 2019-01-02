using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    class Message
    {
        public AgentLink MyAgentLink { get; private set; }
        public OsmAgent FromAgent { get; private set; }
        public OsmAgent ToAgent { get; private set; }
        public OpinionSubject Subject { get; private set; }
        public Vector<double> Opinion { get; private set; }

        public Message(OsmAgent from_agent, OsmAgent to_agent, AgentLink agent_link, Vector<double> opinion)
        {
            this.FromAgent = from_agent;
            this.ToAgent = to_agent;
            this.MyAgentLink = agent_link;
            this.Subject = from_agent.MySubject;
            this.Opinion = opinion;
        }

        public double GetToWeight()
        {
            if (this.MyAgentLink.SourceAgent == this.ToAgent)
            {
                return this.MyAgentLink.SourceWeight;
            }
            else
            {
                return this.MyAgentLink.TargetWeight;
            }
        }
    }
}
