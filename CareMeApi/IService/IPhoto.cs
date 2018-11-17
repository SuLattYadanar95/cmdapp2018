using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeApi.IService
{
    public interface IPhoto
    {
        string uploadPhoto(string stringInBase64);
    }
}