using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class DomainApiRequestHelper
    {
        public static async Task<List<tbDomain>> Get(int id = 0, string tags = "*", string name = "*", int hospitalid = 0, int pagesize = 15, int pageindex = 1)
        {
            string url = $"api/domain/get?id={id}&tags={tags}&name={name}&hospitalid={hospitalid}&pagesize={pagesize}&page={pageindex}";
            return await ApiRequest<List<tbDomain>>.GetRequest(url);
        }
        public static async Task<DomainWithServiceViewModel> GetWithServices(int id, int hospitalid)
        {
            string url = $"api/domain/getwithservices?id={id}&hospitalid={hospitalid}";
            return await ApiRequest<DomainWithServiceViewModel>.GetRequest(url);
        }
        public static async Task<DomainViewModel> GetWithServicesAndProcedures(int id, int airlineid, string tags)
        {
            string url = $"api/domain/getwithservicesandprocedures?id={id}&airlineid={airlineid}&tags={tags}";
            return await ApiRequest<DomainViewModel>.GetRequest(url);
        }

    }
}
