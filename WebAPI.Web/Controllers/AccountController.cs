using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAPI.Common;
using WebAPI.Model.Models;
using WebAPI.Web.App_Start;
using WebAPI.Web.Models;

namespace WebAPI.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "RegisterCaptcha", "Mã xác nhận không đúng!")]
        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await _userManager.FindByEmailAsync(registerViewModel.Email);
                if (userByEmail != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    return View(registerViewModel);
                }

                var userByUserName = await _userManager.FindByNameAsync(registerViewModel.UserName);
                if (userByUserName != null)
                {
                    ModelState.AddModelError("username", "Tài khoản đã tồn tại");
                    return View(registerViewModel);
                }

                var user = new ApplicationUser()
                {
                    FullName = registerViewModel.FullName,
                    Address = registerViewModel.Address,
                    Email = registerViewModel.Email,
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    UserName = registerViewModel.UserName,
                    PhoneNumber = registerViewModel.PhoneNumber
                };
                //password User
                await _userManager.CreateAsync(user, registerViewModel.Password);

                //tìm user admin thông qua Email
                var adminUser = await _userManager.FindByEmailAsync(registerViewModel.Email);

                //nếu thành công thì có giá trị và add admin và user
                if (adminUser != null)
                {
                    await _userManager.AddToRolesAsync(adminUser.Id, new string[] { "User" });
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/newuser.html"));
                content = content.Replace("{{UserName}}", adminUser.FullName);
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap.html");

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                //gửi 2 mail
                MailHelper.SendMail(adminUser.Email, "Đăng kí thành công", content); //khách hàng
                MailHelper.SendMail(adminEmail, "Có tài khoản tên: " + adminUser.Email + " vừa đăng kí thành công", content); //admin

                ViewData["SuccessMsg"] = "Đăng kí thành công";
            }

            return View();
        }
    }
}