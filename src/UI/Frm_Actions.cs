using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static AITool.AITOOL;

namespace AITool
{
    public partial class Frm_Actions : Form
    {
        public List<CameraTriggerAction> actions = new List<CameraTriggerAction>();
        public Frm_Actions()
        {
            this.InitializeComponent();
        }

        private void Frm_Actions_Load(object sender, EventArgs e)
        {
            try
            {
                Global_GUI.ConfigureFOLV(this.FOLV_Actions, typeof(CameraTriggerAction), null, null);
                Global_GUI.UpdateFOLV(this.FOLV_Actions, this.actions, true);

                Global_GUI.RestoreWindowState(this);

                string[] triggertypes = Enum.GetNames(typeof(TriggerType));
                //cbType.Items.AddRange(triggertypes);
            }
            catch (Exception ex)
            {

                Log("Error: " + ex.Message);
            }
        }

        private void FOLV_Actions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FOLV_Actions_SelectionChanged(object sender, EventArgs e)
        {
            //if (FOLV_Actions.SelectedObjects != null && FOLV_Actions.SelectedObjects.Count > 0)
            //{
            //    CameraTriggerAction ta = (CameraTriggerAction)FOLV_Actions.SelectedObjects[0];
            //    //tbID.Text = ta.ID;
            //    //tbKey.Text = ta.Key;
            //    //tbPost.Text = ta.PostData ;
            //    //tbURL.Text = ta.URL;
            //    cbType.SelectedItem = ta.Type;

            //}
            //else
            //{
            //    tbID.Text = "";
            //    tbKey.Text = "";
            //    tbPost.Text = "";
            //    tbURL.Text = "";
            //}
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {

        }

        private void Frm_Actions_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }
    }
}
