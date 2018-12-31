using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenComplexMultiAgentSimulator
{
    class PryymakOpinionSharingModel : ModelBase
    {
        public PryymakOpinionSharingModel()
        {
            this.MyModelEnum = ModelEnum.PryymakOpinionSharingModel;
            this.MyControl = new PryymakOpinionSharingModelSettingControl();
            this.MyAnimationForm = new OpinionSharingModelAnimationForm();
        }


    }
}
