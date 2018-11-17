using CareMeMobileApi.Repository;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeMobileApi.Controllers
{
    public class SpecialityController : ApiController
    {
       
              
        private SpecialityRepository specialityRepo = null;

        public SpecialityController()
        {
           
            specialityRepo = new SpecialityRepository();
        }

        [Route("api/specialty/list")]
        [HttpGet]
        public HttpResponseMessage specialitylist(HttpRequestMessage request, string name = null)
        {
            Expression<Func<tbSpecialty, bool>> specialitynamefilter = null;

            if (name != null)
            {
                specialitynamefilter = l => l.Specialty.Contains(name);
            }
            else
            {
                specialitynamefilter = l => l.IsDeleted != true;
            }
            List<tbSpecialty> result = specialityRepo.GetWithoutTracking().Where(a => a.IsDeleted != true).Where(specialitynamefilter).OrderBy(a =>a.Specialty).ToList();
            return request.CreateResponse<List<tbSpecialty>>(HttpStatusCode.OK, result);
        }


    }
}
