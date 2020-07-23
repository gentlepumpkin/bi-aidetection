using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;

public class RichTextBoxEx
{
	//Inherits RichTextBox
	//#Region "Interop-Defines"
	[StructLayout(LayoutKind.Sequential)]
	private struct CHARFORMAT2_STRUCT
	{
		public UInt32 cbSize;
		public UInt32 dwMask;
		public UInt32 dwEffects;
		public readonly Int32 yHeight;
		public readonly Int32 yOffset;
		public readonly Int32 crTextColor;
		public readonly byte bCharSet;
		public readonly byte bPitchAndFamily;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public char[] szFaceName;
		public readonly UInt16 wWeight;
		public readonly UInt16 sSpacing;
		public readonly int crBackColor;
		// Color.ToArgb() -> int
		public readonly int lcid;
		public readonly int dwReserved;
		public readonly Int16 sStyle;
		public readonly Int16 wKerning;
		public readonly byte bUnderlineType;
		public readonly byte bAnimation;
		public readonly byte bRevAuthor;
		public readonly byte bReserved1;
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private extern static IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

	private const int WM_USER = 0x400;
	private const int EM_GETCHARFORMAT = WM_USER + 58;
	private const int EM_SETCHARFORMAT = WM_USER + 68;
	private const int SCF_SELECTION = 0x1;
	private const int SCF_WORD = 0x2;
	private const int SCF_ALL = 0x4;
	//#Region "CHARFORMAT2 Flags"
	private const UInt32 CFE_BOLD = 0x1;
	private const UInt32 CFE_ITALIC = 0x2;
	private const UInt32 CFE_UNDERLINE = 0x4;
	private const UInt32 CFE_STRIKEOUT = 0x8;
	private const UInt32 CFE_Public = 0x10;
	private const UInt32 CFE_LINK = 0x20;
	private const UInt32 CFE_AUTOCOLOR = 0x40000000;
	private const UInt32 CFE_SUBSCRIPT = 0x10000;
	// Superscript and subscript are 
	private const UInt32 CFE_SUPERSCRIPT = 0x20000;
	// mutually exclusive 
	private const int CFM_SMALLCAPS = 0x40;
	// (*) 
	private const int CFM_ALLCAPS = 0x80;
	// Displayed by 3.0 
	private const int CFM_HIDDEN = 0x100;
	// Hidden by 3.0 
	private const int CFM_OUTLINE = 0x200;
	// (*) 
	private const int CFM_SHADOW = 0x400;
	// (*) 
	private const int CFM_EMBOSS = 0x800;
	// (*) 
	private const int CFM_IMPRINT = 0x1000;
	// (*) 
	private const int CFM_DISABLED = 0x2000;
	private const int CFM_REVISED = 0x4000;
	private const int CFM_BACKCOLOR = 0x4000000;
	private const int CFM_LCID = 0x2000000;
	private const int CFM_UNDERLINETYPE = 0x800000;
	// Many displayed by 3.0 
	private const int CFM_WEIGHT = 0x400000;
	private const int CFM_SPACING = 0x200000;
	// Displayed by 3.0 
	private const int CFM_KERNING = 0x100000;
	// (*) 
	private const int CFM_STYLE = 0x80000;
	// (*) 
	private const int CFM_ANIMATION = 0x40000;
	// (*) 
	private const int CFM_REVAUTHOR = 0x8000;
	private const UInt32 CFM_BOLD = 0x1;
	private const UInt32 CFM_ITALIC = 0x2;
	private const UInt32 CFM_UNDERLINE = 0x4;
	private const UInt32 CFM_STRIKEOUT = 0x8;
	private const UInt32 CFM_Public = 0x10;
	private const UInt32 CFM_LINK = 0x20;
	private const UInt32 CFM_SIZE = 0x8000000;
	private const UInt32 CFM_COLOR = 0x40000000;
	private const UInt32 CFM_FACE = 0x20000000;
	private const UInt32 CFM_OFFSET = 0x10000000;
	private const UInt32 CFM_CHARSET = 0x8000000;
	private const UInt32 CFM_SUBSCRIPT = CFE_SUBSCRIPT | CFE_SUPERSCRIPT;
	private const UInt32 CFM_SUPERSCRIPT = CFM_SUBSCRIPT;
	private const byte CFU_UNDERLINENONE = 0x0;
	private const byte CFU_UNDERLINE = 0x1;
	private const byte CFU_UNDERLINEWORD = 0x2;
	// (*) displayed as ordinary underline 
	private const byte CFU_UNDERLINEDOUBLE = 0x3;
	// (*) displayed as ordinary underline 
	private const byte CFU_UNDERLINEDOTTED = 0x4;
	private const byte CFU_UNDERLINEDASH = 0x5;
	private const byte CFU_UNDERLINEDASHDOT = 0x6;
	private const byte CFU_UNDERLINEDASHDOTDOT = 0x7;
	private const byte CFU_UNDERLINEWAVE = 0x8;
	private const byte CFU_UNDERLINETHICK = 0x9;
	private const byte CFU_UNDERLINEHAIRLINE = 0xA;
	// (*) displayed as ordinary underline 

