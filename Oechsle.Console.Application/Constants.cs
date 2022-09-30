using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oechsle.Console.Application
{
    public class Constants
    {
        public struct ContentService
        {
            public const string ContentTypeApplicationJson = "application/json";
            public const string Authorization = "Authorization";
            public const string Bearer = "Bearer  ";
            public const string ContentType = "application/x-www-form-urlencoded";
            public const string Accept = "*/*";
            public const string AcceptLanguage = "Accept-Language";
            public const string UACPU = "UA-CPU";
            public const string CacheControl = "Cache-Control";
            public const string PublicKey = "?public_key=";
            public const string Bin = "?bin=";
            public const string AccessToken = "&access_token=";
            public const string InitialAccessToken = "?access_token=";
            public const string Prefix_GrupoCiencias = "GC-";
            public const string KeyFormat = "ABCDEFGHIJKLMÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz";
            public const string TypeMethod = " | Type: ";
            public const string Url = " | Url: ";
            public const string StatusCode = "?status_code=";
            public const string Message = "&message=";
        }
    }
}
