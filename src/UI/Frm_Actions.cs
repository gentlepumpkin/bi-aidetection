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

        private void FOLV_Actions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FOLV_Actions_SelectionChanged(object sender, EventArgs e)
        {
            if (FOLV_Actions.SelectedObjects != null && FOLV_Actions.SelectedObjects.Count > 0)
            {
                CameraTriggerAction ta = (CameraTriggerAction)FOLV_Actions.SelectedObjects[0];
                tbID.Text = ta.ID;
                tbKey.Text = ta.Key;
                tbPost.Text = ta.PostData ;
                tbURL.Text = ta.URL;
                cbType.SelectedItem = ta.Type;

            }
            else
            {
                tbID.Text = "";
                tbKey.Text = "";
                tbPost.Text = "";
                tbURL.Text = "";
            }
        }
    }
}
