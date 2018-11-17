
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
using System.Threading.Tasks;
using System.Web.Http;

namespace CareMeApi.Controllers
{
    public class FAQController : ApiController
    {
        private FAQRepository repo = null;
        private ServiceRepository serviceRepo = null;
        private ProcedureRepository procedureRepo = null;

        public FAQController()
        {
            CaremeDBContext dbContext = new CaremeDBContext();
            repo = new FAQRepository(dbContext);
            serviceRepo = new ServiceRepository(dbContext);
            procedureRepo = new ProcedureRepository(dbContext);
        }
        // GET: Suggestion
        [HttpPost]
        [Route("api/faq/create")] //UID
        public async Task<HttpResponseMessage> create(HttpRequestMessage request, [FromBody] tbFAQ obj)
        {

            tbFAQ result;
            obj.Accesstime = DateTime.UtcNow.getLocalTime();

            if (obj.ID == default(int))
            {
                obj.IsDeleted = false;               
                result = repo.AddWithGetObj(obj);
                if (result != null)
                {                    
                   //await BookSendApiRequestHelper.SendMessage(string.Format("Your service ticket ID is {0}. Please use that for customer service enquiry and any kind of support.", result.Code), obj.UserId);
                }
            }
            else
            {
                result = repo.UpdatewithObj(obj);
            }

            HttpResponseMessage response = request.CreateResponse<tbFAQ>(HttpStatusCode.OK, result);
            return response;

        }
        [HttpDelete]
        [Route("api/faq/delete")] //UID //check return
        public HttpResponseMessage delete(HttpRequestMessage request, int id)
        {
            var obj = repo.Get().Where(a => a.ID == id).FirstOrDefault();
            obj.IsDeleted = true;
            obj.Accesstime = DateTime.UtcNow.getLocalTime();
            var result = repo.UpdatewithObj(obj);
            HttpResponseMessage response = request.CreateResponse<tbFAQ>(HttpStatusCode.OK, result);
            return response;
        }
        [Route("api/faq/get")] //UID
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string question = "*", string answer = "*", int serviceid = 0,
            int procedureid = 0, int pagesize = 30, int pageindex = 1)
        {

            Expression<Func<tbFAQ, bool>> questionfilter, answerfilter, serviceidfilter, procedureidfilter = null;          
            if (question != "*")
            {
                FuzzySearch _fuzz = new FuzzySearch(question);
                questionfilter = l => _fuzz.IsMatch(l.Question);
            }
            else
            {
                questionfilter = l => l.IsDeleted != true;
            }
            if (answer != "*")
            {
                FuzzySearch _fuzz = new FuzzySearch(answer);
                answerfilter = l => _fuzz.IsMatch(l.Answer);
                //answerfilter = l => l.ManagedbyId == managedbyid;
            }
            else
            {
                answerfilter = l => l.IsDeleted != true;
            }
            if (serviceid != 0)
            {
                serviceidfilter = l => l.ServiceId == serviceid;
            }
            else
            {
                serviceidfilter = l => l.IsDeleted != true;
            }
            if (procedureid != 0)
            {
                procedureidfilter = l => l.ProcedureId == procedureid;
            }
            else
            {
                procedureidfilter = l => l.IsDeleted != true;
            }
            var skipindex = pagesize * (pageindex - 1);
            var objs = repo.GetWithoutTracking().Where(questionfilter).Where(answerfilter).Where(serviceidfilter).Where(procedureidfilter)
                .OrderByDescending(a => a.Question).Skip(skipindex).Take(pagesize).ToList();

            HttpResponseMessage response = request.CreateResponse<List<tbFAQ>>(HttpStatusCode.OK, objs);
            return response;

        }
        [Route("api/faq/getwithservice")] //UID
        [HttpGet]
        public HttpResponseMessage GetWithService(HttpRequestMessage request, string question = "*", string answer = "*", int serviceid = 0,
            int procedureid = 0, int pagesize = 15, int pageindex = 1)
        {

            Expression<Func<tbFAQ, bool>> questionfilter, answerfilter, serviceidfilter, procedureidfilter = null;
            if (question != "*")
            {
                FuzzySearch _fuzz = new FuzzySearch(question);
                questionfilter = l => _fuzz.IsMatch(l.Question);
            }
            else
            {
                questionfilter = l => l.IsDeleted != true;
            }
            if (answer != "*")
            {
                FuzzySearch _fuzz = new FuzzySearch(answer);
                answerfilter = l => _fuzz.IsMatch(l.Answer);
                //answerfilter = l => l.ManagedbyId == managedbyid;
            }
            else
            {
                answerfilter = l => l.IsDeleted != true;
            }
            if (serviceid != 0)
            {
                serviceidfilter = l => l.ServiceId == serviceid;
            }
            else
            {
                serviceidfilter = l => l.IsDeleted != true;
            }
            if (procedureid != 0)
            {
                procedureidfilter = l => l.ProcedureId == procedureid;
            }
            else
            {
                procedureidfilter = l => l.IsDeleted != true;
            }
            var skipindex = pagesize * (pageindex - 1);
            var obj = new FAQViewModel
            {
                FAQs = repo.GetWithoutTracking().Where(questionfilter).Where(answerfilter).Where(serviceidfilter).Where(procedureidfilter)
                .OrderByDescending(a => a.Question).Skip(skipindex).Take(pagesize).ToList(),
                Procedure = procedureRepo.GetWithoutTracking().Where(a => a.ID == procedureid).FirstOrDefault(),
                Service = serviceRepo.GetWithoutTracking().Where(a => a.ID == serviceid).FirstOrDefault()
            };

            HttpResponseMessage response = request.CreateResponse<FAQViewModel>(HttpStatusCode.OK, obj);
            return response;

        }
        [Route("api/faq/getpagecount")]
        [HttpGet]
        public HttpResponseMessage GetPageCount(HttpRequestMessage request, int pagesize = 10)
        {
            var obj = repo.GetWithoutTracking().Where(l => l.IsDeleted != true).Select(a => a.ID).Distinct().Count().GetPageCountInfo(pagesize);
            HttpResponseMessage response = request.CreateResponse<PageCountInfo>(HttpStatusCode.OK, obj);
            return response;
        }
    }
}
