using CareMeApi.IService;
using Core.Extensions;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeApi.Services
{
    public class BookingService: IBookingService
    {
        public bool checkBookingAvailable(int ScheduleDataID)
        {
            var result = false;
            var Now = MyExtension.getLocalTime(DateTime.UtcNow);
            using (var ctx = new CaremeDBContext())
            {
                
                var scheduledata = ctx.tbScheduleDatas.Where(a => a.IsDeleted != true && a.ID == ScheduleDataID
                                                && a.IsStopped != true && a.IsCancelled != true).FirstOrDefault();
                if(scheduledata != null)
                {
                    if(scheduledata.AppointmentDatetime > Now)
                    {
                        int reachedPTCount = scheduledata.ReachedPatientCount ?? 0;
                        int maxPTCount = scheduledata.MaxPatientCount ?? 0;
                        int count = maxPTCount - reachedPTCount;
                        if (count > 0)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }else
                {
                    result = false;
                }
            }

            return result;
        }
    }
}