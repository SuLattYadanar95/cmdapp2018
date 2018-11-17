using CareMeApi.Repository;
using Core.Extensions;
using Data.Models;
using Data.ViewModels;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeApi.Controllers
{
    public class StaffController : ApiController
    {
        CaremeDBContext dbContext;
        private StaffRepository staffRepo = null;

        public StaffController()
        {
            dbContext = new CaremeDBContext();
            staffRepo = new StaffRepository(dbContext);
        }

        [Route("api/test/test")]
        [HttpGet]
        public HttpResponseMessage test(HttpRequestMessage request)
        {

            FCMViewModel fcm = new FCMViewModel();
            fcm.to = "ehjwvdgi70I:APA91bGxzHTzNDDmmo2Kzt5qL2AYflTBl3CjxKyO9P4b3w1wLF-4I8gt3U8dh-Vgk9JyoszHcocW4dU36cu7eEYZf1XEnexHJbBZBzgP-pkGzJjaeQNciVjh7tMDgOjNq_SESaZhWgHw";

            fcmdata fcmdata = new fcmdata();
            fcmdata.body = "1 patient is waiting";
            fcmdata.doctorId = 2;
            fcmdata.title = "Booking notification";
            fcmdata.type = "1001";
            fcm.data = fcmdata;
            FCMRequestHelper.sendTokenMessage(fcm);
            //   AzurePhotoUpload.uploadPhoto(photo);
            return request.CreateResponse<string>(HttpStatusCode.OK, "success");
        }

        [Route("api/test/testtime")]
        [HttpGet]
        public HttpResponseMessage testtime(HttpRequestMessage request)
        {

            var datetime = MyExtension.getLocalTime(DateTime.UtcNow);
            return request.CreateResponse<DateTime>(HttpStatusCode.OK, datetime);
        }


        [HttpPost]
        [Route("api/Staff/login")]
        public HttpResponseMessage Login(HttpRequestMessage request, tbStaff login)
        {

            tbStaff result = staffRepo.GetWithoutTracking().Where(a => a.Username == login.Username && a.Password == login.Password).FirstOrDefault();
            return request.CreateResponse<tbStaff>(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/Staff/getStaffData")]
        public HttpResponseMessage getStaffData(HttpRequestMessage request, String username)
        {

            tbStaff result = staffRepo.GetWithoutTracking().Where(a => a.Username == username).FirstOrDefault();
            return request.CreateResponse<tbStaff>(HttpStatusCode.OK, result);
        }
    }
}
