using System;
using System.Reflection;
using System.Windows.Forms;

namespace AITool
{
    public partial class FrmSplash : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        public FrmSplash()
        {
            this.InitializeComponent();
        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            string AssemVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.lbl_version.Text = $"AITOOL version {AssemVer} built on {Global.RetrieveLinkerTimestamp()}";
        }
    }
}
