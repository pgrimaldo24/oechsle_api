using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oechsle.Console.Application
{
    public class BridgeService
    {
        public TResult GetInvoque<TResult>(string detailUrlKey, string typeRequest)
        {
            TResult result;
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
                var request = (HttpWebRequest)WebRequest.Create(detailUrlKey);
                request.Method = typeRequest;

                Header(ref request, typeRequest);

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        var responseString = reader.ReadToEnd();
                        result = JsonConvert.DeserializeObject<TResult>(responseString);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #region Method private Headers
        private async Task<WebHeaderCollection> GetHeaders(Hashtable table)
        {
            WebHeaderCollection Headers = new WebHeaderCollection();
            foreach (DictionaryEntry entry in table)
            {
                Headers.Add(entry.Key.ToString(), entry.Value != null ? entry.Value.ToString() : null);
            }
            return Headers;
        }

        private Hashtable ParameterHeaderOsb(object Obj, string Token)
        {
            var paramHeaders = new Hashtable();
            paramHeaders.Add(Constants.ContentService.Authorization,
                Constants.ContentService.Bearer + Token);
            return paramHeaders;
        }

        private Hashtable ParameterHeader(object Obj, string Token)
        {
            var paramHeaders = new Hashtable();
            string data = JsonConvert.SerializeObject(Obj);
            paramHeaders.Add(Constants.ContentService.Authorization,
                Constants.ContentService.Bearer + Token);
            return paramHeaders;
        }

        private void Header(ref HttpWebRequest p_request, string p_Method)
        {
            p_request.ContentType = Constants.ContentService.ContentType;
            p_request.Method = p_Method;
            p_request.Accept = Constants.ContentService.Accept;
            p_request.Headers.Add(Constants.ContentService.AcceptLanguage, "en-us\r\n");
            p_request.Headers.Add(Constants.ContentService.UACPU, "x86 \r\n");
            p_request.Headers.Add(Constants.ContentService.CacheControl, "no-cache\r\n");
            p_request.KeepAlive = true;
        }

        #endregion
    }
}
