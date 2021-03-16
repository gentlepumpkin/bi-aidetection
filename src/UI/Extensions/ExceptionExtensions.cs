using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public static class ExceptionExtensions
    {
        public static string Msg(this Exception MyEx2, [CallerMemberName] string MemberName = null)
        {
            //Gets the nested/inner exception if found, and also line and column of error - assuming PDB is in same folder
            string msg = "";
            try
            {
                string typ = "";
                Exception BaseEx = MyEx2.GetBaseException();
                Exception InnerEx = MyEx2.InnerException;
                if (InnerEx != null)
                {
                    typ = InnerEx.GetType().Name;
                    string Extra = GetExtraExceptionInfo(MyEx2);
                    msg = $"{InnerEx.Message} [{typ}] (In {Extra})";
                }
                else if (BaseEx != null)
                {
                    typ = BaseEx.GetType().Name;
                    string Extra = GetExtraExceptionInfo(MyEx2);
                    msg = $"{BaseEx.Message} [{typ}] (In {Extra})";
                }
                else
                {
                    typ = MyEx2.GetType().Name;
                    string Extra = GetExtraExceptionInfo(MyEx2);
                    msg = $"{MyEx2.Message} [{typ}] (In {Extra})";
                }

                //ExtraInfo = $"Mod: {LastMod} Line:{Frames(Frames.Count - 1).GetFileLineNumber}:{Frames(Frames.Count - 1).GetFileColumnNumber}"

                if (msg.IndexOf(MemberName, StringComparison.OrdinalIgnoreCase) == -1)
                {
                    msg = $"{msg} ({MemberName})";
                }

            }
            catch (Exception) { }

            msg = msg.Replace("\r\n", "|");
            return msg;

        }

        public static string GetExtraExceptionInfo(Exception ThisEX)
        {
            string ExtraInfo = "";
            try
            {
                //Dim st As StackTrace = New StackTrace(ThisEX, True)
                if (ThisEX.StackTrace != null)
                {
                    //   at System.String.Substring(Int32 startIndex, Int32 length)
                    //   at AITool.Shell.test2() in C:\Documents\Projects\_GIT\VorlonCD\bi-aidetection\src\UI\Shell.cs:line 91
                    //   at AITool.Shell.test1() in C:\Documents\Projects\_GIT\VorlonCD\bi-aidetection\src\UI\Shell.cs:line 86
                    //   at AITool.Shell.<Shell_Load>d__15.MoveNext() in C:\Documents\Projects\_GIT\VorlonCD\bi-aidetection\src\UI\Shell.cs:line 101

                    string ST = ThisEX.StackTrace;

                    List<string> lines = ST.SplitStr("\r\n");
                    string lastmod = "";
                    for (int i = lines.Count - 1; i >= 0; i--)
                    {
                        if (!lines[i].Contains(" System.") && !lines[i].IsEmpty())
                        {
                            string fnc = lines[i].GetWord("at ", " in");
                            fnc = fnc.GetWord(".", "");
                            //Shell.<Shell_Load>d__15.MoveNext()
                            string tmp = fnc.GetWord("<", ">");
                            if (tmp.IsEmpty())
                            {
                                //Shell.test1()
                                fnc = fnc.Replace("()", "");
                            }
                            else
                            {
                                fnc = fnc.GetWord("", ".") + "." + tmp;
                            }
                            string line = lines[i].GetWord("line ", "");
                            if (!line.IsEmpty())
                                line = ":" + line;

                            if (!lastmod.IsEmpty())
                                fnc = fnc.Replace(lastmod, "");

                            if (!fnc.IsEmpty())
                                ExtraInfo += $"{fnc}{line} > ";

                            lastmod = lines[i].GetWord("at ", ".") + ".";
                        }
                    }

                    //string[] SpltStr = new string[1] { " at " };
                    //string[] Splt = ST.Split(SpltStr, StringSplitOptions.None);
                    //string Lst = Splt[Splt.Length - 1].Replace(".MoveNext()", "").Trim();
                    //Lst = GetWordBetween(Lst, "", " in ");
                    //string[] Splt2 = Lst.Split('.');
                    //string LastMod = Splt2[Splt2.Length - 1].Trim();
                    //StackFrame[] Frames = (new StackTrace(ThisEX, true)).GetFrames();


                    //ExtraInfo = $"Mod: {LastMod} Line:{Frames[Frames.Length - 1].GetFileLineNumber()}";

                    ExtraInfo = ExtraInfo.Trim(" >".ToCharArray());
                }
            }
            catch { }
            return ExtraInfo;
        }

        public static string ToDetailedString(this Exception exception) =>
            ToDetailedString(exception, ExceptionOptions.Default);

        public static string ToDetailedString(this Exception exception, ExceptionOptions options)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var stringBuilder = new StringBuilder();

            AppendValue(stringBuilder, "Type", exception.GetType().FullName, options);

            foreach (PropertyInfo property in exception
                .GetType()
                .GetProperties()
                .OrderByDescending(x => string.Equals(x.Name, nameof(exception.Message), StringComparison.Ordinal))
                .ThenByDescending(x => string.Equals(x.Name, nameof(exception.Source), StringComparison.Ordinal))
                .ThenBy(x => string.Equals(x.Name, nameof(exception.InnerException), StringComparison.Ordinal))
                .ThenBy(x => string.Equals(x.Name, nameof(AggregateException.InnerExceptions), StringComparison.Ordinal)))
            {
                var value = property.GetValue(exception, null);
                if (value == null && options.OmitNullProperties)
                {
                    if (options.OmitNullProperties)
                    {
                        continue;
                    }
                    else
                    {
                        value = string.Empty;
                    }
                }

                AppendValue(stringBuilder, property.Name, value, options);
            }

            return stringBuilder.ToString().TrimEnd('\r', '\n');
        }

        private static void AppendCollection(
            StringBuilder stringBuilder,
            string propertyName,
            IEnumerable collection,
            ExceptionOptions options)
        {
            stringBuilder.AppendLine($"{options.Indent}{propertyName} =");

            var innerOptions = new ExceptionOptions(options, options.CurrentIndentLevel + 1);

            var i = 0;
            foreach (var item in collection)
            {
                var innerPropertyName = $"[{i}]";

                if (item is Exception)
                {
                    var innerException = (Exception)item;
                    AppendException(
                        stringBuilder,
                        innerPropertyName,
                        innerException,
                        innerOptions);
                }
                else
                {
                    AppendValue(
                        stringBuilder,
                        innerPropertyName,
                        item,
                        innerOptions);
                }

                ++i;
            }
        }

        private static void AppendException(
            StringBuilder stringBuilder,
            string propertyName,
            Exception exception,
            ExceptionOptions options)
        {
            var innerExceptionString = ToDetailedString(
                exception,
                new ExceptionOptions(options, options.CurrentIndentLevel + 1));

            stringBuilder.AppendLine($"{options.Indent}{propertyName} =");
            stringBuilder.AppendLine(innerExceptionString);
        }

        private static string IndentString(string value, ExceptionOptions options)
        {
            return value.Replace(Environment.NewLine, Environment.NewLine + options.Indent);
        }

        private static void AppendValue(
            StringBuilder stringBuilder,
            string propertyName,
            object value,
            ExceptionOptions options)
        {
            if (value is DictionaryEntry)
            {
                DictionaryEntry dictionaryEntry = (DictionaryEntry)value;
                stringBuilder.AppendLine($"{options.Indent}{propertyName} = {dictionaryEntry.Key} : {dictionaryEntry.Value}");
            }
            else if (value is Exception)
            {
                var innerException = (Exception)value;
                AppendException(
                    stringBuilder,
                    propertyName,
                    innerException,
                    options);
            }
            else if (value is IEnumerable && !(value is string))
            {
                var collection = (IEnumerable)value;
                if (collection.GetEnumerator().MoveNext())
                {
                    AppendCollection(
                        stringBuilder,
                        propertyName,
                        collection,
                        options);
                }
            }
            else
            {
                stringBuilder.AppendLine($"{options.Indent}{propertyName} = {value}");
            }
        }
    }

    public struct ExceptionOptions
    {
        public static readonly ExceptionOptions Default = new ExceptionOptions()
        {
            CurrentIndentLevel = 0,
            IndentSpaces = 4,
            OmitNullProperties = true
        };

        internal ExceptionOptions(ExceptionOptions options, int currentIndent)
        {
            this.CurrentIndentLevel = currentIndent;
            this.IndentSpaces = options.IndentSpaces;
            this.OmitNullProperties = options.OmitNullProperties;
        }

        internal string Indent { get { return new string(' ', this.IndentSpaces * this.CurrentIndentLevel); } }

        internal int CurrentIndentLevel { get; set; }

        public int IndentSpaces { get; set; }

        public bool OmitNullProperties { get; set; }
    }
}
