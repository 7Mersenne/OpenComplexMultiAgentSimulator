using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    abstract class OsmModelBase : ModelBase
    {
        public AgentNetwork MyAgentNetwork { get; set; }
        protected ExtendRandom UpdateModelRand;
        public OpinionEnvironment MyEnvManager { get; protected set; }
        public SubjectManager MySubjectManager { get; protected set; }
        public double OpinionIntroRate { get; protected set; }
        public double OpinionIntroInterval { get; protected set; }
        public CalcWeightMode MyCalcWeightMode { get; protected set; }
        protected AggregationFunctions MyAggFuncs;
        List<Message> Messages;
        List<OsmAgent> OpinionFormedAgents;
        //public Dictionary<int, RecordStep> MyRecordSteps { get; set; }
        bool SensorCommonWeightMode;
        public double SensorCommonWeight { get; private set; }
        public RecordRound MyRecordRound { get; set; }
        public List<RecordRound> MyRecordRounds { get; set; }


        public OsmModelBase()
        {
            this.CurrentStep = 0;
            this.CurrentRound = 0;
            this.SensorCommonWeightMode = false;
            this.MyAggFuncs = new AggregationFunctions();
            Messages = new List<Message>();
            OpinionFormedAgents = new List<OsmAgent>();
            this.MyRecordRound = new RecordRound();
            this.MyRecordRounds = new List<RecordRound>();
        }

        public override void InitializeToFirstStep()
        {
            foreach (var agent in this.MyAgentNetwork.Agents)
            {
                agent.SetBelief(agent.InitBelief.Clone());
                agent.Opinion = agent.InitOpinion.Clone();
            }
            this.CurrentStep = 0;
            this.Messages.Clear();
            this.OpinionFormedAgents.Clear();
            this.MyRecordRound = new RecordRound(this.CurrentRound, this.MyAgentNetwork.Agents);

        }

        public override void InitializeStep()
        {
            this.Messages.Clear();
        }

        public override void NextStep()
        {
            //sensor observe
            if (this.CurrentStep % this.OpinionIntroInterval == 0)
            {
                var all_sensors = this.MyAgentNetwork.Agents.Where(agent => agent.IsSensor).ToList();
                var observe_num = (int)Math.Ceiling(all_sensors.Count * this.OpinionIntroRate);
                var observe_sensors = all_sensors.Select(agent => agent.AgentID).OrderBy(a => this.UpdateModelRand.Next()).Take(observe_num).Select(id => this.MyAgentNetwork.Agents[id]).ToList();
                var env_messages = this.MyEnvManager.SendMessages(observe_sensors, this.UpdateModelRand);
                Messages.AddRange(env_messages);
            }

            //agent observe
            var op_form_messages = this.AgentSendMessages(OpinionFormedAgents);
            Messages.AddRange(op_form_messages);
            OpinionFormedAgents.Clear();

            //agent receive
            foreach (var message in this.Messages)
            {
                this.UpdateBeliefByMessage(message);
                var op_form_agent = this.UpdateOpinion(message);
                OpinionFormedAgents.Add(op_form_agent);
            }

            this.CurrentStep++;
        }

        public override void RecordStep()
        {
            this.MyRecordRound.RecordStepMessages(this.Messages);
            this.MyRecordRound.RecordStepAgents(this.MyAgentNetwork.Agents, this.MySubjectManager);
        }

        public override void FinalizeStep()
        {
        }

        public override void InitializeToFirstRound()
        {
            this.InitializeToFirstStep();
            this.CurrentRound = 0;
            this.MyRecordRounds = new List<RecordRound>();
        }

        public override void InitializeRound()
        {
            this.InitializeToFirstStep();
        }

        public override void NextRound(int step_count)
        {
            this.UpdateSteps(step_count);
            this.CurrentRound++;
        }

        public override void RecordRound()
        {
            this.MyRecordRounds.Add(this.MyRecordRound);
        }

        public override void FinalizeRound()
        {
            this.MyRecordRound.PrintRecord();
        }

        public void SetRand(ExtendRandom update_model_rand)
        {
            this.UpdateModelRand = update_model_rand;
            return;
        }

        public virtual void SetAgentNetwork(AgentNetwork agent_network)
        {
            this.MyAgentNetwork = agent_network;
            return;
        }

        public void SetSubjectManager(SubjectManager subject_mgr)
        {
            this.MySubjectManager = subject_mgr;
            this.MyEnvManager = subject_mgr.OSM_Env;
            this.MyEnvManager.AddEnvironment(this.MyAgentNetwork);
            return;
        }

        public void SetOpinionIntroRate(double op_intro_rate)
        {
            this.OpinionIntroRate = op_intro_rate;
            return;
        }

        public void SetOpinionIntroInterval(int interval_step)
        {
            this.OpinionIntroInterval = interval_step;
            return;
        }

        public void SetInitWeightsMode(CalcWeightMode mode)
        {
            this.MyCalcWeightMode = mode;
            return;
        }

        public void SetSensorCommonWeight(double sensor_common_weight)
        {
            this.SensorCommonWeightMode = true;
            this.SensorCommonWeight = sensor_common_weight;
        }

        protected virtual void UpdateBeliefByMessage(Message message)
        {
            Vector<double> receive_op;
            var pre_belief = message.ToAgent.Belief;
            var weight = message.GetToWeight();

            if (message.Subject != message.ToAgent.MySubject)
            {
                var to_subject = message.ToAgent.MySubject;
                receive_op = message.Subject.ConvertOpinionForSubject(message.Opinion, to_subject);
            }
            else
            {
                receive_op = message.Opinion.Clone();
            }

            var updated_belief = this.MyAggFuncs.UpdateBelief(pre_belief, weight, receive_op);
            if (message.FromAgent.AgentID < 0)
            {
                double sensor_weight;
                if (this.SensorCommonWeightMode)
                {
                    sensor_weight = this.SensorCommonWeight;
                }
                else
                {
                    sensor_weight = this.MyEnvManager.SensorRate;
                }
                updated_belief = this.MyAggFuncs.UpdateBelief(pre_belief, sensor_weight, receive_op);
            }

            message.ToAgent.SetBelief(updated_belief);
        }

        protected virtual OsmAgent UpdateOpinion(Message message)
        {
            var belief_list = message.ToAgent.Belief.ToList();
            var op_list = message.ToAgent.Opinion.ToList();
            var op_threshold = message.ToAgent.OpinionThreshold;

            for (int dim = 0; dim < belief_list.Count; dim++)
            {
                if (belief_list[dim] > op_threshold && op_list[dim] != 1)
                {
                    message.ToAgent.Opinion.Clear();
                    message.ToAgent.Opinion[dim] = 1;
                    return message.ToAgent;
                }
            }
            return null;
        }

        protected virtual List<Message> AgentSendMessages(List<OsmAgent> op_formed_agents)
        {
            List<Message> messages = new List<Message>();
            foreach (var agent in op_formed_agents)
            {
                if (agent == null) continue;
                var opinion = agent.Opinion.Clone();
                foreach (var to_agent in agent.GetNeighbors())
                {
                    var agent_link = agent.AgentLinks.Where(link => link.SourceAgent == to_agent || link.TargetAgent == to_agent).First();
                    messages.Add(new Message(agent, to_agent, agent_link, opinion));
                }
            }

            return messages;
        }
    }
}
