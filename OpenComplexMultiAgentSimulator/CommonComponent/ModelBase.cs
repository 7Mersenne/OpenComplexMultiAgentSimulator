using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenComplexMultiAgentSimulator
{
    abstract class ModelBase : IModel
    {
        public string ModelName { get; set; } = "";
        public UserControl MyControl { get; set; }
        public Form MyAnimationForm { get; set; }


        public virtual void FinalizeRound()
        {
            throw new NotImplementedException();
        }

        public virtual void FinalizeStep()
        {
            throw new NotImplementedException();
        }

        public virtual void InitializeAgents()
        {
            throw new NotImplementedException();
        }

        public virtual void InitializeConfiguration()
        {
            throw new NotImplementedException();
        }

        public virtual void InitializeEnvironments()
        {
            throw new NotImplementedException();
        }

        public virtual void InitializeRound()
        {
            throw new NotImplementedException();
        }

        public virtual void InitializeStep()
        {
            throw new NotImplementedException();
        }

        public virtual void InvokeAnimationForm(SettingForm setting_form)
        {
            this.MyAnimationForm.Show();
            this.MyAnimationForm.Left = setting_form.Right;
        }

        public virtual void InvokeSettingControl(SettingForm setting_form)
        {
            this.MyControl.Dock = DockStyle.Fill;
            this.MyControl.Visible = true;
            setting_form.Controls.Add(this.MyControl);
        }

        public virtual void NextRound()
        {
            throw new NotImplementedException();
        }

        public virtual void NextStep()
        {
            throw new NotImplementedException();
        }

        public virtual void RecordRound()
        {
            throw new NotImplementedException();
        }

        public virtual void RecordStep()
        {
            throw new NotImplementedException();
        }

        public virtual void RegisterConfiguration()
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateRounds(int round_count)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateSteps(int step_count)
        {
            throw new NotImplementedException();
        }
    }
}
