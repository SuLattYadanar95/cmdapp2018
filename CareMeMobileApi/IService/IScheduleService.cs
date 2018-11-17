using CareMeMobileApi.ViewModels;
using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CareMeMobileApi.IService
{
    public interface IScheduleService
    {
        CountInTimeFrame hospitalSchedulesWithPatient(DateTime date, string TimeFrame,
                                              int doctorid);

        Expression<Func<tbSchedule, bool>> daysQuery(string day);

        NotiTimeFrameViewModel scheduleNotis(DateTime date, int doctorid, string TimeFrame);

    }
}