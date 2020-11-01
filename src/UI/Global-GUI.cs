using BrightIdeasSoftware;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static AITool.AITOOL;

namespace AITool
{
    public static class Global_GUI
    {

        // ====================================================================
        // ALL FUNCTIONS HERE ARE GUI HELPER FUNCTIONS AND NOT UNIQUE TO AITOOL
        // NO direct UI interaction
        // ====================================================================


        public static void UpdateFOLV(FastObjectListView olv, IList objs, bool Follow = false, ColumnHeaderAutoResizeStyle ResizeColsStyle = ColumnHeaderAutoResizeStyle.None, bool FullRefresh = false, bool UseSelected = false, object SelectObject = null, [CallerMemberName()] string memberName = null)
        {

            Global_GUI.InvokeIFRequired(olv, () =>
            {

                try
                {
                    int oldcnt = olv.Items.Count;

                    if (oldcnt == 0)
                    {
                        olv.EmptyListMsg = "Loading...";
                    }

                    //Debug.Print($"{DateTime.Now} - {memberName} > UpdateFOLV: {objs.Count} items, Follow={Follow}");

                    olv.Freeze();  //accessed from another thread

                    if (FullRefresh)
                    {
                        olv.ClearCachedInfo();
                        olv.ClearObjects();
                    }

                    if (FullRefresh || olv.Items.Count == 0)  //full refresh of new objects
                        olv.SetObjects(objs, true);
                    else
                        olv.AddObjects(objs);  //minimize list changes

                    if (olv.Items.Count > 0)
                    {
                        if (Follow)
                        {
                            if (SelectObject != null && UseSelected)
                            {
                                //use the given object as selected
                                olv.SelectedObject = SelectObject;  //olv.Items.Count - 1;
                            }
                            else if (SelectObject == null && UseSelected && olv.IsFiltering && olv.GetItemCount() > 0)
                            {
                                //use the last filtered object as the selected object
                                olv.SelectedObject = olv.FilteredObjects.Cast<object>().Last();
                            }
                            else if (!olv.IsFiltering)
                            {
                                //just use the last object given to us
                                olv.SelectedObject = objs[objs.Count - 1];  //olv.Items.Count - 1;
                            }


                            if (olv.SelectedObject != null)
                                olv.EnsureModelVisible(olv.SelectedObject);

                        }

                        //update column size only if did not restore folv state file or forced
                        if (olv.Tag == null)
                        {
                            olv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                            olv.Tag = "resizedcols";
                        }
                        else if (ResizeColsStyle != ColumnHeaderAutoResizeStyle.None)
                        {
                            olv.AutoResizeColumns(ResizeColsStyle);
                            olv.Tag = "resizedcols";
                        }
                    }
                    else
                    {
                        olv.EmptyListMsg = "Empty";
                    }

                }
                catch (Exception ex)
                {
                    Log("Error: " + Global.ExMsg(ex));
                }
                finally
                {
                    olv.Unfreeze();
                    if (FullRefresh)
                        olv.Refresh();
                }

            });

        }

        public static void FilterFOLV(FastObjectListView OLV, string FilterText, bool Filter)
        {
            using var cw = new Global_GUI.CursorWait();

            try
            {
                if (!string.IsNullOrEmpty(FilterText))
                {
                    if (Filter)
                    {
                        OLV.UseFiltering = true;
                    }
                    else
                    {
                        OLV.UseFiltering = false;
                    }
                    TextMatchFilter filter = TextMatchFilter.Regex(OLV, FilterText);
                    OLV.ModelFilter = filter;
                    HighlightTextRenderer renderererer = new HighlightTextRenderer(filter);
                    SolidBrush brush = renderererer.FillBrush as SolidBrush ?? new SolidBrush(Color.Transparent);
                    brush.Color = System.Drawing.Color.FromArgb(100, Color.LightSeaGreen); //
                    renderererer.FillBrush = brush;
                    OLV.DefaultRenderer = renderererer;
                    Global.SaveSetting("SearchText", FilterText);

                }
                else
                {
                    OLV.ModelFilter = null;
                }
                OLV.Refresh();
                //Application.DoEvents();
            }
            catch { }
        }

