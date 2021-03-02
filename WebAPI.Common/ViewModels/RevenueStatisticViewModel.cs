using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Common.ViewModels
{
    public class RevenueStatisticViewModel
    {
        public DateTime Date { get; set; }

        //doanh thu
        public decimal Revenues { get; set; }

        //tiền lãi
        public decimal Benefit { get; set; }
    }
}