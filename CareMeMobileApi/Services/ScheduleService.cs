using CareMeMobileApi.IService;
using CareMeMobileApi.ViewModels;
using Data.Helper;
using Data.Models;
using Data.ViewModels;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareMeMobileApi.Services
{
    public class ScheduleService:ScheduleQuery, IScheduleService
    {
        //public CountInTimeFrame hospitalSchedulesWithPatient(DateTime date, string TimeFrame, 
        //                                    int doctorid)
        //{
        //    using (CaremeDBContext context = new CaremeDBContext())
        //    {
        //        DateTime scheduledate = date;
        //        var day = scheduledate.DayOfWeek.ToString();

        //        CountInTimeFrame citf_data = new CountInTimeFrame();
        //        citf_data.TimeFrame = TimeFrame; 

        //        DateTime? combined = null;

        //        List<tbSchedule> schedules = context.tbSchedules.Where(a => a.IsDeleted != true
        //                        && a.DoctorID == doctorid).Where(daysQuery(day)).ToList(); //date
        //        List<HospitalListViewModel> hlistvm = new List<HospitalListViewModel>();
        //        foreach (var item in schedules)
        //        {
        //            var newdate = scheduledate.ToShortDateString();//tochange
        //            var time = item.Fromtime.Value.ToShortTimeString();
        //            combined = DateTime.Parse(newdate + " " + time);

        //            HospitalListViewModel hlvm = new HospitalListViewModel();
        //            hlvm.fromTime = item.Fromtime;
        //            hlvm.toTime = item.Totime;
        //            hlvm.patientCount = context.tbAppointments.Where(a => a.IsDeleted != true
        //                                    && a.DoctorId == item.DoctorID
        //                                    && a.HospitalId == item.HospitalID && a.Day == day && a.AppointmentDateTime == combined).Count();
        //            hlvm.hospitalId = item.HospitalID;
        //            hlvm.hospitalName = item.HospitalName;
        //            hlvm.appointmentDateTime = combined;
        //            hlvm.scheduleID = item.ID;
        //            hlistvm.Add(hlvm);
        //        }
        //        citf_data.hospitalList = hlistvm;
        //        var nextday = date.AddDays(1).Date;
        //        citf_data.totalPatientCount = context.tbAppointments.Where(a => a.IsDeleted != true).Where(a => a.DoctorId == doctorid
        //                                                && a.Day == day && a.AppointmentDateTime >= date.Date && a.AppointmentDateTime <= nextday).Count();

        //        return citf_data;
        //    }
        //}

        public CountInTimeFrame hospitalSchedulesWithPatient(DateTime date, string TimeFrame,
                                           int doctorid)
        {
            using (CaremeDBContext context = new CaremeDBContext())
            {
                DateTime scheduledate = date;
                var day = scheduledate.DayOfWeek.ToString();

                CountInTimeFrame citf_data = new CountInTimeFrame();
                citf_data.TimeFrame = TimeFrame;

                DateTime? combined = null;

                //List<tbSchedule> schedules = context.tbSchedules.Where(a => a.IsDeleted != true
                //                && a.DoctorID == doctorid).Where(daysQuery(day)).ToList(); //date

               

                date = date.Date;
                var dateto = date.AddDays(1);
                List<tbScheduleData> schedules = context.tbScheduleDatas.Where(a => a.IsDeleted != true
                                                && a.DoctorID == doctorid && a.AppointmentDatetime >= date && a.AppointmentDatetime <= dateto).ToList();


                List<HospitalListViewModel> hlistvm = new List<HospitalListViewModel>();
                foreach (var item in schedules)
                {
                    //var newdate = scheduledate.ToShortDateString();//tochange
                    //var time = item.Fromtime.Value.ToShortTimeString();
                    //combined = DateTime.Parse(newdate + " " + time);

                    HospitalListViewModel hlvm = new HospitalListViewModel();
                    hlvm.fromTime = item.Fromtime;
                    hlvm.toTime = item.Totime;
                    hlvm.patientCount = context.tbAppointments.Where(a => a.IsDeleted != true
                                            && a.DoctorId == item.DoctorID
                                            && a.HospitalId == item.HospitalID && a.AppointmentDateTime == item.AppointmentDatetime).Count();
                    hlvm.hospitalId = item.HospitalID;
                    hlvm.hospitalName = item.HospitalName;
                    hlvm.appointmentDateTime = item.AppointmentDatetime;
                    hlvm.scheduleID = item.ID;
                    hlistvm.Add(hlvm);
                }
                citf_data.hospitalList = hlistvm;
                var nextday = date.AddDays(1).Date;
                citf_data.totalPatientCount = context.tbAppointments.Where(a => a.IsDeleted != true).Where(a => a.DoctorId == doctorid
                                                         && a.AppointmentDateTime >= date.Date && a.AppointmentDateTime <= nextday).Count();

                return citf_data;
            }
        }


        //public NotiTimeFrameViewModel scheduleNotis(DateTime date, int doctorid, string TimeFrame)
        //{
        //    using(var ctx = new CaremeDBContext())
        //    {
        //        var day = date.DayOfWeek.ToString();
        //        DateTime? combined = null;

        //        List<tbSchedule> schedules = ctx.tbSchedules.Where(a => a.IsDeleted != true
        //                          && a.DoctorID == doctorid).Where(daysQuery(day)).ToList();
        //        List<NotiViewModel> NotiList = new List<NotiViewModel>();
        //        foreach (var item in schedules)
        //        {
        //            combined = HelperExtension.getCombinedDateTime(date, item.Fromtime); 
        //            var appointments = ctx.tbAppointments.Where(a => a.IsDeleted != true
        //                                        && a.DoctorId == item.DoctorID
        //                                        && a.HospitalId == item.HospitalID && a.Day == day
        //                                        && a.AppointmentDateTime == combined);

        //            NotiViewModel notiMD = new NotiViewModel();
        //            notiMD.scheduleID = item.ID;
        //            notiMD.AppointmentDateTime = combined ?? date;
        //            notiMD.hospitalID = item.HospitalID ?? 0;
        //            notiMD.hospitalName = item.HospitalName;
        //            notiMD.patientCount = appointments.Count();
        //            if (notiMD.patientCount > 0)
        //            {
        //                notiMD.LastActivtBookedTime = appointments.OrderByDescending(a => a.Accesstime)
        //                                       .FirstOrDefault().Accesstime ?? date;
        //            }

        //            NotiList.Add(notiMD);
        //        }
        //        NotiTimeFrameViewModel NotiObj = new NotiTimeFrameViewModel();
        //        if(schedules.Count > 0)
        //        {
        //            NotiObj.NotiList = NotiList;
        //            NotiObj.Timeframe = TimeFrame;
        //        }
        //        return NotiObj;
        //    }
        //}


        public NotiTimeFrameViewModel scheduleNotis(DateTime date, int doctorid, string TimeFrame)
        {
            using (var ctx = new CaremeDBContext())
            {
                date = date.Date;
                var day = date.DayOfWeek.ToString();
                DateTime? combined = null;

                //List<tbSchedule> schedules = ctx.tbSchedules.Where(a => a.IsDeleted != true
                //                  && a.DoctorID == doctorid).Where(daysQuery(day)).ToList();
                var dateto = date.AddDays(1).Date;


                List<tbScheduleData> schedules = ctx.tbScheduleDatas.Where(a => a.IsDeleted != true
                                                && a.DoctorID == doctorid && a.AppointmentDatetime >= date && a.AppointmentDatetime <= dateto).ToList();

                List<NotiViewModel> NotiList = new List<NotiViewModel>();
                foreach (var item in schedules)
                {
                    //combined = HelperExtension.getCombinedDateTime(date, item.Fromtime);
                    var appointments = ctx.tbAppointments.Where(a => a.IsDeleted != true
                                                && a.DoctorId == item.DoctorID
                                                && a.HospitalId == item.HospitalID 
                                                && a.AppointmentDateTime == item.AppointmentDatetime);

                    NotiViewModel notiMD = new NotiViewModel();
                    notiMD.scheduleID = item.ID;
                    notiMD.AppointmentDateTime = item.AppointmentDatetime ?? date;
                    notiMD.hospitalID = item.HospitalID ?? 0;
                    notiMD.hospitalName = item.HospitalName;
                    notiMD.patientCount = appointments.Count();
                    notiMD.MaxPatientCount = item.MaxPatientCount ??0;
                    notiMD.IsLimited = item.IsLimited;
                    notiMD.IsStopped = item.IsStopped;

                    if (notiMD.patientCount > 0)
                    {
                        notiMD.LastActivtBookedTime = appointments.OrderByDescending(a => a.Accesstime)
                                               .FirstOrDefault().Accesstime ?? date;
                    }

                    NotiList.Add(notiMD);
                }
                NotiTimeFrameViewModel NotiObj = new NotiTimeFrameViewModel();
                if (schedules.Count > 0)
                {
                    NotiObj.NotiList = NotiList;
                    NotiObj.Timeframe = TimeFrame;
                }
                return NotiObj;
            }
        }
    }
}