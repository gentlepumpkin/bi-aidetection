using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_Faces : Form
    {
        public Frm_Faces()
        {
            InitializeComponent();
        }

        private void Frm_Faces_Load(object sender, EventArgs e)
        {

            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.


            Global_GUI.ConfigureFOLV(FOLV_Faces, typeof(ClsFace));
            Global_GUI.ConfigureFOLV(FOLV_FaceFiles, typeof(ClsFaceFile));

            Global_GUI.UpdateFOLV(FOLV_Faces, AITOOL.FaceMan.Faces, FullRefresh: true);

            Global_GUI.RestoreWindowState(this);
        }

        private void Frm_Faces_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void FOLV_Faces_SelectionChanged(object sender, EventArgs e)
        {
            FaceSelectionChanged();
        }

        private void FaceSelectionChanged()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            try
            {
                if (this.FOLV_Faces.SelectedObjects != null && this.FOLV_Faces.SelectedObjects.Count > 0)
                {

                    //set current selected object
                    ClsFace face = (ClsFace)this.FOLV_Faces.SelectedObjects[0];

                    string mainface = Path.Combine(face.FaceStoragePath, $"{face.Name}.jpg");
                    if (File.Exists(mainface))
                    {
                        pictureBoxCurrentFace.BackgroundImage = Image.FromFile(mainface);
                    }
                    else
                    {
                        pictureBoxCurrentFace.BackgroundImage = null;
                    }

                    Global_GUI.UpdateFOLV(FOLV_FaceFiles, face.Files, FullRefresh: true);
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                AITOOL.Log($"Error: {ex.Msg()}");

            }


        }
    }
}
