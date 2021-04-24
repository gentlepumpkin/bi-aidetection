using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Octokit;
using Octokit.Helpers;
using Octokit.Internal;

using Telegram.Bot.Types;

using static AITool.AITOOL;

namespace AITool
{
    public partial class Frm_UpdateCheck : Form
    {
        private DateTime CurrentVerTime = DateTime.MinValue;
        private GitHubClient client = null;
        private IReadOnlyList<Release> releases = null;

        public Frm_UpdateCheck()
        {
            InitializeComponent();
        }

        private void Frm_UpdateCheck_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);



        }

        private void Frm_UpdateCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }


        private class ReleaseNote
        {
            public DateTime Date;
            public String Title = "";
            public String Body = "";
            public string Type = "";
            public string Version = "";
        }

        private async void bt_check_Click(object sender, EventArgs e)
        {

            try
            {

                Assembly CurAssm = Assembly.GetExecutingAssembly();
                string AssemVer = CurAssm.GetName().Version.Major + "." + CurAssm.GetName().Version.Minor + "." + CurAssm.GetName().Version.Build;
                CurrentVerTime = Global.RetrieveLinkerTimestamp();
                lbl_CurrentVersion.Text = $"{AssemVer} ({CurrentVerTime.ToShortDateString()})";

                List<ReleaseNote> notes = new List<ReleaseNote>();

                using Global_GUI.CursorWait cw = new Global_GUI.CursorWait();
                bt_check.Enabled = false;
                bt_check.Text = "Checking...";

                //first get the most recent release:
                if (client.IsNull())
                    client = new GitHubClient(new ProductHeaderValue("AITOOL-VORLONCD"));


                Log("Loading latest Github release...");

                releases = await client.Repository.Release.GetAll("VorlonCD", "bi-aidetection");

                //var assets = await client.rel Release.GetAllAssets("VorlonCD", "bi-aidetection", releases[0]);
                //var myAsset_zipFile = assets[0];

                string releasever = $"{releases[0].TagName} ({releases[0].PublishedAt.Value.LocalDateTime.ToShortDateString()})";
                linkLabelRelease.Text = releasever;

                ReleaseNote rn = new ReleaseNote();
                rn.Date = releases[0].PublishedAt.Value.LocalDateTime;
                rn.Title = releases[0].Name;
                rn.Body = releases[0].Body;
                rn.Version = releases[0].TagName;
                rn.Type = "Release";
                notes.Add(rn);



                //get the latest beta version installer file
                var repo = await client.Repository.Get("VorlonCD", "bi-aidetection");

                var contents = await client.Repository.Content.GetAllContents("VorlonCD", "bi-aidetection", "src/UI/Installer");

                //get the date on the file
                var path = contents[0].Path; //"src/UI/Installer"; 
                var branch = "master";

                var request = new CommitRequest { Path = path, Sha = branch };

                // find the latest commit to the file on a specific branch
                var commitsForFile = await client.Repository.Commit.GetAll(repo.Id, request);
                var mostRecentCommit = commitsForFile[0];
                var authorDate = mostRecentCommit.Commit.Author.Date;
                var fileEditDate = authorDate.LocalDateTime;

                string ver = contents[0].Name.GetWord(".", ".exe");
                string betaver = $"{ver} ({fileEditDate.ToShortDateString()})";
                linkLabelBeta.Text = betaver;

                if (CurAssm.GetName().Version < new Version(ver))
                {
                    lbl_message.Visible = true;
                }
                else
                {
                    lbl_message.Visible = false;
                }

                var commits = await client.Repository.Commit.GetAll("VorlonCD", "bi-aidetection");

                for (int i = 0; i < commits.Count; i++)
                {
                    if (commits[i].Commit.Author.Date > releases[0].PublishedAt.Value.LocalDateTime)
                    {
                        rn = new ReleaseNote();
                        rn.Date = commits[i].Commit.Author.Date.LocalDateTime;
                        rn.Title = !commits[i].Label.IsNull() ? rn.Title : "";
                        rn.Body = commits[i].Commit.Message;
                        bool hasdash = rn.Body.TrimStart().StartsWith("-");
                        bool hasstar = rn.Body.TrimStart().StartsWith("*");
                        if (!hasdash && !hasstar)
                            rn.Body = "* " + rn.Body.Trim();

                        if (i == 0)
                            rn.Version = ver;
                        else
                            rn.Version = "";

                        rn.Type = "Commit";
                        notes.Insert(0, rn);
                    }
                }


                //sort by date
                notes = notes.OrderByDescending((d) => d.Date).ToList();

                StringBuilder Markup = new StringBuilder();


                foreach (var note in notes)
                {
                    Markup.AppendLine($"{note.Version} ({note.Date}) {note.Title}");
                    Markup.AppendLine("");
                    Markup.AppendLine($"{note.Body}");
                    Markup.AppendLine("");
                    Markup.AppendLine("");
                }

                var html = Markdig.Markdown.ToHtml(Markup.ToString());
                webBrowser1.DocumentText = html;

                // get the download URL for this file on a specific branch
                //var file = await client.Repository.Content.GetAllContentsByRef(repo.Id, path, branch);

                bt_InstallBeta.Enabled = true;
                bt_installRelease.Enabled = true;

                //Setup the versions
                //Version latestGitHubVersion = new Version(releases[0].TagName);
                //Version localVersion = new Version("X.X.X");


            }
            catch (Exception ex)
            {

                Log("Error: " + ex.Msg());
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bt_check.Enabled = true;
                bt_check.Text = "Check";
            }
        }

        private void linkLabelRelease_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/VorlonCD/bi-aidetection/releases");
        }

        private void linkLabelBeta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/VorlonCD/bi-aidetection/tree/master/src/UI/Installer");
        }

        private async void bt_installRelease_Click(object sender, EventArgs e)
        {
            try
            {
                using Global_GUI.CursorWait cw = new Global_GUI.CursorWait();

                bt_installRelease.Enabled = false;
                bt_installRelease.Text = "Working";

                string filename = Path.Combine(Directory.GetCurrentDirectory(), releases[0].Assets[0].Name);

                if (!System.IO.File.Exists(filename))
                {
                    var response = await client.Connection.Get<object>(new Uri(releases[0].Assets[0].Url), new Dictionary<string, string>(), "application/octet-stream");
                    System.IO.File.WriteAllBytes(filename, (byte[])response.Body);

                }

                ExploreFile(filename);
                Process.Start(filename);

                this.DialogResult = DialogResult.OK;
                this.Close();
                //if (filename.Has(".exe"))

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, "Error downloading", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bt_installRelease.Enabled = true;
                bt_installRelease.Text = "Download";

            }
        }

        public bool ExploreFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }
            //Clean up file path so it can be navigated OK
            filePath = System.IO.Path.GetFullPath(filePath);
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            return true;
        }

        private async void bt_InstallBeta_Click(object sender, EventArgs e)
        {


            try
            {
                using Global_GUI.CursorWait cw = new Global_GUI.CursorWait();

                bt_InstallBeta.Enabled = false;
                bt_InstallBeta.Text = "Working";

                //get the latest beta version installer file
                var repo = await client.Repository.Get("VorlonCD", "bi-aidetection");

                var contents = await client.Repository.Content.GetAllContents("VorlonCD", "bi-aidetection", "src/UI/Installer");

                //Octokit.ApiException: 'Unsupported 'Accept' header: 'application/octet-stream'. Must accept 'application/json'.'

                string filename = Path.Combine(Directory.GetCurrentDirectory(), contents[0].Name);

                if (!System.IO.File.Exists(filename))
                {
                    var response = await client.Connection.Get<object>(new Uri(contents[0].DownloadUrl), new Dictionary<string, string>(), "application/octet-stream");

                    System.IO.File.WriteAllBytes(filename, (byte[])response.Body);

                }
                ExploreFile(filename);
                Process.Start(filename);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, "Error downloading", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bt_InstallBeta.Enabled = true;
                bt_InstallBeta.Text = "Download";

            }
        }

        private void linkLabelReportIssue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/VorlonCD/bi-aidetection/issues");
        }

        private void linkLabelIPCam_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://ipcamtalk.com/threads/tool-tutorial-free-ai-person-detection-for-blue-iris.37330/");
        }

        private void btn_Donate_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/sponsors/VorlonCD");
        }
    }
}
