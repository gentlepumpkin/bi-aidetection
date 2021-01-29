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
    public partial class Frm_Variables : Form
    {
        public List<ClsProp> props = new List<ClsProp>();
        public Frm_Variables()
        {
            InitializeComponent();
        }

        private void Frm_Variables_Load(object sender, EventArgs e)
        {
            Global_GUI.ConfigureFOLV(FOLV_Vars, typeof(ClsProp));
            Global_GUI.UpdateFOLV(FOLV_Vars, props, ResizeColsStyle: ColumnHeaderAutoResizeStyle.ColumnContent);
            Global_GUI.RestoreWindowState(this);
        }

        private void Frm_Variables_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void FOLV_Vars_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FOLV_Vars.SelectedObjects != null && this.FOLV_Vars.SelectedObjects.Count > 0)
            {
                string clip = "";
                foreach (ClsProp prop in this.FOLV_Vars.SelectedObjects)
                {
                    clip += prop.Name + " ";
                }
                Clipboard.SetText(clip.Trim());
            }
            else
            {
                //Clipboard.SetText("");
            }
        }
    }
}