	public struct HighlightWord
	{
		public string Word;
		public Color Clr;
		public string RequiredChar; //required char {}
	}

	public struct RtfColor
	{
		public Color Clr;
		public string RtfClr;
	}

	public struct FoundItem
	{
		public string Word;
		public int Start;
		public int Length;
		public Color Clr;
	}

	public Dictionary<string, RtfColor> RtfColors = new Dictionary<string, RtfColor>();

	public int CurrentTextLength = 0;
	public int MaxTextLength = 524288; //65536;
	public long MaxRTFWriteTime = 75;
	public long LastRTFWriteTime = 0;
	public bool AutoScroll = true;
	private RichTextBox _RTF;
	private Dictionary<string, Color> KnownColors { get; set; } = new Dictionary<string, Color>();
	public RichTextBoxEx(RichTextBox RTF, bool AutoScroll)
	{
		this.AutoScroll = AutoScroll;
		this._RTF = RTF;
		this._RTF.DetectUrls = false;
		this._RTF.ForeColor = Color.White;
		this._RTF.BackColor = Color.DarkBlue;
		this._RTF.Font = new Font("Consolas", 8, FontStyle.Regular); //Lucida Console, 8.25pt
		this._RTF.WordWrap = false;

		foreach (Color clr in (new ColorConverter()).GetStandardValues())
		{
			KnownColors.Add(clr.Name.ToLower(), clr);
		}

	}

	public void RtfColorUpdate()
	{

		string[] colors = Enum.GetNames(typeof(KnownColor));

		foreach (string c in colors)
		{
			ColorConverter cc = new ColorConverter();
			Color clr = (Color)cc.ConvertFromString(c);
			//Console.WriteLine(clr.Name)
			RtfColor myclr = new RtfColor();
			myclr.Clr = clr;
			myclr.RtfClr = "\\red" + clr.R.ToString() + "\\green" + clr.G.ToString() + "\\blue" + clr.B.ToString();
			RtfColors.Add(clr.Name.ToLower(), myclr);
		}
	}

