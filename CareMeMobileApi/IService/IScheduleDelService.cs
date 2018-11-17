using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.IService
{
    public interface IScheduleDelService
    {
        bool deleteSchedulesUnderHospital(int doctorID, int hospitalID);

        tbSchedule deleteSchedulesByID(int scheduleID);
    }
}