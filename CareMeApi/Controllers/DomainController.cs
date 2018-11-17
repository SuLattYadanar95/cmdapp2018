using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using CareMeApi.Repository;
using Data.Helper;

namespace CareMeApi.Controllers
{
    public class DomainController : ApiController
    {

       
        private DomainRepository repo = null;
        private ServiceRepository serviceRepo = null;
        private ProcedureRepository procedureRepo = null;
        private CaremeDBContext dbContext;

        public DomainController()
        {
          
            dbContext = new CaremeDBContext();
            repo = new DomainRepository(dbContext);
            serviceRepo = new ServiceRepository(dbContext);
            procedureRepo = new ProcedureRepository(dbContext);
        }

        [Route("api/domain/get")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request, int id = 0, string tags = "*", string name = "*",int hospitalid=0, int pagesize = 15, int pageindex = 1)
           
        {
            
                Expression<Func<tbDomain, bool>> filter, tagsfilter, namefilter, hospitalidfilter = null;
                if (id != 0)
                {
                    filter = l => l.ID == id;
                }
                else
                {
                    filter = l => l.IsDeleted != true;
                }
                if (tags != "*")
                {
                    tagsfilter = l => l.Tags.Contains(tags);
                }
                else
                {
                    tagsfilter = l => l.IsDeleted != true;
                }
                if (name != "*")
                {
                    namefilter = l => l.Name.StartsWith(name);
                }
                else
                {
                    namefilter = l => l.IsDeleted != true;
                }
                if (hospitalid != 0)
                {
                    hospitalidfilter = l => l.HospitalId == hospitalid;
                }
                else
                {
                    hospitalidfilter = l => l.IsDeleted != true;
                }

                var skipindex = pagesize * (pageindex - 1);
                var objs = repo.GetWithoutTracking().Where(filter).Where(namefilter).Where(tagsfilter).Where(hospitalidfilter).OrderBy(a => a.Name).Skip(skipindex).Take(pagesize).ToList();
                HttpResponseMessage response = request.CreateResponse<List<tbDomain>>(HttpStatusCode.OK, objs);
                return response;
            
            
        }

        [Route("api/domain/getpagecount")]
        [HttpGet]
        public HttpResponseMessage GetPageCount(HttpRequestMessage request, int airlineid =0,string tags = "*", int pagesize = 10)
        {
            Expression<Func<tbDomain, bool>> airlineidfilter, tagsfilter = null;
            if (airlineid != 0)
            {
                airlineidfilter = l => l.HospitalId == airlineid;
            }
            else
            {
                airlineidfilter = l => l.IsDeleted != true;
            }
            if (tags != "*")
            {
                tagsfilter = l => l.Tags.Contains(tags);
            }
            else
            {
                tagsfilter = l => l.IsDeleted != true;
            }
            var obj = repo.GetWithoutTracking().Where(l => l.IsDeleted != true).Select(a => a.ID).Distinct().Count().GetPageCountInfo(pagesize);
            HttpResponseMessage response = request.CreateResponse<PageCountInfo>(HttpStatusCode.OK, obj);
            return response;
        }

        [Route("api/domain/getwithservicesandprocedures")]
        [HttpGet]
        public HttpResponseMessage GetWithServicesAndProceduresById(HttpRequestMessage request, int id, int airlineid = 0, string tags = "*")

        {
            Expression<Func<tbDomain, bool>> airlineidfilter, tagsfilter = null;
            if (airlineid != 0)
            {
                airlineidfilter = l => l.HospitalId == airlineid;
            }
            else
            {
                airlineidfilter = l => l.IsDeleted != true;
            }
            if (tags != "*")
            {
                tagsfilter = l => l.Tags.Contains(tags);
            }
            else
            {
                tagsfilter = l => l.IsDeleted != true;
            }
            DomainViewModel vm = new DomainViewModel()
            {
                Domain = repo.GetWithoutTracking().Where(a => a.ID == id).FirstOrDefault(),
                ServiceInfo = serviceRepo.GetWithoutTracking().Where(a => a.DomainId == id && a.IsDeleted != true).ToList().Select(a => new ServiceViewModel() {
                    Procedures = procedureRepo.GetWithoutTracking().Where(b => b.ServiceID == a.ID && a.IsDeleted != true).ToList(),
                    Service = a
                }).ToList()
            };

            HttpResponseMessage response = request.CreateResponse<DomainViewModel>(HttpStatusCode.OK, vm);
            return response;
        }
        [Route("api/domain/getwithservices")]
        [HttpGet]
        public HttpResponseMessage GetWithServices(HttpRequestMessage request, int id, int hospitalid = 0,string tags = "*")

        {
            Expression<Func<tbDomain, bool>> hospitalidfilter, tagsfilter = null;
            if (hospitalid != 0)
            {
                hospitalidfilter = l => l.HospitalId == hospitalid;
            }
            else
            {
                hospitalidfilter = l => l.IsDeleted != true;
            }
            if (tags != "*")
            {
                tagsfilter = l => l.Tags.Contains(tags);
            }
            else
            {
                tagsfilter = l => l.IsDeleted != true;
            }
            DomainWithServiceViewModel vm = new DomainWithServiceViewModel()
            {
                Domain = repo.GetWithoutTracking().Where(a => a.ID == id).FirstOrDefault(),
                Services = serviceRepo.GetWithoutTracking().Where(a => a.DomainId == id && a.IsDeleted != true).ToList()
            };

            HttpResponseMessage response = request.CreateResponse<DomainWithServiceViewModel>(HttpStatusCode.OK, vm);
            return response;
        }


    }
}
