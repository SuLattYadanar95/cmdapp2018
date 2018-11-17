using Data.Models;
using DataDC.Models;
using System;
using System.Collections.Generic;

namespace DataDC.ViewModels
{
    public class ItemViewModel
    {
        public Nullable<int> ID { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string BodyHtml { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Type { get; set; }
        public Nullable<bool> IsAvailable { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public Nullable<bool> IsPopular { get; set; }
        public string Author { get; set; }
        public Nullable<double> Rating { get; set; }
        public string Photo { get; set; }
        public string Tags { get; set; }
        public Nullable<System.Guid> Guid { get; set; }
        public string PhotoURL
        {
            get
            {
                if (this.Photo != null)
                {
                    return string.Format("https://portalvhdslvb28rs1c3tmc.blob.core.windows.net/yammo/foody/{0}", Photo);
                }
                return "https://portalvhdslvb28rs1c3tmc.blob.core.windows.net/yammo/foody/logo.png";
            }
        }
        public string CategoryTitle { get; set; }
        public string CategoryCode { get; set; }
        public string CollectionTitle { get; set; }
        public Nullable<int> CollectionID { get; set; }
        public string UserId { get; set; }

    }
    
}