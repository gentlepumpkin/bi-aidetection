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
        public long Hits { get; set; } = 0;
        public DateTime LastHitTime { get; set; } = DateTime.MinValue;
        public DateTime CreatedTime { get; set; } = DateTime.MinValue;
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
                   Name.EqualsIgnoreCase(other.Name);
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
        public OrderedDictionary ObjectDict { get; set; } = new OrderedDictionary();
        public string TypeName { get; set; } = "";
        public int EnabledCount { get; set; } = 0;
        public string Camera = "";
        [JsonIgnore]
        private Camera cam = null;
        public ClsRelevantObjectManager()
        {
        }
        public ClsRelevantObjectManager(string Objects, string TypeName, string Camera)
        {
            this.TypeName = TypeName;
            this.Camera = Camera;
            this.FromString(Objects);
        }

        public List<ClsRelevantObject> ToList()
        {
            this.AddDefaults();

            List<ClsRelevantObject> ret = new List<ClsRelevantObject>();
            this.EnabledCount = 0;
            int Order = 0;
            foreach (DictionaryEntry entry in this.ObjectDict)
            {
                ClsRelevantObject ro = (ClsRelevantObject)entry.Value;

                if (ro.Enabled)
                {
                    this.EnabledCount++;
                    Order++;
                    ro.Priority = Order;
                    ret.Add(ro);
                }
            }
            //force disabled items to be lower priority
            foreach (DictionaryEntry entry in this.ObjectDict)
            {
                ClsRelevantObject ro = (ClsRelevantObject)entry.Value;

                if (!ro.Enabled)
                {
                    Order++;
                    ro.Priority = Order;
                    ret.Add(ro);
                }
            }

            return ret;
        }
        public void FromList(List<ClsRelevantObject> InList)
        {
            this.ObjectDict.Clear();
            this.EnabledCount = 0;
            int order = 0;

            foreach (var ro in InList)
            {
                if (!this.ObjectDict.Contains(ro.Name.ToLower()))
                {
                    if (ro.Enabled)
                    {
                        order++;
                        this.EnabledCount++;
                        ro.Priority = order;
                        this.ObjectDict.Add(ro.Name.ToLower(), ro);
                    }
                }
            }

            //force disabled items to be lower priority
            foreach (var ro in InList)
            {
                if (!this.ObjectDict.Contains(ro.Name.ToLower()))
                {
                    if (!ro.Enabled)
                    {
                        order++;
                        ro.Priority = order;
                        this.ObjectDict.Add(ro.Name.ToLower(), ro);
                    }
                }
            }
        }
        public void FromString(string Objects)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            try
            {
                //take anything before the first semicolon:
                if (Objects.Contains(";"))
                    Objects = Objects.GetWord("", ";");

                this.EnabledCount = 0;
                int order = 0;

                List<string> lst = Objects.SplitStr(",");
                List<string> deflst = AppSettings.Settings.ObjectPriority.ToLower().SplitStr(",");

                foreach (var obj in lst)
                {
                    ClsRelevantObject ro = new ClsRelevantObject(obj);

                    if (!ObjectDict.Contains(ro.Name.ToLower()))
                    {
                        if (ro.Enabled)
                            this.EnabledCount++;

                        order = deflst.IndexOf(ro.Name.ToLower());

                        if (order > -1)
                            ro.Priority = order + 1;

                        ObjectDict.Add(ro.Name.ToLower(), ro);
                    }
                }

                AddDefaults();


            }
            catch (Exception ex)
            {
                AITOOL.Log("Error: " + ex.Msg());
            }
        }

        public void AddDefaults()
        {
            //Make sure the default objects are always in the list

            List<string> deflst = AppSettings.Settings.ObjectPriority.SplitStr(",");
            bool AlreadyHasItems = ObjectDict.Count > 0;

            for (int i = 0; i < deflst.Count; i++)
            {
                ClsRelevantObject ro = new ClsRelevantObject(deflst[i]);

                if (!ObjectDict.Contains(ro.Name.ToLower()))
                {
                    //if no items are currently in the dictionary, assume we want to ENABLE all the objects
                    //  Otherwise, Disable objects so that existing lists from old versions dont suddenly have everything enabled that shouldnt be
                    if (!AlreadyHasItems)
                        ro.Enabled = true;
                    else
                        ro.Enabled = false;

                    if (ro.Enabled)
                        this.EnabledCount++;

                    ro.Priority = i + 1;

                    ObjectDict.Add(ro.Name.ToLower(), ro);
                }
                else
                {
                    if (!AlreadyHasItems)
                        ro.Priority = i + 1;
                }
            }
        }


        public bool TryDelete(ClsRelevantObject ro)
        {
            bool ret = false;

            if (ro.IsNull())
                return false;

            if (ObjectDict.Contains(ro.Name.ToLower()))
            {
                ObjectDict.Remove(ro.Name.ToLower());
                ret = true;
            }

            return ret;

        }
        public bool TryAdd(ClsRelevantObject ro, bool Enable)
        {
            bool ret = false;

            if (ro.IsNull())
                return false;

            if (!ObjectDict.Contains(ro.Name.ToLower()))
            {
                ro.Priority = ObjectDict.Count + 1;
                ro.Enabled = Enable;

                if (ro.Enabled)
                    this.EnabledCount++;
                ObjectDict.Add(ro.Name.ToLower(), ro);
                ret = true;
            }

            return ret;

        }
        public bool TryAdd(string objname, bool Enable)
        {
            if (objname.IsEmpty() || this.TypeName.IsEmpty())
                return false;

            return this.TryAdd(new ClsRelevantObject(objname), Enable);
        }

        public override string ToString()
        {
            string ret = "";
            foreach (DictionaryEntry entry in this.ObjectDict)
            {
                ClsRelevantObject ro = (ClsRelevantObject)entry.Value;
                if (ro.Enabled)
                    ret += ro.ToString() + ", ";
            }
            return ret.Trim(" ,".ToCharArray());
        }

        public ClsRelevantObject Get(string objname)
        {
            if (this.ObjectDict.Contains(objname.ToLower()))
                return (ClsRelevantObject)this.ObjectDict[objname.ToLower()];
            else if (this.ObjectDict.Contains("everything"))
                return (ClsRelevantObject)this.ObjectDict["everything"];

            return null;

        }

        public ResultType IsRelevant(string Label, bool IsNew, string DbgDetail = "")
        {
            ClsPrediction pred = new ClsPrediction();
            pred.Label = Label;
            return IsRelevant(new List<ClsPrediction>() { pred }, IsNew, DbgDetail);
        }
        public ResultType IsRelevant(ClsPrediction pred, bool IsNew, string DbgDetail = "")
        {
            return IsRelevant(new List<ClsPrediction>() { pred }, IsNew, DbgDetail);
        }

        public ResultType IsRelevant(List<ClsPrediction> preds, bool IsNew, string DbgDetail = "")
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            ResultType ret = ResultType.UnwantedObject;

            //if nothing is 'enabled' assume everything should be let through to be on the safe side  (As if they passed an empty list)
            if (this.ObjectDict.Count == 0 || this.EnabledCount == 0)  //assume if no list is provided to always return relevant
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
                        ClsRelevantObject ro = this.Get(label);

                        if (!ro.IsNull())
                        {
                            if (ro.Enabled)
                            {
                                if (Global.IsTimeBetween(DateTime.Now, ro.ActiveTimeRange))
                                {
                                    if (pred.Confidence.Round() >= ro.Threshold_lower && pred.Confidence.Round() <= ro.Threshold_upper)
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
                                            if (!relevant.Contains(label))
                                                relevant += label + ",";
                                        }

                                    }
                                    else
                                    {
                                        if (!nothreshold.Contains(label))
                                            nothreshold += label + $"({pred.Confidence.Round()}%),";
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
                                    notenabled += notenabled + ",";
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

                //always try to add the current prediction to the list (disabled) to give them the option of enabling later
                foreach (ClsPrediction pred in preds)
                {
                    this.TryAdd(pred.Label, false);


                    if (this != this.cam.DefaultTriggeringObjects)
                        this.cam.DefaultTriggeringObjects.TryAdd(pred.Label, false);

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

                if (ret != ResultType.Relevant)
                {
                    AITOOL.Log($"Debug: RelevantObjectManager: Enabled={this.EnabledCount} of {this.ObjectDict.Count}, Skipping {this.TypeName}{DbgDetail} because objects were not defined to trigger, or were set to ignore: Relevant='{relevant.Trim(", ".ToCharArray())}', Irrelevant='{notrelevant.Trim(", ".ToCharArray())}', Caused ignore='{ignored.Trim(", ".ToCharArray())}', Not Enabled={notenabled.Trim(" ,".ToCharArray())}, Not Time={nottime.Trim(" ,".ToCharArray())}, No Threshold Match={nothreshold.Trim(" ,".ToCharArray())}  All Triggering Objects='{this.ToString()}', {preds.Count} predictions(s)");
                }
                else
                {
                    AITOOL.Log($"Debug: RelevantObjectManager: Enabled={this.EnabledCount} of {this.ObjectDict.Count}, Valid prediction for {this.TypeName}{DbgDetail} because object(s) '{relevant.Trim(", ".ToCharArray())}' were in trigger objects list '{this.ToString()}'");

                }
            }

            return ret;

        }

    }
}
