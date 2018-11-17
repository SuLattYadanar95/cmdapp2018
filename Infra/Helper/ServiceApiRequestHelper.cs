using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.ViewModels;
namespace Infra.Helper
{
    public class ServiceApiRequestHelper
    {
        
        public static async Task<List<tbService>> Get(int id=0,string title = "*",string tag="*",string domaincode="*", bool isactive = false,int hospitalid=0, int pagesize = 15, int pageindex = 1)
        {
            string url = $"api/service/get?id={id}&title={title}&tag={tag}&domaincode={domaincode}&hospitalid={hospitalid}&isactive={isactive}&pagesize={pagesize}&pageindex={pageindex}";
            return await ApiRequest<List<tbService>>.GetRequest(url);
        }
        public static async Task<ServiceViewModel> GetWithProcedures(int id)
        {
            string url = string.Format("api/service/getwithprocedures?id={0}", id);
            return await ApiRequest<ServiceViewModel>.GetRequest(url);
        }
        public static async Task<PageCountInfo> GetPageCountInfo(int pagesize)
        {
            string url = string.Format("api/service/getpagecount?pagesize={0}", pagesize);
            return await ApiRequest<PageCountInfo>.GetRequest(url);
        }
        public static async Task<List<string>> GetDomain()
        {
            string url = string.Format("api/service/getdomain");
            return await ApiRequest<List<string>>.GetRequest(url);
        }
        public static async Task<List<string>> GetCatgory(int facilityid = 0)
        {
            string url = $"api/service/getcategory?facilityid={facilityid}";
            return await ApiRequest<List<string>>.GetRequest(url);
        }

    }
}