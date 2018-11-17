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
    public class HospitalApiRequestHelper
    {
        public static async Task<PagedListClient<tbHospital>> GetHospitalWithPaging(string hospitalname = null, int pagesize = 10, int page = 1)
        {
            string url = string.Format("api/hospital/list?hospitalname={0}&pagesize={1}&page={2}", hospitalname, pagesize, page);
            var data = await ApiRequest<PagedListServer<tbHospital>>.GetRequest(url);

            var model = new PagedListClient<tbHospital>();
            var pagedList = new StaticPagedList<tbHospital>(data.Results, page, pagesize, data.TotalCount);
            model.Results = pagedList;
            model.TotalCount = data.TotalCount;
            model.TotalPages = data.TotalPages;
            return model;
        }
        public static async Task<List<tbHospital>> Get(string township = null, int pagesize = 10, int page = 1)
        {
            string url = $"api/hospital/get?township={township}&pagesize={pagesize}&page={page}";
            return await ApiRequest<List<tbHospital>>.GetRequest(url);
        }
        public static async Task<tbHospital> GetHospitalById(int ID)
        {
            string url = string.Format("api/hospital/GetHospitalById?ID={0}", ID);
            tbHospital result = await ApiRequest<tbHospital>.GetRequest(url);
            return result;
        }

        public static async Task<tbHospital> UpSertHospital(tbHospital hospital)
        {
            var url = "api/hospital/UpsertHospital";
            tbHospital result = await ApiRequest<tbHospital>.PostRequest(url, hospital);
            return result;
        }

        public static async Task<tbHospital> hospitaldelete(int ID)
        {
            var url = string.Format("api/hospital/hospitaldelete?ID={0}", ID);
            tbHospital result = await ApiRequest<tbHospital>.GetRequest(url);
            return result;
        }

        public static async Task<List<tbLocation>> getstate()
        {
            string url = string.Format("api/hospital/GetState");
            return await ApiRequest<List<tbLocation>>.GetRequest(url);
        }

        public static async Task<IEnumerable<string>> GetTownShip(string state = null)
        {
            string url = string.Format("api/hospital/GetTownShip?state={0}", state);
            return await ApiRequest<IEnumerable<string>>.GetRequest(url);
        }

        #region bot web
        public static async Task<IEnumerable<HospitalSchedules>> scheduledHospitalList(int doctorID, int hospitalID = 0, DateTime? AppointmentDate = null)
        {
            //int doctorID, int hospitalID = 0, DateTime? AppointmentDate= null
            string url = string.Format("api/Hospital/scheduledHospitalList?doctorID={0}&hospitalID={1}&AppointmentDate={2}", doctorID, hospitalID, AppointmentDate);
            return await ApiRequest<IEnumerable<HospitalSchedules>>.GetRequest(url);
        }
        #endregion
    }
}
