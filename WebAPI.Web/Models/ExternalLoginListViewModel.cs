﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Web.Models
{
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { set; get; }
    }
}