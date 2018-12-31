using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenComplexMultiAgentSimulator
{
    public partial class SettingForm : Form
    {
        ModelBase MyModel;
        List<ModelBase> Models;


        public SettingForm()
        {
            this.Models = new List<ModelBase>();

            this.InitializeSettingControls();
            this.UserInitializeComponent();
            this.InitializeProperty();
        }

        public void UserInitializeComponent()
        {
            this.InitializeComponent();

            foreach (ModelEnum model in Enum.GetValues(typeof(ModelEnum)))
            {
                this.comboBoxModel.Items.Add(model.ToString());
            }
            this.comboBoxModel.SelectedIndex = 0;
        }

        void InitializeSettingControls()
        {
            ModelBase model = null;
            foreach (ModelEnum model_enum in Enum.GetValues(typeof(ModelEnum)))
            {
                switch (model_enum)
                {
                    case ModelEnum.PryymakOpinionSharingModel:
                        model = new PryymakOpinionSharingModel();
                        break;
                    default:
                        break;
                }
                model.InvokeAnimationForm(this);
                model.InvokeSettingControl(this);
                this.Models.Add(model);
            }

        }

        void InitializeProperty()
        {


        }

    }
}
