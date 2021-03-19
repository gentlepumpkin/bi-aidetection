using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_AnnoAdjust : Form
    {
        public Frm_AnnoAdjust()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.colors?view=net-5.0");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void Frm_AnnoAdjust_Load(object sender, EventArgs e)
        {
            LoadAnnoSettings();
        }

        private void LoadAnnoSettings()
        {
            try
            {
                if (AppSettings.Settings.RectDetectionTextForeColor.Name == "Gainsboro")  //not using gainsboro for foreground color any longer
                    AppSettings.Settings.RectDetectionTextForeColor = Color.Black;


                tb_BorderWidthPixels.Text = AppSettings.Settings.RectBorderWidth.ToString();

                tb_RelevantColor.Text = AppSettings.Settings.RectRelevantColor.Name;
                tb_RelevantAlpha.Text = AppSettings.Settings.RectRelevantColorAlpha.ToString();

                tb_IrrelevantColor.Text = AppSettings.Settings.RectIrrelevantColor.Name;
                tb_IrrelevantAlpha.Text = AppSettings.Settings.RectIrrelevantColorAlpha.ToString();

                tb_MaskedColor.Text = AppSettings.Settings.RectMaskedColor.Name;
                tb_MaskedAlpha.Text = AppSettings.Settings.RectMaskedColorAlpha.ToString();

                tb_FontForeColor.Text = AppSettings.Settings.RectDetectionTextForeColor.Name;
                tb_FontName.Text = AppSettings.Settings.RectDetectionTextFont;
                tb_FontSize.Text = AppSettings.Settings.RectDetectionTextSize.ToString();

                cb_UseAssignedBackColor.Checked = AppSettings.Settings.RectDetectionTextBackColor.Name == "Gainsboro";

                if (cb_UseAssignedBackColor.Checked)
                {
                    tb_FontBackColor.Text = "";
                    tb_FontBackColor.Enabled = false;
                }
                else
                {
                    tb_FontBackColor.Text = AppSettings.Settings.RectDetectionTextBackColor.Name;
                    tb_FontBackColor.Enabled = true;
                }

                UpdateExample();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Msg());
            }
        }

        private void UpdateCheckbox()
        {

        }

        private void UpdateExample()
        {
            try
            {

                Lbl_Example.Text = "This is example text";
                Lbl_Example.Font = new Font(tb_FontName.Text.Trim(), tb_FontSize.Text.ToFloat());
                Lbl_Example.ForeColor = Global.ConvertStringToColor(tb_FontForeColor.Text);

                if (cb_UseAssignedBackColor.Checked)
                {
                    Color backclr;
                    if (!Lbl_Example.Tag.IsNull() && Lbl_Example.Tag is string)
                    {
                        List<string> splt = Lbl_Example.Tag.ToString().SplitStr(",");
                        backclr = Color.FromArgb(splt[0].ToInt(), Global.ConvertStringToColor(splt[1]));
                    }
                    else
                    {
                        backclr = Color.FromArgb(tb_RelevantAlpha.Text.ToInt(), Global.ConvertStringToColor(tb_RelevantColor.Text));
                    }
                    Lbl_Example.BackColor = backclr;
                }
                else
                {
                    Lbl_Example.BackColor = Global.ConvertStringToColor(tb_FontBackColor.Text);
                }

            }
            catch (Exception ex)
            {
                Lbl_Example.Text = ex.Message;
                //MessageBox.Show("Error: " + ex.Msg());
            }
        }

        private void Save()
        {
            try
            {

                AppSettings.Settings.RectBorderWidth = tb_BorderWidthPixels.Text.ToInt();

                AppSettings.Settings.RectRelevantColor = Global.ConvertStringToColor(tb_RelevantColor.Text);
                AppSettings.Settings.RectRelevantColorAlpha = tb_RelevantAlpha.Text.ToInt();

                AppSettings.Settings.RectIrrelevantColor = Global.ConvertStringToColor(tb_IrrelevantColor.Text);
                AppSettings.Settings.RectIrrelevantColorAlpha = tb_IrrelevantAlpha.Text.ToInt();

                AppSettings.Settings.RectMaskedColor = Global.ConvertStringToColor(tb_MaskedColor.Text);
                AppSettings.Settings.RectMaskedColorAlpha = tb_MaskedAlpha.Text.ToInt();

                AppSettings.Settings.RectDetectionTextForeColor = Global.ConvertStringToColor(tb_FontForeColor.Text);
                if (AppSettings.Settings.RectDetectionTextForeColor.Name == "Gainsboro")
                    AppSettings.Settings.RectDetectionTextForeColor = Global.ConvertStringToColor("Black");

                AppSettings.Settings.RectDetectionTextFont = tb_FontName.Text;
                AppSettings.Settings.RectDetectionTextSize = tb_FontSize.Text.ToInt();

                if (cb_UseAssignedBackColor.Checked)
                {
                    AppSettings.Settings.RectDetectionTextBackColor = Global.ConvertStringToColor("Gainsboro");
                }
                else
                {
                    AppSettings.Settings.RectDetectionTextBackColor = Global.ConvertStringToColor(tb_FontBackColor.Text);
                }

            }
            catch (Exception ex)
            {
                Lbl_Example.Text = ex.Message;
                MessageBox.Show("Error: " + ex.Msg());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tb_RelevantColor_Enter(object sender, EventArgs e)
        {
            Lbl_Example.Tag = $"{tb_RelevantAlpha.Text},{tb_RelevantColor.Text}";
            this.UpdateExample();
        }

        private void tb_IrrelevantColor_Enter(object sender, EventArgs e)
        {
            Lbl_Example.Tag = $"{tb_IrrelevantAlpha.Text},{tb_IrrelevantColor.Text}";
            this.UpdateExample();

        }

        private void tb_MaskedColor_Enter(object sender, EventArgs e)
        {
            Lbl_Example.Tag = $"{tb_MaskedAlpha.Text},{tb_MaskedColor.Text}";
            this.UpdateExample();
        }

        private void tb_MaskedColor_Leave(object sender, EventArgs e)
        {
            Lbl_Example.Tag = $"{tb_MaskedAlpha.Text},{tb_MaskedColor.Text}";
            this.UpdateExample();

        }

        private void tb_MaskedAlpha_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_MaskedAlpha_Leave(object sender, EventArgs e)
        {
            Lbl_Example.Tag = $"{tb_MaskedAlpha.Text},{tb_MaskedColor.Text}";
            this.UpdateExample();

        }

        private void tb_IrrelevantColor_Leave(object sender, EventArgs e)
        {
            Lbl_Example.Tag = $"{tb_IrrelevantAlpha.Text},{tb_IrrelevantColor.Text}";
            this.UpdateExample();
        }

        private void tb_IrrelevantAlpha_Leave(object sender, EventArgs e)
        {
            Lbl_Example.Tag = $"{tb_IrrelevantAlpha.Text},{tb_IrrelevantColor.Text}";
            this.UpdateExample();
        }

        private void tb_RelevantColor_Leave(object sender, EventArgs e)
        {
            Lbl_Example.Tag = $"{tb_RelevantAlpha.Text},{tb_RelevantColor.Text}";
            this.UpdateExample();
        }

        private void tb_RelevantAlpha_Leave(object sender, EventArgs e)
        {
            Lbl_Example.Tag = $"{tb_RelevantAlpha.Text},{tb_RelevantColor.Text}";
            this.UpdateExample();
        }

        private void tb_RelevantAlpha_Enter(object sender, EventArgs e)
        {
            Lbl_Example.Tag = $"{tb_RelevantAlpha.Text},{tb_RelevantColor.Text}";
            this.UpdateExample();
        }

        private void tb_FontName_Leave(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void tb_FontName_Enter(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void tb_FontSize_Leave(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void tb_FontSize_Enter(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void tb_FontForeColor_Leave(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void tb_FontForeColor_Enter(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void tb_FontBackColor_Leave(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void tb_FontBackColor_Enter(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void cb_UseAssignedBackColor_Leave(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void cb_UseAssignedBackColor_Enter(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void Lbl_Example_Click(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void Frm_AnnoAdjust_Click(object sender, EventArgs e)
        {
            this.UpdateExample();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            colorDialog1.Color = Global.ConvertStringToColor(tb_RelevantColor.Text, tb_RelevantAlpha.Text);
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_RelevantColor.Text = colorDialog1.Color.Name;
                tb_RelevantAlpha.Text = colorDialog1.Color.A.ToString();
            }
        }

        private void cb_UseAssignedBackColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_UseAssignedBackColor.Checked)
            {
                tb_FontBackColor.Text = "";
                tb_FontBackColor.Enabled = false;
            }
            else
            {
                if (AppSettings.Settings.RectDetectionTextBackColor.Name == "Gainsboro")
                {
                    tb_FontBackColor.Text = tb_RelevantColor.Text;
                }
                else
                {
                    tb_FontBackColor.Text = AppSettings.Settings.RectDetectionTextBackColor.Name;

                }
                tb_FontBackColor.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Global.ConvertStringToColor(tb_IrrelevantColor.Text, tb_IrrelevantAlpha.Text);
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_IrrelevantColor.Text = colorDialog1.Color.Name;
                tb_IrrelevantAlpha.Text = colorDialog1.Color.A.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Global.ConvertStringToColor(tb_MaskedColor.Text, tb_MaskedAlpha.Text);
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_MaskedColor.Text = colorDialog1.Color.Name;
                tb_MaskedAlpha.Text = colorDialog1.Color.A.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Global.ConvertStringToColor(tb_FontForeColor.Text, "");
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_FontForeColor.Text = colorDialog1.Color.Name;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Global.ConvertStringToColor(tb_FontBackColor.Text, "");
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_FontBackColor.Text = colorDialog1.Color.Name;
            }

        }
    }

}
