using Data.Models;
using Data.ViewModels;
using Infra.Helper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class ScheduleApiRequestHelper
    {
        public static async Task<PagedListClient<ScheduleDoctorViewModel>> list(int id = 0, string docname = null, DateTime? fromdate = null, DateTime? todate = null, int pagesize = 10, int page = 1,int hospitalid=0)
        {
            string url = string.Format("api/schedule/list?id={0}&docname={1}&fromdate={2}&todate={3}&pagesize={4}&page={5}&hospitalid={6}", id, docname, fromdate, todate, pagesize, page,hospitalid);
            var data = await ApiRequest<PagedListServer<ScheduleDoctorViewModel>>.GetRequest(url);

            var model = new PagedListClient<ScheduleDoctorViewModel>();
            var pagedList = new StaticPagedList<ScheduleDoctorViewModel>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;

        }

        public static async Task<List<tbScheduleData>> schedulebydoctorid(int docid = 0, int hospitalid = 0, DateTime? appDate = null, string Type = "next")
        {
            string url = string.Format("api/schedule/schedulebydoctorid?docid={0}&hospitalid={1}&appDate={2}&Type={3}", docid, hospitalid, appDate, Type);
            var data = await ApiRequest<List<tbScheduleData>>.GetRequest(url);
            return data;
        }

        public static async Task<tbScheduleData> getScheduleDetail(int ID =0)
        {
            string url = string.Format("api/Schedule/getScheduleDetail?ID={0}",ID);
            var data = await ApiRequest<tbScheduleData>.GetRequest(url);
            return data;
        }

        
    }
}
