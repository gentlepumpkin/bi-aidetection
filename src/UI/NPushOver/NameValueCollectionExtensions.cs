using NPushover.RequestObjects;
using System;
using System.Collections.Specialized;

namespace NPushover
{
    internal static class NameValueCollectionExtensions
    {
        public static void AddConditional(this NameValueCollection collection, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
                collection.Add(key, value);
        }

        public static void AddConditional(this NameValueCollection collection, string key, string[] value, string separator = ",")
        {
            if (value != null && value.Length > 0)
                collection.Add(key, string.Join(separator, value));
        }

        public static void AddConditional(this NameValueCollection collection, string key, bool value)
        {
            if (value)
                collection.Add(key, 1);
        }

        public static void AddConditional(this NameValueCollection collection, string key, OS value)
        {
            if (value != OS.Any)
                collection.Add(key, value.ToString().ToLowerInvariant());
        }

        public static void Add(this NameValueCollection collection, string key, Uri value)
        {
            if (value != null)
                collection.Add(key, value.AbsoluteUri);
        }

        public static void Add(this NameValueCollection collection, string key, TimeSpan value)
        {
            if (value != null)
                collection.Add(key, (int)value.TotalSeconds);
        }

        public static void Add(this NameValueCollection collection, string key, int value)
        {
            collection.Add(key, value.ToString());
        }
    }
}
