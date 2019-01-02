using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    class RecordRound
    {
        public int Round { get; private set; }
        public List<int> CorrectSizes { get; private set; }
        public List<int> IncorrectSizes { get; private set; }
        public List<int> UndeterSizes { get; private set; }
        public List<int> StepMessageSizes { get; private set; }
        public List<int> ActiveSensorSizes { get; private set; }
        public List<int> ActiveAgentSizes { get; private set; }
        public List<int> DeterminedSensorSizes { get; private set; }
        public List<int> SensorSizes { get; private set; }
        public List<int> NetworkSizes { get; private set; }
        public Dictionary<OsmAgent, Vector<double>> AgentReceiveOpinionsInStep { get; private set; }

        public RecordRound()
        {

        }

        public RecordRound(int cur_round, List<OsmAgent> agents)
        {
            this.Round = cur_round;
            this.AgentReceiveOpinionsInStep = new Dictionary<OsmAgent, Vector<double>>();
            this.CorrectSizes = new List<int>();
            this.IncorrectSizes = new List<int>();
            this.UndeterSizes = new List<int>();
            this.StepMessageSizes = new List<int>();
            this.ActiveSensorSizes = new List<int>();
            this.ActiveAgentSizes = new List<int>();
            this.DeterminedSensorSizes = new List<int>();
            this.SensorSizes = new List<int>();
            this.NetworkSizes = new List<int>();

            foreach (var agent in agents)
            {
                var undeter_op = agent.InitOpinion.Clone();
                undeter_op.Clear();
                this.AgentReceiveOpinionsInStep.Add(agent, undeter_op);
            }
        }


        public void RecordStepAgents(List<OsmAgent> agents, SubjectManager subject_mgr)
        {
            var cor_dim = subject_mgr.OSM_Env.CorrectDim;
            var cor_subject = subject_mgr.OSM_Env.EnvSubject;
            var correct_size = agents.Where(agent => agent.MySubject.SubjectName == cor_subject.SubjectName && agent.GetOpinionDim() == cor_dim).Count();
            var undeter_size = agents.Where(agent => agent.GetOpinionDim() == -1).Count();
            var network_size = agents.Count;
            var incorrect_size = network_size - correct_size - undeter_size;
            var sensor_size = agents.Where(agent => agent.IsSensor).Count();
            var determined_sensor_size = agents.Where(agent => agent.IsSensor && agent.IsDetermined()).Count();

            this.CorrectSizes.Add(correct_size);
            this.UndeterSizes.Add(undeter_size);
            this.NetworkSizes.Add(network_size);
            this.IncorrectSizes.Add(incorrect_size);
            this.SensorSizes.Add(sensor_size);
            this.DeterminedSensorSizes.Add(determined_sensor_size);
        }


        public void RecordStepMessages(List<Message> step_messages)
        {
            foreach (var step_message in step_messages)
            {
                Vector<double> receive_op = null;
                if (step_message.Subject != step_message.ToAgent.MySubject)
                {
                    var to_subject = step_message.ToAgent.MySubject;
                    receive_op = step_message.Subject.ConvertOpinionForSubject(step_message.Opinion, to_subject);
                }
                else
                {
                    receive_op = step_message.Opinion.Clone();
                }

                this.AgentReceiveOpinionsInStep[step_message.ToAgent] += receive_op;
            }

            var active_sensor_size = step_messages.Where(message => message.FromAgent.AgentID < 0).Select(message => message.ToAgent.AgentID).Count();
            var active_agent_size = step_messages.Where(message => message.FromAgent.AgentID >= 0).Select(message => message.ToAgent.AgentID).Count();
            var step_message_size = step_messages.Count;

            this.ActiveSensorSizes.Add(active_sensor_size);
            this.ActiveAgentSizes.Add(active_agent_size);
            this.StepMessageSizes.Add(step_message_size);
        }

        public bool IsReceived(OsmAgent agent)
        {
            if (this.AgentReceiveOpinionsInStep[agent].L2Norm() == 0) return false;
            return true;
        }

        public void PrintRecord()
        {
            double network_size = this.NetworkSizes.Last();

            Console.WriteLine(
               $"|round:{this.Round:D4}|" +
               $"|cor:{Math.Round(this.CorrectSizes.Last() / network_size, 3):F3}|" +
               $"|incor:{Math.Round(this.IncorrectSizes.Last() / network_size, 3):F3}|" +
               $"|undeter:{Math.Round(this.UndeterSizes.Last() / network_size, 3):F3}|");
        }
    }
}
