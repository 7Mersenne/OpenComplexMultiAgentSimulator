using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenComplexMultiAgentSimulator
{
    interface IModel
    {
        ModelEnum MyModelEnum { get; set; }
        int CurrentStep { get; set; }
        int CurrentRound { get; set; }
        UserControl MyControl { get; set; }
        Form MyAnimationForm { get; set; }

        //form
        void InvokeAnimationForm(SettingForm setting_form);
        void InvokeSettingControl(SettingForm setting_form);

        //configuration
        void InitializeConfiguration();
        void RegisterConfiguration();

        //agent environment
        void InitializeAgents();
        void InitializeEnvironments();

        //step
        void InitializeStep();
        void NextStep();
        void RecordStep();
        void FinalizeStep();
        void UpdateSteps(int step_count);

        //round
        void InitializeRound();
        void NextRound();
        void RecordRound();
        void FinalizeRound();
        void UpdateRounds(int round_count);

    }
}
