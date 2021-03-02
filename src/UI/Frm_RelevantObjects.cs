using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_RelevantObjects : Form
    {
        public ClsRelevantObjectManager ObjectManager = null;
        private List<ClsRelevantObject> ObjectList = new List<ClsRelevantObject>();

        private ClsRelevantObject ro = null;
        private bool NeedsSaving = false;
        private bool Loading = false;

        public Frm_RelevantObjects()
        {
            InitializeComponent();
        }

        private void Frm_RelevantObjects_Load(object sender, EventArgs e)
        {
            Loading = true;

            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            Global_GUI.ConfigureFOLV(FOLV_RelevantObjects, typeof(ClsRelevantObject));

            this.FOLV_RelevantObjects.BooleanCheckStateGetter = delegate (Object rowObject)
            {
                return ((ClsRelevantObject)rowObject).Enabled;
            };

            this.FOLV_RelevantObjects.BooleanCheckStatePutter = delegate (Object rowObject, bool newValue)
            {
                ((ClsRelevantObject)rowObject).Enabled = newValue;
                return newValue;
            };

            this.ObjectList = ObjectManager.ToList();

            Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.ObjectList);
            Global_GUI.RestoreWindowState(this);

            Global_GUI.GroupboxEnableDisable(groupBox1, cb_enabled);

            Loading = false;

        }

        private void Frm_RelevantObjects_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void cb_enabled_CheckedChanged(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            Global_GUI.GroupboxEnableDisable(groupBox1, cb_enabled);

            NeedsSaving = true;

            this.SaveRO();
        }

        private void FOLV_RelevantObjects_SelectionChanged(object sender, EventArgs e)
        {

            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            SelectionChanged();
        }

        private void SelectionChanged()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            try
            {
                if (this.FOLV_RelevantObjects.SelectedObjects != null && this.FOLV_RelevantObjects.SelectedObjects.Count > 0)
                {
                    ////save the last one just in case
                    this.SaveRO();

                    //set current selected object
                    this.ro = (ClsRelevantObject)this.FOLV_RelevantObjects.SelectedObjects[0];

                    //enable toolstrip buttons
                    toolStripButtonDelete.Enabled = true;
                    toolStripButtonDown.Enabled = true;
                    toolStripButtonUp.Enabled = true;
                }
                else
                {
                    toolStripButtonDelete.Enabled = false;
                    toolStripButtonDown.Enabled = false;
                    toolStripButtonUp.Enabled = false;
                    this.ro = null;
                }

            }
            catch (Exception ex)
            {
                AITOOL.Log($"Error: {ex.Msg()}");

            }

            LoadRO();

        }

        private void LoadRO([CallerMemberName()] string memberName = null)
        {
            Loading = true;

            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            try
            {
                if (this.ro.IsNull())
                {
                    this.cb_enabled.Checked = false;
                    this.tb_Name.Text = "";
                    this.tb_Time.Text = "";
                    this.tb_ConfidenceLower.Text = "";
                    this.tb_ConfidenceUpper.Text = "";

                }
                else
                {
                    this.cb_enabled.Checked = this.ro.Enabled;
                    this.tb_Name.Text = this.ro.Name;
                    this.tb_Time.Text = this.ro.ActiveTimeRange;
                    this.tb_ConfidenceLower.Text = this.ro.Threshold_lower.ToString();
                    this.tb_ConfidenceUpper.Text = this.ro.Threshold_upper.ToString();

                    if (this.tb_Name.Text.EqualsIgnoreCase("NEW OBJECT") || this.tb_Name.Text.IsEmpty())
                        this.tb_Name.Enabled = true;
                    else
                        this.tb_Name.Enabled = false;

                    if (this.ro.Trigger)
                    {
                        rb_trigger.Checked = true;
                        rb_ignore.Checked = false;
                    }
                    else
                    {
                        rb_trigger.Checked = false;
                        rb_ignore.Checked = true;
                    }

                }

                Global_GUI.GroupboxEnableDisable(groupBox1, cb_enabled);


            }
            catch (Exception ex)
            {

                AITOOL.Log("Error: " + ex.Msg());
            }
            finally
            {
                NeedsSaving = false;
                Loading = false;

            }
        }
        private void SaveRO([CallerMemberName()] string memberName = null)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            try
            {
                if (!Loading && NeedsSaving && !this.ro.IsNull() && this.tb_Name.Enabled && !this.tb_Name.Text.IsEmpty())
                {
                    this.ro.Enabled = this.cb_enabled.Checked;
                    this.ro.Name = this.tb_Name.Text;
                    this.ro.ActiveTimeRange = this.tb_Time.Text;
                    this.ro.Threshold_lower = this.tb_ConfidenceLower.Text.ToDouble();
                    this.ro.Threshold_upper = this.tb_ConfidenceUpper.Text.ToDouble();
                    this.ro.Trigger = this.rb_trigger.Checked;
                    this.FOLV_RelevantObjects.Refresh();
                }

            }
            catch (Exception ex)
            {

                AITOOL.Log("Error: " + ex.Msg());
            }
            finally
            {
                NeedsSaving = false;
            }
        }

        private void FOLV_RelevantObjects_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.LoadRO();
            //SelectionChanged();
        }

        private void FOLV_RelevantObjects_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            FormatRow(sender, e);
        }

        private void FormatRow(object Sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            try
            {
                ClsRelevantObject ro = (ClsRelevantObject)e.Model;

                // If SPI IsNot Nothing Then
                if (ro.Enabled && ro.Trigger)
                    e.Item.ForeColor = Color.Green;

                else if (ro.Enabled && !ro.Trigger)
                    e.Item.ForeColor = Color.DarkOrange;

                else if (!ro.Enabled)
                    e.Item.ForeColor = Color.Gray;
            }
            catch (Exception) { }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            try
            {
                SaveRO();

                List<ClsRelevantObject> rolist = new List<ClsRelevantObject>();
                foreach (ClsRelevantObject ro in FOLV_RelevantObjects.Objects)
                {
                    rolist.Add(ro);
                }

                this.ObjectManager.FromList(rolist);

                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {

                AITOOL.Log("Error: " + ex.Msg());
            }
        }

        private void toolStripButtonUp_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            if (this.ro == null)
                return;

            int idx = this.ObjectList.IndexOf(this.ro);

            if (idx > 0)
            {
                this.ObjectList.Move(idx, idx - 1);

                this.ro = this.ObjectList[idx - 1];

                for (int i = 0; i < this.ObjectList.Count; i++)
                {
                    this.ObjectList[i].Priority = i + 1;
                }

                Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);

            }
        }

        private void toolStripButtonDown_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            if (this.ro == null)
                return;

            int idx = this.ObjectList.IndexOf(this.ro);

            if (idx > -1 && idx < this.ObjectList.Count - 1)
            {
                this.ObjectList.Move(idx, idx + 1);

                this.ro = this.ObjectList[idx + 1];

                for (int i = 0; i < this.ObjectList.Count; i++)
                {
                    this.ObjectList[i].Priority = i + 1;
                }

                Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);

            }
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            int idx = this.ObjectList.IndexOf(this.ro);

            if (idx > -1)
            {
                this.ObjectList.Remove(this.ro);
                if (idx - 1 > -1)
                    this.ro = this.ObjectList[idx - 1];

                for (int i = 0; i < this.ObjectList.Count; i++)
                {
                    this.ObjectList[i].Priority = i + 1;
                }
                Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            SaveRO();

            ClsRelevantObject ro = new ClsRelevantObject("NEW OBJECT");


            this.ro = ro;
            this.ro.Priority = this.ObjectList.Count + 1;
            this.ObjectList.Add(ro);
            Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.ObjectManager = new ClsRelevantObjectManager(AppSettings.Settings.ObjectPriority, this.ObjectManager.TypeName, this.ObjectManager.Camera);
            this.ObjectList = this.ObjectManager.ToList();
            Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);

        }

        private void tb_Name_Leave(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.SaveRO();

        }

        private void rb_trigger_CheckedChanged(object sender, EventArgs e)
        {
            NeedsSaving = true;
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.SaveRO();

        }

        private void rb_ignore_CheckedChanged(object sender, EventArgs e)
        {
            NeedsSaving = true;
            this.SaveRO();

        }

        private void tb_ConfidenceLower_Leave(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.SaveRO();

        }

        private void tb_ConfidenceUpper_TextChanged(object sender, EventArgs e)
        {
            NeedsSaving = true;
            this.SaveRO();

        }

        private void tb_ConfidenceUpper_Leave(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.SaveRO();

        }

        private void tb_Time_Leave(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.SaveRO();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.SaveRO();
        }

        private void groupBox1_Leave(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.SaveRO();

        }

        private void tb_Name_TextChanged(object sender, EventArgs e)
        {
            NeedsSaving = true;
            this.SaveRO();

        }

        private void tb_ConfidenceLower_TextChanged(object sender, EventArgs e)
        {
            NeedsSaving = true;
            this.SaveRO();

        }
    }
}
