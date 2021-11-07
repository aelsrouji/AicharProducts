using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Products.Web.SD;

namespace Products.Web.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.Get;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

    }
}
