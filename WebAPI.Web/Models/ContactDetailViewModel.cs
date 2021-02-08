using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Web.Models
{
    public class ContactDetailViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "Tên Không để được trống")]
        public string Name { set; get; }

        [MaxLength(50, ErrorMessage = "Số điện thoại không vượt quá 50 ký tự")]
        public string Phone { set; get; }

        [MaxLength(250, ErrorMessage = "Email không vượt quá 250 ký tự")]
        public string Email { set; get; }

        [MaxLength(500, ErrorMessage = "Tên Website không vượt quá 500 ký tự")]
        public string Website { set; get; }

        [MaxLength(500, ErrorMessage = "Địa chỉ không vượt quá 500 ký tự")]
        public string Address { set; get; }

        public string Other { set; get; }

        public double? Lat { set; get; } //kinh độ

        public double? Lng { set; get; }  //vĩ độ

        public bool Status { set; get; }
    }
}