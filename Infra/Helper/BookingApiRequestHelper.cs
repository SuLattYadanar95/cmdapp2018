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
    public class BookingApiRequestHelper
    {
        //Get Docor List By Schedule Time
        public static async Task<PagedListClient<tbDoctor>> DoctorListByTime(DateTime? fromtime = null, DateTime? totime = null, int pagesize = 0, int page = 0,int hospitalid=0, int appointmentid = 0, DateTime? date= null)
        {
            string url = string.Format("api/Booking/DoctorListByTime?fromtime={0}&totime={1}&pagesize={2}&page={3}&hospitalid={4}&appointmentid={5}&date={6}", fromtime,totime, pagesize, page, hospitalid, appointmentid, date);
            var data = await ApiRequest<PagedListServer<tbDoctor>>.GetRequest(url);
            var model = new PagedListClient<tbDoctor>();
            var pagedList = new StaticPagedList<tbDoctor>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }

        //Get Appointment Schedule by Date
        public static async Task<AppointScheduleViewModel> AppointmentSchedule(DateTime? datetime = null, string type = null,int hospitalid=0)
        {
            string url = string.Format("api/Booking/AppointmentSchedule?datetime={0}&type={1}&hospitalid={2}", datetime, type, hospitalid);
            return await ApiRequest<AppointScheduleViewModel>.GetRequest(url);
        }

        //Get Doctor List by Schedule Date
        //public static async Task<PagedListClient<tbScheduleData>> GetDoctorWithPaging(string doctorname = null, string hospitalname = null, int pagesize = 10, int page = 1, DateTime? datetime = null)
        //{
        //    string url = string.Format("api/Doctor3/ListByDate?doctorname={0}&hospitalname={1}&pagesize={2}&page={3}&datetime={4}", doctorname, hospitalname, pagesize, page, datetime);
        //    var data = await ApiRequest<PagedListServer<tbScheduleData>>.GetRequest(url);

        //    var model = new PagedListClient<tbScheduleData>();
        //    var pagedList = new StaticPagedList<tbScheduleData>(data.Results, page, pagesize, data.TotalCount);
        //    model.Results = pagedList;
        //    model.TotalCount = data.TotalCount;
        //    model.TotalPages = data.TotalPages;
        //    return model;
        //}
        //public static async Task<PagedListClient<ScheduleDoctorViewModel>> GetDoctorWithPaging(DateTime? datetime = null, int pagesize = 5, int page = 1)
        //{
        //    string url = string.Format("api/Doctor3/ListByDate?datetime={0}&pagesize={1}&page={2}", datetime, pagesize, page);
        //    var data = await ApiRequest<PagedListServer<ScheduleDoctorViewModel>>.GetRequest(url);
        //    var model = new PagedListClient<ScheduleDoctorViewModel>();
        //    var pagedList = new StaticPagedList<ScheduleDoctorViewModel>(data.Results, page, pagesize, data.TotalCount);
        //    model.Results = pagedList;
        //    model.TotalCount = data.TotalCount;
        //    model.TotalPages = data.TotalPages;
        //    return model;
        //}

        //getAppointments
        public static async Task<List<tbAppointment>> getAppointments(string userid = null)
        {
            string url = string.Format("api/Booking/getAppointments?userid={0}", userid);
            var data = await ApiRequest<List<tbAppointment>>.GetRequest(url);
            return data;
        }

        //Get Patient By ID
        public static async Task<tbPatient> GetPatientByID(int ID)
        {
            string url = string.Format("api/Booking/getScheduleByID?ID={0}", ID);
            var data = await ApiRequest<tbPatient>.GetRequest(url);
            return data;
        }
        public static async Task<PatientAppointmentViewModel> UpsertPatient(PatientAppointmentViewModel patient)
        {
            var url = "api/Patient/UpsertPatient";
            PatientAppointmentViewModel result = await ApiRequest<PatientAppointmentViewModel>.PostRequest(url, patient);
            return result;
        }
        public static async Task<List<tbScheduleData>> scheduleListbyDoctorID(int doctorid = 0, string docname = null,int hospitalid=0, DateTime? date=null)
        {
            string url = string.Format("api/Booking/scheduleListbyDoctorID?doctorid={0}&docname={1}&hospitalid={2}&date={3}", doctorid, docname, hospitalid, date);
            var data = await ApiRequest<List<tbScheduleData>>.GetRequest(url);
            return data;
        }

        public static async Task<PagedListClient<ScheduleBookingViewModel>> list(int docid = 0, int specialityid = 0, string docname = null, DateTime? datetime = null, DateTime? fromtime = null, DateTime? totime = null, int pagesize = 5, int page = 1,int hospitalid=0)
        {
            string url = string.Format("api/Booking/list?docid={0}&specialityid={1}&docname={2}&datetime={3}&fromtime={4}&totime={5}&pagesize={6}&page={7}&hospitalid={8}", docid, specialityid, docname, datetime, fromtime, totime, pagesize, page,hospitalid);
            var data = await ApiRequest<PagedListServer<ScheduleBookingViewModel>>.GetRequest(url);
            var model = new PagedListClient<ScheduleBookingViewModel>();
            var pagedList = new StaticPagedList<ScheduleBookingViewModel>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }
    }

}
