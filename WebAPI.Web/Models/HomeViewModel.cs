using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Web.Models
{
    public class HomeViewModel
    {
        //gộp 2 bảng
        public IEnumerable<SlideViewModel> Slides { set; get; }

        public IEnumerable<ProductViewModel> LastestProducts { set; get; }

        public IEnumerable<ProductViewModel> TopSaleProducts { set; get; }
    }
}