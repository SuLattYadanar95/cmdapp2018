using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CareMeApi.IService
{
    public interface IQueryService
    {
        Expression<Func<tbSchedule, bool>> daysQuery(string day);

    }
}