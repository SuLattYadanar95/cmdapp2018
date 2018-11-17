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
    public class DoctorApiRequestHelper
    {
        //public static async Task<PagedListClient<DoctorHospitalViewModel>> GetDoctorWithPagingOriginal(string doctorname = null, string hospitalname = null, int pagesize = 10, int page = 1)
        //{
        //    string url = string.Format("api/doctor/listOriginal?doctorname={0}&hospitalname={1}&pagesize={2}&page={3}", doctorname, hospitalname, pagesize, page);
        //    var data = await ApiRequest<PagedListServer<DoctorHospitalViewModel>>.GetRequest(url);

        //    var model = new PagedListClient<DoctorHospitalViewModel>();
        //    var pagedList = new StaticPagedList<DoctorHospitalViewModel>(data.Results, page, pagesize, data.TotalCount);
        //    model.Results = pagedList;
        //    model.TotalCount = data.TotalCount;
        //    model.TotalPages = data.TotalPages;
        //    return model;
        //}

        public static async Task<PagedListClient<DoctorHospitalViewModel>> GetDoctorWithPaging(string doctorname = null, 
            string hospitalname = null, int pagesize = 10, int page = 1,int doctorid=0,int specialtyid = 0,string specialties = null, int hospitalid=0)
        {
            string url = $"api/doctor/list?doctorname={doctorname}&doctorid={doctorid}&specialtyid={specialtyid}&specialties={specialties}&hospitalname={hospitalname}&hospitalid={hospitalid}&pagesize={pagesize}&page={page}";
            var data = await ApiRequest<PagedListServer<DoctorHospitalViewModel>>.GetRequest(url);

            var model = new PagedListClient<DoctorHospitalViewModel>();
            var pagedList = new StaticPagedList<DoctorHospitalViewModel>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }
        public static async Task<PagedListServer<tbDoctor>> Get(int doctorid = 0, string doctorname = null, int hospitalid = 0,
           string hospitalname = null, int pagesize = 10, int page = 1, int specialtyid = 0, string specialties = null)
        {
            string url = $"api/doctor/get?doctorname={doctorname}&doctorid={doctorid}&specialtyid={specialtyid}&specialties={specialties}&hospitalname={hospitalname}&hospitalid={hospitalid}&pagesize={pagesize}&page={page}";
            return await ApiRequest<PagedListServer<tbDoctor>>.GetRequest(url);
        }
        public static async Task<List<string>> GetSpecialties(int doctorid = 0, string doctorname = null, int hospitalid = 0, string hospitalname = null, int pagesize = 10, int page = 1)
        {
            string url = $"api/doctor/specialties?doctorname={doctorname}&doctorid={doctorid}&hospitalid={hospitalid}&hospitalname={hospitalname}&pagesize={pagesize}&page={page}";
            return await ApiRequest<List<string>>.GetRequest(url);

        }
        public static async Task<DoctorSpecialityViewModel> GetDoctorById(int ID)
        {
            string url = string.Format("api/Doctor/GetDoctorByID?ID={0}", ID);
            DoctorSpecialityViewModel result = await ApiRequest<DoctorSpecialityViewModel>.GetRequest(url);
            return result;
        }

        public static async Task<DoctorSpecialityViewModel> UpSertDoctor(DoctorSpecialityViewModel dhvm)
        {
            var url = "api/Doctor/UpsertDoctor";
            DoctorSpecialityViewModel result = await ApiRequest<DoctorSpecialityViewModel>.PostRequest(url, dhvm);
            return result;
        }

        public static async Task<DoctorHospitalViewModel> doctordelete(int ID, int hospitalid=0)
        {
            var url = string.Format("api/Doctor/DoctorDelete?ID={0}&hospitalid={1}", ID, hospitalid);
            DoctorHospitalViewModel result = await ApiRequest<DoctorHospitalViewModel>.GetRequest(url);
            return result;
        }

        public static async Task<tbScheduleData> scheduledetaildelete(int ID)
        {
            var url = string.Format("api/Schedule/ScheduleDetailDelete?ID={0}", ID);
            tbScheduleData result = await ApiRequest<tbScheduleData>.GetRequest(url);
            return result;
        }

        public static async Task<tbSchedule> scheduledelete(int ID)
        {
            var url = string.Format("api/Doctor/ScheduleDelete?ID={0}", ID);
            tbSchedule result = await ApiRequest<tbSchedule>.GetRequest(url);
            return result;
        }


        public static async Task<DoctorSpecialitiesViewModel> GetDocSpecialtyListFilter()
        {
            string url = string.Format("api/Doctor/GetDocSpecialtyListFilter");
            return await ApiRequest<DoctorSpecialitiesViewModel>.GetRequest(url);
        }

        public static async Task<List<tbSpecialty>> getSpecialityList()
        {
            string url = string.Format("api/Doctor/getSpecialtyList");
            return await ApiRequest<List<tbSpecialty>>.GetRequest(url);
        }


        public static async Task<tbSchedule> GetScheduleByID(int ID)
        {
            string url = string.Format("api/Doctor/getScheduleByID?ID={0}", ID);
            var data = await ApiRequest<tbSchedule>.GetRequest(url);
            return data;
        }

        public static async Task<tbScheduleData> UpSertSchedule(tbScheduleData schedule)
        {
            var url = "api/Doctor/UpsertSchedule";
            tbScheduleData result = await ApiRequest<tbScheduleData>.PostRequest(url, schedule);
            return result;
        }

        public static async Task<tbSchedule> UpSertSchedule2(tbSchedule schedule)
        {
            var url = "api/Doctor/UpsertSchedule2";
            tbSchedule result = await ApiRequest<tbSchedule>.PostRequest(url, schedule);
            return result;
        }

        public static async Task<tbScheduleData> UpSertGenerateSchedule(DateTime? fromdatepicker = null, DateTime? todatepicker = null, int doctorid = 0, int hospitalid = 0, string hospitalname = null)
        {
            string url = string.Format("api/Doctor/UpSertGenerateSchedule?fromdatepicker={0}&todatepicker={1}&doctorid={2}&hospitalid={3}&hospitalname={4}", fromdatepicker, todatepicker, doctorid, hospitalid, hospitalname);
            var data = await ApiRequest<tbScheduleData>.GetRequest(url);
            return data;
        }

        public static async Task<List<tbSchedule>> scheduleListbyDoctorID(int doctorid = 0)
        {
            string url = string.Format("api/Doctor/scheduleListbyDoctorID?doctorid={0}", doctorid);
            var data = await ApiRequest<List<tbSchedule>>.GetRequest(url);
            return data;
        }

        public static async Task<List<tbSchedule>> getgenerateschedulelist(int id = 0)
        {
            string url = string.Format("api/Doctor/getgenerateschedulelist?doctorid={0}", id);
            var data = await ApiRequest<List<tbSchedule>>.GetRequest(url);
            return data;
        }

        public static async Task<List<tbDoctorHospital>> getdoctorlistbyhospital(int hospitalID = 0)
        {
            string url = string.Format("api/Doctor/getdoctorlistbyhospital?hospitalID={0}", hospitalID);
            var data = await ApiRequest<List<tbDoctorHospital>>.GetRequest(url);
            return data;
        }

        public static async Task<List<tbDoctor>> DoctorList()
        {
            string url = string.Format("api/Doctor/DoctorList");
            var data = await ApiRequest<List<tbDoctor>>.GetRequest(url);
            return data;
        }


    }
}
