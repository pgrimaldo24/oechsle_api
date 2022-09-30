using Oechsle.Console.Application.Dto;
using System;
using System.Collections.Generic;
using Newtonsoft.Json; 
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Oechsle.Console.Application
{
    class Program
    {
        static void Main(string[] args)
        { 
            var response = GetInvoque<EmployeeDto>("http://dummy.restapiexample.com/api/v1/employees", "GET");
            var oAuth = new RequestAuthDto
            {
                User = "admin",
                Password = "9cde1assdd-13fa-42bf-9453-6d453cf9df74"
            };

            var auth = PostInvoque<RequestAuthDto, ResponseAuthDto>(oAuth, "https://localhost:5001/api/Auth/auth", "", "POST");
            foreach (var item in response.Employees)
            {
                var oEmployee = new Employee
                {
                    employee_name = item.employee_name,
                    employee_salary = item.employee_salary,
                    employee_age = item.employee_age,
                    profile_image = item.profile_image
                }; 
                PostInvoque<Employee, ResponseDto>(oEmployee, "https://localhost:5001/api/Employee/PostRegisterEmployee", auth.Data.Token, "POST");
            } 
        }

        public static TResult PostInvoque<T, TResult>(T obj, string detailUrlKey, string token, string typeRequest)
        {
            TResult result;
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
                var request = HttpWebRequest.Create(detailUrlKey) as HttpWebRequest;
                request.Method = typeRequest;

                if (!string.IsNullOrEmpty(token))
                    request.Headers = GetHeaders(false ? ParameterHeaderOsb(obj, token) : ParameterHeader(obj, token));

                var data = JsonConvert.SerializeObject(obj);
                byte[] byteArray = Encoding.UTF8.GetBytes(data);

                request.ContentType = Constants.ContentService.ContentTypeApplicationJson;
                request.ContentLength = byteArray.Length;
                request.Expect = Constants.ContentService.ContentTypeApplicationJson;
                request.Accept = Constants.ContentService.ContentTypeApplicationJson;

                var handler = new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback +=
                (sender, certificate, chain, errors) =>
                {
                    return true;
                };

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
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

        public static TResult GetInvoque<TResult>(string detailUrlKey, string typeRequest)
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
        private static WebHeaderCollection GetHeaders(Hashtable table)
        {
            WebHeaderCollection Headers = new WebHeaderCollection();
            foreach (DictionaryEntry entry in table)
            {
                Headers.Add(entry.Key.ToString(), entry.Value != null ? entry.Value.ToString() : null);
            }
            return Headers;
        }

        private static Hashtable ParameterHeaderOsb(object Obj, string Token)
        {
            var paramHeaders = new Hashtable();
            paramHeaders.Add(Constants.ContentService.Authorization,
                Constants.ContentService.Bearer + Token);
            return paramHeaders;
        }

        private static Hashtable ParameterHeader(object Obj, string Token)
        {
            var paramHeaders = new Hashtable();
            string data = JsonConvert.SerializeObject(Obj);
            paramHeaders.Add(Constants.ContentService.Authorization,
                Constants.ContentService.Bearer + Token);
            return paramHeaders;
        }

        private static void Header(ref HttpWebRequest p_request, string p_Method)
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
