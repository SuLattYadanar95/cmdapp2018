using Data.Helper;
using Data.Models;
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
    public class SuggestionController : ApiController
    {
        private SuggestionRepository repo = null;
      

        public SuggestionController()
        {
            
            repo = new SuggestionRepository();
        }
        // GET: Suggestion
        [HttpPost]
        [Route("api/suggestion/create")] //UID
        public async Task<HttpResponseMessage> create(HttpRequestMessage request, [FromBody] tbSuggestion obj)
        {

            tbSuggestion result;
            obj.Accesstime = DateTime.UtcNow.getLocalTime();

            if (obj.ID == default(int))
            {
                var code = Convert.ToInt32(repo.GetWithoutTracking().Where(d => d.IsDeleted != true).Select(d => d.CodeIndex ?? 0).DefaultIfEmpty(0).Max());
                obj.IsDeleted = false;
                obj.Code = "S".getCode(code + 1, "000000");
                obj.CodeIndex = code + 1;
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

            HttpResponseMessage response = request.CreateResponse<tbSuggestion>(HttpStatusCode.OK, result);
            return response;

        }
        [HttpDelete]
        [Route("api/suggestion/delete")] //UID //check return
        public HttpResponseMessage delete(HttpRequestMessage request, int id)
        {
            var obj = repo.Get().Where(a => a.ID == id).FirstOrDefault();
            obj.IsDeleted = true;
            obj.Accesstime = DateTime.UtcNow.getLocalTime();
            var result = repo.UpdatewithObj(obj);
            HttpResponseMessage response = request.CreateResponse<tbSuggestion>(HttpStatusCode.OK, result);
            return response;
        }
        [Route("api/suggestion/get")] //UID
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string code = null, bool isResponded = false, string subject = "*", int managedbyid = 0,
            string managedbyname = "*", DateTime? createdat = null, string userid = "*", string username = "*", int pagesize = 15, int pageindex = 1)
        {

            Expression<Func<tbSuggestion, bool>> codefilter, isrepondedfilter, subjectfilter, respondedbyidfilter, respondedbynamefilter,
            createdatfilter, useridfilter, usernamefilter = null;

            if (code != "*")
            {
                codefilter = l => l.Code == code;
            }
            else
            {
                codefilter = l => l.IsDeleted != true;
            }
            if (isResponded)
            {
                isrepondedfilter = l => l.IsReponded == isResponded;
            }
            else
            {
                isrepondedfilter = l => l.IsDeleted != true;
            }
            if (subject != "*")
            {
                subjectfilter = l => l.Subject.StartsWith(subject);
            }
            else
            {
                subjectfilter = l => l.IsDeleted != true;
            }
            if (managedbyid != 0)
            {
                respondedbyidfilter = l => l.ManagedbyId == managedbyid;
            }
            else
            {
                respondedbyidfilter = l => l.IsDeleted != true;
            }
            if (managedbyname != "*")
            {
                respondedbynamefilter = l => l.IsReponded == isResponded;
            }
            else
            {
                respondedbynamefilter = l => l.IsDeleted != true;
            }
            if (createdat != null)
            {
                createdatfilter = l => l.CreatedAt == createdat;
            }
            else
            {
                createdatfilter = l => l.IsDeleted != true;
            }
            if (userid != "*")
            {
                useridfilter = l => l.UserId == userid;
            }
            else
            {
                useridfilter = l => l.IsDeleted != true;
            }
            if (username != "*")
            {
                usernamefilter = l => l.UserName == username;
            }
            else
            {
                usernamefilter = l => l.IsDeleted != true;
            }
            var objs = repo.GetWithoutTracking().Where(codefilter).Where(subjectfilter).Where(respondedbyidfilter).Where(respondedbynamefilter)
                .Where(createdatfilter).Where(useridfilter).Where(usernamefilter).OrderByDescending(a => a.CreatedAt).ToList();

            HttpResponseMessage response = request.CreateResponse<List<tbSuggestion>>(HttpStatusCode.OK, objs);
            return response;


        }
    }
}
