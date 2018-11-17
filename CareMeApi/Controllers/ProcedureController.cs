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
    public class ProcedureController : ApiController
    {
     
        private ProcedureRepository repo = null;
        
        public ProcedureController()
        {
           
            repo = new ProcedureRepository();
           
        }

        [Route("api/procedure/get")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int id = 0, string code = "*", string tag = "*",int serviceId =0,string servicetitle ="*", int pagesize = 15, int pageindex = 1)
           
        {
            Expression<Func<tbProcedure, bool>> filter,tagfilter, codefilter, serviceidfilter, servicetitlefilter = null;
            if (id != 0)
            {
                filter = l => l.ID == id;
            }
            else
            {
                filter = l => l.IsDeleted != true;
            }
            if (code != "*")
            {
                codefilter = l => l.Code == code;
            }
            else
            {
                codefilter = l => l.IsDeleted != true;
            }
            if (tag != "*")
            {
                tagfilter = l => l.Tag == tag;
            }
            else
            {
                tagfilter = l => l.IsDeleted != true;
            }
            if (serviceId !=0)
            {
                serviceidfilter = l => l.ServiceID == serviceId;
            }
            else
            {
                serviceidfilter = l => l.IsDeleted != true;
            }
            if (servicetitle != "*")
            {
                servicetitlefilter = l => l.ServiceTitle.StartsWith(servicetitle);
            }
            else
            {
                servicetitlefilter = l => l.IsDeleted != true;
            }

            var skipindex = pagesize * (pageindex - 1);
            var objs = repo.GetWithoutTracking().Where(filter).Where(codefilter).Where(tagfilter).OrderBy(a => a.Description).Skip(skipindex).Take(pagesize).ToList();
            HttpResponseMessage response = request.CreateResponse<List<tbProcedure>>(HttpStatusCode.OK, objs);
            return response;
        }

        [Route("api/procedure/getpagecount")]
        [HttpGet]
        public HttpResponseMessage GetPageCount(HttpRequestMessage request, int pagesize = 10)
        {
            var obj = repo.GetWithoutTracking().Where(l => l.IsDeleted != true).Select(a => a.ID).Distinct().Count().GetPageCountInfo(pagesize);
            HttpResponseMessage response = request.CreateResponse<PageCountInfo>(HttpStatusCode.OK, obj);
            return response;
        }
       



    }
}
