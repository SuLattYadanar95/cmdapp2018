using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.ViewModels;
namespace Infra.Helper
{
    public class FAQApiRequestHelper
    {
        public static async Task<tbFAQ> CreateOrEdit(tbFAQ obj)
        {
            return await ApiRequest<tbFAQ>.PostRequest("api/faq/create", obj);
        }
        public static async Task<List<tbFAQ>> Get(int id=0,int serviceid=0, int procedureid=0, int pagesize = 15, int pageindex = 1)
        {
            string url = $"api/faq/get?id={id}&serviceid={serviceid}&procedureid={procedureid}&pagesize={pagesize}&pageindex={pageindex}";
            return await ApiRequest<List<tbFAQ>>.GetRequest(url);
        }
        public static async Task<FAQViewModel> GetWithService(int id = 0, int serviceid = 0, int procedureid = 0, int pagesize = 15, int pageindex = 1)
        {
            string url = $"api/faq/getwithservice?id={id}&serviceid={serviceid}&procedureid={procedureid}&pagesize={pagesize}&pageindex={pageindex}";
            return await ApiRequest<FAQViewModel>.GetRequest(url);
        }
        public static async Task<PageCountInfo> GetPageCountInfo(int pagesize)
        {
            string url = string.Format("api/faq/getpagecount?pagesize={0}", pagesize);
            return await ApiRequest<PageCountInfo>.GetRequest(url);
        }
        public static async Task<int> Delete(int id)
        {
            string url = string.Format("api/faq/delete?id={0}", id);
            var result = await ApiRequest<tbFAQ>.GetRequest(url);
            return result.ID;
        }


    }
}