	public void LogToRTF(string Msg)
	{


		//Static Count As Long = 0

		try
		{

			if (this._RTF == null || this._RTF.IsDisposed)
			{
				return;
			}
			//the more text in the RTF window, the slower it is to update and scroll
			if (this.LastRTFWriteTime >= this.MaxRTFWriteTime)
			{
				UIOp(this._RTF, () =>
				{
					if (this._RTF.Visible)
					{
						this._RTF.Clear();
						this.CurrentTextLength = 0;
						Msg = $"(Log window cleared for performance reasons @ {this.LastRTFWriteTime}ms text log time)\r\n{Msg}";
					}
				});
			}

			if (this._RTF.TextLength + Msg.Length >= this.MaxTextLength)
			{
				UIOp(this._RTF, () =>
				{
					this._RTF.Clear();
					this.CurrentTextLength = 0;
					Msg = $"(Log window cleared for performance reasons @ {this.MaxTextLength} bytes)\r\n{Msg}";
				});
			}

			Stopwatch sw = Stopwatch.StartNew();

			string[] parts = Msg.Split('{');

			//parts(0) = "this is a "
			//parts(1) = "red}red"
			//parts(2) = "white} word"
			Color clr = Color.White;
			Color LastClr = clr;
			foreach (string part in parts)
			{
				int eb = part.IndexOf("}");
				if (eb > -1)
				{
					string clrname = part.Substring(0, eb).Replace("Color.", "");
					string txt = part.Substring(eb + 1);
					FontStyle fs = FontStyle.Regular;
					clr = LastClr;
					string[] clrs = clrname.Split('|');
					bool Updated = false;
					foreach (string nm in clrs)
					{
						if (nm.ToLower() == "bold")
						{
							fs = FontStyle.Bold;
							Updated = true;
						}
						else if (nm.ToLower() == "italic")
						{
							fs = FontStyle.Italic;
							Updated = true;
						}
						else if (nm.ToLower() == "regular")
						{
							fs = FontStyle.Regular;
							Updated = true;
						}
						else if (nm.ToLower() == "underline")
						{
							fs = FontStyle.Underline;
							Updated = true;
						}
						else if (nm.ToLower() == "cr" || nm.ToLower() == "lf" || nm.ToLower() == "crlf")
						{
							//ignore
						}
						else
						{
							//assume a color
							//clr = Color.FromName(clrname)
							//Dim cc = New ColorConverter
							Color c = new Color();
							if (KnownColors.TryGetValue(nm.ToLower(), out c))
							{
								Updated = true;
								clr = c;
								if (clr.R == 0 && clr.B == 0 && clr.A == 0 && clr.G == 0)
								{
									clr = Color.White;
								}
								LastClr = clr;
							}

						}
					}
					if (Updated)
					{
						UIOp(this._RTF, () => this.AppendText(txt, clr, fs));
					}
					else //put it back the way it was
					{
						UIOp(this._RTF, () => this.AppendText("{" + part, Color.White, FontStyle.Regular));
					}
				}
				else
				{
					UIOp(this._RTF, () => this.AppendText(part, Color.White, FontStyle.Regular));
				}
			}

			UIOp(this._RTF, () =>
			{
				this._RTF.AppendText("\r\n");
				if (this._RTF.Visible && this.AutoScroll)
				{
					this._RTF.SelectionStart = this._RTF.Text.Length;
					this._RTF.ScrollToCaret();
				}
			});

			this.LastRTFWriteTime = sw.ElapsedMilliseconds;


		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message, false);
		}
		finally
		{

		}
	}

