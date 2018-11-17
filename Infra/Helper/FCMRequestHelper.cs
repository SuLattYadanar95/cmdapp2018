using Data.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class FCMRequestHelper
    {
        public static string sendTokenMessage(FCMViewModel FcmModel)
        {
            string serverKey = "AIzaSyAzCVo8xuNAc3Keh5IYlsSLUyUQ1fFq9Ws";//AIzaSyD0Ik8KiMVfKk2e0FBsYE0pCMCdXm8jFMY 
            //AIzaSyBXIrhOfJRvU-fRQK_T5G99-wlTgmS8qN4
            //

            try
            {
                var result = "-1";
                var webAddr = "https://fcm.googleapis.com/fcm/send";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(FcmModel); //  "{\"to\": \"" + token + "\",\"Payload\": \"" + "BookingNoti" + "\"  ,\"data\": {\"message\": \"" + "1 new patient is waiting" + "\",}}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
