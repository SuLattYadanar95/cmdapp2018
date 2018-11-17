using CareMeApi.Repository;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CareMeApi.Controllers
{
    public class BodyPartController : ApiController
    {
        CaremeDBContext dbContext;
        private BodyPartRepository bodypartRepo = null;
        public BodyPartController()
        {
            dbContext = new CaremeDBContext();
            bodypartRepo = new BodyPartRepository(dbContext);
        }
        [Route("api/bodypart/get")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string bodypart = null,string symptomeng = null, 
            string symptomzg = null, string symptomun = null,string specialty=null, int pagesize = 10, int page = 1)
        {
            List<tbBodyPart> results = null;
            Expression<Func<tbBodyPart, bool>> bodypartfilter, specialtyfilter,
                symptomengfilter, symptomzgfilter, symptomunfilter;
            if (bodypart != null)
            {
                bodypartfilter = l => l.BodyPart.StartsWith(bodypart);
            }
            else
            {
                bodypartfilter = l => l.IsDeleted != true;
            }
            if (specialty != null)
            {
                specialtyfilter = l => l.Specialty.StartsWith(specialty);
            }
            else
            {
                specialtyfilter = l => l.IsDeleted != true;
            }
            if (symptomeng != null)
            {
                symptomengfilter = l => l.Symptom_English.StartsWith(symptomeng);
            }
            else
            {
                symptomengfilter = l => l.IsDeleted != true;
            }
            if (symptomun != null)
            {
                symptomunfilter = l => l.Symptom_Myanmar.StartsWith(symptomun);
            }
            else
            {
                symptomunfilter = l => l.IsDeleted != true;
            }
            if (symptomzg != null)
            {
                symptomzgfilter = l => l.Symptom_Myanmar_ZG.StartsWith(symptomzg);
            }
            else
            {
                symptomzgfilter = l => l.IsDeleted != true;
            }
            if (pagesize != 0)
            {
                var skipindex = pagesize * (page - 1);
                results = dbContext.tbBodyParts.Where(bodypartfilter).Where(specialtyfilter)
                    .Where(symptomzgfilter).Where(symptomunfilter).Where(symptomengfilter)
                    .OrderBy(a => a.BodyPart).Skip(skipindex).Take(page).ToList();
            }
            else
            {
                results = dbContext.tbBodyParts.Where(bodypartfilter).Where(specialtyfilter)
                    .Where(symptomzgfilter).Where(symptomunfilter).Where(symptomengfilter)
                    .OrderBy(a => a.BodyPart).ToList();
            }
            return request.CreateResponse<List<tbBodyPart>>(HttpStatusCode.OK, results);
        }

    }
}
