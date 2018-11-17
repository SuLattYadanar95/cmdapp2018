using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class ActivityLogApiRequestHelper
    {
        public static async Task<List<tbActivityLog>> Get(DateTime fromDate, DateTime toDate, string eventpayload = "*", int pagesize = 15, int pageindex = 1)
        {
            string url = string.Format("api/activitylog/get?eventpayload={0}&fromdate={1}&todate={2}&pageindex={3}", eventpayload, fromDate, toDate, pagesize, pageindex);
            return await BotApiRequest<List<tbActivityLog>>.GetRequest(url);
        }
        public static async Task<int> Delete(int id)
        {
            string url = string.Format("api/activitylog/delete?id={0}", id);
            return await BotApiRequest<int>.GetRequest(url);
        }
        public static async Task<tbActivityLog> CreateOrEdit(tbActivityLog obj)
        {
            return await BotApiRequest<tbActivityLog>.PostRequest("api/activitylog/create", obj);
        }
    }
}
