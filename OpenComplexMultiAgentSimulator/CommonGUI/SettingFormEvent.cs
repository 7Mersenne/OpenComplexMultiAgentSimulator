using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenComplexMultiAgentSimulator
{
    public partial class SettingForm : Form
    {

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
