using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AITool.Global;
using static AITool.AITOOL;
using Amazon.Rekognition.Model;
using Newtonsoft.Json;

namespace AITool
{
    public class ClsFace : IEquatable<ClsFace>
    {
        public string Name { get; set; } = "";
        public long Hits { get; set; } = 0;
        public string FaceStoragePath { get; set; } = "";
        public Dictionary<string, ClsFaceFile> Files { get; set; } = new Dictionary<string, ClsFaceFile>();
        //public List<ClsFaceFile> Files = new List<ClsFaceFile>();

        [JsonConstructor]
        public ClsFace()
        {
            this.Update();
        }

        public ClsFace(string name)
        {
            Name = name;
            this.Update();
        }

        public void Update()
        {
            this.FaceStoragePath = Path.Combine(AppSettings.Settings.FacesPath, this.Name);
            if (!Directory.Exists(this.FaceStoragePath))
                Directory.CreateDirectory(this.FaceStoragePath);

        }

        public bool TryAddFile(string Filename, out string Newfilename)
        {
            Newfilename = "";

            if (Filename.IsEmpty() || !File.Exists(Filename))
                return false;

            string fname = Path.GetFileName(Filename);
            Newfilename = Path.Combine(this.FaceStoragePath, fname);

            if (!this.Files.ContainsKey(fname.ToLower()))
            {
                //if in different location, copy it in
                if (!Filename.EqualsIgnoreCase(Newfilename))
                    File.Copy(Filename, Newfilename, true);

                ClsFaceFile ff = new ClsFaceFile(Newfilename);

                ff.OriginalPath = Filename;

                ff.OriginalCamera = GetCamera(ff.OriginalPath, true).Name;

                this.Files.Add(fname.ToLower(), ff);

                return true;
            }

            return false;

        }
        public override bool Equals(object obj)
        {
            return Equals(obj as ClsFace);
        }

        public bool Equals(ClsFace other)
        {
            return other != null &&
                   Name.EqualsIgnoreCase(other.Name);
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(ClsFace left, ClsFace right)
        {
            return EqualityComparer<ClsFace>.Default.Equals(left, right);
        }

        public static bool operator !=(ClsFace left, ClsFace right)
        {
            return !(left == right);
        }
    }

    public class ClsFaceFile : IEquatable<ClsFaceFile>
    {
        public ClsFaceFile(string filepath)
        {
            this.FilePath = filepath;
            this.Name = Path.GetFileName(filepath);
            this.DateAdded = DateTime.Now;
            this.Update();
        }

        public void Update()
        {
            if (File.Exists(this.FilePath))
            {
                this.Exists = true;
                this.DateFileModified = new FileInfo(this.FilePath).LastWriteTime;
            }
            else
            {
                this.Exists = false;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClsFaceFile);
        }

        public bool Equals(ClsFaceFile other)
        {
            return other != null &&
                   Name.EqualsIgnoreCase(other.Name);
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public string Name { get; set; } = "";
        public int RegisterTimeMS { get; set; } = 0;
        public string FilePath { get; set; } = "";
        public bool Exists = false;
        public DateTime DateRegistered { get; set; } = DateTime.MinValue;
        public DateTime DateAdded { get; set; } = DateTime.MinValue;
        public DateTime DateFileModified { get; set; } = DateTime.MinValue;
        public string OriginalPath { get; set; } = "";
        public string OriginalCamera { get; set; } = "Unknown";

        public static bool operator ==(ClsFaceFile left, ClsFaceFile right)
        {
            return EqualityComparer<ClsFaceFile>.Default.Equals(left, right);
        }

        public static bool operator !=(ClsFaceFile left, ClsFaceFile right)
        {
            return !(left == right);
        }
    }
    public class ClsFaceManager
    {
        public List<ClsFace> Faces { get; set; } = new List<ClsFace>();
        public int MaxFilesPerFace = 512;
        private object FaceLock = new object();
        public ClsFaceManager()
        {
            //update in background thread
            Task.Run(this.Update);
        }

        public void Update()
        {
            using var Trace = new Trace();

            int missingfiles = 0;
            int addedfiles = 0;

            try
            {

                Log("Debug: Updating faces...");


                if (!Directory.Exists(AppSettings.Settings.FacesPath))
                    Directory.CreateDirectory(AppSettings.Settings.FacesPath);


                lock (FaceLock)
                {

                    this.TryAddFace("Unknown");

                    //Add any existing subfolders as new faces if not already in the list
                    string[] facedirs = Directory.GetDirectories(AppSettings.Settings.FacesPath, "*", SearchOption.TopDirectoryOnly);
                    foreach (var facedir in facedirs)
                        this.TryAddFace(facedir);

                    foreach (var Face in this.Faces)
                    {

                        foreach (var file in Face.Files.Values)
                            file.Update();

                        //remove any missing files
                        var itemsToRemove = Face.Files.Where(f => !f.Value.Exists).ToArray();
                        foreach (var item in itemsToRemove)
                            Face.Files.Remove(item.Key);

                        missingfiles += itemsToRemove.Length;

                        //scan the folder for new files
                        List<FileInfo> newfiles = Global.GetFiles(Face.FaceStoragePath, "*.jpg;*.jpeg;*.bmp;*.png", SearchOption.TopDirectoryOnly);
                        foreach (FileInfo fi in newfiles)
                            if (Face.TryAddFile(fi.FullName, out string newfilename))
                                addedfiles++;

                    }

                }



            }
            catch (Exception ex)
            {

                Log($"Error: {ex.Msg()}");
            }

            Log($"{this.Faces.Count} faces. Added {addedfiles} new files, Removed {missingfiles} missing files.");


        }

        public bool TryAddFile(string filename, string face = "")
        {
            if (filename.IsEmpty())
                return false;

            if (face.IsEmpty())
                face = "Unknown";

            ClsFace FoundFace = this.TryAddFace(face);

            bool added = FoundFace.TryAddFile(filename, out string Outfilename);

            //delete from unknown folder if it was originally from there and it moved
            if (added && filename.Has("\\unknown\\") && !Outfilename.Has("\\unknown\\"))
                Global.SafeFileDelete(filename);

            return added;
        }

        public ClsFace TryAddFace(string face)
        {
            ClsFace FoundFace = new ClsFace(face);

            int fnd = this.Faces.IndexOf(FoundFace);

            if (fnd > -1)
                FoundFace = this.Faces[fnd];
            else
            {
                lock (FaceLock)
                    this.Faces.Add(FoundFace);
            }

            return FoundFace;
        }

    }
}
