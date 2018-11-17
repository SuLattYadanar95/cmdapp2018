using CaremebotMSApi.Repository;
using Data.Helper;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaremebotMSApi.Controllers
{
    public class ActivityLogController : ApiController
    {
        private ActivityLogRepository repo = null;
        public ActivityLogController()
        {
            repo = new ActivityLogRepository();
        }
        [HttpPost]
        [Route("api/activitylog/create")]

        public HttpResponseMessage create(HttpRequestMessage request, [FromBody] tbActivityLog obj)
        {

            tbActivityLog result;
            obj.Accesstime = DateTime.UtcNow.getLocalTime();
            obj.IsDeleted = false;
            if (obj.ID == default(int))
            {
                result = repo.AddWithGetObj(obj);
            }
            else
            {
                result = repo.UpdatewithObj(obj);
            }
            HttpResponseMessage response = request.CreateResponse<tbActivityLog>(HttpStatusCode.OK, result);
            return response;


        }
    }
}
