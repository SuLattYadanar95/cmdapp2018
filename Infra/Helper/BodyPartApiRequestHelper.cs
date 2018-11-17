using Data.Models;
using Data.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class BodyPartApiRequestHelper
    {
        
        public static async Task<List<tbBodyPart>> Get(string bodypart = null, string symptomeng = null,
            string symptomzg = null, string symptomun = null, string specialty = null, int pagesize = 10, int page = 1)
        {
            string url = $"api/bodypart/get?bodypart={bodypart}&symptomeng={symptomeng}&symptomzg={symptomzg}&symptomun={symptomun}&" +
                $"specialty={specialty}&pagesize={pagesize}&page={page}";
            return await ApiRequest<List<tbBodyPart>>.GetRequest(url);
        }
       
    }
}
