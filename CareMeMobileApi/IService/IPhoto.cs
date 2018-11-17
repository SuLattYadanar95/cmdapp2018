using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.IService
{
    public interface IPhoto
    {
        string uploadPhoto(string stringInBase64);
    }
}