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

        //config
        public virtual void InitializeConfig()
        {
            throw new NotImplementedException();
        }

        public virtual void ApplyConfigToModel()
        {

        }

        public virtual void ImportConfigJson()
        {
            throw new NotImplementedException();
        }

        public virtual void ExportConfigJson()
        {

        }

        public virtual void CopyConfig()
        {

        }

        //step
        public virtual void InitializeToFirstStep()
        {

        }

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
            foreach (var step in Enumerable.Range(0, step_count))
            {
                this.InitializeStep();
                this.NextStep();
                this.RecordStep();
                this.FinalizeStep();
            }
        }

        //round
        public virtual void InitializeToFirstRound()
        {
            throw new NotImplementedException();
        }

        public virtual void InitializeRound()
        {
            throw new NotImplementedException();
        }

        public virtual void NextRound(int step_count)
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

        public virtual void UpdateRounds(int round_count, int step_count)
        {
            foreach (var round in Enumerable.Range(0, round_count))
            {
                this.InitializeRound();
                this.NextRound(step_count);
                this.RecordRound();
                this.FinalizeRound();
            }
        }

    }
}
