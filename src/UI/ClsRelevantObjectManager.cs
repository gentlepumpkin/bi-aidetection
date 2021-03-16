using Newtonsoft.Json;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public class ClsRelevantObject : IEquatable<ClsRelevantObject>
    {

        public string Name { get; set; } = "";
        public bool Enabled { get; set; } = true;
        public bool Trigger { get; set; } = true;
        public int Priority { get; set; } = 999;
        public double Threshold_lower { get; set; } = 30;
        public double Threshold_upper { get; set; } = 100;
        public string ActiveTimeRange { get; set; } = "00:00:00-23:59:59";
        public bool IgnoreMask { get; set; } = false;
        public long Hits { get; set; } = 0;
        public DateTime LastHitTime { get; set; } = DateTime.MinValue;
        public DateTime CreatedTime { get; set; } = DateTime.MinValue;
        public int LastHashCode = 0;

        [JsonConstructor]
        public ClsRelevantObject()
        {
            this.Update();
        }
        public ClsRelevantObject(string Name)
        {
            if (Name.TrimStart().StartsWith("-"))
                this.Trigger = false;
            else
                this.Trigger = true;

            if (Name.Equals("*"))
                Name = "Everything";

            this.Name = Name.Trim(" -".ToCharArray()).UpperFirst();
            this.Priority = Priority;
            this.CreatedTime = DateTime.Now;
            this.Update();

        }

        public void Update()
        {
            this.LastHashCode = this.GetHashCode();
        }
        public override string ToString()
        {
            if (this.Trigger)
                return this.Name;
            else
                return "-" + this.Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClsRelevantObject);
        }

        public bool Equals(ClsRelevantObject other)
        {
            return other != null &&
                   this.GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = 349655125;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name.ToLower());
            hashCode = hashCode * -1521134295 + Threshold_lower.GetHashCode();
            hashCode = hashCode * -1521134295 + Threshold_upper.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ActiveTimeRange.ToLower());
            return hashCode;
        }

        public static bool operator ==(ClsRelevantObject left, ClsRelevantObject right)
        {
            return EqualityComparer<ClsRelevantObject>.Default.Equals(left, right);
        }

        public static bool operator !=(ClsRelevantObject left, ClsRelevantObject right)
        {
            return !(left == right);
        }
    }

    //================================================================================================================================================
    //================================================================================================================================================

    public class ClsRelevantObjectManager
    {
        public OrderedDictionary ObjectDict { get; set; } = null;
        public List<ClsRelevantObject> ObjectList { get; set; } = new List<ClsRelevantObject>();
        public string TypeName { get; set; } = "";
        public int EnabledCount { get; set; } = 0;
        public string Camera { get; set; } = "";
        [JsonIgnore]
        private Camera cam = null;
        [JsonIgnore]
        private Camera defaultcam = null;
        [JsonIgnore]
        private bool Initialized = false;
        private int LastHashCode = 0;
        private List<ClsRelevantObject> _DefaultObjectsList = new List<ClsRelevantObject>();
        private object ROLockObject = new object();
        [JsonConstructor]
        public ClsRelevantObjectManager()
        {
            //this.Init();
        }

        public ClsRelevantObjectManager(ClsRelevantObjectManager manager)
        {
            this.TypeName = manager.TypeName;
            this.Camera = manager.Camera;
            this.Init(manager.cam);
            this.ObjectList = this.FromList(manager.ObjectList, true, ExactMatchOnly: true);
            this.Update();
        }
        public ClsRelevantObjectManager(string Objects, string TypeName, Camera cam)
        {
            this.TypeName = TypeName;

            this.Init(cam);

            if (!Objects.Trim(", ".ToCharArray()).IsEmpty())
                this.ObjectList = this.FromString(Objects, true, true);
            else
                this.Reset();

            this.Update();

        }

        public void Init(Camera cam = null)
        {
            if (!cam.IsNull())
                this.cam = cam;

            if (!this.cam.IsNull())
                this.Camera = this.cam.Name;

            //dont initialize until we have a list of cameras available
            if (!this.Initialized || !this.ObjectDict.IsNull() && !AppSettings.Settings.CameraList.IsNull() && AppSettings.Settings.CameraList.Count > 0)
            {

                lock (ROLockObject)
                {
                    //migrate from the dictionary to the list - dictionary no longer used
                    if (!this.ObjectDict.IsNull())
                    {
                        if (this.ObjectDict.Count > 0)
                        {
                            ObjectList.Clear();
                            foreach (Object item in ObjectDict.Values)
                            {
                                if (item is DictionaryEntry)
                                {
                                    ClsRelevantObject ro = (ClsRelevantObject)((DictionaryEntry)item).Value;
                                    ObjectList.Add(ro.CloneJson());
                                }
                                else if (item is ClsRelevantObject)
                                {
                                    ClsRelevantObject ro = (ClsRelevantObject)item;
                                    ObjectList.Add(ro.CloneJson());
                                }
                                else
                                {
                                    AITOOL.Log($"Warn: Old object is {item.GetType().FullName}??");
                                }
                            }

                        }

                        this.ObjectDict = null;
                    }

                    //Add default settings
                    this.Initialized = true;

                    Update();
                }
            }


        }

        public void AddDefaults()
        {
            //Add defaults if missing
            this.ObjectList = this.FromList(this.GetDefaultObjectList(false), false, ExactMatchOnly: true);

        }
        public void Update()
        {

            //sort
            //this.ObjectList = this.ObjectList.OrderByDescending(ro => ro.Enabled).ThenBy(ro => ro.Priority).ThenBy(ro => ro.CreatedTime).ThenBy(ro => ro.Name).ToList();

            if (this.ObjectList.Count == 0 && !this.Camera.EqualsIgnoreCase("default"))
                this.Reset();

            //make sure no priority dupes
            for (int i = 0; i < this.ObjectList.Count; i++)
            {
                this.ObjectList[i].Update();
                this.ObjectList[i].Priority = i + 1;
            }

        }

        public ClsRelevantObject Delete(ClsRelevantObject ro, out int NewIDX)
        {
            ClsRelevantObject ret = ro;

            NewIDX = 0;

            if (ro == null)
                return ro;

            ClsRelevantObject rofound = this.Get(ro, false, out int FoundIDX, true);
            if (!rofound.IsNull())
            {
                this.ObjectList.RemoveAt(FoundIDX);
                NewIDX = FoundIDX - 1;

                if (NewIDX > -1)
                {
                    ret = this.ObjectList[NewIDX];
                }
            }

            return ret;

        }

        public ClsRelevantObject MoveUp(ClsRelevantObject ro, out int NewIDX)
        {
            ClsRelevantObject ret = ro;
            NewIDX = 0;

            if (ro == null)
                return ro;

            ClsRelevantObject rofound = this.Get(ro, false, out int FoundIDX, true);
            if (!rofound.IsNull())
            {
                NewIDX = FoundIDX - 1;

                if (NewIDX > -1)
                {
                    this.ObjectList.Move(FoundIDX, NewIDX);
                    ret = this.ObjectList[NewIDX];
                }
            }

            return ret;

        }
        public ClsRelevantObject MoveDown(ClsRelevantObject ro, out int NewIDX)
        {
            ClsRelevantObject ret = ro;
            NewIDX = 0;

            if (ro == null)
                return ro;

            ClsRelevantObject rofound = this.Get(ro, false, out int FoundIDX, true);
            if (!rofound.IsNull())
            {
                NewIDX = FoundIDX + 1;

                if (NewIDX < this.ObjectList.Count - 1)
                {
                    this.ObjectList.Move(FoundIDX, NewIDX);
                    ret = this.ObjectList[NewIDX];
                }
            }

            return ret;

        }
        public void Reset()
        {
            AITOOL.Log($"Using Relevant Objects list from the 'Default' camera for {this.TypeName} RelevantObjectManager.");
            this.ObjectList = this.GetDefaultObjectList(true);
        }

        public List<ClsRelevantObject> GetDefaultObjectList(bool Clear)
        {
            List<ClsRelevantObject> ret = this._DefaultObjectsList;
            //get the default camera list

            try
            {
                if (this.defaultcam.IsNull())
                    this.defaultcam = AITOOL.GetCamera("Default", true);

                if (!this.defaultcam.IsNull())  //probably here to soon
                {

                    if (this.defaultcam.DefaultTriggeringObjects.ObjectList.Count > 0 && !this.Camera.EqualsIgnoreCase(this.defaultcam.Name))
                    {
                        this._DefaultObjectsList = this.defaultcam.DefaultTriggeringObjects.CloneObjectList();
                    }
                    else
                    {
                        this._DefaultObjectsList = this.FromString(AppSettings.Settings.ObjectPriority, Clear, false);
                    }

                    ret = this._DefaultObjectsList;

                }

            }
            catch (Exception ex)
            {
                AITOOL.Log($"Error: ({this.TypeName}) {ex.Msg()}");
            }

            return ret;
        }

        public List<ClsRelevantObject> CloneObjectList()
        {
            List<ClsRelevantObject> ret = new List<ClsRelevantObject>();
            foreach (var ro in this.ObjectList)
            {
                ret.Add(ro.CloneJson());  //cloning so that when we add default settings from another object manager instance we dont change the original
            }
            return ret;
        }

        public int GetHashCode()
        {
            int ret = 0;
            foreach (var ro in this.ObjectList)
                ret += ro.GetHashCode();
            return ret;
        }

        public List<ClsRelevantObject> FromList(List<ClsRelevantObject> InList, bool Clear, bool ExactMatchOnly)
        {

            List<ClsRelevantObject> ret = new List<ClsRelevantObject>();

            if (InList.Count == 0)
                return ret;

            this.Init();
            if (!this.Initialized)
                return ret;

            if (Clear)
            {
                this.ObjectList.Clear();
                this.EnabledCount = 0;
            }

            ret.AddRange(this.ObjectList);

            int order = ret.Count - 1;

            bool AlreadyHasItems = ret.Count > 0;
            int dupes = 0;

            foreach (var ro in InList)
            {
                ClsRelevantObject rofound = this.Get(ro, false, out int FoundIDX, ExactMatchOnly, ret);

                if (rofound.IsNull())
                {
                    //Only add items as enabled if we started out from an empty list
                    if (AlreadyHasItems)
                        ro.Enabled = false;

                    order++;
                    this.EnabledCount++;
                    ro.Priority = order;
                    ro.Update();
                    ret.Add(ro.CloneJson());
                }
                else
                {
                    dupes++;
                }
            }

            ////force disabled items to be lower priority
            //foreach (var ro in InList)
            //{
            //    ClsRelevantObject rofound = this.Get(ro, false, out int FoundIDX);
            //    if (rofound.IsNull())
            //    {
            //        //Only add items as enabled if we started out from an empty list
            //        if (!AlreadyHasItems)
            //            ro.Enabled = true;
            //        else
            //            ro.Enabled = false;

            //        if (!ro.Enabled)
            //        {
            //            order++;
            //            ro.Priority = order;
            //            ro.Update();
            //            ret.Add(ro);
            //        }
            //    }
            //}

            return ret;
        }
        public List<ClsRelevantObject> FromString(string Objects, bool Clear, bool ExactMatchonly)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            List<ClsRelevantObject> ret = new List<ClsRelevantObject>();

            this.Init();
            if (!this.Initialized)
                return ret;


            try
            {

                if (Clear)
                {
                    this.ObjectList.Clear();
                    this.EnabledCount = 0;
                }
                //take anything before the first semicolon:
                if (Objects.Contains(";"))
                    Objects = Objects.GetWord("", ";");

                bool AlreadyHasItems = ObjectList.Count > 0;

                List<string> lst = Objects.SplitStr(",");
                List<ClsRelevantObject> DefaultObjectList = new List<ClsRelevantObject>();

                if (!this.Camera.EqualsIgnoreCase("default"))
                    DefaultObjectList = this.GetDefaultObjectList(false);

                foreach (var obj in lst)
                {
                    ClsRelevantObject ro = new ClsRelevantObject(obj);

                    ClsRelevantObject rofound = this.Get(ro, false, out int FoundIDX, ExactMatchonly, ret);

                    if (rofound.IsNull())
                    {
                        //Only add items as enabled if we started out from an empty list
                        if (AlreadyHasItems)
                            ro.Enabled = false;

                        if (ro.Enabled)
                            this.EnabledCount++;

                        //set the order if found in the default list
                        ClsRelevantObject defRo = this.Get(ro, false, out int DefFoundIDX, false, DefaultObjectList);

                        if (DefFoundIDX > -1)
                            ro.Priority = DefFoundIDX + 1;
                        else
                            ro.Priority = ret.Count + 1;

                        ret.Add(ro);
                    }
                }


            }
            catch (Exception ex)
            {
                AITOOL.Log("Error: " + ex.Msg());
            }

            return ret;
        }

        public bool TryAdd(ClsRelevantObject ro, bool Enable, out int AddedIDX)
        {
            bool ret = false;
            AddedIDX = -1;

            if (ro.IsNull())
                return false;

            ClsRelevantObject rofound = this.Get(ro, false, out int FoundIDX, true);

            if (rofound.IsNull())
            {
                ro.Priority = this.ObjectList.Count + 1;
                ro.Enabled = Enable;

                if (ro.Enabled)
                    this.EnabledCount++;

                ro.Update();
                this.ObjectList.Add(ro);
                AddedIDX = this.ObjectList.Count - 1;
                ret = true;
            }

            return ret;

        }
        public bool TryAdd(string objname, bool Enable, out int AddedIDX)
        {
            AddedIDX = -1;

            if (objname.IsEmpty() || this.TypeName.IsEmpty())
                return false;

            return this.TryAdd(new ClsRelevantObject(objname), Enable, out AddedIDX);
        }

        public override string ToString()
        {
            string ret = "";
            foreach (var ro in this.ObjectList)
            {
                if (ro.Enabled)
                    ret += ro.ToString() + ", ";
            }
            return ret.Trim(" ,".ToCharArray());
        }

        public ClsRelevantObject Get(ClsRelevantObject roobj, bool AllowEverything, out int FoundIDX, bool ExactMatchOnly, List<ClsRelevantObject> ObjList = null)
        {

            if (ObjList.IsNull())
                ObjList = this.ObjectList;

            //look for exact hashcode first
            FoundIDX = ObjList.IndexOf(roobj);

            if (FoundIDX > -1)
                return ObjList[FoundIDX];

            //search for the last hashcode in case the object has changed
            for (int i = 0; i < ObjList.Count; i++)
            {
                if (ObjList[i].LastHashCode != 0 && ObjList[i].LastHashCode == roobj.LastHashCode && !roobj.Name.EqualsIgnoreCase("new object"))
                {
                    FoundIDX = i;
                    return ObjList[i];
                }

            }

            //fall back to name only search
            if (!ExactMatchOnly)
                return this.Get(roobj.Name, AllowEverything, out FoundIDX, ObjList);

            return null;


        }
        public ClsRelevantObject Get(string objname, bool AllowEverything, out int FoundIDX, List<ClsRelevantObject> ObjList = null)
        {

            if (ObjList.IsNull())
                ObjList = this.ObjectList;

            FoundIDX = -1;
            //Get only by label name
            for (int i = 0; i < ObjList.Count; i++)
            {
                if (ObjList[i].Name.EqualsIgnoreCase(objname))
                {
                    FoundIDX = i;
                    return ObjList[i];
                }
            }

            if (AllowEverything)
            {
                for (int i = 0; i < ObjList.Count; i++)
                {
                    if (ObjList[i].Name.EqualsIgnoreCase("everything"))
                    {
                        FoundIDX = i;
                        return ObjList[i];
                    }
                }
            }

            return null;

        }

        public ResultType IsRelevant(string Label, out bool IgnoreMask, string DbgDetail = "")
        {
            ClsPrediction pred = new ClsPrediction();
            pred.Label = Label;
            return IsRelevant(new List<ClsPrediction>() { pred }, true, out IgnoreMask, DbgDetail);
        }
        public ResultType IsRelevant(ClsPrediction pred, bool IsNew, out bool IgnoreMask, string DbgDetail = "")
        {
            return IsRelevant(new List<ClsPrediction>() { pred }, IsNew, out IgnoreMask, DbgDetail);
        }

        public ResultType IsRelevant(List<ClsPrediction> preds, bool IsNew, out bool IgnoreMask, string DbgDetail = "")
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            ResultType ret = ResultType.UnwantedObject;
            IgnoreMask = false;

            this.Init();
            if (!this.Initialized)
                return ret;


            //if nothing is 'enabled' assume everything should be let through to be on the safe side  (As if they passed an empty list)
            if (this.ObjectList.Count == 0 || this.EnabledCount == 0)  //assume if no list is provided to always return relevant
                return ResultType.Relevant;

            //if fred is found, the whole prediction will be ignored
            //triggering_objects = person, car, -FRED
            //found objects = person, fred

            if (preds != null && preds.Count > 0)
            {
                //find at least one thing in the triggered objects list in order to send
                string notrelevant = "";
                string relevant = "";
                string ignored = "";
                string notenabled = "";
                string nottime = "";
                string nothreshold = "";
                bool ignore = false;

                foreach (ClsPrediction pred in preds)
                {
                    string label = pred.Label;


                    if (pred.Result == ResultType.Relevant || IsNew)
                    {
                        ClsRelevantObject ro = this.Get(label, AllowEverything: true, out int FoundIDX);

                        if (!ro.IsNull())
                        {
                            if (ro.Enabled)
                            {
                                if (Global.IsTimeBetween(DateTime.Now, ro.ActiveTimeRange))
                                {
                                    //assume if confidence is 0 it has not been set yet (dynamic masking routine, etc)
                                    if (pred.Confidence == 0 || pred.Confidence.Round() >= ro.Threshold_lower && pred.Confidence.Round() <= ro.Threshold_upper)
                                    {
                                        ro.LastHitTime = DateTime.Now;
                                        ro.Hits++;
                                        if (!ro.Trigger)
                                        {
                                            ignore = true;
                                            if (!ignored.Contains(label))
                                                ignored += label + ",";
                                        }
                                        else
                                        {
                                            ret = ResultType.Relevant;
                                            IgnoreMask = ro.IgnoreMask;
                                            if (!relevant.Contains(label))
                                                relevant += label + ",";
                                        }

                                    }
                                    else
                                    {
                                        if (!nothreshold.Contains(label))
                                            nothreshold += label + $" ({pred.Confidence.Round()}%),";
                                    }

                                }
                                else
                                {
                                    if (!nottime.Contains(label))
                                        nottime += label + ",";
                                }

                            }
                            else
                            {
                                if (!notenabled.Contains(label))
                                    notenabled += label + ",";
                            }
                        }
                        else
                        {
                            if (!notrelevant.Contains(label))
                                notrelevant += label + ",";
                        }
                    }
                    else
                    {
                        if (!notrelevant.Contains(label))
                            notrelevant += label + ",";
                    }

                }

                //Add to the main list
                if (this.cam == null)
                    this.cam = AITOOL.GetCamera(this.Camera);

                if (this.defaultcam == null)
                    this.defaultcam = AITOOL.GetCamera("Default", true);

                //always try to add the current prediction to the list (disabled) to give them the option of enabling later
                foreach (ClsPrediction pred in preds)
                {
                    this.TryAdd(pred.Label, false, out int AddedIDX);


                    //add it to the Current camera list (disabled)
                    this.cam.DefaultTriggeringObjects.TryAdd(pred.Label, false, out int AddedIDX2);

                    //add it to the default camera list (disabled)
                    if (!this.defaultcam.IsNull())
                        this.defaultcam.DefaultTriggeringObjects.TryAdd(pred.Label, false, out int AddedIDX3);

                }


                if (ignore)
                    ret = ResultType.IgnoredObject;

                if (!DbgDetail.IsEmpty())
                    DbgDetail = $" ({DbgDetail})";

                if (relevant.IsEmpty())
                    relevant = "(NONE)";

                if (notrelevant.IsEmpty())
                    notrelevant = "(NONE)";

                if (ignored.IsEmpty())
                    ignored = "(NONE)";

                if (notenabled.IsEmpty())
                    notenabled = "(NONE)";

                if (nottime.IsEmpty())
                    nottime = "(NONE)";

                if (nothreshold.IsEmpty())
                    nothreshold = "(NONE)";

                string maskignore = "";
                if (IgnoreMask)
                    maskignore = " (Mask will be ignored)";

                if (ret != ResultType.Relevant)
                {
                    AITOOL.Log($"Debug: RelevantObjectManager: Skipping '{this.TypeName}{DbgDetail}' because objects were not defined to trigger, or were set to ignore: Relevant='{relevant.Trim(", ".ToCharArray())}', Irrelevant='{notrelevant.Trim(", ".ToCharArray())}', Caused ignore='{ignored.Trim(", ".ToCharArray())}', Not Enabled={notenabled.Trim(" ,".ToCharArray())}, Not Time={nottime.Trim(" ,".ToCharArray())}, No Threshold Match={nothreshold.Trim(" ,".ToCharArray())}  All Triggering Objects='{this.ToString()}', {preds.Count} predictions(s), Enabled={this.EnabledCount} of {this.ObjectList.Count}");
                }
                else
                {
                    AITOOL.Log($"Trace: RelevantObjectManager: Object is valid for '{this.TypeName}{DbgDetail}' because object(s) '{relevant.Trim(", ".ToCharArray())}' were in trigger objects list '{this.ToString()}',{maskignore} Enabled={this.EnabledCount} of {this.ObjectList.Count}");

                }
            }

            return ret;

        }

    }
}
