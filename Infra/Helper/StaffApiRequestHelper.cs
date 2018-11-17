using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class StaffApiRequestHelper
    {
        public static async Task<tbStaff> Login(tbStaff staff)
        {
            var url = string.Format("api/Staff/login");
            tbStaff result = await ApiRequest<tbStaff>.PostRequest(url,staff);
            return result;
        }

        public static tbStaff getStaffData(string username)
        {
            string url = string.Format("api/Staff/getStaffData?username={0}", username);
            tbStaff response = null;
            ApiRequest<tbStaff>.Get(url, out response);
            return response;

        }

    }
}
