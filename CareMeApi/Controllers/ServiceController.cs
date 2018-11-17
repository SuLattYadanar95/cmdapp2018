using Data.Helper;
using Data.Models;
using Data.ViewModels;
using CareMeApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeApi.Controllers
{
    public class ServiceController : ApiController
    {
     
        private ServiceRepository repo = null;
        private ProcedureRepository procedureRepo = null;
        public ServiceController()
        {
            procedureRepo = new ProcedureRepository();
            repo = new ServiceRepository();
        }

        [Route("api/service/get")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int id = 0, string title= "*", string tag = "*",int domainid= 0,string domainname ="*",int hospitalId = 0,bool isactive =true, int pagesize = 15, int pageindex = 1)
           
        {
            Expression<Func<tbService, bool>> filter,tagfilter, titlefilter,domainidfilter,hospitalidfilter,isactivefilter = null;
            if (id != 0)
            {
                filter = l => l.ID == id;
            }
            else
            {
                filter = l => l.IsDeleted != true;
            }
            if (title != "*")
            {
                titlefilter = l => l.Title == title;
            }
            else
            {
                titlefilter = l => l.IsDeleted != true;
            }
            if (domainid != 0)
            {
                domainidfilter = l => l.DomainId == domainid;
            }
            else
            {
                domainidfilter = l => l.IsDeleted != true;
            }
           
            if (tag != "*")
            {
                tagfilter = l => l.Tag == tag;
            }
            else
            {
                tagfilter = l => l.IsDeleted != true;
            }
            if (isactive)
            {
                isactivefilter = l => l.IsActive == isactive;
            }
            else
            {
                isactivefilter = l => l.IsDeleted != true;
            }
            if (hospitalId != 0)
            {
                hospitalidfilter = l => l.HospitalId == hospitalId;
            }
            else
            {
                hospitalidfilter = l => l.IsDeleted != true;
            }


            var skipindex = pagesize * (pageindex - 1);
            var objs = repo.GetWithoutTracking().Where(filter).Where(titlefilter).Where(tagfilter).Where(domainidfilter).Where(hospitalidfilter).Where(isactivefilter).OrderBy(a => a.Title).Skip(skipindex).Take(pagesize).ToList();
            HttpResponseMessage response = request.CreateResponse<List<tbService>>(HttpStatusCode.OK, objs);
            return response;
        }
        [Route("api/service/getwithprocedures")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)

        {
            ServiceViewModel vm = new ServiceViewModel() {
                Service = repo.GetWithoutTracking().Where(a => a.ID == id).FirstOrDefault(),
                Procedures = procedureRepo.GetWithoutTracking().Where(a => a.ServiceID == id).ToList()
            };
          
            HttpResponseMessage response = request.CreateResponse<ServiceViewModel>(HttpStatusCode.OK, vm);
            return response;
        }

        [Route("api/service/getpagecount")]
        [HttpGet]
        public HttpResponseMessage GetPageCount(HttpRequestMessage request, int pagesize = 10)
        {
            var obj = repo.GetWithoutTracking().Where(l => l.IsDeleted != true).Select(a => a.Title).Distinct().Count().GetPageCountInfo(pagesize);
            HttpResponseMessage response = request.CreateResponse<PageCountInfo>(HttpStatusCode.OK, obj);
            return response;
        }

        [Route("api/service/getdomain")]
        [HttpGet]
        public HttpResponseMessage GetDomain(HttpRequestMessage request, int pagesize = 15, int pageindex = 1)
        {

            var skipindex = pagesize * (pageindex - 1);
            var objs = repo.GetWithoutTracking().Where(a => a.IsDeleted != true).Select(a => a.Title).Distinct().OrderBy(a => a).Skip(skipindex).Take(pagesize).ToList();
            HttpResponseMessage response = request.CreateResponse<List<string>>(HttpStatusCode.OK, objs);
            return response;
        }
        [Route("api/service/getcategory")]
        [HttpGet]
        public HttpResponseMessage GetCategory(HttpRequestMessage request, int facilityid = 0, int pagesize = 15, int pageindex = 1)
        {
            Expression<Func<tbService, bool>> facilityidfilter;
            if (facilityid != 0)
            {
                facilityidfilter = l => l.HospitalId == facilityid;
            }
            else
            {
                facilityidfilter = l => l.IsDeleted != true;
            }
            var skipindex = pagesize * (pageindex - 1);
            var objs = repo.GetWithoutTracking().Where(a => a.IsDeleted != true).Where(facilityidfilter).Select(a => a.Tag).Distinct().OrderBy(a => a).Skip(skipindex).Take(pagesize).ToList();
            HttpResponseMessage response = request.CreateResponse<List<string>>(HttpStatusCode.OK, objs);
            return response;
        }



    }
}
