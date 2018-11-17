using Data.Helper;
using Data.Models;
using Data.ViewModels;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace CaremebotMSApi.Helper
{
    public static class ResourceHelper
    {

        public static string goback_icon_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/icons/goback.png"; }
        }
        public static string contactus_icon_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/icons/contactus.png"; }
        }
        public static string seemore_icon_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/icons/seemore.png"; }
        }
        public static string location_icon_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/icons/location.png"; }
        }
        public static string showingnow_icon_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/icons/showingnow.png"; }
        }
        public static string comingsoon_icon_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/icons/comingsoon.png"; }
        }
        public static string welcome_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/airticket/welcome_placeholder.png"; }
        }
        public static string buy_ticket_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/airticket/bookflight.jpg"; }
        }
        public static string ticket_chat_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/airticket/chat.jpg"; }
        }
        public static bool isStart_words(this string value)
        {
            return new string[] { "hi", "Hi", "<GET_STARTED_PAYLOAD>", "BUY_PAYLOAD", "start over", "get started", "start", "hello", "🤔 Start over", "🤟 ျပန္စမည္" }.Contains(value.Trim().ToLower());
        }
        public static bool isBookDoctor_words(this string value)
        {
            return new string[] { "appointment", "book", "book a doctor", "find doctor", "find", "doctor", "search doctor", "search", "look a doctor", "look doctor" }.Contains(value.Trim().ToLower());
        }
        public static bool isFindPaediatrician_words(this string value)
        {
            return new string[] { "doctor for a kid", "paediatrician", "find a paediatrician", "look for a paediatrician", "find a child doctor", "child doctor",
                "doctor", "search a paediatrician" }.Contains(value.Trim().ToLower());
        }
        public static bool isFindPhysician_words(this string value)
        {
            return new string[] { "doctor for an adult", "physician", "find a physician" }.Contains(value.Trim().ToLower());
        }
        public static bool isFindHospital_words(this string value)
        {
            return new string[] { "find doctor by hospital", "start with hospital", "find with hospital" }.Contains(value.Trim().ToLower());
        }
        public static string help_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string lifestyletip_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string walking_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string gym_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string usefularticle_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string healtharticle_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string specialty_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string doctor_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string hospital_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string pillreminder_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string exercisereminder_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static string meditation_img_url
        {
            get { return "https://kktstroage.azureedge.net/yammo/support.jpg"; }
        }
        public static List<HeroCardViewModel> subscriptionTypes {
            get {
                return new List<HeroCardViewModel> {
                            new HeroCardViewModel { Name = "Lifestyle tip", Image = ResourceHelper.lifestyletip_img_url },
                            new HeroCardViewModel { Name = "Useful article", Image = ResourceHelper.usefularticle_img_url },
                            new HeroCardViewModel { Name = "Health article", Image = ResourceHelper.healtharticle_img_url }
                            };
                }
        }
        public static List<HeroCardViewModel> activityTypes
        {
            get
            {
                return new List<HeroCardViewModel> {
                            new HeroCardViewModel { Name = "Pill reminder", Description="Remind me for taking medicine regularly.", Image = ResourceHelper.lifestyletip_img_url },
                            new HeroCardViewModel { Name = "Exercise reminder", Description="Stick to do exercise. I need a reminder",Image = ResourceHelper.usefularticle_img_url },
                            new HeroCardViewModel { Name = "Meditation reminder", Description="I need time to mediate and I love it",Image = ResourceHelper.healtharticle_img_url }
                            };
            }
        }
        public static List<HeroCardViewModel> exerciseTypes
        {
            get
            {
                return new List<HeroCardViewModel> {
                            new HeroCardViewModel { Name = "For walking", Description="Walking is good exercise too.", Image = ResourceHelper.lifestyletip_img_url },
                            new HeroCardViewModel { Name = "Go to gym", Description="I used to go to gym.",Image = ResourceHelper.usefularticle_img_url },
                            
                            };
            }
        }
        public static int hospitalId { get { return Convert.ToInt16(WebConfigurationManager.AppSettings["HospitalId"]); } }
        public static string appId { get { return WebConfigurationManager.AppSettings["MicrosoftAppId"]; } }
        public static string appPassword { get { return WebConfigurationManager.AppSettings["MicrosoftAppPassword"]; } }
        public static string senderId { get { return WebConfigurationManager.AppSettings["FBPageId"] ; } }
        public static string senderName { get { return WebConfigurationManager.AppSettings["BotId"]; } }
        public static string serviceUri { get { return "https://facebook.botframework.com/"; } }
        
    }
}