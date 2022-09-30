using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oechsle.Console.Application.Dto
{
    public class ResponseAuthDto
    {
        public long TransactionId { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
    }

    public class RequestAuthDto
    {
        public string User { get; set; }
        public string Password { get; set; }
    }

    public class Data
    {
        public string Token { get; set; }
        public string Status { get; set; }
    }
}