	private void UIOp(Control c, Action action)
	{
		//UIOp(Me._RTF, Sub() Me._RTF.AppendText(txt + CRLF, clr, fs))
		if (c !=null && !c.IsDisposed && !c.Disposing)
		{
			if (c.InvokeRequired)
			{
				c.Invoke(action);
			}
			else
			{
				action();
			}
		}
	}
	public void HighlightRTF(string Source, List<HighlightWord> Tags, Color KeyWordColor, Color SymbolColor)
	{

		//Dim tim As New Stopwatch
		//tim.Start()
		try
		{
			if (Source.Trim().Length > 0)
			{

				//LockWindowUpdate(RtfBx.Handle)
				if (RtfColors.Count == 0)
				{
					RtfColorUpdate();
				}

				StringBuilder RtfText = new StringBuilder();

				//insert the header:
				RtfText.Append("{\\rtf1\\ansi\\deff0{\\fonttbl{\\f0\\fnil\\fcharset0 Lucida Console;}}");

				//Get all the colors used in tags:
				//{\colortbl ;\red255\green255\blue0;\red0\green191\blue255;\red255\green255\blue255;}
				//            CF1                    CF2                    CF3
				RtfText.Append("{\\colortbl ");

				Hashtable CFClr = new Hashtable();
				int num = 1;

				foreach (HighlightWord item in Tags)
				{
					if (!CFClr.ContainsKey(item.Clr.Name.ToLower()))
					{
						CFClr.Add(item.Clr.Name.ToLower(), "\\cf" + num.ToString() + " ");
						RtfText.Append(";" + RtfColors[item.Clr.Name.ToLower()].RtfClr);
						num = num + 1;
					}
				}

				if (!KeyWordColor.IsEmpty)
				{
					if (!CFClr.ContainsKey(KeyWordColor.Name.ToLower()))
					{
						CFClr.Add(KeyWordColor.Name.ToLower(), "\\cf" + num.ToString() + " ");
						RtfText.Append(";" + RtfColors[KeyWordColor.Name.ToLower()].RtfClr);
						num = num + 1;
					}
				}
				if (!SymbolColor.IsEmpty)
				{
					if (!CFClr.ContainsKey(SymbolColor.Name.ToLower()))
					{
						CFClr.Add(SymbolColor.Name.ToLower(), "\\cf" + num.ToString() + " ");
						RtfText.Append(";" + RtfColors[SymbolColor.Name.ToLower()].RtfClr);
						num = num + 1;
					}
				}

				//add the default forground color of the RTF box:
				if (!CFClr.ContainsKey(this._RTF.ForeColor.Name.ToLower()))
				{
					CFClr.Add(this._RTF.ForeColor.Name.ToLower(), "\\cf" + num.ToString() + " ");
					RtfText.Append(";" + RtfColors[this._RTF.ForeColor.Name.ToLower()].RtfClr);
				}

				RtfText.Append(";}");

				//look for all the hits first....
				SortedDictionary<int, FoundItem> ScannerResult = new SortedDictionary<int, FoundItem>();
				int Dupes = 0;

				if (Tags.Count > 0)
				{

					foreach (HighlightWord item in Tags)
					{
						int pos = 0;
						do
						{
							pos = Source.IndexOf(item.Word, pos, StringComparison.OrdinalIgnoreCase);
							if (pos > -1)
							{
								//{=}
								bool AddIt = false;
								if (item.RequiredChar != null && item.RequiredChar.Length > 0)
								{
									int RcPos = pos + item.Word.Length;
									if (RcPos < Source.Length)
									{
										string RC = Source.Substring(RcPos, item.RequiredChar.Length);
										if (RC == item.RequiredChar)
										{
											AddIt = true;
										}
									}
									//make sure we found a whole word:
									if (AddIt)
									{
										int BefPos = pos - 1;
										if (BefPos < 0)
										{
											AddIt = true;
										}
										else
										{
											string RC = Source.Substring(BefPos, 1);
											if (RC == " " || RC == "\n" || RC == "\r")
											{
												AddIt = true;
											}
											else
											{
												AddIt = false;
											}
										}
									}
								}
								else
								{
									AddIt = true;
								}
								if (AddIt)
								{
									FoundItem fi = new FoundItem();
									fi.Word = item.Word;
									fi.Start = pos;
									fi.Length = item.Word.Length;
									fi.Clr = item.Clr;
									if (!ScannerResult.ContainsKey(fi.Start))
									{
										ScannerResult.Add(fi.Start, fi);
									}
									else
									{
										Dupes = Dupes + 1;
									}
								}
								pos += 1;
							}
						} while (!(pos == -1));
					}

				}
				else
				{
					//assume we look for  meta{   } and =
					int pos = 0;
					for (pos = 0; pos < Source.Length; pos++)
					{
						char c = Source[pos];
						if (c == '{')
						{
							//add the {
							FoundItem fi = new FoundItem();
							fi.Word = c.ToString();
							fi.Start = pos;
							fi.Length = 1;
							fi.Clr = SymbolColor;
							if (!ScannerResult.ContainsKey(fi.Start))
							{
								ScannerResult.Add(fi.Start, fi);
							}
							else
							{
								Dupes = Dupes + 1;
							}

							//search back to the beginning of the keyword:
							int Pos2 = 0;
							int BegKeyPos = -1;
							for (Pos2 = pos - 1; Pos2 >= 0; Pos2--)
							{
								string RC = Source.Substring(Pos2, 1);
								if (RC == " " || RC == "\n" || RC == "\r" || RC == "}")
								{
									BegKeyPos = Pos2 + 1;
									break;
								}
							}
							if (BegKeyPos == -1)
							{
								BegKeyPos = 0;
							}
							int BegKeyLen = pos - BegKeyPos;
							if (BegKeyLen > 0)
							{
								FoundItem fi2 = new FoundItem();
								fi2.Word = Source.Substring(BegKeyPos, BegKeyLen);
								fi2.Start = BegKeyPos;
								fi2.Length = BegKeyLen;
								fi2.Clr = KeyWordColor;
								if (!ScannerResult.ContainsKey(fi2.Start))
								{
									ScannerResult.Add(fi2.Start, fi2);
								}
								else
								{
									Dupes = Dupes + 1;
								}
							}

						}
						else if (c == '}' || c == '=')
						{
							FoundItem fi = new FoundItem();
							fi.Word = c.ToString();
							fi.Start = pos;
							fi.Length = 1;
							fi.Clr = SymbolColor;
							if (!ScannerResult.ContainsKey(fi.Start))
							{
								ScannerResult.Add(fi.Start, fi);
							}
							else
							{
								Dupes = Dupes + 1;
							}
						}
					}

				}

				// Sort the collection based on the start index of the occurrence, to simplify the job of the parser.
				//ScannerResult.Sort(Function(lhs As FoundItem, rhs As FoundItem) lhs.Start - rhs.Start)

				//Append body
				RtfText.Append("\\viewkind4\\uc1\\pard" + CFClr[this._RTF.ForeColor.Name.ToLower()].ToString().Trim() + "\\lang1033\\f0\\fs17");

				//Set default color:
				//RtfText.Append()

				int i = 0;
				int LastIndex = 0;

				string Chunk = "";

				foreach (KeyValuePair<int, FoundItem> itm in ScannerResult)
				{

					if (LastIndex < itm.Value.Start)
					{
						//(LastIndex < (ScannerResult(i).Start + ScannerResult(i).Length)) Then
						//append anything before the last search word was found
						Chunk = ParseRtf(Source.Substring(LastIndex, (itm.Value.Start - LastIndex)));

						if (Chunk.Length > 0)
						{
							RtfText.Append(Chunk);
						}
					}

					//Append color for the word/tag
					RtfText.Append(CFClr[itm.Value.Clr.Name.ToLower()]);

					//Append the word/tag

					Chunk = ParseRtf(Source.Substring(itm.Value.Start, itm.Value.Length));

					RtfText.Append(Chunk);

					//Append the default rtf color for the LAST color
					RtfText.Append(CFClr[this._RTF.ForeColor.Name.ToLower()]);

					LastIndex = (itm.Value.Start + itm.Value.Length);
				}

				if (LastIndex < Source.Length)
				{
					Chunk = ParseRtf(Source.Substring(LastIndex, (Source.Length - LastIndex)));

					RtfText.Append(Chunk);
				}

				//append the lst }

				RtfText.Append("}");

				this._RTF.Rtf = RtfText.ToString().Replace("\r\n", "\\par" + "\r\n");


				//RtfBx.Update()

			}

		}
		catch (Exception ex)
		{
			Debug.WriteLine("Error highlighting RTF box: " + ex.Message, false);
		}
		//tim.Stop()
		//M("   HighlightRTF time: " & tim.ElapsedMilliseconds, True)
	}

