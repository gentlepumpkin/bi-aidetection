using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_Actions:Form
    {
        public List<CameraTriggerAction> actions = new List<CameraTriggerAction>();
        public Frm_Actions()
        {
            InitializeComponent();
        }

        private void Frm_Actions_Load(object sender, EventArgs e)
        {
            try
            {
                Global_GUI.ConfigureFOLV(ref FOLV_Actions, typeof(CameraTriggerAction), null, null);
                Global_GUI.UpdateFOLV(ref FOLV_Actions, actions, true);
            }
            catch (Exception ex)
            {

                Global.Log("Error: " + ex.Message);
            }
        }
    }
}
