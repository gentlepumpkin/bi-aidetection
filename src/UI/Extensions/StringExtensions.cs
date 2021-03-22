using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public static class StringExtensions
    {
        [DebuggerStepThrough]
        public static string JoinStr(this List<string> values, string separator)
        {
            //this custom join should not be used for CVS type output because it does not include empty items
            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Count == 0)
                return String.Empty;

            if (separator == null)
                separator = String.Empty;

            StringBuilder sb = new StringBuilder("");

            for (int i = 0; i < values.Count; i++)
            {
                if (!values[i].IsEmpty())
                    sb.Append(values[i].Trim() + separator);
            }

            return sb.ToString().Trim(separator.ToCharArray());

        }

        [DebuggerStepThrough]
        public static List<string> SplitStr(this string InList, string Separators, bool RemoveEmpty = true, bool TrimStr = true, bool ToLower = false)
        {
            List<string> Ret = new List<string>();
            if (!string.IsNullOrWhiteSpace(InList))
            {
                StringSplitOptions SSO = StringSplitOptions.None;

                if (RemoveEmpty)
                    SSO = StringSplitOptions.RemoveEmptyEntries;

                string[] splt = InList.Split(Separators.ToCharArray(), SSO);
                for (int i = 0; i < splt.Length; i++)
                {
                    if (ToLower)
                        splt[i] = splt[i].ToLower();

                    if (RemoveEmpty && !string.IsNullOrWhiteSpace(splt[i]))
                    {
                        if (TrimStr)
                            Ret.Add(splt[i].Trim());
                        else
                            Ret.Add(splt[i]);
                    }
                    else if (!RemoveEmpty)
                    {
                        if (TrimStr)
                            Ret.Add(splt[i].Trim());
                        else
                            Ret.Add(splt[i]);
                    }

                }
            }
            return Ret;
        }


        //[DebuggerStepThrough]
        public static string Append(this string value, string newvalue, string Separators, string ListSeparators = ",;")
        {
            //appends only if the string doesnt already have it and trims the separator characters 

            if (newvalue.IsEmpty())
                return value;

            if (value.IsEmpty())
                return newvalue.Trim((Separators + ListSeparators).ToCharArray());

            List<string> newlist = newvalue.SplitStr(ListSeparators);
            List<string> existinglist = value.SplitStr((Separators + ListSeparators).Replace(" ", ""));
            string newstr = "";
            foreach (var item in newlist)
            {
                if (!Global.IsInList(item, existinglist, TrueIfEmpty: true))
                {
                    newstr += item + ", ";
                }
            }

            return (value.Trim((Separators + ListSeparators).ToCharArray()) + Separators + newstr).Trim((Separators + ListSeparators).ToCharArray());

        }
        [DebuggerStepThrough]
        public static string Truncate(this string value, int maxLength, bool ellipsis)
        {
            if (string.IsNullOrEmpty(value)) return value;

            if (value.Length <= maxLength) return value;

            if (ellipsis) return value.Substring(0, maxLength) + "...";

            return value.Substring(0, maxLength);

        }
        [DebuggerStepThrough]
        public static string ReplaceChars(this string value, char replacechar)
        {
            if (string.IsNullOrEmpty(value)) return value;

            return new string(replacechar, value.Length);

        }

        [DebuggerStepThrough]
        public static double ToDouble(this string value)
        {
            if (!value.IsNull())
                return Convert.ToDouble(value.Trim());
            else
                return 0;
        }

        [DebuggerStepThrough]
        public static int ToInt(this string value)
        {
            if (!value.IsNull())
                return Convert.ToInt32(value.Trim());
            else
                return 0;
        }

        public static float ToFloat(this string value)
        {
            if (!value.IsNull())
                return Convert.ToSingle(value.Trim());
            else
                return 0;
        }
        [DebuggerStepThrough]
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        [DebuggerStepThrough]
        public static bool IsNull(this object obj)
        {
            if (obj == null)
                return true;

            if (obj is string && string.IsNullOrWhiteSpace((string)obj))
                return true;

            return false;
        }
        [DebuggerStepThrough]
        public static string CleanString(this string inp, string ReplaceStr = " ")
        {
            if (inp == null || string.IsNullOrWhiteSpace(inp))
            {
                return "";
            }
            else
            {
                return inp.Replace("\0", ReplaceStr).Replace("\r", ReplaceStr).Replace("\n", ReplaceStr);
            }
        }
        [DebuggerStepThrough]
        public static string UpperFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            //char[] a = s.ToCharArray();
            //a[0] = char.ToUpper(a[0]);
            //return new string(a);
            return s.FormatPascalAndAcronym();
        }
        private static string FormatPascalAndAcronym(this string input)
        {
            //Input:  "QWERTYSomeThing OmitTRYSomeThing MayBeWorkingFYI"
            //Output: "QWERTY Some Thing Omit TRY Some Thing May Be Working FYI" 

            var builder = new StringBuilder(Char.ToUpper(input[0]).ToString());
            if (builder.Length > 0)
            {
                for (var index = 1; index < input.Length; index++)
                {
                    char prevChar = input[index - 1];
                    char nextChar = index + 1 < input.Length ? input[index + 1] : '\0';

                    bool isNextLower = Char.IsLower(nextChar);
                    bool isNextUpper = Char.IsUpper(nextChar);
                    bool isPresentUpper = Char.IsUpper(input[index]);
                    bool isPrevLower = Char.IsLower(prevChar);
                    bool isPrevUpper = Char.IsUpper(prevChar);
                    bool PrevSpace = Char.IsWhiteSpace(prevChar);

                    if (!string.IsNullOrWhiteSpace(prevChar.ToString()) &&
                        ((isPrevUpper && isPresentUpper && isNextLower) ||
                        (isPrevLower && isPresentUpper && isNextLower) ||
                        (isPrevLower && isPresentUpper && isNextUpper)))
                    {
                        builder.Append(' ');
                        builder.Append(Char.ToUpper(input[index]));
                    }
                    else if (PrevSpace)
                        builder.Append(Char.ToUpper(input[index]));

                    else
                    {
                        builder.Append(input[index]);
                    }
                }
            }
            return builder.ToString();
        }
        public static bool IsStringBefore(this string teststring, string first, string second)
        {

            //test something like this - make sure we arnt picking up the semicolon that could be part of a URL:
            //person, car ; http://URL/;
            bool ret = false;
            int firstidx = teststring.IndexOf(first, StringComparison.OrdinalIgnoreCase);

            if (firstidx > -1)
            {
                int secondidx = teststring.IndexOf(second, StringComparison.OrdinalIgnoreCase);
                if (secondidx > -1)
                {
                    if (firstidx < secondidx)
                    {
                        ret = true;
                    }
                }
                else
                {
                    ret = true;
                }
            }

            return ret;

        }
        [DebuggerStepThrough]
        public static bool Has(this string value, string FindStr)
        {
            if (value.IndexOf(FindStr, StringComparison.OrdinalIgnoreCase) >= 0)
                return true;
            else
                return false;
        }
        [DebuggerStepThrough]
        public static bool EqualsIgnoreCase(this string value, string FindStr)
        {
            if (string.Equals(value, FindStr, StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }
        [DebuggerStepThrough]
        public static string GetWord(this string InpStr, string JustBefore, string JustAfter, Int32 LastPos = 0, Int32 FirstPos = 0, bool NoTrim = false, bool MustFindJustAfter = false)
        {
            string Ret = "";

            try
            {
                string[] JB = JustBefore.Split('|');
                string[] JA = JustAfter.Split('|');
                int JBPos = 0;
                int JAPos = 0;
                string BefStr = "";
                string AftStr = "";
                int WordLen = 0;
                string RetWord = "";

                if (JustBefore.Length > 0)
                {
                    foreach (string BefStrTmp in JB)
                    {
                        BefStr = BefStrTmp;
                        if (BefStr.Length > 0)
                        {
                            JBPos = InpStr.IndexOf(BefStr, FirstPos, StringComparison.OrdinalIgnoreCase);
                            if (JBPos >= 0)
                                break;
                        }
                    }
                }
                else
                    JBPos = FirstPos;
                if (JBPos == -1)
                    return Ret;
                int FirstFnd = InpStr.Length;
                foreach (string AftStrTmp in JA)
                {
                    AftStr = AftStrTmp;
                    if (AftStr.Length > 0)
                    {
                        Int32 count = InpStr.Length - (JBPos + BefStr.Length);
                        Int32 StartIndex = JBPos + BefStr.Length;
                        JAPos = InpStr.IndexOf(AftStr, StartIndex, count, StringComparison.OrdinalIgnoreCase);
                        if (JAPos >= 0)
                            // If JAPos <= FirstFnd Then FirstFnd = JAPos
                            FirstFnd = Math.Min(JAPos, FirstFnd);
                    }
                }

                // If FirstFnd <= JAPos Then
                JAPos = FirstFnd;
                // End If

                if (JAPos == -1 || JAPos == 0 || JustAfter.Length == 0)
                {
                    if (!MustFindJustAfter)
                        JAPos = InpStr.Length;
                }

                if (JAPos <= JBPos)
                    return Ret;

                WordLen = JAPos - (JBPos + BefStr.Length);
                if (WordLen > 0)
                {
                    RetWord = InpStr.Substring(JBPos + BefStr.Length, WordLen);
                    LastPos = JAPos; // JBPos + BefStr.Length + RetWord.Length
                    if (NoTrim)
                        Ret = RetWord;
                    else
                        Ret = RetWord.Trim();
                }
            }
            // Return ""

            catch (Exception)
            {
            }
            finally
            {

            }

            return Ret;
        }

        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("sdsgtj;lrjwteojtkslkdjsl;dvlbmv.bmvlfu7r0tret-rereigjejgkgljg42");

        //this is not truly secure, but better than storing plain text in the JSON file
        public static string Encrypt(this string input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(input),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(this string encryptedData)
        {
            if (String.IsNullOrEmpty(encryptedData))
                return "";

            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return System.Text.Encoding.Unicode.GetString(decryptedData);
            }
            catch
            {
                return "";
            }
        }

        public static SecureString ToSecureString(this string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(this SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        /// <summary>
        /// Implement's VB's Like operator logic.
        /// </summary>
        public static bool IsLike(this string s, string pattern)
        {
            // Characters matched so far
            int matched = 0;

            // Loop through pattern string
            for (int i = 0; i < pattern.Length;)
            {
                // Check for end of string
                if (matched > s.Length)
                    return false;

                // Get next pattern character
                char c = pattern[i++];
                if (c == '[') // Character list
                {
                    // Test for exclude character
                    bool exclude = (i < pattern.Length && pattern[i] == '!');
                    if (exclude)
                        i++;
                    // Build character list
                    int j = pattern.IndexOf(']', i);
                    if (j < 0)
                        j = s.Length;
                    HashSet<char> charList = CharListToSet(pattern.Substring(i, j - i));
                    i = j + 1;

                    if (charList.Contains(s[matched]) == exclude)
                        return false;
                    matched++;
                }
                else if (c == '?') // Any single character
                {
                    matched++;
                }
                else if (c == '#') // Any single digit
                {
                    if (!Char.IsDigit(s[matched]))
                        return false;
                    matched++;
                }
                else if (c == '*') // Zero or more characters
                {
                    if (i < pattern.Length)
                    {
                        // Matches all characters until
                        // next character in pattern
                        char next = pattern[i];
                        int j = s.IndexOf(next, matched);
                        if (j < 0)
                            return false;
                        matched = j;
                    }
                    else
                    {
                        // Matches all remaining characters
                        matched = s.Length;
                        break;
                    }
                }
                else // Exact character
                {
                    if (matched >= s.Length || c != s[matched])
                        return false;
                    matched++;
                }
            }
            // Return true if all characters matched
            return (matched == s.Length);
        }

        /// <summary>
        /// Converts a string of characters to a HashSet of characters. If the string
        /// contains character ranges, such as A-Z, all characters in the range are
        /// also added to the returned set of characters.
        /// </summary>
        /// <param name="charList">Character list string</param>
        private static HashSet<char> CharListToSet(string charList)
        {
            HashSet<char> set = new HashSet<char>();

            for (int i = 0; i < charList.Length; i++)
            {
                if ((i + 1) < charList.Length && charList[i + 1] == '-')
                {
                    // Character range
                    char startChar = charList[i++];
                    i++; // Hyphen
                    char endChar = (char)0;
                    if (i < charList.Length)
                        endChar = charList[i++];
                    for (int j = startChar; j <= endChar; j++)
                        set.Add((char)j);
                }
                else set.Add(charList[i]);
            }
            return set;
        }

    }
}
