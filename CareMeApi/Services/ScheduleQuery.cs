using CareMeApi.IService;
using Data.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CareMeApi.Services
{
    public class ScheduleQuery: IQueryService
    {
        public Expression<Func<tbSchedule, bool>> daysQuery(string day)
        {
            var predicate = PredicateBuilder.False<tbSchedule>();
            predicate = predicate.Or(p => p.IsDeleted != true);
            switch (day)
            {
                case "Monday":
                    predicate = predicate.And(p => p.IsMonday == true);
                    break;
                case "Tuesday":
                    predicate = predicate.And(p => p.IsTuesday == true);
                    break;
                case "Wednesday":
                    predicate = predicate.And(p => p.IsWednesday == true);
                    break;
                case "Thursday":
                    predicate = predicate.And(p => p.IsThursday == true);
                    break;
                case "Friday":
                    predicate = predicate.And(p => p.IsFriday == true);
                    break;
                case "Saturday":
                    predicate = predicate.And(p => p.IsSaturday == true);
                    break;
                case "Sunday":
                    predicate = predicate.And(p => p.IsSunday == true);
                    break;
            }
            return predicate;
        }

    }
}