        public static void ConfigureFOLV(FastObjectListView FOLV, Type Cls, System.Drawing.Font Fnt, ImageList ImageList,
                                         string PrimarySortColumnName = "",
                                         SortOrder PrimarySortOrder = SortOrder.Ascending,
                                         string SecondarySortColumnName = "",
                                         SortOrder SecondarySortOrder = SortOrder.Ascending,
                                         List<string> FilterColumnList = null,
                                         Color Clr = new Color(),
                                         int RowHeight = 0,
                                         bool ShowGroups = false,
                                         bool GridLines = true)
        {


            //Global_GUI.InvokeIFRequired(FOLV, () =>
            //{

            try
            {

                FOLV.AllowColumnReorder = true;
                FOLV.CopySelectionOnControlC = true;
                FOLV.FullRowSelect = true;
                FOLV.GridLines = GridLines;
                FOLV.HideSelection = false;
                FOLV.IncludeColumnHeadersInCopy = true;
                FOLV.OwnerDraw = true;
                FOLV.SelectColumnsOnRightClick = true;
                FOLV.SelectColumnsOnRightClickBehaviour = ObjectListView.ColumnSelectBehaviour.InlineMenu;
                FOLV.SelectedColumnTint = Color.LawnGreen;
                FOLV.ShowCommandMenuOnRightClick = true;
                FOLV.ShowFilterMenuOnRightClick = true;
                FOLV.ShowGroups = false;
                FOLV.ShowImagesOnSubItems = true;
                FOLV.ShowItemCountOnGroups = true;
                FOLV.ShowItemToolTips = true;
                FOLV.ShowSortIndicators = true;
                FOLV.SortGroupItemsByPrimaryColumn = true;
                FOLV.TintSortColumn = true;
                FOLV.UseFiltering = true;
                FOLV.UseHyperlinks = false; //may cause column save/restore error?
                FOLV.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
                FOLV.UseCellFormatEvents = true;
                FOLV.UseNotifyPropertyChanged = true;

                if (ImageList != null)
                {
                    FOLV.SmallImageList = ImageList;
                }

                if (Fnt != null)
                {
                    FOLV.Font = Fnt;
                }
                else
                {
                    FOLV.Font = new Font("Consolas", 8, FontStyle.Regular);
                }

                if (Clr.IsEmpty)
                {
                    FOLV.ForeColor = Clr;
                }

                PropertyInfo[] IIProps2 = Cls.GetProperties(); //Cls.GetType().GetProperties


                // Uncomment this to see a fancy cell highlighting while editing
                EditingCellBorderDecoration EC = new EditingCellBorderDecoration(true);
                EC.UseLightbox = true;

                FOLV.AddDecoration(EC);
                FOLV.BuildList();
                int colcnt = 0;

                foreach (PropertyInfo ei in IIProps2)
                {
                    if (typeof(IEnumerable).IsAssignableFrom(ei.PropertyType) && !(ei.PropertyType.Name == "String"))
                    {
                        continue;
                    }
                    else if (ei.PropertyType.Name == "Object")
                    {
                        continue;
                    }

                    colcnt = colcnt + 1;
                    OLVColumn cl = new OLVColumn();
                    if (ImageList != null)
                    {

                        if (string.Equals(FOLV.Name, "folv_history", StringComparison.OrdinalIgnoreCase))
                        {
                            if (colcnt == 1)
                            {
                                cl.ImageGetter = GetImageForHistoryList;
                            }
                            else if (ei.Name == "IsPerson")
                            {
                                cl.ImageGetter = GetImageForHistoryListPerson;
                                cl.AspectToStringConverter = delegate (object x)
                                {
                                    return String.Empty;
                                };
                            }
                            else if (ei.Name == "Success")
                            {
                                cl.ImageGetter = GetImageForHistoryListSuccess;
                                cl.AspectToStringConverter = delegate (object x)
                                {
                                    return String.Empty;
                                };
                            }
                        }
                        //else if (FOLV.Name == "FOLV_BlocklistViewer" && ei.Name == "RegionalInternetRegistry")
                        //{
                        //	cl.ImageGetter = GetImageForBlocklistViewerRIR;
                        //}
                        //else if (FOLV.Name == "FOLV_Apps" && colcnt == 1)
                        //{
                        //	cl.ImageGetter = GetImageForProdList;
                        //}
                    }
                    //cl.AspectName = ei.Name
                    cl.UseFiltering = true;
                    cl.Searchable = true;
                    cl.Text = ei.Name;
                    cl.Name = ei.Name;

                    cl.DataType = ei.PropertyType;
                    cl.AspectName = ei.Name;

                    if (cl.Name == "Func" || ei.PropertyType.Name == "Int64" || ei.PropertyType.Name == "Int32" || ei.PropertyType.Name == "Timespan")
                    {
                        cl.TextAlign = HorizontalAlignment.Right;
                    }

                    if (ei.Name == "UniqueID" || ei.Name == "FoundElementList")
                    {
                        cl.MinimumWidth = 0;
                        cl.MaximumWidth = 0;
                        cl.Width = 0;
                    }
                    else
                    {
                        cl.MinimumWidth = 50;
                        //cl.MaximumWidth = 20000
                        //cl.Width = -2
                    }

                    //if (ei.Name.ToLower().Contains("url"))
                    //{
                    //	cl.Hyperlink = true;
                    //}
                    //If cl.DataType = GetType(Boolean) Then
                    //    cl.CheckBoxes = True
                    //    cl.IsEditable = False
                    //End If


                    if (string.Equals(ei.Name, PrimarySortColumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        FOLV.PrimarySortColumn = cl;
                        FOLV.PrimarySortOrder = PrimarySortOrder; //SortOrder.Descending

                        //cl.ImageGetter = AddressOf GetImageFromProd
                    }
                    if (string.Equals(ei.Name, SecondarySortColumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        FOLV.SecondarySortColumn = cl;
                        FOLV.SecondarySortOrder = SecondarySortOrder; //SortOrder.Descending
                    }

                    FOLV.AllColumns.Add(cl);  //Columns vs AllColumns?

                }

                //restore column state if it exists
                FOLVRestoreState(FOLV);

                //OLV.RebuildColumns()
                FOLV.Refresh();
                FOLV.BuildList();
                FOLV.RebuildColumns();

                //Application.DoEvents();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {

            }

            //});

        }

        public class FOLVObjectListViewState
        {
            public int VersionNumber = 1;
            public int NumberOfColumns = 1;
            public View CurrentView;
            public int SortColumn = -1;
            public bool IsShowingGroups;
            public SortOrder LastSortOrder = SortOrder.None;
            public ArrayList ColumnIsVisible = new ArrayList();
            public ArrayList ColumnDisplayIndicies = new ArrayList();
            public ArrayList ColumnWidths = new ArrayList();
        }

        public static bool FOLVRestoreState(FastObjectListView FOLV)
        {
            bool Ret = false;

            try
            {
                string Filename = Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), $"ListSaveState-{FOLV.Name}.JSON");

                if (!File.Exists(Filename))
                    return false;

                FOLVObjectListViewState olvState = Global.ReadFromJsonFile<FOLVObjectListViewState>(Filename);

                // The number of columns has changed. We have no way to match old
                // columns to the new ones, so we just give up.
                if (olvState == null || olvState.NumberOfColumns != FOLV.AllColumns.Count)
                {
                    Log("Debug: olvState Is null OrElse olvState.NumberOfColumns <> FOLV.AllColumns.Count.");
                    return false;
                }

                if (olvState.SortColumn == -1)
                {
                    FOLV.PrimarySortColumn = null;
                    FOLV.PrimarySortOrder = SortOrder.None;
                }
                else
                {
                    FOLV.PrimarySortColumn = FOLV.AllColumns[olvState.SortColumn]; // FOLV.AllColumns(olvState.SortColumn)
                    FOLV.PrimarySortOrder = olvState.LastSortOrder;
                }

                for (int i = 0; i <= olvState.NumberOfColumns - 1; i++)
                {
                    OLVColumn column = FOLV.AllColumns[i]; // FOLV.AllColumns(i)
                    column.Width = System.Convert.ToInt32(olvState.ColumnWidths[i]);
                    column.IsVisible = System.Convert.ToBoolean(olvState.ColumnIsVisible[i]);
                    column.LastDisplayIndex = System.Convert.ToInt32(olvState.ColumnDisplayIndicies[i]);
                }

                // ReSharper disable RedundantCheckBeforeAssignment
                if (olvState.IsShowingGroups != FOLV.ShowGroups)
                    // ReSharper restore RedundantCheckBeforeAssignment
                    FOLV.ShowGroups = olvState.IsShowingGroups;

                if (FOLV.View == olvState.CurrentView)
                    FOLV.RebuildColumns();
                else
                    FOLV.View = olvState.CurrentView;

                //tell other functions not to resize columns unless forced
                FOLV.Tag = "resizedcols";

                Ret = true;
            }
            catch (Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex));
            }

