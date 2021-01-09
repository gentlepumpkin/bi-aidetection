using NPushover.RequestObjects;
using System;
using System.Collections.Specialized;
using System.Net.Http;

namespace NPushover
{
    internal static class MultipartFormDataContentExtensions
    {
        public static void AddConditional(this MultipartFormDataContent collection, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
                collection.Add(new StringContent(value), $"\"{key}\"");
        }


        public static void AddConditional(this MultipartFormDataContent collection, string key, string[] value, string separator = ",")
        {
            if (value != null && value.Length > 0)
                collection.Add(new StringContent(string.Join(separator, value)), $"\"{key}\"");
        }

        public static void AddConditional(this MultipartFormDataContent collection, string key, bool value)
        {
            if (value)
                collection.Add(new StringContent("1"), $"\"{key}\"");
        }

        public static void AddConditional(this MultipartFormDataContent collection, string key, OS value)
        {
            if (value != OS.Any)
                collection.Add(new StringContent(value.ToString().ToLowerInvariant()), $"\"{key}\"");
        }

        public static void Add(this MultipartFormDataContent collection, string key, Uri value)
        {
            if (value != null)
                collection.Add(new StringContent(value.AbsoluteUri), $"\"{key}\"");
        }

        public static void Add(this MultipartFormDataContent collection, string key, TimeSpan value)
        {
            if (value != null)
                collection.Add(new StringContent(((int)value.TotalSeconds).ToString()), $"\"{key}\"");
        }

        public static void Add(this MultipartFormDataContent collection, string key, int value)
        {
            collection.Add(new StringContent(value.ToString()), $"\"{key}\"");
        }
    }
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
