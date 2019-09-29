using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ACMTTU.NoteSharing.Shared.Neptune.HTTP
{
    class HeaderPropagator
    {
        public static HttpRequestMessage propagateHeader(HttpRequestMessage copyFrom, HttpRequestMessage copyTo)
        {
            foreach(KeyValuePair<string, IEnumerable<string>> hPair in copyFrom.Headers)
            {
                copyTo.Headers.Add(hPair.Key, hPair.Value);
            }
            return copyTo;
        }
        public static HttpRequestMessage propagateHeader(HttpRequestMessage copyFrom, HttpRequestMessage copyTo, string key)
        {
            if (!copyFrom.Headers.Contains(key))
            {
                throw new ArgumentException("Request did not contain header with key " + key, "copyFrom");
            }
            copyTo.Headers.Add(key, copyFrom.Headers.GetValues(key));
            return copyTo;
        }
    }
}