            return Ret;
        }

        public static bool FOLVSaveState(FastObjectListView FOLV)
        {
            bool ret = false;

            try
            {
                FOLVObjectListViewState olvState = new FOLVObjectListViewState();

                olvState.VersionNumber = 1;
                olvState.NumberOfColumns = FOLV.AllColumns.Count; // FOLV.AllColumns.Count
                olvState.CurrentView = FOLV.View;

                // If we have a sort column, it is possible that it is not currently being shown, in which
                // case, it's Index will be -1. So we calculate its index directly. Technically, the sort
                // column does not even have to a member of AllColumns, in which case IndexOf will return -1,
                // which is works fine since we have no way of restoring such a column anyway.
                if (FOLV.PrimarySortColumn != null)
                    olvState.SortColumn = FOLV.AllColumns.IndexOf(FOLV.PrimarySortColumn);// FOLV.AllColumns.IndexOf(FOLV.PrimarySortColumn)
                olvState.LastSortOrder = FOLV.PrimarySortOrder;
                olvState.IsShowingGroups = FOLV.ShowGroups;

                // If FOLV.Columns.Count > 0 AndAlso FOLV.Columns(0).LastDisplayIndex = -1 Then  'FOLV.AllColumns(0).LastDisplayIndex = -1
                // 'FOLV.RememberDisplayIndicies()
                // End If

                if (FOLV.Columns.Count == 0)
                {
                    Log("Error: No columns to save?");
                    return false;
                }

                foreach (OLVColumn column in FOLV.AllColumns)  // FOLV.AllColumns
                {
                    olvState.ColumnIsVisible.Add(column.IsVisible);
                    olvState.ColumnDisplayIndicies.Add(column.LastDisplayIndex);
                    olvState.ColumnWidths.Add(column.Width);
                }

                // Now that we have stored our state, save to json
                string Filename = Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), $"ListSaveState-{FOLV.Name}.JSON");
                string json = Global.WriteToJsonFile<FOLVObjectListViewState>(Filename, olvState);

                if (json != null && !string.IsNullOrEmpty(json))
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex));
            }

            return ret;
        }


        public static void RestoreWindowState(Form Frm)
        {

            try
            {
                //private void MyForm_Load(object sender, EventArgs e)
                //{
                //    if (Properties.Settings.Default.IsMaximized)
                //        WindowState = FormWindowState.Maximized;
                //    else if (Screen.AllScreens.Any(screen => screen.WorkingArea.IntersectsWith(Properties.Settings.Default.WindowPosition)))
                //    {
                //        StartPosition = FormStartPosition.Manual;
                //        DesktopBounds = Properties.Settings.Default.WindowPosition;
                //        WindowState = FormWindowState.Normal;
                //    }
                //}
                //
                //private void MyForm_FormClosing(object sender, FormClosingEventArgs e)
                //{
                //    Properties.Settings.Default.IsMaximized = WindowState == FormWindowState.Maximized;
                //    Properties.Settings.Default.WindowPosition = DesktopBounds;
                //    Properties.Settings.Default.Save();
                //}

                Point SavLocation = (Point)(Global.GetSetting(Frm.Name + "_Location", new Point())); //Frm.RestoreBounds.Location

                if (SavLocation.IsEmpty)
                    goto endOfTry; //we did not previously save settings

                bool SavMaximized = System.Convert.ToBoolean(Global.GetSetting(Frm.Name + "_Maximized", false));
                bool SavMinimized = System.Convert.ToBoolean(Global.GetSetting(Frm.Name + "_Minimized", false));
                object ObjSize = Global.GetSetting(Frm.Name + "_Size", Frm.RestoreBounds.Size);
                Size SavSize = (Size)ObjSize; //CType(ObjSize, System.Drawing.Size)

                //Debug.Print("Size before: " & Me.Size.ToString)
                //Debug.Print("Loc before: " & Me.Location.ToString)
                //Debug.Print("WindowState Before: " & Me.WindowState.ToString)
                //DetectScreenName()

                //============================================================================================================
                //No reliable way of detecting if a screen has been turned off!   This will only detect if fully disconnected
                //============================================================================================================

                Rectangle tstrect = new Rectangle(SavLocation, SavSize);
                if (!SavLocation.IsEmpty && SavLocation.X > 0 && IsVisibleOnAnyScreen(tstrect))
                {
                    Frm.Location = SavLocation;
                    if (!SavSize.IsEmpty && SavSize.Width > 5 && SavSize.Height > 5)
                        Frm.Size = SavSize;
                    //else
                    //Debug.Print("Saved size not valid.");
                }
                //else
                //Debug.Print("Saved location not valid.");


                if (SavMaximized)
                    Frm.WindowState = FormWindowState.Maximized;
                else if (SavMinimized)
                    Frm.WindowState = FormWindowState.Normal; //FormWindowState.Minimized

                if (Frm.Tag != null && string.Equals(Frm.Tag.ToString(), "SAVE", StringComparison.OrdinalIgnoreCase))
                {
                    List<Control> ctls = GetControls(Frm);

                    foreach (Control ctl in ctls)
                        if (ctl is SplitContainer)
                        {
                            SplitContainer sc = (SplitContainer)ctl;
                            sc.SplitterDistance = System.Convert.ToInt32(Global.GetSetting($"{Frm.Name}.SplitContainer.{sc.Name}.SplitterDistance", sc.SplitterDistance));
                        }
                        else if (ctl is TabControl)
                        {
                            TabControl tc = (TabControl)ctl;
                            tc.SelectedIndex = System.Convert.ToInt32(Global.GetSetting($"{Frm.Name}.TabControl.{tc.Name}.SelectedIndex", tc.SelectedIndex));
                        }
                    //else if (ctl is ComboBox)
                    //{
                    //	ComboBox cc = (ComboBox)ctl;
                    //	if (cc.Items.Count == 0 && string.IsNullOrEmpty(System.Convert.ToString(cc.Text)))
                    //	{
                    //		List<string> lst = new List<string>();
                    //		lst = (List<string>)(GetSetting($"{Frm.Name}.ComboBox.{cc.Name}.Items", lst));
                    //		foreach (string st in lst)
                    //			cc.Items.Add(st);
                    //		cc.Text = System.Convert.ToString(GetSetting($"{Frm.Name}.ComboBox.{cc.Name}.Text", cc.Text));
                    //	}
                    //}
                    //else if (ctl is TextBox)
                    //{
                    //	TextBox tc = (TextBox)ctl;
                    //	if (string.IsNullOrEmpty(System.Convert.ToString(tc.Text)))
                    //		tc.Text = System.Convert.ToString(GetSetting($"{Frm.Name}.TextBox.{tc.Name}.Text", tc.Text));
                    //}
                    //else if (ctl is CheckBox)
                    //{
                    //	CheckBox tc = (CheckBox)ctl;
                    //	if (tc.CheckState == CheckState.Indeterminate)
                    //	{
                    //		tc.Checked = System.Convert.ToBoolean(GetSetting($"{Frm.Name}.CheckBox.{tc.Name}.Checked", tc.Checked));
                    //		tc.CheckState = (CheckState)(tc.Checked ? CheckState.Checked : CheckState.Unchecked);
                    //	}
                    //}

                }
                //If Screen.AllScreens.Any(Function(screen__1) screen__1.WorkingArea.IntersectsWith(Properties.Settings.[Default].WindowPosition)) Then
                //    StartPosition = FormStartPosition.Manual
                //    DesktopBounds = Properties.Settings.[Default].WindowPosition
                //    Frm.WindowState = FormWindowState.Normal
                //End If

                //Debug.Print("Size After: " & Me.Size.ToString)
                //Debug.Print("Loc After: " & Me.Location.ToString)
                //Debug.Print("WindowState After: " & Me.WindowState.ToString)

            }
            catch (Exception)
            {

            }
            finally
            {


            }
        endOfTry:
            1.GetHashCode(); //VBConversions note: C# requires an executable line here, so a dummy line was added.
        }

        public static void SaveWindowState(Form Frm)
        {


            try
            {

                if (Frm.WindowState == FormWindowState.Maximized)
                {
                    Global.SaveSetting(Frm.Name + "_Location", Frm.RestoreBounds.Location);
                    Global.SaveSetting(Frm.Name + "_Size", Frm.RestoreBounds.Size);
                    Global.SaveSetting(Frm.Name + "_Maximized", true);
                    Global.SaveSetting(Frm.Name + "_Minimized", false);
                }
                else if (Frm.WindowState == FormWindowState.Normal)
                {
                    Global.SaveSetting(Frm.Name + "_Location", Frm.Location);
                    Global.SaveSetting(Frm.Name + "_Size", Frm.Size);
                    Global.SaveSetting(Frm.Name + "_Maximized", false);
                    Global.SaveSetting(Frm.Name + "_Minimized", false);
                }
                else
                {
                    Global.SaveSetting(Frm.Name + "_Location", Frm.RestoreBounds.Location);
                    Global.SaveSetting(Frm.Name + "_Size", Frm.RestoreBounds.Size);
                    Global.SaveSetting(Frm.Name + "_Maximized", false);
                    Global.SaveSetting(Frm.Name + "_Minimized", true);
                }

                if (Frm.Tag != null && string.Equals(Frm.Tag.ToString(), "SAVE", StringComparison.OrdinalIgnoreCase))
                {
                    List<Control> ctls = GetControls(Frm);

                    foreach (Control ctl in ctls)
                    {
                        if (ctl is SplitContainer)
                        {
                            SplitContainer sc = (SplitContainer)ctl;
                            Global.SaveSetting($"{Frm.Name}.SplitContainer.{sc.Name}.SplitterDistance", sc.SplitterDistance);
                        }
                        else if (ctl is TabControl)
                        {
                            TabControl tc = (TabControl)ctl;
                            Global.SaveSetting($"{Frm.Name}.TabControl.{tc.Name}.SelectedIndex", tc.SelectedIndex);
                        }
                        else if (ctl is FastObjectListView)
                        {
                            FastObjectListView folv = (FastObjectListView)ctl;
                            FOLVSaveState(folv);
                        }

                    }

                    //else if (ctl is ComboBox)
                    //{
                    //	ComboBox cc = (ComboBox)ctl;
                    //	List<string> lst = new List<string>();
                    //	foreach (object cbi in cc.Items)
                    //		lst.Add(cbi.ToString());
                    //	SaveSetting($"{Frm.Name}.ComboBox.{cc.Name}.Items", lst);
                    //	SaveSetting($"{Frm.Name}.ComboBox.{cc.Name}.Text", cc.Text);
                    //}
                    //else if (ctl is TextBox)
                    //{
                    //	TextBox tc = (TextBox)ctl;
                    //	SaveSetting($"{Frm.Name}.TextBox.{tc.Name}.Text", tc.Text);
                    //}
                    //else if (ctl is CheckBox)
                    //{
                    //	CheckBox tc = (CheckBox)ctl;
                    //	SaveSetting($"{Frm.Name}.CheckBox.{tc.Name}.Checked", tc.Checked);
                    //}

                }

            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }


        public static List<Control> GetControls(Control frm)
        {
            List<Control> ctls = new List<Control>();

            foreach (Control c in frm.Controls)
            {
                if (!c.IsDisposed && c.Name != "" && c.Enabled)
                {
                    ctls.Add(c);
                }
                if (c.HasChildren)
                {
                    ctls.AddRange(GetControls(c));
                }
            }

            return ctls;
        }
        private static bool IsVisibleOnAnyScreen(Rectangle rect)
        {
            bool Ret = false;
            int I = 0;
            foreach (Screen screen in Screen.AllScreens)
            {
                I++;
                //Debug.Print($"Screen {I}: Name={screen.DeviceName}, Primary={screen.Primary}, Working={screen.WorkingArea}, Bounds={screen.Bounds}");
                if (screen.WorkingArea.IntersectsWith(rect))
                {
                    //Debug.Print("...IntersectsWith");
                    Ret = true;
                }
            }

            return Ret;
        }

        public static void InvokeIFRequired(Control control, MethodInvoker action)
        {
            // This will let you update any control from another thread - It only invokes IF NEEDED for better performance 
            // See TextBoxLogger.Log for example

            if (control != null && !control.IsDisposed && !control.Disposing && control.IsHandleCreated && control.FindForm().IsHandleCreated && !IsClosing.ReadFullFence())
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(action);
                }
                else
                {
                    action();
                }

            }
        }

        public static string GetImageForHistoryList(object row)
        {
            string RetKey = "";

            try
            {

                string png = "32.png";  //make empty string to avoid using the png version of the images

                History hist = (History)row;
                if (hist.Success)
                {
                    RetKey = "success" + png;
                }
                else if (hist.WasSkipped)
                {
                    RetKey = "error" + png;
                }
                else if (!hist.Success && hist.Detections.IndexOf("false alert", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    RetKey = "nothing" + png;
                }
                else
                {
                    RetKey = "detection" + png;
                }


            }
            catch (Exception)
            {
            }

            return RetKey;

        }
        public static string GetImageForHistoryListPerson(object row)
        {
            string RetKey = "";

            try
            {
                string png = "32.png";  //make empty string to avoid using the png version of the images

                History hist = (History)row;
                if (hist.IsPerson)
                {
                    RetKey = "person" + png;
                }
                else
                {
                    RetKey = "";
                }


            }
            catch { }

            return RetKey;

        }

        public static string GetImageForHistoryListSuccess(object row)
        {
            string RetKey = "";

            try
            {
                //"Airplane", "Bear", "Bicycle", "Bird", "Boat", "Bus", "Car", "Cat", "Cow", "Dog", "Horse", "Motorcycle", "Person", "Sheep", "Truck"

                string png = "32.png";  //make empty string to avoid using the png version of the images

                History hist = (History)row;
                if (hist.Success)
                {
                    if (hist.IsPerson)
                    {
                        RetKey = "person" + png;
                    }
                    else if (hist.IsAnimal)
                    {
                        if (hist.Detections.IndexOf("bear", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            RetKey = "bear" + png;
                        }
                        else if (hist.Detections.IndexOf("dog", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            RetKey = "dog" + png;
                        }
                        else if (hist.Detections.IndexOf("cat", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            RetKey = "cat" + png;
                        }
                        else if (hist.Detections.IndexOf("bird", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            RetKey = "bird" + png;
                        }
                        else if (hist.Detections.IndexOf("horse", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            RetKey = "horse" + png;
                        }
                        else
                        {
                            RetKey = "alien" + png;
                        }


                    }
                    else if (hist.IsVehicle)
                    {
                        if (hist.Detections.IndexOf("truck", StringComparison.OrdinalIgnoreCase) >= 0 || hist.Detections.IndexOf("bus", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            RetKey = "truck" + png;
                        }
                        else if (hist.Detections.IndexOf("car", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            RetKey = "car" + png;
                        }
                        else if (hist.Detections.IndexOf("motorcycle", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            RetKey = "motorcycle" + png;
                        }
                        else if (hist.Detections.IndexOf("bicycle", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            RetKey = "bicycle" + png;
                        }
                    }
                }
                else
                {
                    RetKey = "";
                }


            }
            catch { }

            return RetKey;

        }


        public class CursorWait : IDisposable
        {
            public CursorWait(bool appStarting = false, bool applicationCursor = true)
            {
                Cursor.Current = appStarting ? Cursors.AppStarting : Cursors.WaitCursor;
                if (applicationCursor)
                {
                    System.Windows.Forms.Application.UseWaitCursor = true;
                }
                //System.Windows.Forms.Application.DoEvents();
            }

            public void Dispose()
            {
                Cursor.Current = Cursors.Default;
                System.Windows.Forms.Application.UseWaitCursor = false;
                //Windows.Forms.Application.DoEvents()
            }
        }

    }
}
