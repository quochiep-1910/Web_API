using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Web.Models
{
    public class PageViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Image { get; set; }

        public string Content { get; set; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }

        public bool Status { get; set; }
    }
}