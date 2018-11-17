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
   public class AppointmentApiRequestHelper
    {
        public static async Task<PagedListClient<tbAppointment>> GetAppointmentWithPaging(string name = null, string status = null, int pagesize = 10, int page = 1, DateTime? datetime = null, DateTime? time = null, int hospitalid = 2, int doctorid = 0)
        {
            string url = string.Format("api/appointment/bookinglist?name={0}&status={1}&pagesize={2}&page={3}&datetime={4}&time={5}&hospitalid={6}&doctorid={7}", name, status, pagesize, page, datetime, time, hospitalid, doctorid);
            var data = await ApiRequest<PagedListServer<tbAppointment>>.GetRequest(url);

            var model = new PagedListClient<tbAppointment>();
            var pagedList = new StaticPagedList<tbAppointment>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }

        public static async Task<tbAppointment> StatusChange(int id, string statuschange)
        {
            string url = string.Format("api/appointment/statuschange?id={0}&statuschange={1}", id, statuschange);
            var data = await ApiRequest<tbAppointment>.GetRequest(url);

            return data;
        }


        public static async Task<PagedListClient<tbAppointment>> NewStatusChange(int id, string statuschange, int pagesize = 0, int page = 0)
        {
            string url = string.Format("api/appointment/newstatuschange?id={0}&statuschange={1}", id, statuschange, pagesize, page);//api/appointment/newstatuschange
            var data = await ApiRequest<PagedListServer<tbAppointment>>.GetRequest(url);
            var model = new PagedListClient<tbAppointment>();
            var pagedList = new StaticPagedList<tbAppointment>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }

        public static async Task<List<tbScheduleData>> timelist(int doctorid = 0, DateTime? datetime = null)
        {
            string url = string.Format("api/appointment/timelist?doctorid={0}&datetime={1}", doctorid, datetime);
            return await ApiRequest<List<tbScheduleData>>.GetRequest(url);
        }

        public static async Task<List<tbDoctor>> doctorlist(int hospitalid = 2, DateTime? datetime = null)
        {
            string url = string.Format("api/appointment/doctorlist?hospitalid={0}&datetime={1}", hospitalid, datetime);
            return await ApiRequest<List<tbDoctor>>.GetRequest(url);
        }

        //public static async Task<PagedListClient<tbAppointment>> GetAppointmentWaitingListWithPaging(string name = null, string status = null, int pagesize = 10, int page = 1)
        //{
        //    string url = string.Format("api/appointment/waitinglist?name={0}&status={1}&pagesize={2}&page={3}", name, status, pagesize, page);
        //    var data = await ApiRequest<PagedListServer<tbAppointment>>.GetRequest(url);

        //    var model = new PagedListClient<tbAppointment>();
        //    var pagedList = new StaticPagedList<tbAppointment>(data.Results, page, pagesize, data.TotalCount);
        //    model.Results = pagedList;
        //    model.TotalCount = data.TotalCount;
        //    model.TotalPages = data.TotalPages;
        //    return model;
        //}

        //public static async Task<PagedListClient<tbAppointment>> GetAppointmentHistoryListWithPaging(string name = null,string status = null, int pagesize = 10, int page = 1)
        //{
        //    string url = string.Format("api/appointment/historylist?name={0}&status={1}&pagesize={2}&page={3}", name,status, pagesize, page);
        //    var data = await ApiRequest<PagedListServer<tbAppointment>>.GetRequest(url); 
        //    var model = new PagedListClient<tbAppointment>();
        //    var pagedList = new StaticPagedList<tbAppointment>(data.Results, page, pagesize, data.TotalCount);
        //    model.Results = pagedList;
        //    model.TotalCount = data.TotalCount;
        //    model.TotalPages = data.TotalPages;
        //    return model;
        //}

    }
}
