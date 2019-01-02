using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    class OsmAgent : AgentBase
    {
        public bool IsSensor { get; set; }
        public OpinionSubject MySubject { get; protected set; }
        public Vector<double> InitOpinion { get; protected set; }
        public Vector<double> Opinion { get; set; }
        public List<AgentLink> AgentLinks { get; protected set; }
        public double OpinionThreshold { get; protected set; }
        public Vector<double> InitBelief { get; private set; }
        public Vector<double> Belief { get; private set; }

        public OsmAgent()
        {

        }

        public OsmAgent(Node node)
        {
            this.AgentID = node.ID;
            this.AgentLinks = new List<AgentLink>();
            this.IsSensor = false;
        }

        public OsmAgent(int node_id)
        {
            this.AgentID = node_id;
            this.AgentLinks = new List<AgentLink>();
            this.IsSensor = false;
        }

        public OsmAgent AttachAgentLinks(List<AgentLink> agent_links)
        {
            this.AgentLinks.AddRange(agent_links.Where(agent_link => agent_link.SourceAgent.AgentID == this.AgentID || agent_link.TargetAgent.AgentID == this.AgentID).ToList());
            return this;
        }

        public OsmAgent SetInitBelief(Vector<double> init_belief)
        {
            this.InitBelief = init_belief.Clone();
            this.Belief = init_belief.Clone();
            return this;
        }

        public OsmAgent SetBelief(Vector<double> belief)
        {
            if (Belief.Count != belief.Count)
            {
                throw new Exception(nameof(OsmAgent) + " Error irregular beleif dim");
            }

            this.Belief = belief.Clone();
            return this;
        }


        public OsmAgent SetBeliefFromList(List<double> belief_list)
        {
            if (Belief.Count != belief_list.Count)
            {
                throw new Exception(nameof(OsmAgent) + " Error irregular beleif list");
            }

            var new_belief = Vector<double>.Build.Dense(belief_list.ToArray());
            this.Belief = new_belief;
            //Console.WriteLine(this.Belief.ToString());
            return this;
        }

        public void SetCommonWeight(double common_weight)
        {
            foreach (var link in this.AgentLinks)
            {
                link.SetWeight(this, common_weight);
            }
        }

        public int GetOpinionDim()
        {
            var max_dim = Opinion.Count;

            for (int dim = 0; dim < max_dim; dim++)
            {
                if (this.Opinion[dim] == 1) return dim;
            }
            return -1;
        }

        public bool IsDetermined()
        {
            var undeter_op = this.Opinion.Clone();
            undeter_op.Clear();

            return (!this.Opinion.Equals(undeter_op)) ? true : false;
        }

        public bool IsChanged()
        {
            var init_op = this.InitOpinion.Clone();

            return (!this.Opinion.Equals(init_op)) ? true : false;
        }

        public List<OsmAgent> GetNeighbors()
        {
            var neighbors = new List<OsmAgent>();
            foreach (var agent_link in this.AgentLinks)
            {
                if (agent_link.TargetAgent.AgentID < 0) continue;
                OsmAgent neighbor_agent;
                neighbor_agent = agent_link.TargetAgent.AgentID == this.AgentID ? agent_link.SourceAgent : agent_link.TargetAgent;

                neighbors.Add(neighbor_agent);
            }
            return neighbors;
        }

        public OsmAgent SetSubject(OpinionSubject subject)
        {
            this.MySubject = subject;
            var op_vector = Vector<double>.Build.Dense(this.MySubject.SubjectDimSize, 0.0);
            this.SetInitOpinion(op_vector);
            return this;
        }

        public OsmAgent SetInitOpinion(Vector<double> init_op_vector)
        {
            if (this.MySubject.SubjectDimSize != init_op_vector.Count)
            {
                throw new Exception("error not equal subject dim and init op dim");
            }
            this.InitOpinion = init_op_vector.Clone();
            this.Opinion = init_op_vector.Clone();
            return this;
        }

        public OsmAgent SetSensor(bool is_sensor)
        {
            this.IsSensor = is_sensor;
            return this;
        }

        public OsmAgent SetThreshold(double threshold)
        {
            this.OpinionThreshold = threshold;
            return this;
        }
    }
}
