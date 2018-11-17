using Data.Models;
using Data.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
   public class HistoryApiRequestHelper
    {
        public static async Task<PagedListClient<tbAppointment>> GetAppointmentHistoryListWithPaging(string name = null, DateTime? datetime = null, int pagesize = 10, int page = 1, int hospitalid=0)
        {
            string url = string.Format("api/appointment/historylist?name={0}&datetime={1}&pagesize={2}&page={3}", name, datetime, pagesize, page, hospitalid);
            var data = await ApiRequest<PagedListServer<tbAppointment>>.GetRequest(url);
            var model = new PagedListClient<tbAppointment>();
            var pagedList = new StaticPagedList<tbAppointment>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }
    }
}
