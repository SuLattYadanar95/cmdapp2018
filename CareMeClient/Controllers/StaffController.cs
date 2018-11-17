using Data.Models;
using Extensions;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CareMeClient.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(tbStaff staff)
        {
            tbStaff staffdata = await StaffApiRequestHelper.Login(staff);
            if (staffdata != null)
            {
                FormsAuthentication.SetAuthCookie(staffdata.Username, false);
                SetCookie(staffdata.ID, staffdata.Name, staffdata.Username, staffdata.Role,staffdata.HospitalID ?? 0, staffdata.HospitalName);
                return RedirectToAction("Index", "DoctorSchedule");
            }
            else
            {
                ViewBag.Status = "Unauthorize";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            DeleteCookie();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Staff");
        }

        public void DeleteCookie()
        {
            if (Request.Cookies["mc"] != null)
            {
                HttpCookie myCookie = new HttpCookie("mc");
                myCookie.Expires = MyExtension.getLocalTime(DateTime.UtcNow).AddDays(-1);
                Response.Cookies.Add(myCookie);
            }
        }

        public void SetCookie(int ID, string Name, string Username, string Role,int HospitalID,string HospitalName)
        {
            HttpCookie myCookie = HttpContext.Request.Cookies["mc"] ?? new HttpCookie("mc");
            myCookie.Values["ID"] = ID.ToString();
            myCookie.Values["Name"] = HttpUtility.UrlEncode(Name.ToString());
            myCookie.Values["Username"] = HttpUtility.UrlEncode(Username.ToString());
         //   myCookie.Values["ServiceName"] = ServiceName.ToString();
            myCookie.Values["Role"] = Role.ToString();
            myCookie.Values["HospitalID"] = HospitalID.ToString();
            myCookie.Values["HospitalName"] = Server.UrlEncode(HospitalName.ToString());

            myCookie.Expires = MyExtension.getLocalTime(DateTime.UtcNow).AddDays(1);
            HttpContext.Response.Cookies.Add(myCookie);
        }

    }
}