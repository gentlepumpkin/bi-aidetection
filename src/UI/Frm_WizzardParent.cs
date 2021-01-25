using System;
using System.Linq;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_WizzardParent : Form
    {
        Form[] frm = { new Frm_WizardBIServer(), new Frm_WizardAI() };
        int top = -1;
        int count;

        public Frm_WizzardParent()
        {
            count = frm.Length;
            InitializeComponent();
        }


        private void LoadNewForm()
        {
            frm[top].TopLevel = false;
            frm[top].AutoScroll = true;
            frm[top].Dock = DockStyle.Fill;
            this.pnlContent.Controls.Clear();
            this.pnlContent.Controls.Add(frm[top]);
            frm[top].Show();
        }


        private void Back()
        {
            top--;

            if (top <= -1)
            {
                return;
            }
            else
            {
                btnBack.Enabled = true;
                btnNext.Enabled = true;
                LoadNewForm();
                if (top - 1 <= -1)
                {
                    btnBack.Enabled = false;
                }
            }

            if (top >= count)
            {
                btnNext.Enabled = false;
            }
        }
        private void Next()
        {

            top++;
            if (top >= count)
            {
                return;
            }
            else
            {
                btnBack.Enabled = true;
                btnNext.Enabled = true;
                LoadNewForm();
                if (top + 1 == count)
                {
                    btnNext.Enabled = false;
                }
            }

            if (top <= 0)
            {
                btnBack.Enabled = false;
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            Back();
        }

        private void Frm_WizzardParent_Load(object sender, EventArgs e)
        {
            Next();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }
    }
}
