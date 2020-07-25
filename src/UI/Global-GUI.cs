using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace AITool
{
    public static class Global_GUI
    {


		public static void UpdateFOLV(ref FastObjectListView olv, IEnumerable Collection, bool ResizeCols = false)
		{

			olv.Freeze();

			try
			{

				//AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve

				olv.ClearObjects();
				//FOLV_Installs.Clear()
				//FOLV_DetailsList.Refresh()
				olv.SetObjects(Collection, true);
				//update column size only if did not restore folv state file or forced
				if (olv.Tag == null || ResizeCols)
				{
					olv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
				}
				if (olv.Items.Count == 0)
				{
					olv.EmptyListMsg = "Nothing found.";
				}

			}
			catch (Exception ex)
			{
				Global.Log("Error: " + Global.ExMsg(ex));
			}
			finally
			{
				//RemoveHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve
				olv.Unfreeze();

			}
		}
		public static void ConfigureFOLV(ref FastObjectListView FOLV, Type Cls, System.Drawing.Font Fnt, ImageList ImageList, string PrimarySortColumnName = "", SortOrder PrimarySortOrder = SortOrder.Ascending, string SecondarySortColumnName = "", SortOrder SecondarySortOrder = SortOrder.Ascending, List<string> FilterColumnList = null, Color Clr = new Color(), int RowHeight = 0, bool ShowGroups = false)
		{

			try
			{

				FOLV.AllowColumnReorder = true;
				FOLV.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
				FOLV.CopySelectionOnControlC = true;
				FOLV.FullRowSelect = true;
				FOLV.GridLines = true;
				FOLV.HideSelection = false;
				FOLV.IncludeColumnHeadersInCopy = true;
				FOLV.OwnerDraw = true;
				FOLV.SelectColumnsOnRightClick = true;
				FOLV.SelectColumnsOnRightClickBehaviour = ObjectListView.ColumnSelectBehaviour.ModelDialog;
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
				FOLV.CellEditActivation = ObjectListView.CellEditActivateMode.None;
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
						//if (FOLV.Name == "FOLV_UpdateList" && colcnt == 1)
						//{
						//	cl.ImageGetter = GetImageForUpdateList;
						//}
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

					if (ei.PropertyType.Name == "Int64" || ei.PropertyType.Name == "Int32" || ei.PropertyType.Name == "Timespan")
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
						cl.MinimumWidth = 100;
						//cl.MaximumWidth = 20000
						//cl.Width = -2
					}

					if (ei.Name.ToLower().Contains("url"))
					{
						cl.Hyperlink = true;
					}
					//If cl.DataType = GetType(Boolean) Then
					//    cl.CheckBoxes = True
					//    cl.IsEditable = False
					//End If


					if (ei.Name.ToLower() == PrimarySortColumnName.ToLower())
					{
						FOLV.PrimarySortColumn = cl;
						FOLV.PrimarySortOrder = PrimarySortOrder; //SortOrder.Descending

						//cl.ImageGetter = AddressOf GetImageFromProd
					}
					if (ei.Name.ToLower() == SecondarySortColumnName.ToLower())
					{
						FOLV.SecondarySortColumn = cl;
						FOLV.SecondarySortOrder = SecondarySortOrder; //SortOrder.Descending
					}

					FOLV.Columns.Add(cl);

				}


				//OLV.RebuildColumns()
				FOLV.Refresh();
				FOLV.BuildList();
				Application.DoEvents();

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
			finally
			{
				
			}
		}

//		public string GetImageForProdList(object row)
//		{
//			string RetKey = "";

//			try
//			{
//				if (!(row is ClsProdConfEntryShort))
//				{
//					//INSTANT C# WARNING: 'Exit Try' statements have no equivalent in C#, so it has been replaced with a 'goto' statement:
//					//ORIGINAL LINE: Exit Try
//					goto ExitLabel1;
//				}

//				ClsProdConfEntryShort Prod = (ClsProdConfEntryShort)row;
//				if (Frm_Main.ImageList1.Images.ContainsKey(Prod.Source.ToString().ToLower()))
//				{
//					RetKey = Prod.Source.ToString().ToLower();
//				}
//				else
//				{
//					RetKey = "unknown";
//				}

//			}
//			catch (Exception ex)
//			{
//				if (DebugApp)
//				{
//					Debug.Print("Error: " + ExMsg(ex));
//				}
//			}
//ExitLabel1:
//			return RetKey;

//		}


		public class CursorWait:IDisposable
		{
			public CursorWait(bool appStarting = false, bool applicationCursor = true)
			{
				Cursor.Current = appStarting ? Cursors.AppStarting : Cursors.WaitCursor;
				if (applicationCursor)
				{
					System.Windows.Forms.Application.UseWaitCursor = true;
				}
				System.Windows.Forms.Application.DoEvents();
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
