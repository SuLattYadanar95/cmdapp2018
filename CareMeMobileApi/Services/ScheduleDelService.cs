using CareMeMobileApi.IService;
using CareMeMobileApi.Repository;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.Services
{
    public class ScheduleDelService:IScheduleDelService
    {
        CaremeDBContext dbContext;

        private ScheduleRepository scheduleRepo = null;
        private HospitalRepository hospitalRepo = null;
        public ScheduleDelService()
        {
            dbContext = new CaremeDBContext();
            scheduleRepo = new ScheduleRepository(dbContext);
            hospitalRepo = new HospitalRepository(dbContext);
        }

        public bool deleteSchedulesUnderHospital(int doctorID, int hospitalID)
        {
            bool status = false;
            List<tbSchedule> scheduleList = scheduleRepo.Get().Where(a => a.DoctorID == doctorID
                                && a.HospitalID == hospitalID).ToList();
            int delCount = 0;
            foreach(var item in scheduleList)
            {
                item.IsDeleted = true;
                tbSchedule delResult = scheduleRepo.UpdatewithObj(item);
                if(delResult != null)
                {
                    delCount += 1;
                }
            }
            if(delCount == scheduleList.Count())
            {
                status = true;
            }
            return status;
        }

        public tbSchedule deleteSchedulesByID(int scheduleID)
        {
            tbSchedule UpdatedSchedule = null;
            tbSchedule schedule = scheduleRepo.Get().Where(a => a.ID == scheduleID).FirstOrDefault();
            schedule.IsDeleted = true;
            UpdatedSchedule = scheduleRepo.UpdatewithObj(schedule);
            return UpdatedSchedule;
        }
    }
}