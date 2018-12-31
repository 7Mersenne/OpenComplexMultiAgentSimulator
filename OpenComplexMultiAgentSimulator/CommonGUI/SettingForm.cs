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
        IModel MyModel;
        List<IModel> Models;


        public SettingForm()
        {
            this.Models = new List<IModel>();

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
            IModel model = null;
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

        private void comboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var model in this.Models)
            {
                model.MyControl.Visible = false;
                model.MyAnimationForm.Hide();
            }

            var model_enum = (ModelEnum)Enum.Parse(typeof(ModelEnum), this.comboBoxModel.Text, false);
            this.MyModel = this.Models.First(model => model.MyModelEnum == model_enum);

            this.MyModel.MyControl.Visible = true;
            this.MyModel.MyAnimationForm.Show();
        }
    }
}
