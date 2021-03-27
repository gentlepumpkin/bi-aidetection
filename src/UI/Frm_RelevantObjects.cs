using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SQLite;

namespace AITool
{
    public partial class Frm_RelevantObjects : Form
    {
        private ClsRelevantObjectManager ObjectManager = null;
        private ClsRelevantObjectManager TempObjectManager = null;
        public string ROMName = "";
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
                return !rowObject.IsNull() && ((ClsRelevantObject)rowObject).Enabled;
            };

            this.FOLV_RelevantObjects.BooleanCheckStatePutter = delegate (Object rowObject, bool newValue)
            {
                if (rowObject.IsNull())
                    return false;
                ((ClsRelevantObject)rowObject).Enabled = newValue;
                return newValue;
            };

            this.FillCombo();
            this.SetROM();
            this.LoadROMList();

            Global_GUI.RestoreWindowState(this);

            Loading = false;

        }

        private void LoadROMList()
        {
            //Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.TempObjectManager.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);
            Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.TempObjectManager.ObjectList, FullRefresh: true);

            Global_GUI.GroupboxEnableDisable(groupBox1, cb_enabled);
        }

        private void FillCombo()
        {
            try
            {
                toolStripComboBoxCameras.Items.Clear();
                int idx = 0;
                int fnd = -1;
                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    foreach (PropertyInfo prop in cam.GetType().GetProperties())
                    {
                        if (prop.PropertyType == typeof(ClsRelevantObjectManager))
                        {
                            ClsRelevantObjectManager rom = (ClsRelevantObjectManager)prop.GetValue(cam);
                            string item = $"{cam.Name}\\{rom.TypeName}";
                            toolStripComboBoxCameras.Items.Add(item);
                            if (item.EqualsIgnoreCase(this.ROMName))
                                fnd = idx;
                            idx++;
                        }
                    }
                    string item2 = $"{cam.Name}\\{cam.maskManager.MaskTriggeringObjects.TypeName}";
                    toolStripComboBoxCameras.Items.Add(item2);
                    if (item2.EqualsIgnoreCase(this.ROMName))
                        fnd = idx;
                    idx++;

                }

                if (fnd != -1)
                    toolStripComboBoxCameras.SelectedIndex = fnd;
                else if (idx > 0)
                    toolStripComboBoxCameras.SelectedIndex = 0;

                this.Text = $"Relevant Objects - {this.ROMName}";

            }
            catch (Exception ex)
            {

                AITOOL.Log($"Error: {ex.Message}");
            }
        }

        private void SetROM()
        {
            try
            {
                if (this.ROMName.IsEmpty())
                    return;

                this.TempObjectManager = null;

                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    foreach (PropertyInfo prop in cam.GetType().GetProperties())
                    {
                        if (prop.PropertyType == typeof(ClsRelevantObjectManager))
                        {
                            ClsRelevantObjectManager rom = (ClsRelevantObjectManager)prop.GetValue(cam);
                            if ($"{cam.Name}\\{rom.TypeName}".EqualsIgnoreCase(this.ROMName))
                            {
                                this.TempObjectManager = rom;
                                break;
                            }
                        }
                    }

                    string item = $"{cam.Name}\\{cam.maskManager.MaskTriggeringObjects.TypeName}";
                    if (item.EqualsIgnoreCase(this.ROMName))
                    {
                        this.TempObjectManager = cam.maskManager.MaskTriggeringObjects;
                        break;
                    }
                }

                if (this.TempObjectManager.IsNull())
                    MessageBox.Show($"Error: Could not match '{this.ROMName}' to existing RelevantObjectManager?");

            }
            catch (Exception ex)
            {

                AITOOL.Log($"Error: {ex.Message}");
            }
        }

        private void SaveROM()
        {
            try
            {
                if (this.ROMName.IsEmpty())
                    return;


                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    foreach (PropertyInfo prop in cam.GetType().GetProperties())
                    {
                        if (prop.PropertyType == typeof(ClsRelevantObjectManager))
                        {
                            ClsRelevantObjectManager rom = (ClsRelevantObjectManager)prop.GetValue(cam);
                            if ($"{cam.Name}\\{rom.TypeName}".EqualsIgnoreCase(this.ROMName))
                            {
                                rom.ObjectList = this.TempObjectManager.ObjectList;
                                //rom = this.TempObjectManager;
                                break;
                            }
                        }
                    }

                    string item = $"{cam.Name}\\{cam.maskManager.MaskTriggeringObjects.TypeName}";
                    if (item.EqualsIgnoreCase(this.ROMName))
                    {
                        cam.maskManager.MaskTriggeringObjects.ObjectList = this.TempObjectManager.ObjectList;
                        break;
                    }
                }


            }
            catch (Exception ex)
            {

                AITOOL.Log($"Error: {ex.Message}");
            }
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
                    this.cb_enabled.Enabled = false;
                    this.tb_Name.Text = "";
                    this.tb_Time.Text = "";
                    this.tb_ConfidenceLower.Text = "";
                    this.tb_ConfidenceUpper.Text = "";

                }
                else
                {
                    this.cb_enabled.Checked = this.ro.Enabled;
                    this.cb_enabled.Enabled = true;
                    this.tb_Name.Text = this.ro.Name;
                    this.tb_Time.Text = this.ro.ActiveTimeRange;
                    this.tb_ConfidenceLower.Text = this.ro.Threshold_lower.ToString();
                    this.tb_ConfidenceUpper.Text = this.ro.Threshold_upper.ToString();

                    if (this.tb_Name.Text.EqualsIgnoreCase("NEW OBJECT") || this.tb_Name.Text.IsEmpty())
                        this.tb_Name.Enabled = true;
                    else
                        this.tb_Name.Enabled = false;

                    this.cb_ObjectTriggers.Checked = this.ro.Trigger;

                    this.cb_ObjectIgnoreImageMask.Checked = this.ro.IgnoreImageMask;
                    this.cb_ObjectIgnoreDynamicMask.Checked = this.ro.IgnoreDynamicMask.Value;

                    if (this.ROMName.Has("\\default"))
                    {
                        this.cb_ObjectIgnoreDynamicMask.Enabled = true;
                        this.cb_ObjectIgnoreImageMask.Enabled = true;
                    }
                    else
                    {
                        this.cb_ObjectIgnoreDynamicMask.Enabled = false;
                        this.cb_ObjectIgnoreImageMask.Enabled = false;
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
                if (!Loading && NeedsSaving && !this.ro.IsNull() && !this.tb_Name.Text.IsEmpty())
                {
                    this.ro.Enabled = this.cb_enabled.Checked;
                    this.ro.Name = this.tb_Name.Text;
                    this.ro.ActiveTimeRange = this.tb_Time.Text;
                    this.ro.Threshold_lower = this.tb_ConfidenceLower.Text.ToDouble();
                    this.ro.Threshold_upper = this.tb_ConfidenceUpper.Text.ToDouble();
                    this.ro.Trigger = this.cb_ObjectTriggers.Checked;
                    this.ro.IgnoreImageMask = this.cb_ObjectIgnoreImageMask.Checked;
                    this.ro.IgnoreDynamicMask = this.cb_ObjectIgnoreDynamicMask.Checked;
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

                //this.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {

                AITOOL.Log("Error: " + ex.Msg());
            }
        }

        private void Save()
        {
            SaveRO();

            List<ClsRelevantObject> rolist = new List<ClsRelevantObject>();
            foreach (ClsRelevantObject ro in FOLV_RelevantObjects.Objects)
            {
                rolist.Add(ro);
            }

            this.TempObjectManager.ObjectList = this.TempObjectManager.FromList(rolist, true, true);
            this.TempObjectManager.Update();

            //this.SaveROM();
        }

        private void toolStripButtonUp_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            if (this.ro == null)
                return;

            this.ro = this.TempObjectManager.MoveUp(ro, out int NewIDX);

            Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.TempObjectManager.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);

        }

        private void toolStripButtonDown_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            if (this.ro == null)
                return;

            this.ro = this.TempObjectManager.MoveDown(ro, out int NewIDX);

            Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.TempObjectManager.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);

        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            this.ro = this.TempObjectManager.Delete(ro, out int NewIDX);

            Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.TempObjectManager.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            SaveRO();

            ClsRelevantObject ro = new ClsRelevantObject("NEW OBJECT");
            this.ro = ro;

            if (this.TempObjectManager.TryAdd(ro, true, out int NewIDX))
                Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.TempObjectManager.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.TempObjectManager.Reset(); // = new ClsRelevantObjectManager(AppSettings.Settings.ObjectPriority, this.ObjectManager.TypeName, this.ObjectManager.Camera);
            this.ro = null;
            Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.TempObjectManager.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);

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

        private void FOLV_RelevantObjects_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tb_Time_TextChanged(object sender, EventArgs e)
        {
            NeedsSaving = true;
            this.SaveRO();
        }

        private void cb_ObjectTriggers_CheckedChanged(object sender, EventArgs e)
        {
            NeedsSaving = true;
            this.SaveRO();
        }

        private void cb_IgnoreMask_CheckedChanged(object sender, EventArgs e)
        {
            NeedsSaving = true;
            this.SaveRO();
        }

        private void btn_adddefaults_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            this.TempObjectManager.AddDefaults(); // = new ClsRelevantObjectManager(AppSettings.Settings.ObjectPriority, this.ObjectManager.TypeName, this.ObjectManager.Camera);
            this.ro = null;
            Global_GUI.UpdateFOLV(FOLV_RelevantObjects, this.TempObjectManager.ObjectList, UseSelected: true, SelectObject: this.ro, FullRefresh: true, ForcedSelection: true);
        }

        private void toolStripComboBoxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxCameras.SelectedItem.ToString().IsNotEmpty())
            {
                this.ROMName = toolStripComboBoxCameras.SelectedItem.ToString();
                this.Text = $"Relevant Objects - {this.ROMName}";
                this.SetROM();
                this.LoadROMList();
                this.SelectionChanged();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void toolStripComboBoxCameras_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
