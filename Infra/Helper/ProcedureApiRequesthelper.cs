using Data.Models;
using Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class ProcedureApiRequestHelper
    {
        public static async Task<List<tbProcedure>> Get(int id = 0, string code = "*",string tag ="*",int serviceid=0,string servicetitle ="*", int pagesize = 15, int pageindex = 1)
        {
            string url = $"api/procedure/get?id={id}&code={code}&tag={tag}&serviceId={serviceid}&servicetitle={servicetitle}&pagesize={pagesize}&pageindex={pageindex}";
            return await ApiRequest<List<tbProcedure>>.GetRequest(url);
        }
       
        public static async Task<PageCountInfo> GetPageCountInfo(int id= 0, string code = "*",int pagesize = 15)
        {
            string url = string.Format("api/procedure/getpagecount?id={0}&code={1}&pagesize={2}",id,code, pagesize);
            return await ApiRequest<PageCountInfo>.GetRequest(url);
        }
    }
}