	public string ParseRtf(string token)
	{
		return token.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}");
		//.Replace("?", "\?").Replace("~", "\~").Replace("'", "\'").Replace("-", "\-").Replace("|", "\|").Replace(":", "\:").Replace("*", "\*")
	}


	public void AppendText(string text, Color color, FontStyle FS)
	{
		try
		{
			if (this._RTF.IsDisposed)
			{
				return;
		
			}

			//if (this.CurrentTextLength + text.Length >= 65536)
			//{
			//	//Debug.WriteLine($"Error: Too much in the RTF box!  Clearing {this.CurrentTextLength}.");
			//	this._RTF.Clear();
			//	this.CurrentTextLength = 0;
			//}

			//If Me.Rtf.Length + text.Length >= Me.MaxLength OrElse Me.TextLength + text.Length >= Me.MaxLength Then
			//    Debug.WriteLine("Error: Too much in the RTF box!  Clearing.")
			//    Me.Clear()
			//End If

			this._RTF.SelectionStart = this._RTF.TextLength;
			this._RTF.SelectionLength = 0;

			bool ChangedFont = false;
			Font cf = this._RTF.SelectionFont;

			if (this._RTF.SelectionFont.Style != FS)
			{
				ChangedFont = true;
				this._RTF.SelectionFont = new Font(cf, cf.Style | FS);
			}

			this._RTF.SelectionColor = color;

			CurrentTextLength = CurrentTextLength + text.Length;

			this._RTF.AppendText(text);

			this._RTF.SelectionColor = this._RTF.ForeColor;

			if (ChangedFont)
			{
				this._RTF.SelectionFont = new Font(cf, FontStyle.Regular);
			}

		}
		catch (Exception ex)
		{
			Debug.WriteLine("Error: " + ex.Message); //assume overflow
			this._RTF.Clear();
		}
	}


	private void SetSelectionStyle(UInt32 mask, UInt32 effect)
	{
		CHARFORMAT2_STRUCT cf = new CHARFORMAT2_STRUCT();
		cf.cbSize = Convert.ToUInt32(Marshal.SizeOf(cf));
		cf.dwMask = mask;
		cf.dwEffects = effect;
		IntPtr wpar = new IntPtr(SCF_SELECTION);
		IntPtr lpar = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
		Marshal.StructureToPtr(cf, lpar, false);
		IntPtr res = SendMessage(this._RTF.Handle, EM_SETCHARFORMAT, wpar, lpar);
		Marshal.FreeCoTaskMem(lpar);
	}

	private int GetSelectionStyle(UInt32 mask, UInt32 effect)
	{
		CHARFORMAT2_STRUCT cf = new CHARFORMAT2_STRUCT();
		cf.cbSize = Convert.ToUInt32(Marshal.SizeOf(cf));
		cf.szFaceName = new char[32];
		IntPtr wpar = new IntPtr(SCF_SELECTION);
		IntPtr lpar = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
		Marshal.StructureToPtr(cf, lpar, false);
		IntPtr res = SendMessage(this._RTF.Handle, EM_GETCHARFORMAT, wpar, lpar);
		cf = (CHARFORMAT2_STRUCT)Marshal.PtrToStructure(lpar, typeof(CHARFORMAT2_STRUCT));
		int state = 0;
		if ((cf.dwMask & mask) == mask)
		{
			if ((cf.dwEffects & effect) == effect)
			{
				state = 1;
			}
			else
			{
				state = 0;
			}
		}
		else
		{
			state = -1;
		}
		Marshal.FreeCoTaskMem(lpar);
		return state;
	}
}
