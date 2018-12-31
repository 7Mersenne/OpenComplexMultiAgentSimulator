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


        public SettingForm()
        {
            this.UserInitialize();
            InitializeComponent();
        }

        void UserInitialize()
        {
            this.Text = nameof(OpenComplexMAS);
            this.MyModel = new PryymakOpinionSharingModel();
            this.MyModel.InvokeSettingControl(this);
            this.MyModel.InvokeAnimationForm(this);

        }

    }
}
