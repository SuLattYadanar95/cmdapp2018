using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class LocationApiRequestHelper
    {
        public static async Task<List<tbLocation>> Get(string fromplace = "*", string toplace = "*")
        {
            string url = string.Format("api/location/get?fromplace={0}&toplace={1}", fromplace, toplace);
            return await ApiRequest<List<tbLocation>>.GetRequest(url);
        }
    }
}
