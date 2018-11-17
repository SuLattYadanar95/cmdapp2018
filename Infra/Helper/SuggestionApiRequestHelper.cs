using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class SuggestionApiRequestHelper
    {
        public static async Task<tbSuggestion> CreateOrEdit(tbSuggestion obj)
        {
            return await ApiRequest<tbSuggestion>.PostRequest("api/suggestion/create", obj);
        }
        public static async Task<List<tbSuggestion>> Get(string code = null, bool isResponded = false, string subject = "*", int managedbyid = 0,
            string managedbyname = "*", DateTime? createdat = null, string userid = "*", string username = "*", int pagesize = 15, int pageindex = 1)
        {
            //to change id with uniqueID // moviectr line 58 to fix
            string url = string.Format("api/suggestion/get?code={0}&isresponded={1}&subject={2}&managedbyid={3}&managedbyname={4}" +
                "createdat={5}&userid={6}&username={7}&pagesize={8}&pageindex={9}", code, isResponded, subject, managedbyid, managedbyname, createdat, userid, username);
            return await ApiRequest<List<tbSuggestion>>.GetRequest(url);

        }
        public static async Task<tbSuggestion> Detail(int id = 0)
        {
            string url = string.Format("api/suggestion?id={0}", id);
            return await ApiRequest<tbSuggestion>.GetRequest(url);
        }
        public static async Task<int> Delete(int id)
        {
            string url = string.Format("api/suggestion/delete?id={0}", id);
            var result = await ApiRequest<tbSuggestion>.GetRequest(url);
            return result.ID;
        }


    }
}
