using CareMeMobileApi.Repository;
using Data.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeMobileApi.Controllers
{
    public class HospitalController : ApiController
    {
        private HospitalRepository hospitalRepo = null;

        public HospitalController()
        {
           
            hospitalRepo = new HospitalRepository();
        }

        [HttpGet, Route("api/hospital/list")]
        public HttpResponseMessage hospitallist(HttpRequestMessage request, string searchValue = null, string hospitalname = null, string hospitaladdress = null)
        {
            Expression<Func<tbHospital, bool>> hospitalnamefilter, hospitaladdressfilter = null;

            if (hospitalname != null)
            {
                hospitalnamefilter = l => l.Name.Contains(hospitalname);
            }
            else
            {
                hospitalnamefilter = l => l.IsDeleted != true;
            }
            if (hospitaladdress != null)
            {
                hospitaladdressfilter = l => l.Address.Contains(hospitalname);
            }
            else
            {
                hospitaladdressfilter = l => l.IsDeleted != true;
            }
            var predicate = PredicateBuilder.False<tbHospital>();
            if (searchValue != null)
            {

                predicate = predicate.Or(p => p.Name.Contains(searchValue));
                predicate = predicate.Or(p => p.Address.Contains(searchValue));
            }
            else
            {
                predicate = l => l.IsDeleted != true;
            }
            List<tbHospital> result = hospitalRepo.GetWithoutTracking().Where(a => a.IsDeleted != true).Where(hospitalnamefilter).Where(hospitaladdressfilter).Where(predicate).OrderBy(a => a.Name).ToList();
            return request.CreateResponse<List<tbHospital>>(HttpStatusCode.OK, result);
        }

    }
}
