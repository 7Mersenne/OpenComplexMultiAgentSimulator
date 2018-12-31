using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenComplexMultiAgentSimulator
{
    abstract class ModelBase
    {
        public ModelEnum MyModelEnum { get; set; }
        public int CurrentStep { get; set; }
        public int CurrentRound { get; set; }
        public UserControl MyControl { get; set; }
        public Form MyAnimationForm { get; set; }

        //form
        public virtual void InvokeAnimationForm(SettingForm setting_form)
        {
            this.MyAnimationForm.Hide();
            this.MyAnimationForm.Left = setting_form.Right;
        }

        public virtual void InvokeSettingControl(SettingForm setting_form)
        {
            setting_form.Controls.Add(this.MyControl);
            this.MyControl.Dock = DockStyle.Fill;
            this.MyControl.Visible = false;
        }

        //configuration
        public virtual void InitializeConfiguration()
        {
            throw new NotImplementedException();
        }

        public virtual void RegisterConfiguration()
        {
            throw new NotImplementedException();
        }

        //agent and environment

        public virtual void InitializeAgents()
        {
            throw new NotImplementedException();
        }

        public virtual void InitializeEnvironments()
        {
            throw new NotImplementedException();
        }

        //step

        public virtual void InitializeStep()
        {
            throw new NotImplementedException();
        }

        public virtual void NextStep()
        {
            throw new NotImplementedException();
        }

        public virtual void RecordStep()
        {
            throw new NotImplementedException();
        }

        public virtual void FinalizeStep()
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateSteps(int step_count)
        {
            throw new NotImplementedException();
        }

        //round
        public virtual void InitializeRound()
        {
            throw new NotImplementedException();
        }

        public virtual void NextRound()
        {
            throw new NotImplementedException();
        }

        public virtual void RecordRound()
        {
            throw new NotImplementedException();
        }

        public virtual void FinalizeRound()
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateRounds(int round_count)
        {
            throw new NotImplementedException();
        }

    }
}
