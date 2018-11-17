using Data.Models;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareMeClient.Helper
{
    public static class CookieHelper
    {
        public static string CookieName = "mc";

        public static int getstaffID()
        {
            return getStaff().ID;
        }
        public static string getstaffName()
        {
            return getStaff().Name;
        }
        public static string getstaffUsername()
        {
            return getStaff().Username;
        }
        public static string getstaffRole()
        {
            return getStaff().Role;
        }

        public static int getstaffHospitalID()
        {
            return getStaff().HospitalID ?? 0;
        }

        public static string getstaffHospitalName()
        {
            return getStaff().HospitalName;
        }

        public static tbStaff getStaff()
        {
            tbStaff staff = new tbStaff();
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var Cookie = HttpContext.Current.Request.Cookies[CookieName];
                if (Cookie != null)
                {
                    staff.ID = Convert.ToInt32(Cookie["ID"]);
                    staff.Name =HttpUtility.UrlDecode(Cookie["Name"]);
                    staff.Username = HttpUtility.UrlDecode(Cookie["Username"]);
                    staff.Role = Cookie["Role"];
                    staff.HospitalID = Convert.ToInt32(Cookie["HospitalID"]);
                    staff.HospitalName = HttpUtility.UrlDecode(Cookie["HospitalName"]);
                    return staff;
                }
                else
                {
                    fillCookie();
                    var Cookie2 = HttpContext.Current.Request.Cookies[CookieName];
                    if (Cookie2 != null)
                    {
                        staff.ID = Convert.ToInt32(Cookie2["ID"]);
                        staff.Name = HttpUtility.UrlDecode(Cookie2["Name"]);
                        staff.Username = HttpUtility.UrlDecode(Cookie2["Username"]);
                        staff.Role = Cookie2["Role"];
                        staff.HospitalID = Convert.ToInt32(Cookie2["HospitalID"]);
                        staff.HospitalName = HttpUtility.UrlDecode(Cookie2["HospitalName"]);
                    }
                    return staff;
                }
            }
            else
            {
                return staff;
            }
        }

        public static void fillCookie()
        {
            string username = HttpContext.Current.User.Identity.Name;
            tbStaff staff = StaffApiRequestHelper.getStaffData(username);
            SetCookie(staff.ID, staff.Name, staff.Username, staff.Role,staff.HospitalID ?? 0, staff.HospitalName);
        }

        public static void SetCookie(int ID, string Name, string Username, string Role,int HospitalID,string HospitalName)
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["mc"] ?? new HttpCookie("mc");
            myCookie.Values["ID"] = ID.ToString();
            myCookie.Values["Name"] =HttpUtility.UrlEncode(Name.ToString());
            myCookie.Values["Username"] =HttpUtility.UrlEncode(Username.ToString());
            myCookie.Values["Role"] = Role.ToString();
            myCookie.Values["HospitalID"] = HospitalID.ToString();
            myCookie.Values["HospitalName"] =HttpUtility.UrlEncode(HospitalName.ToString());
            myCookie.Expires = getLocalTime(DateTime.UtcNow).AddDays(1);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }

        public static DateTime getLocalTime(this DateTime utc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time"));
        }





    }


}